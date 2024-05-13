// See https://aka.ms/new-console-template for more information
using System.ComponentModel.Design;

internal class Program
{
    private static void Main(string[] args)
    {
        Dictionary<String, Recipe> dictionaryRecipe = new Dictionary<string, Recipe>();
        Appmenu menu = new Appmenu(dictionaryRecipe);
        menu.appMenu();
    }
}

class recipeLogger
{
    private Dictionary<String, Recipe> dictionaryRecipe;
    public recipeLogger(Dictionary<string, Recipe> dictionaryRecipe)
    {
        this.dictionaryRecipe = dictionaryRecipe;
    }

    public void LogDetails()
    {
        Console.WriteLine("Enter the number of recipes: ");
        int recipeNum;
        if (int.TryParse(Console.ReadLine(), out recipeNum))
        {
            for (int i = 0; i < recipeNum; i++)
            {
                Console.Write("Enter recipe name:");
                String recipeName = Console.ReadLine();
                Recipe recipe = new Recipe();
                recipe.EnterData();
                dictionaryRecipe.Add(recipeName, recipe);
            }
            String ans;
            do
            {
                Console.WriteLine("Display Ingredients and steps?(Y/N)");
                ans = Console.ReadLine();
                switch (ans)
                {
                    case "Y":
                        foreach (var recipeEntry in dictionaryRecipe)
                        {
                            Console.WriteLine($"Recipe Name:{recipeEntry.Key}");
                            recipeEntry.Value.RecipeDisplay();
                        }
                        break;
                    case "N":
                        Appmenu menu = new Appmenu(dictionaryRecipe);
                        menu.appMenu();
                        break;
                    default:
                        Console.WriteLine("Please enter a valid input");
                        break;
                }


            } while (ans != "N");
        }
        else
        {
            Console.WriteLine("Please enter a number");
        }
    }
    public void recipeList()
    {
        foreach (var recipeEntry in dictionaryRecipe)
        {
            Console.WriteLine($"Recipe Name:{recipeEntry.Key}");
        }
    }
    public void recipeFinder()
    {
        Console.Write("Please enter the name of the recipe:");
        string recipeName = Console.ReadLine();
        if (dictionaryRecipe.ContainsKey(recipeName))
        {
            Console.WriteLine($"Recipe Name:{recipeName}");
            dictionaryRecipe[recipeName].RecipeDisplay();
        }
        else
        {
            Console.WriteLine("Recipe does not exist");
        }
    }
}
class ingredientMenu
{
    private Dictionary<String, Recipe> dictionaryRecipe;
    public ingredientMenu(Dictionary<string, Recipe> dictionaryRecipe)
    {
        this.dictionaryRecipe = dictionaryRecipe;
        // This code will show menu
        Recipe recipe = new Recipe();
        while (true)
        {
            Console.WriteLine("------------------------------------------------");
            Console.WriteLine("Enter 1 to Enter Recipe Details");
            Console.WriteLine("Enter 2 to Display Recipe");
            Console.WriteLine("Enter 3 to Scale Recipe");
            Console.WriteLine("Enter 4 to Reset Quantities");
            Console.WriteLine("Enter 5 to Clear Recipes");
            Console.WriteLine("Enter 6 to go back to main menu");
            Console.WriteLine("------------------------------------------------");
            string ans = Console.ReadLine();
            switch (ans)
            {
                case "1":
                    recipe.EnterData();
                    break;
                case "2":
                    recipe.RecipeDisplay();
                    break;
                case "3":
                    Console.WriteLine("Enter a scale of 0.5,2 or 3");
                    double scale1 = Convert.ToDouble(Console.ReadLine());
                    recipe.RecipeScale(scale1);
                    break;
                case "4":
                    recipe.ResetRecipe();
                    break;
                case "5":
                    recipe.ClearRecipe();
                    break;
                case "6":
                    Appmenu appMenu = new Appmenu(dictionaryRecipe);
                    appMenu.appMenu();
                    break;
                default:
                    Console.WriteLine("Wrong value. Please try again");
                    break;
            }
        }
    }
}

class Recipe
{
    private String[] ingredients;
    private double[] amount;
    private String[] units;
    private String[] steps;
    private double[] calories;
    private string[] foodGroup;

