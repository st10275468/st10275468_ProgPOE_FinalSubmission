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

        private void btnAddIngredient_Click(object sender, RoutedEventArgs e)
        {
            
            double quantity = 0;
            double calories = 0;
            string name = txtIngredientName.Text;
            double.TryParse(txtQuantity.Text, out quantity);
            string measurement = txtMeasurement.Text;
            double.TryParse(txtCalories.Text, out calories);
            string foodGroup = cmbFoodGroup.Text;

            currentRecipe.GetIngredients(name, quantity, measurement, calories, foodGroup);

            
            
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
            ((MainWindow)this.Owner).AddRecipeToList(currentRecipe);
            MessageBox.Show("Recipe successfully added");
            txtName.Clear();
            txtName.Focus();
            currentRecipe = new Recipe("");
        }

        private void btnAddStep_Click(object sender, RoutedEventArgs e)
        {

            string stepDescription = txtDescription.Text;
            currentRecipe.GetSteps(stepDescription);

            MessageBox.Show("Step successfully added");
            txtDescription.Clear();
            txtDescription.Focus();
        }
       

    }
}