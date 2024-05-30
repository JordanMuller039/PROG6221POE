using System;
using System.Collections.Generic;
using System.Linq;

namespace POE
{
    internal class RecipeOperations
    {
        public delegate void CalorieNotification(string message);
        private List<Recipe> recipes = new List<Recipe>();
        public event CalorieNotification OnCalorieNotification;

        public void CreateRecipe()
        {
            Console.WriteLine("Enter the name of the recipe:");
            string name = Console.ReadLine();
            var recipe = new Recipe(name);

            Console.WriteLine("How many ingredients in this recipe?");
            int numIngredients = Convert.ToInt32(Console.ReadLine());

            for (int i = 0; i < numIngredients; i++)
            {
                Console.WriteLine($"Please enter the name of ingredient {i + 1}:");
                string ingredientName = Console.ReadLine();

                Console.WriteLine($"Please enter the quantity and measurement of {ingredientName} (e.g., 200 grams):");
                string[] parts = Console.ReadLine().Split(' ');

                if (parts.Length < 2)
                {
                    Console.WriteLine("Invalid input. Please enter the quantity and measurement again.");
                    i--;
                    continue;
                }

                double quantity = Convert.ToDouble(parts[0]);
                string measurement = string.Join(" ", parts.Skip(1));

                Console.WriteLine($"Please enter the food group of {ingredientName} (e.g., Carbohydrates, Sugars, Fats):");
                string foodGroup = Console.ReadLine();

                Console.WriteLine($"Please enter the number of calories in {ingredientName}:");
                int calories = Convert.ToInt32(Console.ReadLine());

                recipe.AddIngredient(ingredientName, quantity, measurement, foodGroup, calories);
            }

            Console.WriteLine("How many steps in this recipe?");
            int numSteps = Convert.ToInt32(Console.ReadLine());

            for (int i = 0; i < numSteps; i++)
            {
                Console.WriteLine($"Please enter step {i + 1}:");
                recipe.AddStep(Console.ReadLine());
            }

            recipes.Add(recipe);
            Console.WriteLine("Recipe created!");
        }

        public void ViewRecipes()
        {
            if (recipes.Count == 0)
            {
                Console.WriteLine("No recipes available to view.");
                return;
            }

            var sortedRecipes = recipes.OrderBy(r => r.Name).ToList();

            Console.WriteLine("Available recipes:");
            for (int i = 0; i < sortedRecipes.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {sortedRecipes[i].Name}");
            }

            Console.WriteLine("Enter the number of the recipe you wish to view:");
            int choice = Convert.ToInt32(Console.ReadLine()) - 1;

            if (choice >= 0 && choice < sortedRecipes.Count)
            {
                var selectedRecipe = sortedRecipes[choice];
                Console.WriteLine(selectedRecipe);
                int totalCalories = selectedRecipe.CalculateTotalCalories();

                if (totalCalories > 300 && OnCalorieNotification != null)
                {
                    OnCalorieNotification($"Warning: The recipe '{selectedRecipe.Name}' exceeds 300 calories (Total: {totalCalories} calories).");
                }
            }
            else
            {
                Console.WriteLine("Invalid choice.");
            }
        }

        public void ScaleRecipe(int recipeIndex, double factor)
        {
            if (recipeIndex >= 0 && recipeIndex < recipes.Count)
            {
                var recipe = recipes[recipeIndex];
                recipe.Scale(factor);
                Console.WriteLine("Recipe scaled successfully.");
            }
            else
            {
                Console.WriteLine("Invalid recipe index.");
            }
        }

        public void ResetRecipeScaling(int recipeIndex)
        {
            if (recipeIndex >= 0 && recipeIndex < recipes.Count)
            {
                var recipe = recipes[recipeIndex];
                recipe.ResetScaling(1); // Assuming the factor is 1 for reset
                Console.WriteLine("Recipe scaling reset successfully.");
            }
            else
            {
                Console.WriteLine("Invalid recipe index.");
            }
        }

        public void DeleteRecipe(int? recipeIndex = null)
        {
            if (recipeIndex.HasValue)
            {
                if (recipeIndex >= 0 && recipeIndex < recipes.Count)
                {
                    recipes.RemoveAt(recipeIndex.Value);
                    Console.WriteLine("Recipe deleted successfully.");
                }
                else
                {
                    Console.WriteLine("Invalid recipe index.");
                }
            }
            else
            {
                recipes.Clear();
                Console.WriteLine("All recipes deleted successfully.");
            }
        }
    }
}
