using System.Collections.Generic;
using System.Linq;
using System;

using NUnit.Framework;
using BreweryTrackingSystem.models;
using Microsoft.EntityFrameworkCore;

namespace BreweryTrackingSystemTests
{
    [TestFixture]
    public class RecipeTests
    {
        BitsContext dbContext;
        Recipe? r;

        List<Recipe>? recipes;

        [SetUp]
        public void SetUp()
        {
            dbContext = new BitsContext();
        }

        [Test]
        public void GetAllTest()
        {
            recipes = dbContext.Recipes.OrderBy(i => i.Name).ToList();
            Assert.AreEqual(4, recipes.Count);
            Assert.AreEqual("Cascade Orange Pale Ale", recipes[0].Name);
        }

        [Test]
        public void GetByPrimaryKeyTest()
        {
            r = dbContext.Recipes.Find(1);
            Assert.IsNotNull(r);
            Assert.AreEqual("Fuzzy Tales Juicy IPA", r.Name);
        }

        [Test]
        public void GetUsingWhere()
        {
            recipes = dbContext.Recipes.Where(r => r.Volume > 40).OrderBy(i => i.Name).ToList();
            Assert.AreEqual(1, recipes.Count);
            Assert.AreEqual("Cascade Orange Pale Ale", recipes[0].Name);

            recipes = dbContext.Recipes
                .Where(r => r.BoilTime == 75)
                .Where(r => r.Volume < 40)
                .OrderBy(i => i.Name).ToList();
            Assert.AreEqual(1, recipes.Count);
            Assert.AreEqual("Krampus' Special Sauce", recipes[0].Name);

        }
    }
}



