using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Thermometer.Domain;
using Moq;
using Thermometer.IO;
using System.Collections.Generic;
using Thermometer.Common;

namespace Thermometer.UnitTest
{
   [TestClass()]
   public class ThresholdModelUnitTest
   {
      [TestMethod()]
      [TestCategory("ThresholdModel")]
      public void Test_DefaultThresholdSettings()
      {
         var thresholdRepository = new Mock<IThresholdRepository>();
         thresholdRepository.Setup(m => m.GetAll()).Returns(new List<IThresholdData>());
         ThresholdModel model = new ThresholdModel(new ThermometerModel(), thresholdRepository.Object);

         List<IThresholdData> target = model.ThresholdSettings;

         // Assert
         Assert.AreEqual(2, target.Count);

         IThresholdData data = target[0];
         Assert.AreEqual(1, data.Id);
         Assert.AreEqual("Freezing", data.Name);
         Assert.AreEqual(0.0, data.Temperature);
         Assert.AreEqual(ThresholdGoDirection.None, data.Direction);
         Assert.AreEqual(true, data.IgnoreHalfUnit);
         data = target[1];
         Assert.AreEqual(2, data.Id);
         Assert.AreEqual("Boiling", data.Name);
         Assert.AreEqual(100.0, data.Temperature);
         Assert.AreEqual(ThresholdGoDirection.None, data.Direction);
         Assert.AreEqual(true, data.IgnoreHalfUnit);
      }

      [TestMethod()]
      [TestCategory("ThresholdModel")]
      public void Test_UnitPopulationToSettings()
      {
         var thresholdRepository = new Mock<IThresholdRepository>();
         thresholdRepository.Setup(m => m.GetAll()).Returns(new List<IThresholdData>());
         ThresholdModel model = new ThresholdModel(new ThermometerModel(), thresholdRepository.Object);

         // initial 
         Assert.AreEqual(TemperatureUnit.Celsius, model.CurrentTemperatureUnit);
         IThresholdData setting = model.ThresholdSettings[0];
         Assert.AreEqual(0.0, setting.Temperature);
         setting = model.ThresholdSettings[1];
         Assert.AreEqual(100.0, setting.Temperature);

         // act
         model.PopulateTempUnitToSettings(TemperatureUnit.Fahrenheit);

         Assert.AreEqual(TemperatureUnit.Fahrenheit, model.CurrentTemperatureUnit);
         setting = model.ThresholdSettings[0];
         Assert.AreEqual(32, setting.Temperature);
         setting = model.ThresholdSettings[1];
         Assert.AreEqual(212, setting.Temperature);
      }

      [TestMethod()]
      [TestCategory("ThresholdModel")]
      public void Test_UnitPopulationToSettings_UsingInvalidArgument()
      {
         var thresholdRepository = new Mock<IThresholdRepository>();
         thresholdRepository.Setup(m => m.GetAll()).Returns(new List<IThresholdData>());
         ThresholdModel model = new ThresholdModel(new ThermometerModel(), thresholdRepository.Object);

         // initial 
         Assert.AreEqual(TemperatureUnit.Celsius, model.CurrentTemperatureUnit);
         IThresholdData setting = model.ThresholdSettings[0];
         Assert.AreEqual(0.0, setting.Temperature);
         setting = model.ThresholdSettings[1];
         Assert.AreEqual(100.0, setting.Temperature);

         // act
         model.PopulateTempUnitToSettings(TemperatureUnit.Unknown);

         Assert.AreEqual(TemperatureUnit.Celsius, model.CurrentTemperatureUnit);
         setting = model.ThresholdSettings[0];
         Assert.AreEqual(0.0, setting.Temperature);
         setting = model.ThresholdSettings[1];
         Assert.AreEqual(100.0, setting.Temperature);
      }

      [TestMethod()]
      [TestCategory("ThresholdModel")]
      public void Test_UnitPopulationToSettings_UsingSameSettingArgument()
      {
         var thresholdRepository = new Mock<IThresholdRepository>();
         thresholdRepository.Setup(m => m.GetAll()).Returns(new List<IThresholdData>());
         ThresholdModel model = new ThresholdModel(new ThermometerModel(), thresholdRepository.Object);

         // initial 
         Assert.AreEqual(TemperatureUnit.Celsius, model.CurrentTemperatureUnit);
         IThresholdData setting = model.ThresholdSettings[0];
         Assert.AreEqual(0.0, setting.Temperature);
         setting = model.ThresholdSettings[1];
         Assert.AreEqual(100.0, setting.Temperature);

         // act
         model.PopulateTempUnitToSettings(TemperatureUnit.Celsius);

         Assert.AreEqual(TemperatureUnit.Celsius, model.CurrentTemperatureUnit);
         setting = model.ThresholdSettings[0];
         Assert.AreEqual(0.0, setting.Temperature);
         setting = model.ThresholdSettings[1];
         Assert.AreEqual(100.0, setting.Temperature);
      }

      [TestMethod()]
      [TestCategory("ThresholdModel")]
      public void Test_AlertTrigger_DataFromBelowToAboveTrigger()
      {
         var thresholdRepository = new Mock<IThresholdRepository>();
         thresholdRepository.Setup(m => m.GetAll()).Returns(new List<IThresholdData>());
         ThresholdModel model = new ThresholdModel(new ThermometerModel(), thresholdRepository.Object);

         Assert.IsTrue(model.TriggerThresholdAlert(-10.0, 10.0));
      }

