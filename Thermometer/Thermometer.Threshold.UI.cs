using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Thermometer.Common;
using Thermometer.Domain;
using Thermometer.IO;

namespace Thermometer
{
   public partial class ThermometerThresholdUI : Form
   {
      protected ThermometerThresholdUI()
      {
         InitializeComponent();
      }

      public ThermometerThresholdUI(ThresholdModel model, int selectedIndex)
      {
         InitializeComponent();

         _model = model;
         _currThresholdData = model.ThresholdSettings[selectedIndex];

         // Update UI
         _tbx_Name.Text = CurrentData.Name;
         _tbx_Name.Enabled = CurrentData.Id != 1 && CurrentData.Id != 2;

         _tbx_Temperature.Text = CurrentData.Temperature.ToString("F1");

         _cbx_FromDirection.Items.Add(new ComboBox_Direction(ThresholdGoDirection.None.ToString(), ThresholdGoDirection.None));
         _cbx_FromDirection.Items.Add(new ComboBox_Direction(ThresholdGoDirection.Above.ToString(), ThresholdGoDirection.Above));
         _cbx_FromDirection.Items.Add(new ComboBox_Direction(ThresholdGoDirection.Below.ToString(), ThresholdGoDirection.Below));
         _cbx_FromDirection.SelectedIndex = (int)CurrentData.Direction;

         _ckBx_IgnoreHalfUnit.Checked = CurrentData.IgnoreHalfUnit;
         _ckBx_IgnoreHalfUnit.AutoCheck = true;
      }

      private class ComboBox_Direction
      {
         public string Name;
         public ThresholdGoDirection Direction;

         public ComboBox_Direction(string name, ThresholdGoDirection direction)
         {
            Name = name;
            Direction = direction;
         }

         public override string ToString()
         {
            return Name;
         }
      }

      #region Properties
      private ThresholdModel _model = null;
      public ThresholdModel Model
      {
         get { return _model; }
      }

      private IThresholdData _currThresholdData = null;
      public IThresholdData CurrentData
      {
         get { return _currThresholdData; }
      }
      #endregion

      private void _btn_Cancel_Click(object sender, EventArgs e)
      {
         this.Close();
      }

      private void _btn_Save_Click(object sender, EventArgs e)
      {
         // Update memory
         CurrentData.Name = _tbx_Name.Text;
         double displayValue = 0.0;
         if (double.TryParse(_tbx_Temperature.Text, out displayValue))
            CurrentData.Temperature = displayValue;
         CurrentData.Direction = (_cbx_FromDirection.SelectedItem as ComboBox_Direction).Direction;
         CurrentData.IgnoreHalfUnit = _ckBx_IgnoreHalfUnit.Checked;

         // Save db/file
         //Model.SaveAll();

         this.Close();
      }

      private void _ckBx_IgnoreHalfUnit_CheckedChanged(object sender, EventArgs e)
      {

      }
   }
}
