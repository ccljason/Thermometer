using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Thermometer.Common;
using Thermometer.IO;

namespace Thermometer.Domain
{
   public class ThermometerModel
   {
      #region Constructors
      /// <summary>
      /// Default Constructor
      /// </summary>
      public ThermometerModel() : this("xml", null)
      {
      }

      /// <summary>
      /// Constructor
      /// </summary>
      /// <param name="externalSource"></param>
      public ThermometerModel(string externalSource) : this(externalSource, null)
      {
      }

      /// <summary>
      /// Constructor - for unit test
      /// </summary>
      /// <param name="externalSource"></param>
      /// <param name="source"></param>
      public ThermometerModel(string externalSource, IExternalSource source)
      {
         _extSource = externalSource;
         _externalSource = source;
      }
      #endregion

      #region Properties
      private string _extSource = "xml";

      private double _CurrTemp;
      public double CurrentTemperature
      {
         get { return _CurrTemp; }
         set { _CurrTemp = value; }
      }

      private TemperatureUnit _CurrTempUnit = TemperatureUnit.Celsius;
      public TemperatureUnit CurrentTempUnit
      {
         get { return _CurrTempUnit; }
         set 
         {
            _CurrTempUnit = value; 
         }
      }

      /// <summary>
      /// Get the current temperature display
      /// </summary>
      public string CurrentTempDisplay
      {
         get
         {
            return CurrentTemperature.ToString("F1") + " " + CurrentTempUnit.ToString();
         }
      }

      private IExternalSource _externalSource = null;
      public IExternalSource ExternalSource
      {
         get
         {
            if (_externalSource == null)
            {
               switch (_extSource)
               {
                  default:
                  case "xml":
                     _externalSource = new XMLSource();
                     break;
               }
            }
            return _externalSource;
         }
      }

      private ThresholdModel _thresholdModel = null;
      public ThresholdModel ThresholdsModel
      {
         get
         {
            if (_thresholdModel == null)
               _thresholdModel = new ThresholdModel(this);
            return _thresholdModel;
         }
      }

      private bool _triggered = false;
      public bool Triggered
      {
         get { return _triggered; }
         set { _triggered = value; }
      }
      #endregion

      #region Public Methods - called by UI
      /// <summary>
      /// Get the latest tempearture from external source
      /// </summary>
      /// <returns></returns>
      public void GetLatestTemperature()
      {
         ISourceData sourceData = ExternalSource.ConvertSourceToData();

         if (sourceData.Unit == TemperatureUnit.Unknown)
            throw new ThermometerUnknownUnitException();
         
         CurrentTemperature = new TemperatureUnitConvertor().ConvertToNewReading(
            sourceData.Reading, from: sourceData.Unit, to: CurrentTempUnit);

         // Populate to thresholds
         ThresholdsModel.PopulateTempUnitToSettings(CurrentTempUnit);
      }

      /// <summary>
      /// Update this model when temperature unit changes
      /// </summary>
      /// <param name="newUnit"></param>
      public void UpdateByTempUnitChange(TemperatureUnit newUnit)
      {
         if (CurrentTempUnit == newUnit)
            return;

         CurrentTemperature = new TemperatureUnitConvertor().ConvertToNewReading(CurrentTemperature, CurrentTempUnit, newUnit);
         CurrentTempUnit = newUnit;

         // Populate to thresholds
         ThresholdsModel.PopulateTempUnitToSettings(CurrentTempUnit);

      }
      #endregion
   }



}
