namespace AntApp.UI
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            lblTemperature = new Label();
            lblPressure = new Label();
            lblStatus = new Label();
            btnStart = new Button();
            btnStop = new Button();
            chartTemperature = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)chartTemperature).BeginInit();
            SuspendLayout();
            // 
            // lblTemperature
            // 
            lblTemperature.AutoSize = true;
            lblTemperature.Location = new Point(341, 49);
            lblTemperature.Name = "lblTemperature";
            lblTemperature.Size = new Size(59, 25);
            lblTemperature.TabIndex = 0;
            lblTemperature.Text = "label1";
            // 
            // lblPressure
            // 
            lblPressure.AutoSize = true;
            lblPressure.Location = new Point(459, 49);
            lblPressure.Name = "lblPressure";
            lblPressure.Size = new Size(59, 25);
            lblPressure.TabIndex = 1;
            lblPressure.Text = "label2";
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Location = new Point(587, 49);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(59, 25);
            lblStatus.TabIndex = 2;
            lblStatus.Text = "label3";
            // 
            // btnStart
            // 
            btnStart.Location = new Point(44, 44);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(112, 34);
            btnStart.TabIndex = 3;
            btnStart.Text = "Start";
            btnStart.UseVisualStyleBackColor = true;
            btnStart.Click += btnStart_Click;
            // 
            // btnStop
            // 
            btnStop.Location = new Point(181, 44);
            btnStop.Name = "btnStop";
            btnStop.Size = new Size(112, 34);
            btnStop.TabIndex = 4;
            btnStop.Text = "Stop";
            btnStop.UseVisualStyleBackColor = true;
            btnStop.Click += btnStop_Click;
            // 
            // chartTemperature
            // 
            chartArea2.Name = "ChartArea1";
            chartTemperature.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            chartTemperature.Legends.Add(legend2);
            chartTemperature.Location = new Point(44, 97);
            chartTemperature.Name = "chartTemperature";
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            chartTemperature.Series.Add(series2);
            chartTemperature.Size = new Size(602, 332);
            chartTemperature.TabIndex = 5;
            chartTemperature.Text = "chart1";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(693, 481);
            Controls.Add(chartTemperature);
            Controls.Add(btnStop);
            Controls.Add(btnStart);
            Controls.Add(lblStatus);
            Controls.Add(lblPressure);
            Controls.Add(lblTemperature);
            Name = "MainForm";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)chartTemperature).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTemperature;
        private Label lblPressure;
        private Label lblStatus;
        private Button btnStart;
        private Button btnStop;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartTemperature;
    }
}
