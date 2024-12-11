using System.Collections.Generic;
using System.Linq;
using System;

using NUnit.Framework;
using BreweryTrackingSystem.models;
using Microsoft.EntityFrameworkCore;

namespace BreweryTrackingSystemTests
{
    [TestFixture]
    public class IngredientTests
    {
        BitsContext dbContext;
        Ingredient? i;

        List<Ingredient>? ingredients;

        [SetUp]
        public void SetUp()
        {
            dbContext = new BitsContext();
        }

        [Test]
        public void GetAllTest()
        {
            ingredients = dbContext.Ingredients.OrderBy(i => i.Name).ToList();
            Assert.AreEqual(1149, ingredients.Count);
            Assert.AreEqual("Abbaye Belgian", ingredients[0].Name);
        }

        [Test]
        public void GetByPrimaryKeyTest()
        {
            i = dbContext.Ingredients.Find(1);
            Assert.IsNotNull(i);
            Assert.AreEqual("Acid Malt", i.Name);
        }

        [Test]
        public void GetUsingWhere()
        {
            ingredients = dbContext.Ingredients.Where(i => i.OnHandQuantity == 0).OrderBy(i => i.Name).ToList();
            Assert.AreEqual(1126, ingredients.Count);
            Assert.AreEqual("Abbaye Belgian", ingredients[0].Name);

            ingredients = dbContext.Ingredients.Where(i => i.IngredientTypeId == 3).OrderBy(i => i.Name).ToList();
            Assert.AreEqual(266, ingredients.Count);
            Assert.AreEqual("Admiral", ingredients[0].Name);

            ingredients = dbContext.Ingredients.Where(i => i.OnHandQuantity == i.ReorderPoint).OrderBy(i => i.Name).ToList();
            Assert.AreEqual(1126, ingredients.Count);
            Assert.AreEqual("Abbaye Belgian", ingredients[0].Name);
        }

        [Test]
        public void UpdateTest()
        {
            i = dbContext.Ingredients.Find(1);
            i.OnHandQuantity = 1;
            dbContext.Ingredients.Update(i);
            dbContext.SaveChanges();
            i = dbContext.Ingredients.Find(1);
            Assert.AreEqual(1, i.OnHandQuantity);

        }
    }
}



