using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using Thermometer.Common;

namespace Thermometer.IO
{
   /// <summary>
   /// The threshold data
   /// </summary>
   public class ThresholdData : IThresholdData
   {
      public int Id
      {
         get;
         set;
      }

      public string Name
      {
         get;
         set;
      }

      public double Temperature
      {
         get;
         set;
      }

      public ThresholdGoDirection Direction
      {
         get;
         set;
      }

      public bool IgnoreHalfUnit
      {
         get;
         set;
      }
   }

   /// <summary>
   /// The repository for threshold data
   /// </summary>
   public abstract class ThresholdRepository : IThresholdRepository
   {
      #region Properties
      protected abstract string SourceName { get; }

      protected string DefaultFilePath
      {
         get
         {
            return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\" + SourceName;
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

      /// <summary>
      /// Helper function to convert data
      /// </summary>
      /// <param name="unit"></param>
      /// <returns></returns>
      protected ThresholdGoDirection ConvertToChangeDirection(string unit)
      {
         string unitToCheck = unit.ToLower();
         switch (unitToCheck)
         {
            default:
               return ThresholdGoDirection.None;

            case "above":
               return ThresholdGoDirection.Above;

            case "below":
               return ThresholdGoDirection.Below;
         }
      }

      /// <summary>
      /// Helper function to convert data
      /// </summary>
      /// <param name="unit"></param>
      /// <returns></returns>
      protected bool ConvertToIgnoreHalfUnit(string unit)
      {
         switch (unit)
         {
            default:
            case "1":
               return true;
            case "0":
               return false;
         }
      }

      public abstract List<IThresholdData> GetAll();

      public abstract void SaveAll(List<IThresholdData> dataList);

      protected abstract IThresholdData CreateData();
   }

   /// <summary>
   /// Repository for doing threshold data in XML file format
   /// </summary>
   public class XMLThresholdRepository : ThresholdRepository
   {
      protected override string SourceName
      {
         get { return "Thresholds.xml"; }
      }

      protected override IThresholdData CreateData()
      {
         return new ThresholdData();
      }

      public override List<IThresholdData> GetAll()
      {
         List<IThresholdData> dataList = new List<IThresholdData>();

         try
         {
            using (StreamReader reader = new StreamReader(FilePath))
            {
               XDocument xdoc = XDocument.Load(reader.BaseStream);

               dataList = (from element in xdoc.Root.Elements("ThresholdData")
                           select new ThresholdData
                           {
                              Id = (int)element.Attribute("Id"),
                              Name = (string)element.Attribute("Name"),
                              Temperature = (double)element.Attribute("Temperature"),
                              Direction = ConvertToChangeDirection((string)element.Attribute("Direction")),
                              IgnoreHalfUnit = ConvertToIgnoreHalfUnit((string)element.Attribute("IgnoreHalfUnit")),
                           }).ToList<ThresholdData>().ConvertAll(data => data as IThresholdData);

            }
         }
         catch
         {
         }

         return dataList;
      }

      public override void SaveAll(List<IThresholdData> dataList)
      {
         List<ThresholdData> converted = ConvertTo(dataList);
         string tryFile = FilePath;
         using (StreamWriter writer = new StreamWriter(tryFile, false, Encoding.Unicode))
         {
            XmlSerializer serializer = new XmlSerializer(typeof(List<ThresholdData>), new XmlRootAttribute("thresholds"));
            serializer.Serialize(writer, converted);
         }
      }

      protected List<ThresholdData> ConvertTo(List<IThresholdData> from)
      {
         List<ThresholdData> to = new List<ThresholdData>();
         to = from.ConvertAll(data => data as ThresholdData);
         return to;
      }
   }
}
