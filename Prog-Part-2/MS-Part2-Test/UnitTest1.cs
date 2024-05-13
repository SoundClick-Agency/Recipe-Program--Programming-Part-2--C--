using Microsoft.VisualStudio.TestTools.UnitTesting;
using RecipeProgram;

namespace MS_Part2_Test
{
    [TestClass]
    public class RecipeTests
    {
        [TestMethod]
        public void TotalCaloriesCalculation_CalculatesCorrectly()
        {
            // Arrange
            Recipe recipe = new Recipe("Test Recipe");
            Ingredient ingredient1 = new Ingredient("Ingredient 1", 100, "grams", 50, "Protein");
            Ingredient ingredient2 = new Ingredient("Ingredient 2", 150, "grams", 75, "Carbohydrates");

            // Act
            recipe.AddIngredient(ingredient1);
            recipe.AddIngredient(ingredient2);

            // Assert
            Assert.AreEqual(125, recipe.TotalCalories);
        }
    }
}
