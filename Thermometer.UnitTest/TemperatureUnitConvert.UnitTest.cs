using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Thermometer.Domain;
using Thermometer.Common;

namespace Thermometer.UnitTest
{
   [TestClass()]
   public class TemperatureUnitConvertorUnitTest
   {
      [TestMethod()]
      [TestCategory("UnitConvertor")]
      public void Test_C_To_F()
      {
         // arrange
         TemperatureUnitConvertor tool = new TemperatureUnitConvertor();

         // act
         var result = tool.ConvertToNewReading(10.0, TemperatureUnit.Celsius, TemperatureUnit.Fahrenheit);

         // assert
         Assert.AreEqual(50.0, result);
      }

      [TestMethod()]
      [TestCategory("UnitConvertor")]
      public void Test_F_To_C()
      {
         TemperatureUnitConvertor tool = new TemperatureUnitConvertor();

         var result = tool.ConvertToNewReading(10.0, TemperatureUnit.Fahrenheit, TemperatureUnit.Celsius);

         Assert.AreEqual(-12.2, result);
      }

      [TestMethod()]
      [TestCategory("UnitConvertor")]
      public void Test_C_To_F_MaxCInput()
      {
         TemperatureUnitConvertor tool = new TemperatureUnitConvertor();

         var result = tool.ConvertToNewReading(double.MaxValue, TemperatureUnit.Celsius, TemperatureUnit.Fahrenheit);

         Assert.IsTrue(double.IsInfinity((double)result));
      }

      [TestMethod()]
      [TestCategory("UnitConvertor")]
      public void Test_C_To_F_MinCInput()
      {
         TemperatureUnitConvertor tool = new TemperatureUnitConvertor();

         var result = tool.ConvertToNewReading(double.MinValue, TemperatureUnit.Celsius, TemperatureUnit.Fahrenheit);

         Assert.IsTrue(double.IsInfinity((double)result));
      }

      [TestMethod()]
      [TestCategory("UnitConvertor")]
      public void Test_F_To_C_MaxFInput()
      {
         TemperatureUnitConvertor tool = new TemperatureUnitConvertor();

         var result = tool.ConvertToNewReading(double.MaxValue, TemperatureUnit.Fahrenheit, TemperatureUnit.Celsius);

         Assert.IsFalse(double.IsInfinity((double)result));
      }

      [TestMethod()]
      [TestCategory("UnitConvertor")]
      public void Test_F_To_C_MinFInput()
      {
         TemperatureUnitConvertor tool = new TemperatureUnitConvertor();

         var result = tool.ConvertToNewReading(double.MinValue, TemperatureUnit.Fahrenheit, TemperatureUnit.Celsius);

         Assert.IsFalse(double.IsInfinity((double)result));
      }

      [TestMethod()]
      [TestCategory("UnitConvertor")]
      public void Test_Convertor_Properties()
      {
         TemperatureUnitConvertor tool = new TemperatureUnitConvertor(5);
         Assert.AreEqual(5, tool.Decimal);

         tool = new TemperatureUnitConvertor();
         Assert.AreEqual(1, tool.Decimal);
      }

   }
}
