using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

//References
// 1    https://www.loginradius.com/blog/engineering/how-to-create-and-use-dictionary-csharp/
// 2    https://stackoverflow.com/questions/1070766/editing-dictionary-values-in-a-foreach-loop


namespace POE
{
    internal class RecipeOperations
    {
        //Creating the Dictionaries 
        static String[] arrSteps;
        static Dictionary<string, double> dictIngredient = new Dictionary<string, double>();
        static Dictionary<string, string> dictType = new Dictionary<string, string>();

        //Creating variables for if the user chages the ratio so that I can reset the ratio later
        static Boolean isMultiplied = false, isDivide = false;
        static int M = 0, D = 0;

        //Defualt Constructor
        public RecipeOperations() 
        {
            
        }


        //Create Recipe Method which will take in the users input and then create a recipe from that s
        public void CreateRecipe()
        {
            Console.WriteLine("How many ingredients in this recipe?");
            int NumIngredients = Convert.ToInt32(Console.ReadLine());

            //Im using 2 dictionaries that share the same Key however have different values thereby linking them, similar to a parallel array 
            //The shared Key being the Ingredient Name as it is a unique identifier
            for (int i = 0; i < NumIngredients; i++)
            {
                Console.WriteLine("Please enter the Name of the " + (i+1) + " ingredient");
                String IngredientName = Console.ReadLine();

                Console.WriteLine("Please enter the Quantity of the Ingredient " + (i+1));
                double IngredientQuantity = Convert.ToDouble(Console.ReadLine());

                //Adding to dictIngredient
                dictIngredient.Add(IngredientName, IngredientQuantity);

                Console.WriteLine("Please enter the Measurement of the Ingredient " + (i+1));
                String MeasurementType = Console.ReadLine();

                //Adding to the dictType
                dictType.Add(IngredientName, MeasurementType);

            }

            Console.WriteLine("Recipe Created");

        }

        //Overriden ToString method to display to the user the recipe they have created
        public override string ToString()
        {
            String Output = "***************************\nIngredients:";
            for (int i = 0; i < dictIngredient.Count; i++)
            {
                Output = Output + "\n" + dictIngredient.ElementAt(i).Value + " " + dictType.ElementAt(i).Value + " " + dictIngredient.ElementAt(i).Key;
            }
            Output = Output + "\n***************************";

            return Output;
        }

        //If the user chose to Divide the measurements, this method will run
        public void DivideQuantity(int Divide)
        {
            D = Divide;
            isDivide = true;
            foreach (var ingredient in dictIngredient.ToList())
            {
                dictIngredient[ingredient.Key] /= Divide;
            }

            Console.WriteLine("Quantities have been Divided");
            Console.WriteLine("\nUpdated quantities:");
            foreach (var ingredient in dictIngredient)
            {
                Console.WriteLine($"{ingredient.Key}: {ingredient.Value} {dictType[ingredient.Key]}");
            }
        }

        //If the User chose to Multiply the measurements, this method will run
        public void MultiplyQuantity(int Multiply)
        {
            M = Multiply;
            isMultiplied = true;
            foreach (var ingredient in dictIngredient.ToList())
            {
                dictIngredient[ingredient.Key] *= Multiply;
            }

            Console.WriteLine("Quantities have been Multiplied");

            //This outputs back to the user the new updated Quantities
            Console.WriteLine("\nUpdated quantities:");
            foreach (var ingredient in dictIngredient)
            {
                Console.WriteLine($"{ingredient.Key}: {ingredient.Value} {dictType[ingredient.Key]}");
            }
        }

        public void ResetQuant()
        {
            //If the user previously had mutliplied the recipes ratio, this method will divide the recipe in reverse to counter it
            if (isMultiplied==true)
            {
                foreach (var ingredient in dictIngredient.ToList())
                {
                    dictIngredient[ingredient.Key] /= M;
                }

                foreach (var ingredient in dictIngredient)
                {
                    Console.WriteLine($"{ingredient.Key}: {ingredient.Value} {dictType[ingredient.Key]}");
                }

            }
            //If the user previously had divided the recipes ratio, this method will multiply the recipe in reverse to counter it
            else if (isDivide==true) 
            {
                foreach (var ingredient in dictIngredient.ToList())
                {
                    dictIngredient[ingredient.Key] *= D;
                }

                foreach (var ingredient in dictIngredient)
                {
                    Console.WriteLine($"{ingredient.Key}: {ingredient.Value} {dictType[ingredient.Key]}");
                }
            }
        }

        //If the user chose to delete the recipe this method will run and clear the dictionaries
        public void DeleteRecipe()
        {
            dictIngredient.Clear();
            dictType.Clear();
            Console.WriteLine("Recipes have been Wiped!");
        }

    }
}
