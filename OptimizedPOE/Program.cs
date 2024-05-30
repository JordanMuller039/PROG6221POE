using System;

namespace POE
{
    internal class Program
    {
        static void Main(string[] args)
        {
            RecipeOperations recipeOps = new RecipeOperations();
            recipeOps.OnCalorieNotification += NotifyUser;

            string menuChoice;
            do
            {
                menuChoice = UserChoice();
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
                        DeleteRecipe(recipeOps);
                        break;
                    case "6":
                        Console.WriteLine("Exiting...");
                        break;
                    default:
                        Console.WriteLine("Invalid choice, please try again.");
                        break;
                }
            } while (menuChoice != "6");
        }

        static string UserChoice()
        {
            Console.WriteLine("\tMenu");
            Console.WriteLine("1) New Recipe");
            Console.WriteLine("2) View Recipes");
            Console.WriteLine("3) Scale Recipe");
            Console.WriteLine("4) Reset Scale");
            Console.WriteLine("5) Delete Recipe");
            Console.WriteLine("6) Exit");
            return Console.ReadLine();
        }

        static void ScaleRecipe(RecipeOperations recipeOps)
        {
            Console.WriteLine("Enter the recipe number to scale:");
            int recipeIndex = Convert.ToInt32(Console.ReadLine()) - 1;

            Console.WriteLine("Choose a scaling factor (0.5, 2, 3):");
            double factor = Convert.ToDouble(Console.ReadLine());

            if (factor == 0.5 || factor == 2 || factor == 3)
            {
                recipeOps.ScaleRecipe(recipeIndex, factor);
            }
            else
            {
                Console.WriteLine("Invalid scaling factor.");
            }
        }

        static void ResetRecipeScaling(RecipeOperations recipeOps)
        {
            Console.WriteLine("Enter the recipe number to reset scaling:");
            int recipeIndex = Convert.ToInt32(Console.ReadLine()) - 1;

            recipeOps.ResetRecipeScaling(recipeIndex);
        }

        static void DeleteRecipe(RecipeOperations recipeOps)
        {
            Console.WriteLine("Enter the recipe number to delete (leave blank to delete all):");
            string input = Console.ReadLine();

            if (string.IsNullOrEmpty(input))
            {
                recipeOps.DeleteRecipe();
            }
            else
            {
                int recipeIndex = Convert.ToInt32(input) - 1;
                recipeOps.DeleteRecipe(recipeIndex);
            }
        }

        static void NotifyUser(string message)
        {
            Console.WriteLine(message);
        }
    }
}
