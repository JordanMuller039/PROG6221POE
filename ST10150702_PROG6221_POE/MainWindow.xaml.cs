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
        private List<Recipe> recipes = new List<Recipe>();
        private List<string> filteringredients = new List<string>();
        private List<Recipe> menuRecipes = new List<Recipe>();
        private List<Ingredient> originalIngredients;
        public List<string> foodgroups = new List<string>

            {
                "Carbohydrates",
                "Protein",
                "Fats",
                "Vegetables",
                "Nothing"
            };
        public MainWindow()
        {
            InitializeComponent();

            cbFoodGroups.ItemsSource = foodgroups;

        }

        private void UpdateScaleComboBox()
        {
            cbScaleRecipe.Items.Clear();
            foreach (var recipe in recipes)
            {
                cbScaleRecipe.Items.Add(recipe.rName);
            }
        }

        private void CbAddMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbAddMenu.SelectedItem != null)
            {
                Recipe selectedRecipe = (Recipe)cbAddMenu.SelectedItem;
                DisplayRecipe(selectedRecipe);
            }
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
            string foodGroup = (cbFoodGroups.SelectedItem as ComboBoxItem)?.Content.ToString() ?? "Unknown"; // Set default value if null
            if (!int.TryParse(tbICalorie.Text, out int calories))
            {
                MessageBox.Show("Please enter a valid calorie amount.");
                return;
            }

            // Create and add the ingredient
            Ingredient ingredient = new Ingredient { Name = name, Quantity = quantity, Measurement = measurement, FoodGroup = foodGroup, Calories = calories };
            filteringredients.Add(name);
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
            cbIngredients.ItemsSource = filteringredients;
            cbFilterFoodGroup.ItemsSource = foodgroups;
            // Get the steps (one per line)
            steps = new List<string>();
            foreach (var block in rtbSteps.Document.Blocks)
            {
                if (block is Paragraph paragraph)
                {
                    string text = new TextRange(paragraph.ContentStart, paragraph.ContentEnd).Text.Trim();
                    if (!string.IsNullOrEmpty(text))
                    {
                        steps.Add(text);
                    }
                }
            }

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

            recipes.Add(recipe);
            cbRecipe.Items.Add(recipeName);
            cbAddMenu.Items.Add(recipeName);
            UpdateScaleComboBox();

            // Display the recipe in the RichTextBox
            rtbDisplay.Document.Blocks.Clear();
            rtbDisplay.Document.Blocks.Add(new System.Windows.Documents.Paragraph(new System.Windows.Documents.Run(recipe.ToString())));
            recipe.CheckCalories();

            MessageBox.Show("Recipe created successfully!");

            // Clear the form
            tbRecipeName.Clear();
            tbIName.Clear();
            tbIQuantity.Clear();
            cbFoodGroups.SelectedIndex = -1;
            tbICalorie.Clear();
            rtbSteps.Document.Blocks.Clear();
            ingredients.Clear();
            steps.Clear();
        }
        private void NotifyUser(string message)
        {
            MessageBox.Show(message, "Calorie Notification", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void btnViewRecipe_Click(object sender, RoutedEventArgs e)
        {
            string selectedRecipeName = cbRecipe.SelectedItem as string;

            if (string.IsNullOrEmpty(selectedRecipeName))
            {
                MessageBox.Show("Please select a recipe.");
                return;
            }

            // Find the selected recipe
            Recipe selectedRecipe = recipes.FirstOrDefault(r => r.rName == selectedRecipeName);

            if (selectedRecipe == null)
            {
                MessageBox.Show("Recipe not found.");
                return;
            }

            // Display the recipe in the RichTextBox
            rtbDisplay2.Document.Blocks.Clear();
            rtbDisplay2.Document.Blocks.Add(new Paragraph(new Run(selectedRecipe.ToString())));
        }


        // Couldnt get Filtering to work but tried none the less :)
       private void btnFilter_Click(object sender, RoutedEventArgs e)
        {
            var filteredRecipes = recipes.AsEnumerable();

            // Filter by Ingredient if specified
            if (cbIngredients.SelectedItem != null)
            {
                var selectedIngredient = cbIngredients.SelectedItem.ToString();
                filteredRecipes = filteredRecipes.Where(recipe => recipe.Ingredients.Any(i => i.Name == selectedIngredient));
            }

            // Filter by Food Group if specified
            if (cbFilterFoodGroup.SelectedItem != null)
            {
                var selectedFoodGroup = cbFilterFoodGroup.SelectedItem.ToString();
                filteredRecipes = filteredRecipes.Where(recipe => recipe.Ingredients.Any(i => i.FoodGroup == selectedFoodGroup));
            }

            // Filter by Max Calories if specified
            if (!string.IsNullOrEmpty(tbFilterCalories.Text) && int.TryParse(tbFilterCalories.Text, out int maxCalories))
            {
                filteredRecipes = filteredRecipes.Where(recipe => recipe.TotalCalories <= maxCalories);
            }

            // Display the filtered recipes
            rtbDisplay2.Document.Blocks.Clear();
            foreach (var recipe in filteredRecipes)
            {
                rtbDisplay2.AppendText($"{recipe.rName}\n");
                rtbDisplay2.AppendText($"Steps: {recipe.Steps}\n");
                rtbDisplay2.AppendText($"Total Calories: {recipe.TotalCalories}\n");
                rtbDisplay2.AppendText($"Ingredients:\n");
                foreach (var ingredient in recipe.Ingredients)
                {
                    rtbDisplay2.AppendText($"{ingredient.Name} - {ingredient.FoodGroup} - {ingredient.Calories} calories\n");
                }
                rtbDisplay2.AppendText("\n");
            }
        }

        private void btnScaleHalf_Click(object sender, RoutedEventArgs e)
        {
            ScaleRecipe(0.5);
        }

        private void btnScaleDouble_Click(object sender, RoutedEventArgs e)
        {
            ScaleRecipe(2);
        }

        private void btnScaleTriple_Click(object sender, RoutedEventArgs e)
        {
            ScaleRecipe(3);
        }

        private void ScaleRecipe(double factor)
        {
            if (cbScaleRecipe.SelectedItem == null)
            {
                MessageBox.Show("Please select a recipe to scale.");
                return;
            }

            string selectedRecipeName = cbScaleRecipe.SelectedItem.ToString();
            var selectedRecipe = recipes.FirstOrDefault(r => r.rName == selectedRecipeName);

            if (selectedRecipe != null)
            {
                if (originalIngredients == null)
                {
                    originalIngredients = selectedRecipe.Ingredients.Select(i => new Ingredient
                    {
                        Name = i.Name,
                        Quantity = i.Quantity,
                        FoodGroup = i.FoodGroup,
                        Calories = i.Calories
                    }).ToList();
                }

                foreach (var ingredient in selectedRecipe.Ingredients)
                {
                    ingredient.Quantity *= factor;
                }

                DisplayRecipe(selectedRecipe);
            }
        }

            private void DisplayRecipe(Recipe recipe)
        {
            rtbDisplay3.Document.Blocks.Clear();
            rtbDisplay3.AppendText($"{recipe.rName}\n");
            rtbDisplay3.AppendText($"Steps: {recipe.Steps}\n");
            rtbDisplay3.AppendText($"Total Calories: {recipe.TotalCalories}\n");
            rtbDisplay3.AppendText($"Ingredients:\n");
            foreach (var ingredient in recipe.Ingredients)
            {
                rtbDisplay3.AppendText($"{ingredient.Name} - {ingredient.Quantity} - {ingredient.FoodGroup} - {ingredient.Calories} calories\n");
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (cbAddMenu.SelectedItem != null)
            {
                string selectedRecipeName = cbAddMenu.SelectedItem.ToString();
                Recipe selectedRecipe = recipes.FirstOrDefault(r => r.rName == selectedRecipeName);

                if (selectedRecipe != null)
                {
                    menuRecipes.Add(selectedRecipe);
                    rtbDisplay4.AppendText(selectedRecipe.ToString() + "\n");
                }
            }
        }

        private void ShowGraph_Click(object sender, RoutedEventArgs e)
        {
            Dictionary<string, int> foodGroupCounts = new Dictionary<string, int>();

            foreach (var recipe in menuRecipes)
            {
                foreach (var ingredient in recipe.ingredients)
                {
                    if (ingredient.FoodGroup != null)
                    {
                        if (foodGroupCounts.ContainsKey(ingredient.FoodGroup))
                        {
                            foodGroupCounts[ingredient.FoodGroup] += ingredient.Calories;
                        }
                        else
                        {
                            foodGroupCounts[ingredient.FoodGroup] = ingredient.Calories;
                        }
                    }
                }
            }

            if (foodGroupCounts.Count == 0)
            {
                MessageBox.Show("No food group data found.");
                return;
            }

            ChartWindow chartWindow = new ChartWindow();
            chartWindow.SetFoodGroupCounts(foodGroupCounts);
            chartWindow.ShowPieChart();
            chartWindow.Show();
        }

        private void DisplayPieChart(Dictionary<string, int> foodGroupCounts)
        {
            ChartWindow chartWindow = new ChartWindow();
            chartWindow.ShowPieChart();
            chartWindow.Show();
        }
    }
}