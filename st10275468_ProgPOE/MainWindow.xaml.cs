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
        private List<Recipe> recipeList = new List<Recipe>();
        public MainWindow()
        {
            InitializeComponent();
            
        }


        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            AddRecipe addRecipe = new AddRecipe();
            addRecipe.Owner = this;
            addRecipe.ShowDialog();
        }
        public void AddRecipeToList(Recipe recipe)
        {
            recipeList.Add(recipe);
            comboBoxRecipes.Items.Refresh();
        }

          
       
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
                    comboBoxRecipes.Items.Add(recipe.recipeName);
                }
            }
        }
        private void comboBoxRecipes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboBoxRecipes.SelectedIndex != -1)
            {
                Recipe selectedRecipe = recipeList[comboBoxRecipes.SelectedIndex];
                DisplayRecipeDetails(selectedRecipe);
            }
        }
        private void DisplayRecipeDetails(Recipe recipe)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Recipe Name: {recipe.recipeName}");

            sb.AppendLine("Ingredients:");
            foreach (var ingredient in recipe.recipeIngredients)
            {
                sb.AppendLine($"{ingredient.GetIngredient()}");
            }

            sb.AppendLine("Steps:");
            int stepNumber = 1;
            foreach (var step in recipe.recipeSteps)
            {
                sb.AppendLine($"{stepNumber}. {step.GetStep()}");
                stepNumber++;
            }
            double totalCalories = recipe.recipeIngredients.Sum(i => i.ingredientCalories);
            sb.AppendLine($"\nTotal Calories: {totalCalories}");

            if (totalCalories > 300)
            {
                MessageBox.Show("Calories Exceed 300!", "", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            textBlockRecipeDetails.Text = sb.ToString();
        }

        private void btnDisplayAlpha_Click(object sender, RoutedEventArgs e)
        {
            var sortedRecipes = recipeList.OrderBy(r => r.recipeName).ToList();
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("All recipes:");
            foreach (var recipe in sortedRecipes)
            {
                sb.AppendLine(recipe.recipeName);
            }

            textBlockRecipeDetails.Text = sb.ToString();
        }

          private void btnScale_Click(object sender, RoutedEventArgs e)
          {

              if (comboBoxRecipes.SelectedItem != null && comboBoxScale.SelectedItem != null)
              {
                  Recipe recipeChoice = (Recipe)comboBoxRecipes.SelectedItem;
                  string scaleOption = comboBoxScale.SelectedItem.ToString();
                  if (scaleOption != null)
                  {
                    //  ScaleChoice(recipeChoice, scaleOption);
                    //  DisplayRecipeDetails(recipeChoice);
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

        private void btnFilterIngredient_Click(object sender, RoutedEventArgs e)
        {
            string ingredientChoice = txtIngredientChoice.Text;
            if (!string.IsNullOrEmpty(ingredientChoice))
            {
                List<Recipe> recipes = recipeList.Where(r => r.recipeIngredients.Any(i => i.GetIngredient().ToLower().Contains(ingredientChoice.ToLower()))).ToList();
                DisplayFilteredRecipes(recipes);
            }
            else
            {
                MessageBox.Show("Enter an ingredient");
            }
        }
        private void DisplayFilteredRecipes(List<Recipe> recipes)
        {
            if (recipes.Count == 0)
            {
                MessageBox.Show("No recipes with {0} found!", txtIngredientChoice.Text );
            }
            else
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("All recipes with " + txtIngredientChoice.Text +":");
                foreach (var recipe in recipes)
                {
                    sb.AppendLine(recipe.recipeName);
                }
                textBlockRecipeDetails.Text = sb.ToString();
            }
        }
        /*   private void ScaleChoice( Recipe recipeChoice, string scale)
  {
      if (scale == "Half")
      {
          ScaleRecipe(recipeChoice, 0.5);
      }
      if(scale == "Double")
      {
          ScaleRecipe(recipeChoice, 2);
      }
      else if (scale == "Triple")
      {
          ScaleRecipe(recipeChoice, 3);
      }

  }

  private void ScaleRecipe(Recipe recipe, double scale)
  {
      foreach(Ingredient ingredient in recipe.recipeIngredients)
      {
          ingredient.ingredientQuantity = ingredient.ingredientQuantity * scale;
          ingredient.ingredientCalories = ingredient.ingredientCalories * scale;

      }

  }
  */
    }
    
    }


