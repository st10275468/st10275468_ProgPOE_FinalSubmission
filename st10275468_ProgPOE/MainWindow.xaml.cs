using System.Windows;
using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Text;
namespace st10275468_ProgPOE
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Recipe> recipeList = new List<Recipe>(); //Creating a new list of recipes
        public MainWindow()
        {
            InitializeComponent();
            
        }

        /// <summary>
        /// When button is clicked it creates a new instance of addRecipe
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            AddRecipe addRecipe = new AddRecipe();      
            addRecipe.Owner = this;
            addRecipe.ShowDialog();
        }

        /// <summary>
        /// Method that takes in a Recipe as a parameter and adds it to the list of recipes
        /// </summary>
        /// <param name="recipe"></param>
        public void AddRecipeToList(Recipe recipe)
        {                                           
            recipeList.Add(recipe);
            comboBoxRecipes.Items.Refresh();
        }

          
       /// <summary>
       /// If No recipes are found then it will show a message 
       /// If recipes are found then it will add them to the combobox and allow the user to pick one to display
       /// </summary>
       /// <param name="sender"></param>
       /// <param name="e"></param>
        private void btnDisplay_Click(object sender, RoutedEventArgs e)
        {
            if (recipeList.Count == 0)
            {
                MessageBox.Show("No recipes created");
            }
            else
            {
                comboBoxRecipes.Items.Clear();
                foreach (Recipe recipe in recipeList)
                {
                    comboBoxRecipes.Items.Add(recipe.recipeName); //Adding the names to the combobox
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxRecipes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboBoxRecipes.SelectedIndex != -1)
            {
                Recipe selectedRecipe = recipeList[comboBoxRecipes.SelectedIndex];
                DisplayRecipeDetails(selectedRecipe);
            }
        }

        /// <summary>
        /// Method created that takes in a recipe as a parameter and displays its information in a neat format
        /// The parameter is chosen from the combobox
        /// </summary>
        /// <param name="recipe"></param>
        private void DisplayRecipeDetails(Recipe recipe)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Recipe Name: " + recipe.recipeName); //Displaying recipe name

            sb.AppendLine("Ingredients: ");
            foreach (var ingredient in recipe.recipeIngredients) //Displaying Ingredients with a foreach loop to iterate through the list
            {
                sb.AppendLine($"{ingredient.GetIngredient()}");
            }

            sb.AppendLine("Steps:"); //Displaying steps with a foreach loop to iterate through the list
            int stepNumber = 1;
            foreach (var step in recipe.recipeSteps)
            {
                sb.AppendLine($"{stepNumber}. {step.GetStep()}");
                stepNumber++;
            }
            double totalCalories = recipe.recipeIngredients.Sum(i => i.ingredientCalories); //Calculating total calories of the recipe
            sb.AppendLine($"\nTotal Calories: " + totalCalories);

            if (totalCalories > 300)
            {                           //Outputting a warning to the user if the calories exceed 300
                MessageBox.Show("Calories Exceed 300!", "", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            textBlockRecipeDetails.Text = sb.ToString();
        }

        /// <summary>
        /// This method displays all the recipe names in the recipe list in alphabetical order
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDisplayAlpha_Click(object sender, RoutedEventArgs e)
        {
            var sortedRecipes = recipeList.OrderBy(r => r.recipeName).ToList(); //Sorting the recipe list in alphabetical order by the recipe name
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("All recipes:");
            foreach (var recipe in sortedRecipes) //iterating through the recipe list and displaying the sorrted names
            {
                sb.AppendLine(recipe.recipeName);
            }

            textBlockRecipeDetails.Text = sb.ToString();
        }

        /// <summary>
        /// Method created to scale the chosen recipe by either half, double or triple
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
          private void btnScale_Click(object sender, RoutedEventArgs e)
          {

              if (comboBoxRecipes.SelectedItem != null && comboBoxScale.SelectedItem != null)
              {
                  Recipe recipeChoice = (Recipe)comboBoxRecipes.SelectedItem; //Gets the chose recipe from the combo box
                  string scaleOption = comboBoxScale.SelectedItem.ToString(); //Gets the scale option from the combo box
                  if (scaleOption != null)
                  {
                     ScaleChoice(recipeChoice, scaleOption); //calling a method to scale the recipe
                     DisplayRecipeDetails(recipeChoice); //calling methods to display 
                  }
                  else
                  {
                      MessageBox.Show("Select a scale");
                  }
                  }
              else
              {
                  MessageBox.Show("Select a recipe and scale");
              }
          }

        /// <summary>
        /// Method created to filter the recipes by the ingredients
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFilterIngredient_Click(object sender, RoutedEventArgs e)
        {
            string ingredientChoice = txtIngredientChoice.Text; //Getting ingredient from the user
            if (!string.IsNullOrEmpty(ingredientChoice))
            {   //Going through the list of recipes and seeing if any recipes have the ingredient that the user inputted
                List<Recipe> recipes = recipeList.Where(r => r.recipeIngredients.Any(i => i.GetIngredient().ToLower().Contains(ingredientChoice.ToLower()))).ToList();
                DisplayFilteredRecipes(recipes); //Displaying method called
            }
            else
            {
                MessageBox.Show("Cannot filter recipes");
            }
        }
       
        /// <summary>
        /// Method that allows the user to choose a foodgroup from a combobox to filter the recipes by
        /// Unfortunatly could not get this to work
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFilterFoodgroup_Click(object sender, RoutedEventArgs e)
        {
            string foodGroup = cmbFilterFoodGroup.SelectedItem as string; //Getting Foodgroup choice from combo box
            if (!string.IsNullOrEmpty(foodGroup))
            {
              
                foodGroup = foodGroup.ToLower(); //Seeing if any recipes contain the chose foodgroup of ingredients
                List<Recipe> recipes = recipeList.Where(r => r.recipeIngredients.Any(i => i.ingredientgrouping.ToLower() == foodGroup)).ToList();
                 DisplayFilteredRecipes(recipes); //displaying method called
            }
            else
            {
                MessageBox.Show("No recipes found with that food group");
            }
        }


        /// <summary>
        /// Method Created to filter recipes by a max calorie count, If the calories are under the input it will display those recipes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFilterCalories_Click(object sender, RoutedEventArgs e)
        {
            
           if( double.TryParse(txtMaxCalories.Text, out double calories)) //Getting max calories from the user
            { //Finding all the recipes that dont go over the calorie count
                List<Recipe> recipes = recipeList.Where(r =>
                {
                    double totalCalories = r.recipeIngredients.Sum(i => i.ingredientCalories); //Calculatin gthe total calories of each recipe
                    return totalCalories < calories; 
                }).ToList();

                DisplayFilteredRecipes(recipes); //Display method called
            }
            else
            {
                MessageBox.Show("Enter a valid number");
            }



        }
        /// <summary>
        /// Method created that will display all the filtered recipes
        /// </summary>
        /// <param name="recipes"></param>
        private void DisplayFilteredRecipes(List<Recipe> recipes)
        {
            if (recipes.Count == 0)
            {
                MessageBox.Show("No recipes found with that filter");
            }
            else
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("Filtered recipes:"); //Displays the names of the filtered recipes
                foreach (var recipe in recipes)
                {
                    sb.AppendLine(recipe.recipeName);
                }
                textBlockRecipeDetails.Text = sb.ToString();
            }
        }
        /// <summary>
        /// Method that takes in the recipe and scale choice from the user. It gets the input from combo boxes
        /// It uses these choices in the scaleRecipe method below 
        /// </summary>
        /// <param name="recipeChoice"></param>
        /// <param name="scale"></param>
          private void ScaleChoice( Recipe recipeChoice, string scale)
                {
                        if (scale == "Half")
                {
            ScaleRecipe(recipeChoice, 0.5); //If the use chooses half it will be scaled by 0.5
            }
            if(scale == "Double")
            {
            ScaleRecipe(recipeChoice, 2);//If the use chooses double it will be scaled by 2
            }
            else if (scale == "Triple")
            {
            ScaleRecipe(recipeChoice, 3);//If the use chooses triple it will be scaled by 3
            }

            }

        /// <summary>
        /// Method created that takes in a recipe and a scale factor. It then scales the quantity and calories of the selected recipe
        /// </summary>
        /// <param name="recipe"></param>
        /// <param name="scale"></param>
           private void ScaleRecipe(Recipe recipe, double scale)
              {
                 foreach(Ingredient ingredient in recipe.recipeIngredients) //Going through each ingredient in the selected recipe and scaling its properties
              {
                 ingredient.ingredientQuantity = ingredient.ingredientQuantity * scale;
                 ingredient.ingredientCalories = ingredient.ingredientCalories * scale;

                }

                }
                
    }
    
    }


