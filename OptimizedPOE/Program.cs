using System;

namespace POE
{
    internal class Program
    {
        static void Main(string[] args)
        {
            RecipeOperations recipeOps = new RecipeOperations();
            while (true)
            {
                string menuChoice = GetUserChoice();
                ExecuteChoice(menuChoice, recipeOps);
            }
        }

        // This method gives the user a choice for what they want to do
        public static string GetUserChoice()
        {
            Console.WriteLine("\tMenu");
            Console.WriteLine("1) New Recipe");
            Console.WriteLine("2) View Recipes");
            Console.WriteLine("3) Scale Recipe");
            Console.WriteLine("4) Reset Scale");
            Console.WriteLine("5) Clear Recipes");
            Console.WriteLine("6) Exit");
            Console.Write("Enter your choice: ");
            return Console.ReadLine();
        }

        // This method takes the user input and decides what operation to run
        public static void ExecuteChoice(string menuChoice, RecipeOperations recipeOps)
        {
            switch (menuChoice)
            {
                case "1":
                    recipeOps.CreateRecipe();
                    break;

                case "2":
                    recipeOps.ViewRecipes();
                    break;

                case "3":
                    ScaleRecipe(recipeOps);
                    break;

                case "4":
                    ResetRecipeScaling(recipeOps);
                    break;

                case "5":
                    DeleteRecipes(recipeOps);
                    break;

                case "6":
                    Environment.Exit(0);
                    break;

                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }

            Console.WriteLine();
        }

        // This method handles scaling the recipe
        public static void ScaleRecipe(RecipeOperations recipeOps)
        {
            Console.WriteLine("Enter the recipe number to scale:");
            if (int.TryParse(Console.ReadLine(), out int recipeIndex))
            {
                Console.WriteLine("Enter scaling factor (0.5 to halve, 2 to double, 3 to triple):");
                if (double.TryParse(Console.ReadLine(), out double factor))
                {
                    if (factor == 0.5 || factor == 2 || factor == 3)
                    {
                        recipeOps.ScaleRecipe(recipeIndex - 1, factor); // Subtract 1 to convert to zero-based index
                    }
                    else
                    {
                        Console.WriteLine("Invalid scaling factor. Only 0.5, 2, and 3 are allowed.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input for scaling factor.");
                }
            }
            else
            {
                Console.WriteLine("Invalid input for recipe number.");
            }
        }

        // This method handles resetting the scaling of a recipe
        public static void ResetRecipeScaling(RecipeOperations recipeOps)
        {
            Console.WriteLine("Enter the recipe number to reset scaling:");
            if (int.TryParse(Console.ReadLine(), out int recipeIndex))
            {
                recipeOps.ResetRecipeScaling(recipeIndex - 1); // Subtract 1 to convert to zero-based index
            }
            else
            {
                Console.WriteLine("Invalid input for recipe number.");
            }
        }

        // This method handles deleting recipes
        public static void DeleteRecipes(RecipeOperations recipeOps)
        {
            Console.WriteLine("Enter the recipe number to delete or press Enter to delete all recipes:");
            string input = Console.ReadLine();
            if (int.TryParse(input, out int recipeIndex))
            {
                recipeOps.DeleteRecipe(recipeIndex - 1); // Subtract 1 to convert to zero-based index
            }
            else if (string.IsNullOrWhiteSpace(input))
            {
                recipeOps.DeleteRecipe();
            }
            else
            {
                Console.WriteLine("Invalid input for recipe number.");
            }
        }
    }
}
