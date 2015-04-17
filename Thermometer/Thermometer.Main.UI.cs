using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Thermometer.Common;
using Thermometer.Domain;
using Thermometer.IO;

namespace Thermometer
{
   public partial class ThermometerMainUI : Form
   {
      #region Constructors
      /// <summary>
      /// Constructor
      /// </summary>
      public ThermometerMainUI()
      {
         InitializeComponent();

         Initialize();
      }
      #endregion

      #region Properties
      private ThermometerModel _model;
      private List<string> _extSources = new List<string>() { "xml" };
      protected ThermometerModel Model
      {
         get
         {
            if (_model == null)
            {
               string source = ConfigurationManager.AppSettings["ExternalSource"];
               if (_extSources.Any(s => source == s))
                  _model = new ThermometerModel(source);
               else
                  _model = new ThermometerModel();
            }
            return _model;
         }
      }
      #endregion

      /// <summary>
      /// Initialize this class
      /// </summary>
      protected void Initialize()
      {
         // Init UI
         InitializeTemperatureComboBox();
         InitializeThresholdsListBox();


         // set unit from UI to model.
         Model.CurrentTempUnit = TemperatureUnit.Celsius;


         // Init settings
         // Get the reading from external source
         Model.GetLatestTemperature();
         _lbl_TempReading.Text = Model.CurrentTempDisplay;

      }

      private class ComboBox_TempUnitItem
      {
         public string Name;
         public TemperatureUnit Unit;

         public ComboBox_TempUnitItem(string name, TemperatureUnit unit)
         {
            Name = name;
            Unit = unit;
         }

         public override string ToString()
         {
            return Name;
         }
      }

      /// <summary>
      /// Initialize the combobox of temperature unit
      /// </summary>
      protected void InitializeTemperatureComboBox()
      {
         _cbx_TempUnit.Items.Add(new ComboBox_TempUnitItem(TemperatureUnit.Celsius.ToString(), TemperatureUnit.Celsius));
         _cbx_TempUnit.Items.Add(new ComboBox_TempUnitItem(TemperatureUnit.Fahrenheit.ToString(), TemperatureUnit.Fahrenheit));

         // Set first item
         _cbx_TempUnit.SelectedIndex = 0;

         _cbx_TempUnit.SelectedIndexChanged += _cbx_TempUnit_SelectedIndexChanged;
      }

      private class ListBox_ThresholdItem
      {
         public string Name;
         public int Id;

         public ListBox_ThresholdItem(string name, int id)
         {
            Name = name;
            Id = id;
         }

         public override string ToString()
         {
            return Name;
         }
      }

      private void InitializeThresholdsListBox()
      {
         foreach (var item in Model.ThresholdsModel.ThresholdSettings)
         {
            _lbx_Thresholds.Items.Add(new ListBox_ThresholdItem(item.Name, item.Id));
         }

         _lbx_Thresholds.SelectedIndexChanged += _lbx_Thresholds_SelectedIndexChanged;
      }

      /// <summary>
      /// Handle the click event of Refresh button
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
      private void _btn_Refresh_Click(object sender, EventArgs e)
      {
         try
         {
            double oldReading = Model.CurrentTemperature;
            TemperatureUnit oldUnit = Model.CurrentTempUnit;

            Model.GetLatestTemperature();

            if (oldUnit != Model.CurrentTempUnit)
               oldReading = new TemperatureUnitConvertor().ConvertToNewReading(oldReading, from: oldUnit, to: Model.CurrentTempUnit);

            // Handle UI
            this._lbl_TempReading.Text = Model.CurrentTempDisplay;

            // Handle Threshold
            if (Model.ThresholdsModel.TriggerThresholdAlert(prev: oldReading, curr: Model.CurrentTemperature))
            {
               MessageBox.Show("Threshold Alert", "Thermometer", MessageBoxButtons.OK, MessageBoxIcon.Information);
               Model.Triggered = true;
            }

         }
         catch (ThermometerUnknownUnitException)
         {
            MessageBox.Show("Unknown temperature unit from source found", "Thermometer", MessageBoxButtons.OK, MessageBoxIcon.Warning);
         }
      }

      /// <summary>
      /// Handle the selection changed event of Temperature Unit combobox
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
      private void _cbx_TempUnit_SelectedIndexChanged(object sender, System.EventArgs e)
      {
         //object a = _cbx_TempUnit.SelectedItem;
         //string b = _cbx_TempUnit.SelectedText;
         //object c = _cbx_TempUnit.SelectedValue;


         ComboBox_TempUnitItem updatedItem = _cbx_TempUnit.SelectedItem as ComboBox_TempUnitItem;
         if (updatedItem == null)
            return;
         Model.UpdateByTempUnitChange(updatedItem.Unit);

         // Update UI
         _lbl_TempReading.Text = Model.CurrentTempDisplay;
      }

      /// <summary>
      /// Handle the click event of Customize Threshold button
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
      private void _btn_Customize_Click(object sender, EventArgs e)
      {
         ThermometerThresholdUI thresholdUi = new ThermometerThresholdUI(Model.ThresholdsModel, _lbx_Thresholds.SelectedIndex);
         thresholdUi.ShowDialog();
      }

      private void _lbx_Thresholds_SelectedIndexChanged(object sender, EventArgs e)
      {
         //object a = _lbx_Thresholds.SelectedItem;
         //object b = _lbx_Thresholds.SelectedValue;
         //object c = _lbx_Thresholds.SelectedIndex;

         _btn_Customize.Enabled = (_lbx_Thresholds.SelectedIndex >= 0);
      }

   }
}
