=========================================================
                    Recipe Manager Application
=========================================================

Contents:
1. Overview
2. Features
3. How to Run the Application
4. Usage Instructions
5. Known Issues
6. Future Improvements
7. License

=========================================================
1. Overview
=========================================================

The Recipe Manager Application is a console-based program 
that allows users to create, view, scale, reset, and 
delete recipes. Each recipe can include multiple ingredients 
and steps. The application helps manage and organize recipes 
efficiently and provides options to adjust ingredient 
quantities by specified factors.

=========================================================
2. Features
=========================================================

- Create new recipes with specified ingredients and steps.
- View all created recipes with their ingredients and steps.
- Scale recipes by factors of 0.5, 2, or 3.
- Reset the scaling of recipes to their original quantities.
- Delete specific recipes or all recipes at once.

=========================================================
3. How to Run the Application
=========================================================

1. Ensure you have .NET installed on your system.
2. Open a terminal or command prompt.
3. Navigate to the directory containing the application files.
4. Compile the program using the following command:

=========================================================
4. Usage Instructions
=========================================================

Once the application is running, you will be presented 
with a menu with the following options:

1. New Recipe:
- Enter the number of ingredients.
- For each ingredient, enter the name first, followed by the quantity and measurement (e.g., "200 grams").
- Enter the number of steps.
- For each step, enter the step description.

2. View Recipes:
- Displays all the created recipes with their ingredients and steps.

3. Scale Recipe:
- Enter the recipe number to scale.
- Enter the scaling factor (0.5, 2, or 3).

4. Reset Scale:
- Enter the recipe number to reset its scaling to the original quantities.

5. Clear Recipes:
- Enter the recipe number to delete a specific recipe, or press Enter to delete all recipes.

6. Exit:
- Exits the application.

=========================================================
5. Known Issues
=========================================================

- Input validation is minimal; incorrect inputs may cause unexpected behavior.
- Recipes are stored in memory only; exiting the application will lose all recipes.
