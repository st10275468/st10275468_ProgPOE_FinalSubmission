﻿using System.Windows;
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
                sb.AppendLine($"- {ingredient.GetIngredient()}");
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

    }

    
}