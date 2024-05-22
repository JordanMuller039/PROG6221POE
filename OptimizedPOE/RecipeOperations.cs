using System;
using System.Collections.Generic;
using System.Linq;

namespace POE
{
    internal class RecipeOperations
    {
        private List<Recipe> recipes = new List<Recipe>();
        private Dictionary<int, double> scaleFactors = new Dictionary<int, double>();

        public void CreateRecipe()
        {
            Recipe recipe = new Recipe();
            Console.WriteLine("How many ingredients in this recipe?");
            if (!int.TryParse(Console.ReadLine(), out int numIngredients) || numIngredients <= 0)
            {
                Console.WriteLine("Invalid input for the number of ingredients.");
                return;
            }

            for (int i = 0; i < numIngredients; i++)
            {
                Console.WriteLine($"Please enter the name of ingredient {i + 1}:");
                string ingredientName = Console.ReadLine();

                Console.WriteLine($"Please enter the quantity and measurement for {ingredientName} (e.g., '200 grams' or '1 liter'):");
                string input = Console.ReadLine();
                string[] parts = input.Split(new[] { ' ' }, 2, StringSplitOptions.RemoveEmptyEntries);

                if (parts.Length != 2 || !double.TryParse(parts[0], out double quantity))
                {
                    Console.WriteLine("Invalid input for ingredient quantity and measurement.");
                    i--;
                    continue;
                }

                string measurementType = parts[1];

                recipe.AddIngredient(ingredientName, quantity, measurementType);
            }

            Console.WriteLine("How many steps in this recipe?");
            if (!int.TryParse(Console.ReadLine(), out int numSteps) || numSteps <= 0)
            {
                Console.WriteLine("Invalid input for the number of steps.");
                return;
            }

            for (int i = 0; i < numSteps; i++)
            {
                Console.WriteLine($"Please enter step {i + 1}:");
                string step = Console.ReadLine();
                recipe.AddStep(step);
            }

            recipes.Add(recipe);
            scaleFactors[recipes.Count - 1] = 1.0; // Initialize scaling factor to 1
            Console.WriteLine("Recipe Created");
        }

        public void ViewRecipes()
        {
            if (recipes.Count == 0)
            {
                Console.WriteLine("No recipes available.");
                return;
            }

            for (int i = 0; i < recipes.Count; i++)
            {
                Console.WriteLine($"Recipe {i + 1}:\n{recipes[i]}");
            }
        }

        public void ScaleRecipe(int recipeIndex, double factor)
        {
            if (recipeIndex < 0 || recipeIndex >= recipes.Count)
            {
                Console.WriteLine("Invalid recipe index.");
                return;
            }

            if (factor != 0.5 && factor != 2 && factor != 3)
            {
                Console.WriteLine("Invalid scaling factor. Only 0.5, 2, and 3 are allowed.");
                return;
            }

            recipes[recipeIndex].Scale(factor);
            scaleFactors[recipeIndex] *= factor;
            Console.WriteLine("Recipe scaled successfully.");
        }

        public void ResetRecipeScaling(int recipeIndex)
        {
            if (recipeIndex < 0 || recipeIndex >= recipes.Count)
            {
                Console.WriteLine("Invalid recipe index.");
                return;
            }

            double factor = scaleFactors[recipeIndex];
            recipes[recipeIndex].ResetScaling(1 / factor);
            scaleFactors[recipeIndex] = 1.0; // Reset scaling factor
            Console.WriteLine("Recipe scaling reset successfully.");
        }

        public void DeleteRecipe(int? recipeIndex = null)
        {
            if (recipeIndex.HasValue)
            {
                if (recipeIndex.Value < 0 || recipeIndex.Value >= recipes.Count)
                {
                    Console.WriteLine("Invalid recipe index.");
                    return;
                }
                recipes.RemoveAt(recipeIndex.Value);
                scaleFactors.Remove(recipeIndex.Value);
                // Update scaleFactors keys
                var newScaleFactors = new Dictionary<int, double>();
                int i = 0;
                foreach (var factor in scaleFactors.Values)
                {
                    newScaleFactors[i++] = factor;
                }
                scaleFactors = newScaleFactors;
                Console.WriteLine("Recipe deleted successfully.");
            }
            else
            {
                recipes.Clear();
                scaleFactors.Clear();
                Console.WriteLine("All recipes have been cleared.");
            }
        }
    }
}
