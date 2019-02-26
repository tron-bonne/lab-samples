using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RecieptGenerator;
using System.Collections.Generic;

namespace RecieptGenerator.Tests
{
    [TestClass]
    public class Calculation
    {
        [TestMethod]
        public void GetTaxCost_NotImported_NonExempt()
        {
			decimal price = 12.49M;
			bool isImport = false;
			bool isExempt = true;
			RecieptGenerator.Calculation tax = new RecieptGenerator.Calculation();
			var result = tax.GetTaxCost(price, isImport, isExempt);
			Assert.AreEqual(result, 0.0M);
        }

		[TestMethod]
		public void GetTaxCost_NotImported_Exempt()
		{
			decimal price = 10.00M;
			bool isImport = true;
			bool isExempt = true;
			RecieptGenerator.Calculation tax = new RecieptGenerator.Calculation();
			var result = tax.GetTaxCost(price, isImport, isExempt);
			Assert.AreEqual(result, 0.50M);
		}
		[TestMethod]
		public void GetTaxCost_IsImported_NonExempt()
		{
			decimal price = 47.50M;
			bool isImport = true;
			bool isExempt = false;
			RecieptGenerator.Calculation tax = new RecieptGenerator.Calculation();
			var result = tax.GetTaxCost(price, isImport, isExempt);
			Assert.AreEqual(result, 7.15M);
		}
		[TestMethod]
		public void CalculateTotal_ZeroTaxCost()
		{
			decimal price = 12.49M;
			decimal taxCost = 0.00M;
			RecieptGenerator.Calculation tax = new RecieptGenerator.Calculation();
			var result = tax.CalculateTotal(price,taxCost);
			Assert.AreEqual(result, 12.49M);
		}
		[TestMethod]
		public void CalculateTotal_HasTaxCost()
		{
			decimal price = 10.00M;
			decimal taxCost = 0.50M;
			RecieptGenerator.Calculation tax = new RecieptGenerator.Calculation();
			var result = tax.CalculateTotal(price, taxCost);
			Assert.AreEqual(result, 10.50M);
		}
		[TestMethod]
		public void CalculateTotalSales_NotImported_HasExempt()
		{
			List<Product> products = new List<Product>();
			ShoppingBag cart = new ShoppingBag();
			products.Add(new Product
			{
				ShoppingBagID = 1,
				ProductName = "tome",
				Quantity = 1,
				Price = 10.00M,
				IsImport = false,
				IsExempt = true
			});
			cart.Products = products;

			RecieptGenerator.Calculation tax = new RecieptGenerator.Calculation();
			var result = tax.CalculateTotalSales(cart);
			Assert.AreEqual(result, 10.00M);
		}
		[TestMethod]
		public void CalculateTotalSales_NotImport_NonExempt()
		{
			List<Product> products = new List<Product>();
			ShoppingBag cart = new ShoppingBag();
			products.Add(new Product
			{
				ShoppingBagID = 1,
				ProductName = "stuff",
				Quantity = 1,
				Price = 10.00M,
				IsImport = false,
				IsExempt = false
			});
			cart.Products = products;

			RecieptGenerator.Calculation tax = new RecieptGenerator.Calculation();
			var result = tax.CalculateTotalSales(cart);
			Assert.AreEqual(result, 11.00M);
		}
		[TestMethod]
		public void CalculateTotalTax_NotImport()
		{
			List<Product> products = new List<Product>();
			ShoppingBag cart = new ShoppingBag();
			products.Add(new Product
			{
				ShoppingBagID = 1,
				ProductName = "foodstuff",
				Quantity = 1,
				Price = 10.00M,
				IsImport = false,
				IsExempt = true
			});
			products.Add(new Product
			{
				ShoppingBagID = 1,
				ProductName = "stuff",
				Quantity = 1,
				Price = 10.00M,
				IsImport = false,
				IsExempt = false
			});
			cart.Products = products;

			RecieptGenerator.Calculation tax = new RecieptGenerator.Calculation();
			var result = tax.CalculateTotalTax(cart);
			Assert.AreEqual(result, 1.00M);
		}
		[TestMethod]
		public void CalculateTotalTax_Import_Exempt()
		{
			List<Product> products = new List<Product>();
			ShoppingBag cart = new ShoppingBag();
			products.Add(new Product
			{
				ShoppingBagID = 1,
				ProductName = "foodstuff",
				Quantity = 1,
				Price = 10.00M,
				IsImport = true,
				IsExempt = true
			});
			products.Add(new Product
			{
				ShoppingBagID = 1,
				ProductName = "foodstuff",
				Quantity = 1,
				Price = 10.00M,
				IsImport = true,
				IsExempt = true
			});
			cart.Products = products;

			RecieptGenerator.Calculation tax = new RecieptGenerator.Calculation();
			var result = tax.CalculateTotalTax(cart);
			Assert.AreEqual(result, 1.00M);
		}
	}
}
