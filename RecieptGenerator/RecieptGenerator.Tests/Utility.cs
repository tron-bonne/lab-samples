using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecieptGenerator;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RecieptGenerator.Tests
{
	[TestClass]
	public class Utility
	{
		[TestMethod]
		public void Truncate_Decimal_2_Places()
		{
			decimal price = 0.1M;
			var result = Library.Utility.Truncate(price);
			Assert.AreEqual(result, 0.10M);
		}
		[TestMethod]
		public void RoundOff()
		{
			decimal price = 0.14M;
			var result = Library.Utility.TaxRoundOff(price);
			Assert.AreEqual(result, 0.15M);
		}
	}
}
