using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace st10275468_ProgPOE
{
    public class Ingredient
    {                                               //Creating a new object ingredient

        public string ingredientName { get; set; }
        public double ingredientQuantity { get; set; }
        public string unitOfMeasure { get; set; }
        public string ingredientgrouping { get; set; }
        public double ingredientCalories { get; set; }

        public Ingredient(string fName, double fQuantity, string fUnit, string fGroup, double fCalories)
        {
            //Giving the object properties
            ingredientName = fName;
            ingredientQuantity = fQuantity;
            unitOfMeasure = fUnit;
            ingredientgrouping = fGroup;
            ingredientCalories = fCalories;
        }
        public string GetIngredient()
        {
            string ingredient = ingredientQuantity + unitOfMeasure + "'s" + ingredientName + ". Food group: " + ingredientgrouping + ". Calories: " + ingredientCalories;
            return ingredient;
        }

    }
    public class Step
    {
        public string stepDescription { get; set; }  //Creating a new step object


        public Step(string fStepDescription)
        {                                           //Giving the object properties
            stepDescription = fStepDescription;


        }
        public string GetStep()
        {
            string step = "Step " + ": " + stepDescription;
            return step;
        }

    }
    public class Recipe
    {
        public string recipeName { get; set; }
        public List<Ingredient> recipeIngredients { get; set; }     //Storing a list of ingredients in each recipe
        public List<Ingredient> recipeIngredientsBackup { get; set; }
        public List<Step> recipeSteps { get; set; } //Storing a list of steps in each recipe

        public Recipe(string fRecipeName)
        {
            this.recipeName = fRecipeName;       //Giving the recipe object its properties
            recipeIngredients = new List<Ingredient>();
            recipeIngredientsBackup = new List<Ingredient>();
            recipeSteps = new List<Step>();
        }
        public void AddIngredient(Ingredient ingredient)
        {
            recipeIngredients.Add(ingredient);
            recipeIngredientsBackup.Add(new Ingredient(ingredient.ingredientName, ingredient.ingredientQuantity, ingredient.unitOfMeasure, ingredient.ingredientgrouping, ingredient.ingredientCalories));
        }   //Backup list to restore the scaled values to the original ones if needed.
        public void AddStep(Step step)
        {
            recipeSteps.Add(step);
        }
        public void GetIngredients(string fName, double fQuantity, string fUnit, double fCalories, string fGroup)
        {
            Ingredient ingredient = new Ingredient(fName, fQuantity, fUnit, fGroup, fCalories);
            AddIngredient(ingredient);
        }
        public void GetSteps(string fStepDescription)
        {
            Step step = new Step(fStepDescription);
            AddStep(step);
        }
    }
}