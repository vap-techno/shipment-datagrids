namespace ShipmentDataGrids.UI
{
    partial class Form1
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
            this.shipmentDataGridControl = new ShipmentDataGrids.Lib.ShipmentDataGridControl();
            this.shipmentDataGridControl1 = new ShipmentDataGrids.Lib.ShipmentDataGridControl();
            this.SuspendLayout();
            // 
            // shipmentDataGridControl
            // 
            this.shipmentDataGridControl.AutoSize = true;
            this.shipmentDataGridControl.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.shipmentDataGridControl.Location = new System.Drawing.Point(0, 0);
            this.shipmentDataGridControl.Name = "shipmentDataGridControl";
            this.shipmentDataGridControl.Size = new System.Drawing.Size(0, 536);
            this.shipmentDataGridControl.TabIndex = 0;
            // 
            // shipmentDataGridControl1
            // 
            this.shipmentDataGridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.shipmentDataGridControl1.Location = new System.Drawing.Point(0, 0);
            this.shipmentDataGridControl1.Name = "shipmentDataGridControl1";
            this.shipmentDataGridControl1.Size = new System.Drawing.Size(1603, 554);
            this.shipmentDataGridControl1.TabIndex = 1;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1603, 554);
            this.Controls.Add(this.shipmentDataGridControl1);
            this.Controls.Add(this.shipmentDataGridControl);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ShipmentDataGrids.Lib.ShipmentDataGridControl shipmentDataGridControl;
        private Lib.ShipmentDataGridControl shipmentDataGridControl1;
    }
}

