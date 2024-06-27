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
       
        
        public AddRecipe()
        {

            InitializeComponent();
            currentRecipe = new Recipe("");

        }

        
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            
            this.Close();
        }

        /// <summary>
        /// Adds an ingredient to the recipe when the button is clicked.
        /// The user must input all the text boxes first
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddIngredient_Click(object sender, RoutedEventArgs e)
        {
            
            double quantity = 0;
            double calories = 0;
            string name = txtIngredientName.Text;
            double.TryParse(txtQuantity.Text, out quantity);
            string measurement = txtMeasurement.Text;
            double.TryParse(txtCalories.Text, out calories);
            string foodGroup = cmbFoodGroup.Text;

            currentRecipe.GetIngredients(name, quantity, measurement, calories, foodGroup); //Adding the ingredients to the recipe
 
            MessageBox.Show("Ingredient successfully added");
            txtIngredientName.Clear();
            txtQuantity.Clear();
            txtMeasurement.Clear(); //Clearing all the textboxes
            txtCalories.Clear();
            txtIngredientName.Focus();

        }

        /// <summary>
        /// Takes the name and creates a new recipe with the name and adds it to the recipe list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddRecipe_Click_1(object sender, RoutedEventArgs e)
        {
            currentRecipe.recipeName = txtName.Text;
            ((MainWindow)this.Owner).AddRecipeToList(currentRecipe); //adding it to the list of recipes
            MessageBox.Show("Recipe successfully added");
            txtName.Clear();
            txtName.Focus();
            currentRecipe = new Recipe(""); //Setting current recipe back to null so they can create another recipe
        }

        /// <summary>
        /// Allows the user to create steps for the recipes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddStep_Click(object sender, RoutedEventArgs e)
        {

            string stepDescription = txtDescription.Text;
            currentRecipe.GetSteps(stepDescription); //adding the steps to the recipe

            MessageBox.Show("Step successfully added");
            txtDescription.Clear();
            txtDescription.Focus();
        }
       

    }
}