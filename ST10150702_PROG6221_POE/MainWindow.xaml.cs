using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ST10150702_PROG6221_POE
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Ingredient> ingredients = new List<Ingredient>();
        private List<string> steps = new List<string>();
        public MainWindow()
        {
            InitializeComponent();

            // Creating the food groups
            List<string> foodgroups = new List<string>
            {
                "Carbohydrates",
                "Protein",
                "Fats",
                "Vegetables",
                "Nothing"
            };

            cbFoodGroups.ItemsSource = foodgroups;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Get the input values
            string name = tbIName.Text;
            if (!double.TryParse(tbIQuantity.Text.Split(' ')[0], out double quantity))
            {
                MessageBox.Show("Please enter a valid quantity.");
                return;
            }
            string measurement = string.Join(" ", tbIQuantity.Text.Split(' ').Skip(1));
            string foodGroup = (cbFoodGroups.SelectedItem as ComboBoxItem)?.Content.ToString();
            if (!int.TryParse(tbICalorie.Text, out int calories))
            {
                MessageBox.Show("Please enter a valid calorie amount.");
                return;
            }

            // Create and add the ingredient
            Ingredient ingredient = new Ingredient { Name = name, Quantity = quantity, Measurement = measurement, FoodGroup = foodGroup, Calories = calories };
            ingredients.Add(ingredient);

            // Clear the input fields
            tbIName.Clear();
            tbIQuantity.Clear();
            cbFoodGroups.SelectedIndex = -1;
            tbICalorie.Clear();

            MessageBox.Show("Ingredient added successfully!");
        }

        private void CreateRecipe_Click(object sender, RoutedEventArgs e)
        {
            // Get the recipe name
            string recipeName = tbRecipeName.Text;
            // Get the steps (one per line)
            steps = new List<string>(tbSteps.Text.Split(new[] { '\r', '\n' }, System.StringSplitOptions.RemoveEmptyEntries));

            // Create the recipe
            Recipe recipe = new Recipe(recipeName);
            foreach (var ingredient in ingredients)
            {
                recipe.AddIngredient(ingredient.Name, ingredient.Quantity, ingredient.Measurement, ingredient.FoodGroup, ingredient.Calories);
            }
            foreach (var step in steps)
            {
                recipe.AddStep(step);
            }

            // Display the recipe in the RichTextBox
            rtbDisplay.Document.Blocks.Clear();
            rtbDisplay.Document.Blocks.Add(new System.Windows.Documents.Paragraph(new System.Windows.Documents.Run(recipe.ToString())));

            MessageBox.Show("Recipe created successfully!");

            // Clear the form
            tbRecipeName.Clear();
            tbIName.Clear();
            tbIQuantity.Clear();
            cbFoodGroups.SelectedIndex = -1;
            tbICalorie.Clear();
            tbSteps.Clear();
            ingredients.Clear();
            steps.Clear();
        }
    }

    public class Ingredient
    {
        public string Name { get; set; }
        public double Quantity { get; set; }
        public string Measurement { get; set; }
        public string FoodGroup { get; set; }
        public int Calories { get; set; }
    }

    public class Recipe
    {
        public string Name { get; set; }
        private List<Ingredient> ingredients = new List<Ingredient>();
        private List<string> steps = new List<string>();

        public Recipe(string name)
        {
            Name = name;
        }

        public void AddIngredient(string name, double quantity, string measurement, string foodGroup, int calories)
        {
            ingredients.Add(new Ingredient { Name = name, Quantity = quantity, Measurement = measurement, FoodGroup = foodGroup, Calories = calories });
        }

        public void AddStep(string step)
        {
            steps.Add(step);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("===============================");
            sb.AppendLine($"Recipe Name: {Name}");
            sb.AppendLine("===============================");
            sb.AppendLine("Ingredients:");
            foreach (var ingredient in ingredients)
            {
                sb.AppendLine($"{ingredient.Quantity} {ingredient.Measurement} of {ingredient.Name} ({ingredient.FoodGroup}, {ingredient.Calories} calories)");
            }
            sb.AppendLine("===============================");
            sb.AppendLine("Steps:");
            for (int i = 0; i < steps.Count; i++)
            {
                sb.AppendLine($"{i + 1}. {steps[i]}");
            }
            sb.AppendLine("===============================");
            sb.AppendLine($"Total Calories: {CalculateTotalCalories()}");
            return sb.ToString();
        }

        private int CalculateTotalCalories()
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