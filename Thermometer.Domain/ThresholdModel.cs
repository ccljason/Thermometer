using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thermometer.Common;
using Thermometer.IO;

namespace Thermometer.Domain
{
   public class ThresholdModel
   {
      #region Constructors
      /// <summary>
      /// Constructors
      /// </summary>
      /// <param name="thermometerModel"></param>
      public ThresholdModel(ThermometerModel thermometerModel)
         : this(thermometerModel, null)
      {
      }

      /// <summary>
      /// Constructor, for unit test
      /// </summary>
      /// <param name="thermometerModel"></param>
      /// <param name="repo"></param>
      public ThresholdModel(ThermometerModel thermometerModel, 
         IThresholdRepository repo)
      {
         _thermometerModel = thermometerModel;
         _thresholdRepository = repo;
      }
      #endregion

      #region Properites
      private ThermometerModel _thermometerModel = null;
      public ThermometerModel ThermometerModel
      {
         get { return _thermometerModel; }
      }

      private IThresholdRepository _thresholdRepository = null;
      public IThresholdRepository ThresholdRepository
      {
         get
         {
            if (_thresholdRepository == null)
               _thresholdRepository = new XMLThresholdRepository();
            return _thresholdRepository;
         }
      }

      private List<IThresholdData> _thresholdSettings = null;
      public List<IThresholdData> ThresholdSettings
      {
         get
         {
            if (_thresholdSettings == null)
            {
               _thresholdSettings = ThresholdRepository.GetAll();
               if (_thresholdSettings == null || _thresholdSettings.Count <= 0)
                  _thresholdSettings = GetDefaultThresholdSettings();
            }
            return _thresholdSettings;
         }
      }

      private TemperatureUnit _currTempUnit = TemperatureUnit.Celsius;
      public TemperatureUnit CurrentTemperatureUnit
      {
         get { return _currTempUnit; }
         set { _currTempUnit = value; }
      }
      #endregion

      /// <summary>
      /// Helper function to check temperature alert trigger
      /// </summary>
      /// <param name="data"></param>
      /// <param name="prev"></param>
      /// <param name="curr"></param>
      /// <returns></returns>
      protected bool TemperatureTrigger(IThresholdData data, double prev, double curr)
      {
         if (prev == data.Temperature && curr == data.Temperature)
            return false;

         return (prev <= data.Temperature && curr >= data.Temperature ||
            prev >= data.Temperature && curr <= data.Temperature);
      }

      /// <summary>
      /// Helper function to check temperature alert trigger
      /// </summary>
      /// <param name="data"></param>
      /// <param name="prev"></param>
      /// <param name="curr"></param>
      /// <returns></returns>
      protected bool DirectionTrigger(IThresholdData data, double prev, double curr)
      {
         if (data.Direction == ThresholdGoDirection.Above)
            if (prev > data.Temperature && curr < data.Temperature)
               return true;
         if (data.Direction == ThresholdGoDirection.Below)
            if (prev < data.Temperature && curr > data.Temperature)
               return true;

         if (data.Direction == ThresholdGoDirection.None)
            return TemperatureTrigger(data, prev, curr);

         return false;
      }

      /// <summary>
      /// Helper function to check temperature alert trigger
      /// </summary>
      /// <param name="data"></param>
      /// <param name="prev"></param>
      /// <param name="curr"></param>
      /// <returns></returns>
      protected bool IgnoreTrigger(IThresholdData data, double prev, double curr)
      {
         if (DirectionTrigger(data, prev, curr))
         {
            if (data.IgnoreHalfUnit && ThermometerModel.Triggered &&
               ((Math.Abs(curr - data.Temperature) <= 0.5) && (Math.Abs(prev - data.Temperature) <= 0.001) ||
               (Math.Abs(prev - data.Temperature) <= 0.5) && (Math.Abs(curr - data.Temperature) <= 0.001)))
               return false;
            else
               return true;
         }
         else
            return false;
      }

      /// <summary>
      /// Create a list of default threshold settings
      /// </summary>
      /// <returns></returns>
      protected List<IThresholdData> GetDefaultThresholdSettings()
      {
         List<IThresholdData> dataList = new List<IThresholdData>();
         dataList.AddRange(new List<IThresholdData>
            {
               new ThresholdData() { Id = 1, Name = "Freezing", Temperature = 0.0, Direction = ThresholdGoDirection.None, IgnoreHalfUnit = true},
               new ThresholdData() { Id = 2, Name = "Boiling", Temperature = 100.0, Direction = ThresholdGoDirection.None, IgnoreHalfUnit = true},
            });

         CurrentTemperatureUnit = TemperatureUnit.Celsius;
         return dataList;
      }

      /// <summary>
      /// Check the threshold triggers
      /// </summary>
      /// <param name="prev">previous temperature reading</param>
      /// <param name="curr">current temperature reading</param>
      /// <returns>True if alert has to be triggered; false otherwise</returns>
      public bool TriggerThresholdAlert(double prev, double curr)
      {
         foreach (var setting in ThresholdSettings)
         {
            if (IgnoreTrigger(setting, prev, curr))
               return true;
         }
         return false;
      }

      /// <summary>
      /// Convert the temperature units in the threshold settings
      /// </summary>
      /// <param name="unit"></param>
      public void PopulateTempUnitToSettings(TemperatureUnit unit)
      {
         if (CurrentTemperatureUnit == unit || unit == TemperatureUnit.Unknown)
            return;

         foreach (var setting in ThresholdSettings)
         {
            setting.Temperature = new TemperatureUnitConvertor().ConvertToNewReading(setting.Temperature, from: CurrentTemperatureUnit, to: unit);
         }

         CurrentTemperatureUnit = unit;
      }

      public void SaveAll()
      {
         ThresholdRepository.SaveAll(ThresholdSettings);
      }
   }
}
