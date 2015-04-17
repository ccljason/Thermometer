using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Thermometer.Common;

namespace Thermometer.IO
{
   /// <summary>
   /// An abstract class for faking external data source
   /// </summary>
   public abstract class ExternalSource : IExternalSource
   {
      #region Properties
      public abstract string FileName { get; }

      protected string DefaultFilePath
      {
         get
         {
            return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\" + FileName;
         }
      }

      private string _filePath = null;
      public string FilePath
      {
         get
         {
            if (_filePath == null || _filePath.Length == 0)
               _filePath = DefaultFilePath;
            return _filePath;
         }

         set { _filePath = value; }
      }
      #endregion


      public abstract ISourceData ConvertSourceToData();

      protected virtual ISourceData CreateSourceData()
      {
         return new SourceData();
      }

      protected TemperatureUnit ConvertToTempUnit(string unit)
      {
         string unitToCheck = unit.ToLower();
         switch (unitToCheck)
         {
            default:
               return TemperatureUnit.Unknown;

            case "c":
               return TemperatureUnit.Celsius;

            case "f":
               return TemperatureUnit.Fahrenheit;
         }
      }
   }

   /// <summary>
   /// Fake XML file as an external data source for temperature
   /// </summary>
   public class XMLSource : ExternalSource
   {
      #region Properties
      public override string FileName
      {
         get { return "forecast.xml"; }
      }
      #endregion

      /// <summary>
      /// Convert the source (fake) to data
      /// It is to fake data coming as a xml file from either web or local
      /// </summary>
      /// <returns></returns>
      public override ISourceData ConvertSourceToData()
      {
         ISourceData data = CreateSourceData();

         using (StreamReader reader = new StreamReader(FilePath))
         {
            XDocument xdoc = XDocument.Load(reader.BaseStream);

            var first = (from element in xdoc.Root.Elements("day")
                         where (string)element.Attribute("name") == "current"
                         select new
                         {
                            Name = (string)element.Attribute("name"),
                            Temperature = (double)element.Attribute("temperature"),
                            Degree = (string)element.Attribute("degree"),
                         }).ToList().First();

            if (first != null)
            {
               data.Name = first.Name;
               data.Reading = first.Temperature;
               data.Unit = ConvertToTempUnit(first.Degree);
            }
         }

         return data;
      }
   }

   /// <summary>
   /// The data 
   /// </summary>
   public class SourceData : ISourceData
   {
      public string Name
      {
         get;
         set;
      }

      public TemperatureUnit Unit
      {
         get;
         set;
      }

      public double Reading
      {
         get;
         set;
      }

      public double High
      {
         get;
         set;
      }

      public double Low
      {
         get;
         set;
      }
   }


   #region --- To be done ---
   public class JsonSource : ExternalSource
   {
      public override string FileName
      {
         get { return "forecast.json"; }
      }

      public override ISourceData ConvertSourceToData()
      {
         ISourceData data = CreateSourceData();

         using (StreamReader reader = new StreamReader(FilePath))
         {
            string json = reader.ReadToEnd();
            JsonWholeData objectList = JsonConvert.DeserializeObject<JsonWholeData>(json);
            //foreach (JsonSourceData item in objectList.thisss)
            //{
            //   if (item.Name == "current")
            //   {
            //      data.Name = item.Name;
            //      data.Reading = item.Reading;
            //      data.Unit = ConvertToTempUnit(item);   /////// for now...
            //      break;
            //   }
            //}
         }

         return data;
      }

      protected override ISourceData CreateSourceData()
      {
         return new JsonSourceData();
      }
   }

   public class JsonWholeData
   {
      [JsonProperty("forecast")]
      public JsonSourceData thisss { get; set; }
   }
   public class JsonSourceData : ISourceData
   {
      [JsonProperty(PropertyName="name")]
      public string Name
      {
         get;
         set;
      }

      [JsonProperty(PropertyName = "degree")]
      public TemperatureUnit Unit
      {
         get;
         set;
      }

      [JsonProperty(PropertyName = "temperature")]
      public double Reading
      {
         get;
         set;
      }

      [JsonProperty(PropertyName = "high")]
      public double High
      {
         get;
         set;
      }

      [JsonProperty(PropertyName = "low")]
      public double Low
      {
         get;
         set;
      }
   }
   #endregion

}
