﻿using System;
using System.Collections.Generic;
using System.Text;

namespace POE
{
    internal class Recipe
    {
        public string Name { get; set; }
        private List<Ingredient> ingredients;
        private List<string> steps;

        public Recipe(string name)
        {
            Name = name;
            ingredients = new List<Ingredient>();
            steps = new List<string>();
        }

        public void AddIngredient(string name, double quantity, string measurement, string foodGroup, int calories)
        {
            ingredients.Add(new Ingredient { Name = name, Quantity = quantity, Measurement = measurement, FoodGroup = foodGroup, Calories = calories });
        }

        public void AddStep(string step)
        {
            steps.Add(step);
        }

        public void Scale(double factor)
        {
            foreach (var ingredient in ingredients)
            {
                ingredient.Quantity *= factor;
            }
        }

        public void ResetScaling(double factor)
        {
            foreach (var ingredient in ingredients)
            {
                ingredient.Quantity /= factor;
            }
        }

        public int CalculateTotalCalories()
        {
            int totalCalories = 0;
            foreach (var ingredient in ingredients)
            {
                totalCalories += ingredient.Calories;
            }
            return totalCalories;
        }

        public override string ToString()
        {
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

    internal class Ingredient
    {
        public string Name { get; set; }
        public double Quantity { get; set; }
        public string Measurement { get; set; }
        public string FoodGroup { get; set; }
        public int Calories { get; set; }
    }
}
