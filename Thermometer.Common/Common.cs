using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Thermometer.Common
{
   public enum TemperatureUnit
   {
      Unknown,
      Celsius,
      Fahrenheit,
   }

   public enum ThresholdGoDirection
   {
      None = 0,
      Above,
      Below,
   }

}
