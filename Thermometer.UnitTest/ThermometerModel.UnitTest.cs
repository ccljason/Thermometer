using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Thermometer.Domain;
using Thermometer.IO;
using Moq;
using Thermometer.Common;

namespace Thermometer.UnitTest
{
   [TestClass()]
   public class ThermometerModelUnitTest
   {
      [TestMethod()]
      [TestCategory("ThermometerModel")]
      public void Test_GetSource_InitialConditions()
      {
         SourceData sourceData = new SourceData()
         {
            Name = "current", Reading = 15, Unit = TemperatureUnit.Celsius, High = 20, Low = 10,         
         };
         var externalSource = new Mock<IExternalSource>();
         externalSource.Setup(m => m.ConvertSourceToData()).Returns(sourceData);

         ThermometerModel model = new ThermometerModel("xml", externalSource.Object);
         model.GetLatestTemperature();

         Assert.AreEqual(15.0, model.CurrentTemperature);
         Assert.AreEqual(TemperatureUnit.Celsius, model.CurrentTempUnit);
         Assert.AreEqual("15.0 Celsius", model.CurrentTempDisplay);

         Assert.AreEqual(TemperatureUnit.Celsius, model.ThresholdsModel.CurrentTemperatureUnit);
         IThresholdData thresholdData = model.ThresholdsModel.ThresholdSettings[0];
         thresholdData.Temperature = 0.0;
         thresholdData = model.ThresholdsModel.ThresholdSettings[1];
         thresholdData.Temperature = 100.0;
      }

      [TestMethod()]
      [TestCategory("ThermometerModel")]
      public void Test_GetSource_DiffReading()
      {
         SourceData sourceData = new SourceData()
         {
            Name = "current",
            Reading = 15,
            Unit = TemperatureUnit.Celsius,
            High = 20,
            Low = 10,
         };
         var externalSource = new Mock<IExternalSource>();
         externalSource.Setup(m => m.ConvertSourceToData()).Returns(sourceData);

         ThermometerModel model = new ThermometerModel("xml", externalSource.Object);
         model.GetLatestTemperature();

         Assert.AreEqual(15.0, model.CurrentTemperature);
         Assert.AreEqual(TemperatureUnit.Celsius, model.CurrentTempUnit);
         Assert.AreEqual("15.0 Celsius", model.CurrentTempDisplay);

         // Arrange
         sourceData.Reading = 20;

         model.GetLatestTemperature();

         Assert.AreEqual(20.0, model.CurrentTemperature);
         Assert.AreEqual(TemperatureUnit.Celsius, model.CurrentTempUnit);
         Assert.AreEqual("20.0 Celsius", model.CurrentTempDisplay);
      }

      [TestMethod()]
      [TestCategory("ThermometerModel")]
      public void Test_GetSource_SameReadingDiffUnit()
      {
         SourceData sourceData = new SourceData()
         {
            Name = "current",
            Reading = 15,
            Unit = TemperatureUnit.Celsius,
            High = 20,
            Low = 10,
         };
         var externalSource = new Mock<IExternalSource>();
         externalSource.Setup(m => m.ConvertSourceToData()).Returns(sourceData);

         ThermometerModel model = new ThermometerModel("xml", externalSource.Object);
         model.GetLatestTemperature();

         Assert.AreEqual(15.0, model.CurrentTemperature);
         Assert.AreEqual(TemperatureUnit.Celsius, model.CurrentTempUnit);
         Assert.AreEqual("15.0 Celsius", model.CurrentTempDisplay);

         model.UpdateByTempUnitChange(TemperatureUnit.Fahrenheit);

         Assert.AreEqual(59.0, model.CurrentTemperature);
         Assert.AreEqual(TemperatureUnit.Fahrenheit, model.CurrentTempUnit);
         Assert.AreEqual("59.0 Fahrenheit", model.CurrentTempDisplay);

      }

      [TestMethod()]
      [TestCategory("ThermometerModel")]
      [ExpectedException(typeof(ThermometerUnknownUnitException))]
      public void Test_GetSource_UnknownUnitExpception()
      {
         SourceData sourceData = new SourceData()
         {
            Name = "current",
            Reading = 15,
            Unit = TemperatureUnit.Celsius,
            High = 20,
            Low = 10,
         };
         var externalSource = new Mock<IExternalSource>();
         externalSource.Setup(m => m.ConvertSourceToData()).Returns(sourceData);

         ThermometerModel model = new ThermometerModel("xml", externalSource.Object);
         model.GetLatestTemperature();

         Assert.AreEqual(15.0, model.CurrentTemperature);
         Assert.AreEqual(TemperatureUnit.Celsius, model.CurrentTempUnit);
         Assert.AreEqual("15.0 Celsius", model.CurrentTempDisplay);

         // Arrange
         sourceData.Unit = TemperatureUnit.Unknown;

         model.GetLatestTemperature();

         Assert.AreEqual(59.0, model.CurrentTemperature);
         Assert.AreEqual(TemperatureUnit.Fahrenheit, model.CurrentTempUnit);
         Assert.AreEqual("59.0 Celsius", model.CurrentTempDisplay);

      }


   }
}
