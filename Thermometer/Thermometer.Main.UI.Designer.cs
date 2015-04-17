namespace Thermometer
{
   partial class ThermometerMainUI
   {
      /// <summary>
      /// Required designer variable.
      /// </summary>
      private System.ComponentModel.IContainer components = null;

      /// <summary>
      /// Clean up any resources being used.
      /// </summary>
      /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
      protected override void Dispose(bool disposing)
      {
         if (disposing && (components != null))
         {
            components.Dispose();
         }
         base.Dispose(disposing);
      }

      #region Windows Form Designer generated code

      /// <summary>
      /// Required method for Designer support - do not modify
      /// the contents of this method with the code editor.
      /// </summary>
      private void InitializeComponent()
      {
         this._gbx_Temperature = new System.Windows.Forms.GroupBox();
         this._cbx_TempUnit = new System.Windows.Forms.ComboBox();
         this._lbl_UnitSwitch = new System.Windows.Forms.Label();
         this._btn_Refresh = new System.Windows.Forms.Button();
         this._lbl_TempReading = new System.Windows.Forms.Label();
         this._lbl_current = new System.Windows.Forms.Label();
         this._gbx_Thresholds = new System.Windows.Forms.GroupBox();
         this._lbl_ThresholdInstructions = new System.Windows.Forms.Label();
         this._btn_Customize = new System.Windows.Forms.Button();
         this._lbx_Thresholds = new System.Windows.Forms.ListBox();
         this._gbx_Temperature.SuspendLayout();
         this._gbx_Thresholds.SuspendLayout();
         this.SuspendLayout();
         // 
         // _gbx_Temperature
         // 
         this._gbx_Temperature.Controls.Add(this._cbx_TempUnit);
         this._gbx_Temperature.Controls.Add(this._lbl_UnitSwitch);
         this._gbx_Temperature.Controls.Add(this._btn_Refresh);
         this._gbx_Temperature.Controls.Add(this._lbl_TempReading);
         this._gbx_Temperature.Controls.Add(this._lbl_current);
         this._gbx_Temperature.Location = new System.Drawing.Point(12, 12);
         this._gbx_Temperature.Name = "_gbx_Temperature";
         this._gbx_Temperature.Size = new System.Drawing.Size(480, 113);
         this._gbx_Temperature.TabIndex = 0;
         this._gbx_Temperature.TabStop = false;
         this._gbx_Temperature.Text = "Temperature";
         // 
         // _cbx_TempUnit
         // 
         this._cbx_TempUnit.FormattingEnabled = true;
         this._cbx_TempUnit.Location = new System.Drawing.Point(351, 67);
         this._cbx_TempUnit.Name = "_cbx_TempUnit";
         this._cbx_TempUnit.Size = new System.Drawing.Size(100, 21);
         this._cbx_TempUnit.TabIndex = 4;
         // 
         // _lbl_UnitSwitch
         // 
         this._lbl_UnitSwitch.Location = new System.Drawing.Point(16, 70);
         this._lbl_UnitSwitch.Name = "_lbl_UnitSwitch";
         this._lbl_UnitSwitch.Size = new System.Drawing.Size(329, 23);
         this._lbl_UnitSwitch.TabIndex = 3;
         this._lbl_UnitSwitch.Text = "You can switch the temperature unit by selecting the dropdown box.";
         // 
         // _btn_Refresh
         // 
         this._btn_Refresh.Location = new System.Drawing.Point(200, 30);
         this._btn_Refresh.Name = "_btn_Refresh";
         this._btn_Refresh.Size = new System.Drawing.Size(75, 23);
         this._btn_Refresh.TabIndex = 2;
         this._btn_Refresh.Text = "&Refresh";
         this._btn_Refresh.UseVisualStyleBackColor = true;
         this._btn_Refresh.Click += new System.EventHandler(this._btn_Refresh_Click);
         // 
         // _lbl_TempReading
         // 
         this._lbl_TempReading.Location = new System.Drawing.Point(63, 35);
         this._lbl_TempReading.Name = "_lbl_TempReading";
         this._lbl_TempReading.Size = new System.Drawing.Size(131, 23);
         this._lbl_TempReading.TabIndex = 1;
         this._lbl_TempReading.Text = "N/A";
         // 
         // _lbl_current
         // 
         this._lbl_current.AutoSize = true;
         this._lbl_current.Location = new System.Drawing.Point(16, 35);
         this._lbl_current.Name = "_lbl_current";
         this._lbl_current.Size = new System.Drawing.Size(44, 13);
         this._lbl_current.TabIndex = 0;
         this._lbl_current.Text = "Current:";
         // 
         // _gbx_Thresholds
         // 
         this._gbx_Thresholds.Controls.Add(this._lbl_ThresholdInstructions);
         this._gbx_Thresholds.Controls.Add(this._btn_Customize);
         this._gbx_Thresholds.Controls.Add(this._lbx_Thresholds);
         this._gbx_Thresholds.Location = new System.Drawing.Point(12, 147);
         this._gbx_Thresholds.Name = "_gbx_Thresholds";
         this._gbx_Thresholds.Size = new System.Drawing.Size(480, 135);
         this._gbx_Thresholds.TabIndex = 1;
         this._gbx_Thresholds.TabStop = false;
         this._gbx_Thresholds.Text = "Threshold Settings";
         // 
         // _lbl_ThresholdInstructions
         // 
         this._lbl_ThresholdInstructions.Location = new System.Drawing.Point(179, 36);
         this._lbl_ThresholdInstructions.Name = "_lbl_ThresholdInstructions";
         this._lbl_ThresholdInstructions.Size = new System.Drawing.Size(287, 31);
         this._lbl_ThresholdInstructions.TabIndex = 3;
         this._lbl_ThresholdInstructions.Text = "Select threshold setting in the list box and customize by selecting the Customize" +
    " Threshold button.";
         // 
         // _btn_Customize
         // 
         this._btn_Customize.Enabled = false;
         this._btn_Customize.Location = new System.Drawing.Point(237, 70);
         this._btn_Customize.Name = "_btn_Customize";
         this._btn_Customize.Size = new System.Drawing.Size(138, 23);
         this._btn_Customize.TabIndex = 2;
         this._btn_Customize.Text = "&Customize Threshold";
         this._btn_Customize.UseVisualStyleBackColor = true;
         this._btn_Customize.Click += new System.EventHandler(this._btn_Customize_Click);
         // 
         // _lbx_Thresholds
         // 
         this._lbx_Thresholds.FormattingEnabled = true;
         this._lbx_Thresholds.Location = new System.Drawing.Point(19, 32);
         this._lbx_Thresholds.Name = "_lbx_Thresholds";
         this._lbx_Thresholds.Size = new System.Drawing.Size(120, 82);
         this._lbx_Thresholds.TabIndex = 1;
         // 
         // ThermometerMainUI
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(504, 328);
         this.Controls.Add(this._gbx_Thresholds);
         this.Controls.Add(this._gbx_Temperature);
         this.Name = "ThermometerMainUI";
         this.Text = "Thermometer";
         this._gbx_Temperature.ResumeLayout(false);
         this._gbx_Temperature.PerformLayout();
         this._gbx_Thresholds.ResumeLayout(false);
         this.ResumeLayout(false);

      }

      #endregion

      private System.Windows.Forms.GroupBox _gbx_Temperature;
      private System.Windows.Forms.Button _btn_Refresh;
      private System.Windows.Forms.Label _lbl_TempReading;
      private System.Windows.Forms.Label _lbl_current;
      private System.Windows.Forms.ComboBox _cbx_TempUnit;
      private System.Windows.Forms.Label _lbl_UnitSwitch;
      private System.Windows.Forms.GroupBox _gbx_Thresholds;
      private System.Windows.Forms.ListBox _lbx_Thresholds;
      private System.Windows.Forms.Label _lbl_ThresholdInstructions;
      private System.Windows.Forms.Button _btn_Customize;

   }
}

