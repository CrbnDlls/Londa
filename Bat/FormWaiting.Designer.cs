namespace Bat
{
    partial class FormWaiting
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
            this.labelProcess = new System.Windows.Forms.Label();
            this.tableLayoutPanelMain = new System.Windows.Forms.TableLayoutPanel();
            this.labelDots = new System.Windows.Forms.Label();
            this.labelZero = new System.Windows.Forms.Label();
            this.labelRowsCount = new System.Windows.Forms.Label();
            this.tableLayoutPanelMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelProcess
            // 
            this.labelProcess.AutoSize = true;
            this.tableLayoutPanelMain.SetColumnSpan(this.labelProcess, 3);
            this.labelProcess.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelProcess.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelProcess.Location = new System.Drawing.Point(3, 0);
            this.labelProcess.Name = "labelProcess";
            this.labelProcess.Size = new System.Drawing.Size(299, 40);
            this.labelProcess.TabIndex = 0;
            this.labelProcess.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanelMain
            // 
            this.tableLayoutPanelMain.ColumnCount = 3;
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelMain.Controls.Add(this.labelDots, 0, 1);
            this.tableLayoutPanelMain.Controls.Add(this.labelZero, 0, 1);
            this.tableLayoutPanelMain.Controls.Add(this.labelProcess, 0, 0);
            this.tableLayoutPanelMain.Controls.Add(this.labelRowsCount, 2, 1);
            this.tableLayoutPanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelMain.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelMain.Name = "tableLayoutPanelMain";
            this.tableLayoutPanelMain.RowCount = 2;
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelMain.Size = new System.Drawing.Size(305, 84);
            this.tableLayoutPanelMain.TabIndex = 2;
            // 
            // labelDots
            // 
            this.labelDots.AutoSize = true;
            this.labelDots.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelDots.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelDots.Location = new System.Drawing.Point(135, 40);
            this.labelDots.Name = "labelDots";
            this.labelDots.Size = new System.Drawing.Size(34, 44);
            this.labelDots.TabIndex = 4;
            this.labelDots.Text = "...";
            this.labelDots.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelZero
            // 
            this.labelZero.AutoSize = true;
            this.labelZero.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelZero.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelZero.Location = new System.Drawing.Point(3, 40);
            this.labelZero.Name = "labelZero";
            this.labelZero.Size = new System.Drawing.Size(126, 44);
            this.labelZero.TabIndex = 3;
            this.labelZero.Text = "0";
            this.labelZero.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelRowsCount
            // 
            this.labelRowsCount.AutoSize = true;
            this.labelRowsCount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelRowsCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelRowsCount.Location = new System.Drawing.Point(175, 40);
            this.labelRowsCount.Name = "labelRowsCount";
            this.labelRowsCount.Size = new System.Drawing.Size(127, 44);
            this.labelRowsCount.TabIndex = 2;
            this.labelRowsCount.Text = "0";
            this.labelRowsCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FormWaiting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(305, 84);
            this.ControlBox = false;
            this.Controls.Add(this.tableLayoutPanelMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormWaiting";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.TopMost = true;
            this.tableLayoutPanelMain.ResumeLayout(false);
            this.tableLayoutPanelMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelProcess;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelMain;
        private System.Windows.Forms.Label labelRowsCount;
        private System.Windows.Forms.Label labelDots;
        private System.Windows.Forms.Label labelZero;
    }
}