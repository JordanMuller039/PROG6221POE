using System;

/* Summary
 This class contains the main method in which the user is prompted with choices 
for what they would like to do in the program, this class is used alot to navigate 
through the program*/

namespace POE
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Creating a new instance of RecipeOperations Class
            RecipeOperations recipeOps = new RecipeOperations();
            recipeOps.OnCalorieNotification += NotifyUser;

            // Getting the choice from the user
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

        // Displaying all the choices the user has
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

        // If Scale recipe is chosen, the program will ask the ratio and send it to recipeOps
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

        // This is if the user choses to reset the scale
        static void ResetRecipeScaling(RecipeOperations recipeOps)
        {
            Console.WriteLine("Enter the recipe number to reset scaling:");
            int recipeIndex = Convert.ToInt32(Console.ReadLine()) - 1;

            recipeOps.ResetRecipeScaling(recipeIndex);
        }

        // If the user choses to delete the recipe from memory
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

        // This is if the calories exceed 300, the user will be notified
        static void NotifyUser(string message)
        {
            Console.WriteLine(message);
        }
    }
}
