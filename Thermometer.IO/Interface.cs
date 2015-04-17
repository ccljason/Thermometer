using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Thermometer.Common;

namespace Thermometer.IO
{
   #region External Data Source
   public interface ISourceData
   {
      string Name { get; set; }
      double Reading { get; set; }
      TemperatureUnit Unit { get; set; }
   }

   public interface IExternalSource
   {
      ISourceData ConvertSourceToData();
   }
   #endregion



   #region Threshold Data
   public interface IThresholdData
   {
      int Id { get; set; }
      string Name { get; set; }
      double Temperature { get; set; }
      ThresholdGoDirection Direction { get; set; }
      bool IgnoreHalfUnit { get; set; }
   }

   public interface IThresholdRepository
   {
      List<IThresholdData> GetAll();
      void SaveAll(List<IThresholdData> dataList);
   }
   #endregion

}
