using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace POE
{
    internal class Recipe
    {
        public Dictionary<string, double> Ingredients { get; private set; } = new Dictionary<string, double>();
        public Dictionary<string, string> Measurements { get; private set; } = new Dictionary<string, string>();
        public List<string> Steps { get; private set; } = new List<string>();

        public void AddIngredient(string name, double quantity, string measurement)
        {
            Ingredients[name] = quantity;
            Measurements[name] = measurement;
        }

        public void AddStep(string step)
        {
            Steps.Add(step);
        }

        public override string ToString()
        {
            StringBuilder output = new StringBuilder("***************************\nIngredients:");
            foreach (var ingredient in Ingredients)
            {
                output.AppendLine($"{ingredient.Value} {Measurements[ingredient.Key]} {ingredient.Key}");
            }

            output.AppendLine("Steps:");
            for (int i = 0; i < Steps.Count; i++)
            {
                output.AppendLine($"{i + 1}. {Steps[i]}");
            }
            output.AppendLine("***************************");
            return output.ToString();
        }

        public void Scale(double factor)
        {
            foreach (var key in Ingredients.Keys.ToList())
            {
                Ingredients[key] *= factor;
            }
        }

        public void ResetScaling(double factor)
        {
            foreach (var key in Ingredients.Keys.ToList())
            {
                Ingredients[key] /= factor;
            }
        }
    }
}