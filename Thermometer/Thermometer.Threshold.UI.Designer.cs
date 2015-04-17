namespace Thermometer
{
   partial class ThermometerThresholdUI
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
         this._lbl_Name = new System.Windows.Forms.Label();
         this._tbx_Name = new System.Windows.Forms.TextBox();
         this.label1 = new System.Windows.Forms.Label();
         this._tbx_Temperature = new System.Windows.Forms.TextBox();
         this.label2 = new System.Windows.Forms.Label();
         this._cbx_FromDirection = new System.Windows.Forms.ComboBox();
         this._btn_Save = new System.Windows.Forms.Button();
         this._btn_Cancel = new System.Windows.Forms.Button();
         this._ckBx_IgnoreHalfUnit = new System.Windows.Forms.CheckBox();
         this.SuspendLayout();
         // 
         // _lbl_Name
         // 
         this._lbl_Name.AutoSize = true;
         this._lbl_Name.Location = new System.Drawing.Point(12, 29);
         this._lbl_Name.Name = "_lbl_Name";
         this._lbl_Name.Size = new System.Drawing.Size(38, 13);
         this._lbl_Name.TabIndex = 0;
         this._lbl_Name.Text = "Name:";
         // 
         // _tbx_Name
         // 
         this._tbx_Name.Location = new System.Drawing.Point(107, 26);
         this._tbx_Name.Name = "_tbx_Name";
         this._tbx_Name.Size = new System.Drawing.Size(100, 20);
         this._tbx_Name.TabIndex = 1;
         // 
         // label1
         // 
         this.label1.AutoSize = true;
         this.label1.Location = new System.Drawing.Point(12, 61);
         this.label1.Name = "label1";
         this.label1.Size = new System.Drawing.Size(70, 13);
         this.label1.TabIndex = 2;
         this.label1.Text = "Temperature:";
         // 
         // _tbx_Temperature
         // 
         this._tbx_Temperature.Location = new System.Drawing.Point(107, 58);
         this._tbx_Temperature.Name = "_tbx_Temperature";
         this._tbx_Temperature.Size = new System.Drawing.Size(100, 20);
         this._tbx_Temperature.TabIndex = 3;
         // 
         // label2
         // 
         this.label2.AutoSize = true;
         this.label2.Location = new System.Drawing.Point(12, 94);
         this.label2.Name = "label2";
         this.label2.Size = new System.Drawing.Size(95, 13);
         this.label2.TabIndex = 8;
         this.label2.Text = "Go From Direction:";
         // 
         // _cbx_FromDirection
         // 
         this._cbx_FromDirection.FormattingEnabled = true;
         this._cbx_FromDirection.Location = new System.Drawing.Point(108, 91);
         this._cbx_FromDirection.Name = "_cbx_FromDirection";
         this._cbx_FromDirection.Size = new System.Drawing.Size(99, 21);
         this._cbx_FromDirection.TabIndex = 9;
         // 
         // _btn_Save
         // 
         this._btn_Save.Location = new System.Drawing.Point(15, 181);
         this._btn_Save.Name = "_btn_Save";
         this._btn_Save.Size = new System.Drawing.Size(105, 23);
         this._btn_Save.TabIndex = 10;
         this._btn_Save.Text = "&Save and Close";
         this._btn_Save.UseVisualStyleBackColor = true;
         this._btn_Save.Click += new System.EventHandler(this._btn_Save_Click);
         // 
         // _btn_Cancel
         // 
         this._btn_Cancel.Location = new System.Drawing.Point(140, 181);
         this._btn_Cancel.Name = "_btn_Cancel";
         this._btn_Cancel.Size = new System.Drawing.Size(105, 23);
         this._btn_Cancel.TabIndex = 11;
         this._btn_Cancel.Text = "&Cancel";
         this._btn_Cancel.UseVisualStyleBackColor = true;
         this._btn_Cancel.Click += new System.EventHandler(this._btn_Cancel_Click);
         // 
         // _ckBx_IgnoreHalfUnit
         // 
         this._ckBx_IgnoreHalfUnit.Location = new System.Drawing.Point(15, 126);
         this._ckBx_IgnoreHalfUnit.Name = "_ckBx_IgnoreHalfUnit";
         this._ckBx_IgnoreHalfUnit.Size = new System.Drawing.Size(230, 24);
         this._ckBx_IgnoreHalfUnit.TabIndex = 12;
         this._ckBx_IgnoreHalfUnit.Text = "&Ignore +/- 0.5 within the Temperature.";
         this._ckBx_IgnoreHalfUnit.UseVisualStyleBackColor = true;
         this._ckBx_IgnoreHalfUnit.CheckedChanged += new System.EventHandler(this._ckBx_IgnoreHalfUnit_CheckedChanged);
         // 
         // ThermometerThresholdUI
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(257, 221);
         this.Controls.Add(this._ckBx_IgnoreHalfUnit);
         this.Controls.Add(this._btn_Cancel);
         this.Controls.Add(this._btn_Save);
         this.Controls.Add(this._cbx_FromDirection);
         this.Controls.Add(this.label2);
         this.Controls.Add(this._tbx_Temperature);
         this.Controls.Add(this.label1);
         this.Controls.Add(this._tbx_Name);
         this.Controls.Add(this._lbl_Name);
         this.Name = "ThermometerThresholdUI";
         this.Text = "Threshold Setting";
         this.ResumeLayout(false);
         this.PerformLayout();

      }

      #endregion

      private System.Windows.Forms.Label _lbl_Name;
      private System.Windows.Forms.TextBox _tbx_Name;
      private System.Windows.Forms.Label label1;
      private System.Windows.Forms.TextBox _tbx_Temperature;
      private System.Windows.Forms.Label label2;
      private System.Windows.Forms.ComboBox _cbx_FromDirection;
      private System.Windows.Forms.Button _btn_Save;
      private System.Windows.Forms.Button _btn_Cancel;
      private System.Windows.Forms.CheckBox _ckBx_IgnoreHalfUnit;
   }
}