      [TestMethod()]
      [TestCategory("ThresholdModel")]
      public void Test_AlertTrigger_DataFromAboveToBelowTrigger()
      {
         var thresholdRepository = new Mock<IThresholdRepository>();
         thresholdRepository.Setup(m => m.GetAll()).Returns(new List<IThresholdData>());
         ThresholdModel model = new ThresholdModel(new ThermometerModel(), thresholdRepository.Object);

         Assert.IsTrue(model.TriggerThresholdAlert(20.0, -20.0));
      }

      [TestMethod()]
      [TestCategory("ThresholdModel")]
      public void Test_AlertTrigger_DataFromSameToSameTrigger()
      {
         var thresholdRepository = new Mock<IThresholdRepository>();
         thresholdRepository.Setup(m => m.GetAll()).Returns(new List<IThresholdData>());
         ThresholdModel model = new ThresholdModel(new ThermometerModel(), thresholdRepository.Object);

         Assert.IsFalse(model.TriggerThresholdAlert(30.0, 30.0));
      }

      [TestMethod()]
      [TestCategory("ThresholdModel")]
      public void Test_AlertTrigger_DataFromSameToSameOnTrigger()
      {
         var thresholdRepository = new Mock<IThresholdRepository>();
         thresholdRepository.Setup(m => m.GetAll()).Returns(new List<IThresholdData>());
         ThresholdModel model = new ThresholdModel(new ThermometerModel(), thresholdRepository.Object);

         Assert.IsFalse(model.TriggerThresholdAlert(0.0, 0.0));
      }

      [TestMethod()]
      [TestCategory("ThresholdModel")]
      public void Test_AlertTrigger_SettingFromBelow_DataFromBelowToAbove()
      {
         var thresholdRepository = new Mock<IThresholdRepository>();
         thresholdRepository.Setup(m => m.GetAll()).Returns(new List<IThresholdData>());
         ThresholdModel model = new ThresholdModel(new ThermometerModel(), thresholdRepository.Object);

         IThresholdData setting = model.ThresholdSettings[0];
         setting.Direction = ThresholdGoDirection.Below;

         Assert.IsTrue(model.TriggerThresholdAlert(-1.0, 1.0));
      }

      [TestMethod()]
      [TestCategory("ThresholdModel")]
      public void Test_AlertTrigger_SettingFromBelow_DataFromAboveToBelow()
      {
         var thresholdRepository = new Mock<IThresholdRepository>();
         thresholdRepository.Setup(m => m.GetAll()).Returns(new List<IThresholdData>());
         ThresholdModel model = new ThresholdModel(new ThermometerModel(), thresholdRepository.Object);

         IThresholdData setting = model.ThresholdSettings[0];
         setting.Direction = ThresholdGoDirection.Below;

         Assert.IsFalse(model.TriggerThresholdAlert(1.0, -1.0));
      }

      [TestMethod()]
      [TestCategory("ThresholdModel")]
      public void Test_AlertTrigger_SettingFromAbove_DataFromBelowToAbove()
      {
         var thresholdRepository = new Mock<IThresholdRepository>();
         thresholdRepository.Setup(m => m.GetAll()).Returns(new List<IThresholdData>());
         ThresholdModel model = new ThresholdModel(new ThermometerModel(), thresholdRepository.Object);

         IThresholdData setting = model.ThresholdSettings[0];
         setting.Direction = ThresholdGoDirection.Above;

         Assert.IsFalse(model.TriggerThresholdAlert(-1.0, 1.0));
      }

      [TestMethod()]
      [TestCategory("ThresholdModel")]
      public void Test_AlertTrigger_SettingFromAbove_DataFromAboveToBelow()
      {
         var thresholdRepository = new Mock<IThresholdRepository>();
         thresholdRepository.Setup(m => m.GetAll()).Returns(new List<IThresholdData>());
         ThresholdModel model = new ThresholdModel(new ThermometerModel(), thresholdRepository.Object);

         IThresholdData setting = model.ThresholdSettings[0];
         setting.Direction = ThresholdGoDirection.Above;

         Assert.IsTrue(model.TriggerThresholdAlert(1.0, -1.0));
      }

      [TestMethod()]
      [TestCategory("ThresholdModel")]
      public void Test_AlertTrigger_DataFromAboveToBelow_WithinHalf()
      {
         var thresholdRepository = new Mock<IThresholdRepository>();
         thresholdRepository.Setup(m => m.GetAll()).Returns(new List<IThresholdData>());
         ThermometerModel thermoModel = new ThermometerModel();
         ThresholdModel model = new ThresholdModel(thermoModel, thresholdRepository.Object);

         Assert.IsTrue(model.TriggerThresholdAlert(0.0, -0.5));

         // Now, alert has been shown once. So, set thermometer model accordingly to be triggered.
         thermoModel.Triggered = true;

         Assert.IsFalse(model.TriggerThresholdAlert(-0.5, 0.0));

         // doing again will not trigger alert:
         Assert.IsFalse(model.TriggerThresholdAlert(0.0, 0.5));
      }



   }
}
