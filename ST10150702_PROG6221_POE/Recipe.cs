using System;
using System.Collections.Generic;
using System.Text;

/* Summary
 * This class is called when creating or manipulating a recipe in the program
 and is in charge of storing the recipe in memory as well as displaying the 
list of recipes */

namespace POE
{
     public class Recipe
    {
        // Lists
        public string Name { get; set; }
        private List<Ingredient> ingredients;
        private List<string> steps;

        // Default Recipe constructor
        public Recipe(string name)
        {
            Name = name;
            ingredients = new List<Ingredient>();
            steps = new List<string>();
        }

        // This creates a new instance of an ingredients for when the user enters the information 
        public void AddIngredient(string name, double quantity, string measurement, string foodGroup, int calories)
        {
            ingredients.Add(new Ingredient { Name = name, Quantity = quantity, Measurement = measurement, FoodGroup = foodGroup, Calories = calories });
        }

        // This method adds steps to the already created Step list as the user might enter more and more
        public void AddStep(string step)
        {
            steps.Add(step);
        }
        
        // If the user choses to scale the recipe this method will do so acording to the scale chosen
        public void Scale(double factor)
        {
            foreach (var ingredient in ingredients)
            {
                ingredient.Quantity *= factor;
            }
        }

        // Simple method to reset the scaling by factor in which the user chose
        public void ResetScaling(double factor)
        {
            foreach (var ingredient in ingredients)
            {
                ingredient.Quantity /= factor;
            }
        }

        // A simple method to count the total calories for the reciep
        public int CalculateTotalCalories()
        {
            int totalCalories = 0;
            foreach (var ingredient in ingredients)
            {
                totalCalories += ingredient.Calories;
            }
            return totalCalories;
        }


        // summary
        /* This is the toString method which is used every time the user wants to view 
         a recipe from the list and will display all the information needed for the recipe*/
        public override string ToString()
        {
            // I found stringbuilders to be very useful and researched how to use them for this project
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("===============================");
            sb.AppendLine($"Recipe Name: {Name}");
            sb.AppendLine("===============================");
            sb.AppendLine("Ingredients:");
            foreach (var ingredient in ingredients)
            {
                sb.AppendLine($"{ingredient.Quantity} {ingredient.Measurement} of {ingredient.Name} ({ingredient.FoodGroup}, {ingredient.Calories} calories)");
            }
            sb.AppendLine("===============================");
            sb.AppendLine("Steps:");
            for (int i = 0; i < steps.Count; i++)
            {
                sb.AppendLine($"{i + 1}. {steps[i]}");
            }
            sb.AppendLine("===============================");
            sb.AppendLine($"Total Calories: {CalculateTotalCalories()}");
            return sb.ToString();
        }
    }


    /* This internal class did not require its own external class so I
     * created an internal one for when needing to access Ingredients
     * */
    internal class Ingredient
    {
        public string Name { get; set; }
        public double Quantity { get; set; }
        public string Measurement { get; set; }
        public string FoodGroup { get; set; }
        public int Calories { get; set; }
    }
}
