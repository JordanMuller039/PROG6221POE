using System;
using System.Collections.Generic;
using System.Linq;

namespace POE
{
    internal class RecipeOperations
    {
        private List<Recipe> recipes = new List<Recipe>();

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

                recipe.AddIngredient(ingredientName, quantity, measurement);
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
                Console.WriteLine(sortedRecipes[choice]);
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
                // Implementation here if needed
                Console.WriteLine("Reset functionality is not implemented.");
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
