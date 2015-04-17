using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Thermometer.Domain
{
   public class ThermometerUnknownUnitException : Exception
   {
      public ThermometerUnknownUnitException() : base() { }

      public ThermometerUnknownUnitException(string msg) : base(msg) { }

      public ThermometerUnknownUnitException(string msg, Exception inner) : base(msg, inner) { }
   }
}
