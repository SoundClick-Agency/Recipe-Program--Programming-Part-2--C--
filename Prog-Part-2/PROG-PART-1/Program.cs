using System;
using System.Collections.Generic;

namespace RecipeProgram
{
    // Delegate to notify when recipe calories exceed 300
    public delegate void RecipeCaloriesExceededHandler(string recipeName, double totalCalories);

    public class Recipe
    {
        private List<Ingredient> ingredients; // store ingredients (list)
        private List<string> steps; // store steps (list)

        public string Name { get; set; }
        public double TotalCalories { get; private set; }

        public event RecipeCaloriesExceededHandler CaloriesExceeded;

        // Constructor
        public Recipe(string name)
        {
            Name = name;
            ingredients = new List<Ingredient>();
            steps = new List<string>();
            TotalCalories = 0;
        }

        // Add ingredient to recipe
        public void AddIngredient(Ingredient ingredient)
        {
            ingredients.Add(ingredient);
            TotalCalories += ingredient.Calories;

            if (TotalCalories > 300)
            {
                CaloriesExceeded?.Invoke(Name, TotalCalories);
            }
        }

        // Add step to recipe
        public void AddStep(string step)
        {
            steps.Add(step);
        }

        // Display recipe details
        public void DisplayRecipe()
        {
        Console.WriteLine("***************************************************");
                     Console.WriteLine($"Recipe: {Name}");
            Console.WriteLine("Ingredients:");

     // Display ingredients





            foreach (Ingredient ingredient in ingredients)
            {





                Console.WriteLine(ingredient.ToString());
            }

            Console.WriteLine("Steps:");

                           // Display steps    
            for (int i = 0; i < steps.Count; i++)





            {
                Console.WriteLine($"{i + 1}. {steps[i]}");
            }





            Console.WriteLine($"Total Calories: {TotalCalories}");
                                  Console.WriteLine("***************************************************");
        }
    }

    public class Ingredient
    {
        public string Name { get; set; }
                    public double Quantity { get; set; }
                  public string Unit { get; set; }
                public double Calories { get; set; }
        public string FoodGroup { get; set; }

        public Ingredient(string name, double quantity, string unit, double calories, string foodGroup)
        {
                    Name = name;
                    Quantity = quantity;
                 Unit = unit;
                     Calories = calories;
            FoodGroup = foodGroup;
        }     

        public override string ToString()
        {
                     return $"{Quantity} {Unit} of {Name} ({Calories} calories)";
        }
    }

           class Program
    {
                  static List<Recipe> recipes = new List<Recipe>();

        static void Main(string[] args)
        {
            while (true)
            {
                DisplayMainMenu();
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        EnterRecipeDetails();
                             break;
                    case "2":
                        DisplayAllRecipes();
                             break;
                            case "3":
                        Environment.Exit(0);
                                break;
                    default:
                        Console.WriteLine("Invalid choice. Please enter a valid option.");
                        break;
                }
            }
        }

        static void DisplayMainMenu()
        {
            Console.WriteLine("***************************************************");
              Console.WriteLine("Recipe Application");
            Console.WriteLine("***************************************************");
            Console.WriteLine("1. Enter recipe details");
            Console.WriteLine("2. Display all recipes");
              Console.WriteLine("3. Exit");
             Console.WriteLine("***************************************************");
            Console.WriteLine("Enter your choice:");
        }

        static void EnterRecipeDetails()
        {
            Console.WriteLine("***************************************************");
            Console.WriteLine("Enter recipe name:");
            string recipeName = Console.ReadLine();

            Recipe recipe = new Recipe(recipeName);
            recipe.CaloriesExceeded += HandleCaloriesExceeded;

            Console.WriteLine("Enter the number of ingredients:");
            int ingredientCount = Convert.ToInt32(Console.ReadLine());

            // input ingredient details - loop
            for (int i = 0; i < ingredientCount; i++)
            {
                Console.WriteLine($"Enter the name of ingredient {i + 1}:");
                string name = Console.ReadLine();

                Console.WriteLine($"Enter the quantity of {name}:");
                double quantity = Convert.ToDouble(Console.ReadLine());

                Console.WriteLine($"Enter the unit of measurement for {name}:");
                string unit = Console.ReadLine();

                Console.WriteLine($"Enter the number of calories for {name}:");
                double calories = Convert.ToDouble(Console.ReadLine());

                Console.WriteLine($"Enter the food group for {name}:");
                string foodGroup = Console.ReadLine();

                Ingredient ingredient = new Ingredient(name, quantity, unit, calories, foodGroup);
                recipe.AddIngredient(ingredient);
            }

            Console.WriteLine("Enter the number of steps:");
            int stepCount = Convert.ToInt32(Console.ReadLine());

            // input steps - loop
            for (int i = 0; i < stepCount; i++)
            {
                Console.WriteLine($"Enter step {i + 1}:");
                string step = Console.ReadLine();
                recipe.AddStep(step);
            }

            recipes.Add(recipe);
            Console.WriteLine("Recipe added successfully!");
            Console.WriteLine("***************************************************");
        }

        static void DisplayAllRecipes()
        {
            if (recipes.Count == 0)
            {
                Console.WriteLine("No recipes available.");
            }
            else
            {
                recipes.Sort((x, y) => x.Name.CompareTo(y.Name)); // Sort recipes by name

                foreach (Recipe recipe in recipes)
                {
                    Console.WriteLine(recipe.Name);
                }

                Console.WriteLine("***************************************************");
                Console.WriteLine("Enter the name of the recipe to display:");
                string recipeName = Console.ReadLine();

                Recipe selectedRecipe = recipes.Find(r => r.Name.Equals(recipeName, StringComparison.OrdinalIgnoreCase));
                if (selectedRecipe != null)
                {
                    selectedRecipe.DisplayRecipe();
                }
                else
                {
                    Console.WriteLine("Recipe not found.");
                }
            }
        }

        static void HandleCaloriesExceeded(string recipeName, double totalCalories)
        {
            Console.WriteLine($"Warning: The total calories of recipe '{recipeName}' exceed 300. Total calories: {totalCalories}");
        }
    }
}