    public Recipe()
    {
        ingredients = new string[0];
        amount = new double[0];
        units = new String[0];
        steps = new String[0];
        calories = new double[0];
        foodGroup = new String[0];
    }
    public void EnterData()
    // this code is for the data to be entered for example the quantity
    {
        Console.WriteLine("Enter Number of Ingredients:");
        int ingNum = Convert.ToInt32(Console.ReadLine());

        ingredients = new string[ingNum];
        amount = new double[ingNum];
        units = new String[ingNum];
        calories = new double[ingNum];
        foodGroup = new string[ingNum];

        for (int i = 0; i < ingNum; i++)
        {
            Console.WriteLine($"Enter Ingredients Details#{i + 1}:");
            Console.Write("Name:");
            ingredients[i] = Console.ReadLine();
            do
            {
                Console.Write("Quantity:");
            } while (!double.TryParse(Console.ReadLine(), out amount[i]));
            Console.Write("Units of Measurement:");
            units[i] = Console.ReadLine();
            do
            {
                Console.Write("Number of calories:");
            } while (!double.TryParse(Console.ReadLine(), out calories[i]));

            Console.Write("Enter Food Group: ");
            foodGroup[i] = Console.ReadLine();
        }
        //Delegation
        double calExceed = calTotal(calories);
        Console.WriteLine("TOTAL CALORIES: " + calExceed);
        if (calExceed > 300)
        {
            
            Console.WriteLine("!!! TOTAL CALORIES EXCEED 300!!!");
        }

        int stpNum;
        do
        {
            Console.WriteLine("Enter Number of Steps:");
        } while (!int.TryParse(Console.ReadLine(), out stpNum));


        steps = new string[stpNum];

        for (int a = 0; a < stpNum; a++)
        {
            Console.Write($"Steps#{a + 1}:");
            steps[a] = Console.ReadLine();
        }
    }
    public double calTotal(double[] calories)
    {
        double result = 0;
        for (int i = 0; i < calories.Length; i++)
        {
            result += calories[i];
        }
        return result;
    }
    public void RecipeDisplay()
    // to display the the recipe
    {
        Console.WriteLine("Ingredients:");
        for (int i = 0; i < ingredients.Length; i++)

        {
            Console.WriteLine($" - {amount[i]}{units[i]} of {ingredients[i]}");
        }
        Console.WriteLine("Steps:");
        for (int a = 0; a < steps.Length; a++)
        {
            Console.WriteLine($" - {steps[a]}");
        }
        double result = 0;
        for (int i = 0; i < calories.Length; i++)
        {
            result += calories[i];
        }
        if (result > 300)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("!!! TOTAL CALORIES EXCEED 300!!!");
        }
    }
    public void RecipeScale(double scale)
    // this code is to double the amount
    {
        for (int i = 0; i < amount.Length; i++)
        {
            amount[i] *= scale;
        }
    }
    public void ResetRecipe()
    // this is to reset the new amount to the orginal amount
    {
        for (int i = 0; i < amount.Length; i++)
        {
            amount[i] /= 2;
        }
    }
    public void ClearRecipe()
    // Code to clear the whole recipe
    {
        ingredients = new string[0];
        amount = new double[0];
        units = new String[0];
        steps = new String[0];
    }
}

class Appmenu
{
    private Dictionary<String, Recipe> dictionaryRecipe;
    private recipeLogger repL;
    public Appmenu(Dictionary<string, Recipe> dictionaryRecipe)
    {
        this.dictionaryRecipe = dictionaryRecipe;
        repL = new recipeLogger(dictionaryRecipe);
    }
    public void appMenu()
    {
        while (true)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("RECIPE APP");
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("(1) Create Recipe");
            Console.WriteLine("(2) Search for Recipe");
            Console.WriteLine("(3) Display all Recipes");
            Console.WriteLine("(4) Exit Application");
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("Please select an Option:");
            String ans = Console.ReadLine();
            switch (ans)
            {
                case "1":
                    repL.LogDetails();
                    break;
                case "2":
                    repL.recipeFinder();
                    break;
                case "3":
                    repL.recipeList();
                    break;
                case "4":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Please enter a valid input");
                    break;
            }

        }
    }
}

