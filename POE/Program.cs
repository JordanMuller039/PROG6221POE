using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POE
{
    internal class Program
    {
        //Creating Multiply and Divide Variables which will later be given a value by the user
        static int Divide = 1, Multiply = 1;

        //Main Method which just gives the UserChoice method so that the program knows what the user wants to do
        static void Main(string[] args)
        {
            String MenuChoice = UserChoice();
            RecipeOperations(MenuChoice);   
        }


        //This method gives the user a choice for them so that they can create or view methods
        public static String UserChoice()
        {
            Console.WriteLine("\tMenu");
            Console.WriteLine("1) New Recipe");
            Console.WriteLine("2) View Recipe");
            Console.WriteLine("3) Scale Recipe");
            Console.WriteLine("4) Reset Scale");
            Console.WriteLine("5) Clear Recipe");
            String choice = Console.ReadLine();
            return choice;
        }

        //Once the user has chosen an option it will then run this method which takes the input and decides what opertation to run
        public static void RecipeOperations(String MenuChoice) 
        {
            RecipeOperations recipeOPs = new RecipeOperations();
            String mc = "";
            //If user chose to Create a recipe
            if (MenuChoice == "1")
            {
                recipeOPs.CreateRecipe();
                Console.WriteLine("\n");
                mc = UserChoice();
                RecipeOperations(mc);
            }
            //If user chose to view the recipe they just made
            else if (MenuChoice == "2")
            {
                Console.WriteLine(recipeOPs.ToString());
                Console.WriteLine("\n");
                mc = UserChoice();
                RecipeOperations(mc);
            }
            //If the User chose to ratio the quantity
            else if (MenuChoice == "3")
            {
                Console.WriteLine("Type D for Divide or M for Multiply");
                String Ratio = Console.ReadLine().ToLower();
                if(Ratio == "d") 
                {
                    Console.WriteLine("Enter Amount to Divide by:");
                    Divide = Convert.ToInt32(Console.ReadLine());
                    recipeOPs.DivideQuantity(Divide);
                    Console.WriteLine(recipeOPs.ToString());
                    mc = UserChoice();
                    RecipeOperations(mc);

                }
                else if(Ratio == "m")
                {
                    Console.WriteLine("Enter Amount to Multiply by:");
                    Multiply = Convert.ToInt32(Console.ReadLine());
                    recipeOPs.MultiplyQuantity(Multiply);
                    Console.WriteLine(recipeOPs.ToString());
                    mc = UserChoice();
                    RecipeOperations(mc);
                }
            }
            //if the User chose to reset the ratio of the quantity back to its original 
            else if (MenuChoice == "4")
            {
                recipeOPs.ResetQuant();
                Console.WriteLine("\n");
                mc = UserChoice();
                RecipeOperations(mc);
            }
            //If the user chose to delete/clear the recipe from memory
            else if (MenuChoice == "5")
            {
                
            }
        }
    }
}
