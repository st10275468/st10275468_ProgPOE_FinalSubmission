using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace st10275468_ProgPOE
{
   

    /// <summary>
    /// Interaction logic for AddRecipe.xaml
    /// </summary>
    public partial class AddRecipe : Window
    {
        private Recipe currentRecipe;
        public List<Recipe> Recipes { get; private set; }
        public AddRecipe()
        {

            InitializeComponent();
            Recipes = new List<Recipe>();
            currentRecipe = new Recipe("");
        }
        


        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void btnAddIngredient_Click(object sender, RoutedEventArgs e)
        {
           
            double quantity = 0;
            double calories = 0;
            string name = txtIngredientName.Text;
            double.TryParse(txtQuantity.Text, out quantity);
            string measurement = txtMeasurement.Text;
            double.TryParse(txtCalories.Text, out calories);
            string foodGroup = cmbFoodGroup.Text;

            
          
            Ingredient ingredient = new Ingredient(name,quantity,measurement,foodGroup,calories);
            currentRecipe.AddIngredient(ingredient);
            MessageBox.Show("Ingredient successfully added");
            txtIngredientName.Clear();
            txtQuantity.Clear();
            txtMeasurement.Clear();
            txtCalories.Clear();
            txtIngredientName.Focus();
            
        }

        private void btnAddRecipe_Click_1(object sender, RoutedEventArgs e)
        {
          currentRecipe.recipeName = txtName.Text;
            Recipes.Add(currentRecipe);
            MessageBox.Show("Recipe successfully added");
            DisplayCurrentRecipeDetails();
        }

        private void btnAddStep_Click(object sender, RoutedEventArgs e)
        {
           
            string stepDesctription = txtDescription.Text;
            Step step = new Step(stepDesctription);
            currentRecipe.AddStep(step);
            MessageBox.Show("Recipe successfully added");
            txtDescription.Clear();
            txtDescription.Focus();
        }
        private void DisplayCurrentRecipeDetails()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Recipe Name: {currentRecipe.recipeName}");

            sb.AppendLine("Ingredients:");
            foreach (var ingredient in currentRecipe.recipeIngredients)
            {
                sb.AppendLine($"- {ingredient.GetIngredient()}");
            }

            sb.AppendLine("Steps:");
            int stepNumber = 1;
            foreach (var step in currentRecipe.recipeSteps)
            {
                sb.AppendLine($"{stepNumber}. {step.stepDescription}");
                stepNumber++;
            }

            txtDescription.Text = sb.ToString();
        }

    }
}
