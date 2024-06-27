using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ST10150702_PROG6221_POE
{
    public class Recipe
    {
        public delegate void CalorieNotificationDelegate(string message);
        public event CalorieNotificationDelegate CalorieNotification;

        public string rName { get; set; }
        public List<Ingredient> Ingredients { get; set; }
        public string Steps { get; set; }
        public int TotalCalories { get; set; }
        public List<Ingredient> ingredients = new List<Ingredient>();
        public List<string> steps = new List<string>();

        public Recipe(string name)
        {
            rName = name;
        }

        public void AddIngredient(string name, double quantity, string measurement, string foodGroup, int calories)
        {
            ingredients.Add(new Ingredient { Name = name, Quantity = quantity, Measurement = measurement, FoodGroup = foodGroup, Calories = calories });
            CheckCalories();
        }

        public void AddStep(string step)
        {
            steps.Add(step);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("==========================");
            sb.AppendLine($"Recipe Name: {rName}");
            sb.AppendLine("==========================");
            sb.AppendLine("Ingredients:");
            foreach (var ingredient in ingredients)
            {
                sb.AppendLine($"{ingredient.Quantity} {ingredient.Measurement} of {ingredient.Name} ({ingredient.FoodGroup}, {ingredient.Calories} calories)");
            }
            sb.AppendLine("==========================");
            sb.AppendLine("Steps:");
            for (int i = 0; i < steps.Count; i++)
            {
                sb.AppendLine($"{i + 1}. {steps[i]}");
            }
            sb.AppendLine("==========================");
            sb.AppendLine($"Total Calories: {CalculateTotalCalories()}");
            return sb.ToString();
        }

        public void CheckCalories()
        {
            int totalCalories = CalculateTotalCalories();
            if (totalCalories > 300)
            {
                CalorieNotification?.Invoke($"Warning: The total calories for the recipe '{rName}' exceed 300 calories. Total: {totalCalories} calories.");
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
    }
}
