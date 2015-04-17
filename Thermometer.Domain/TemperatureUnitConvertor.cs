using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Thermometer.Common;
using Thermometer.IO;

namespace Thermometer.Domain
{
   /// <summary>
   /// This is the temperature unit convertor
   /// </summary>
   public class TemperatureUnitConvertor
   {
      private const int DEFAULT_DECIMAL_PLACE = 1;

      #region Constructors
      /// <summary>
      /// Default Constructor
      /// </summary>
      public TemperatureUnitConvertor() : this(DEFAULT_DECIMAL_PLACE)
      {
      }

      /// <summary>
      /// Constructor
      /// </summary>
      /// <param name="decimalPlaces">decimal places</param>
      public TemperatureUnitConvertor(int decimalPlaces)
      {
         // This is to check if the input decimalPlaces is valid or not
         if (decimalPlaces < 0 || decimalPlaces > 28)
            Decimal = DEFAULT_DECIMAL_PLACE;
         else
            Decimal = decimalPlaces;
      }
      #endregion

      #region Properties
      private int _decimal;
      /// <summary>
      /// Get/Set the decimal places
      /// </summary>
      public int Decimal
      {
         get { return _decimal; }
         set { _decimal = value; }
      }
      #endregion

      /// <summary>
      /// Convert the given input, reading, based on the from and to temperature unit
      /// </summary>
      /// <param name="reading"></param>
      /// <param name="from"></param>
      /// <param name="to"></param>
      /// <returns></returns>
      public double ConvertToNewReading(double reading, TemperatureUnit from, TemperatureUnit to)
      {
         if (from == to)
            return reading;

         if (from == TemperatureUnit.Celsius && to == TemperatureUnit.Fahrenheit)
            return FromCelsiusToFahrenheit(reading);
         else if (from == TemperatureUnit.Fahrenheit && to == TemperatureUnit.Celsius)
            return FromFahrenheitToCelsius(reading);
         else
            return reading;
      }

      /// <summary>
      /// Convert from Celsius to Fahrenheit
      /// </summary>
      /// <param name="celcius">input Celsius reading</param>
      /// <returns>output Fahrenheit reading</returns>
      protected double FromCelsiusToFahrenheit(double celcius)
      {
         return Math.Round((celcius * 9 / 5 + 32), Decimal);
      }

      /// <summary>
      /// Convert from Fahrenheit to Celsius
      /// </summary>
      /// <param name="fahrenheit">input Fahrenheit reading</param>
      /// <returns>output Celsius reading</returns>
      protected double FromFahrenheitToCelsius(double fahrenheit)
      {
         return Math.Round(((fahrenheit - 32) * 5 / 9), Decimal);
      }
   }
}
