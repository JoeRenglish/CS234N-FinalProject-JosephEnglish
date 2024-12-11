using System.Collections.Generic;
using System.Linq;
using System;

using NUnit.Framework;
using BreweryTrackingSystem.models;
using Microsoft.EntityFrameworkCore;

namespace BreweryTrackingSystemTests
{
	[TestFixture]
	public class AddressTests
	{
		BitsContext dbContext;
		Address? a;

		List<Address>? addresses;

		[SetUp]
		public void SetUp()
		{
			dbContext = new BitsContext();
		}

		[Test]
		public void GetAllTest()
		{
			addresses = dbContext.Addresses.OrderBy(a => a.State).ToList();
			Assert.AreEqual(7, addresses.Count);
			Assert.AreEqual(": 9495 Candida Street", addresses[0].StreetLine1);

		}

        [Test]
        public void GetByPrimaryKeyTest()
        {
            a = dbContext.Addresses.Find(1);
            Assert.IsNotNull(a);
            Assert.AreEqual("800 West 1st Ave", a.StreetLine1);
            Console.WriteLine(a);
        }

		[Test]
        public void GetUsingWhere()
        {
            addresses = dbContext.Addresses.Where(a => a.State.Equals("WA")).OrderBy(a => a.City).ToList();
            Assert.AreEqual(2, addresses.Count);
            Assert.AreEqual("Vancouver", addresses[0].City);
        }

        [Test]
        public void CreateTest()
        {
            a = new Address();
            a.StreetLine1 = "555 5th Ave";
            a.StreetLine2 = "apt 93";
            a.City = "Springfield";
            a.State = "OR";
            a.Country = "USA";
            a.Zipcode = "97477";
            dbContext.Addresses.Add(a);
            dbContext.SaveChanges();
            Assert.IsNotNull(dbContext.Addresses.Find(a.AddressId));
        }

        [Test]
        public void DeleteTest()
        {
            a = dbContext.Addresses.Find(a.AddressId);
            dbContext.Addresses.Remove(a);
            dbContext.SaveChanges();
            Assert.IsNull(dbContext.Addresses.Find(a.AddressId));
        }

        [Test]
        public void UpdateTest()
        {
            a = dbContext.Addresses.Find(1);
            a.StreetLine1 = "800 West 1st Ave";
            a.StreetLine2 = "???";
            a.City = "Shakopee";
            a.State = "MN";
            a.Country = "USA";
            a.Zipcode = "55379";
            dbContext.Addresses.Update(a);
            dbContext.SaveChanges();
            a = dbContext.Addresses.Find(1);
            Assert.AreEqual("???", a.StreetLine2);

        }

    }
}

