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
            lblTemperature = new Label();
            lblPressure = new Label();
            lblStatus = new Label();
            btnStart = new Button();
            btnStop = new Button();
            SuspendLayout();
            // 
            // lblTemperature
            // 
            lblTemperature.AutoSize = true;
            lblTemperature.Location = new Point(148, 47);
            lblTemperature.Name = "lblTemperature";
            lblTemperature.Size = new Size(59, 25);
            lblTemperature.TabIndex = 0;
            lblTemperature.Text = "label1";
            // 
            // lblPressure
            // 
            lblPressure.AutoSize = true;
            lblPressure.Location = new Point(148, 93);
            lblPressure.Name = "lblPressure";
            lblPressure.Size = new Size(59, 25);
            lblPressure.TabIndex = 1;
            lblPressure.Text = "label2";
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Location = new Point(148, 139);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(59, 25);
            lblStatus.TabIndex = 2;
            lblStatus.Text = "label3";
            // 
            // btnStart
            // 
            btnStart.Location = new Point(148, 209);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(112, 34);
            btnStart.TabIndex = 3;
            btnStart.Text = "Start";
            btnStart.UseVisualStyleBackColor = true;
            btnStart.Click += btnStart_Click;
            // 
            // btnStop
            // 
            btnStop.Location = new Point(316, 209);
            btnStop.Name = "btnStop";
            btnStop.Size = new Size(112, 34);
            btnStop.TabIndex = 4;
            btnStop.Text = "Stop";
            btnStop.UseVisualStyleBackColor = true;
            btnStop.Click += btnStop_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnStop);
            Controls.Add(btnStart);
            Controls.Add(lblStatus);
            Controls.Add(lblPressure);
            Controls.Add(lblTemperature);
            Name = "MainForm";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTemperature;
        private Label lblPressure;
        private Label lblStatus;
        private Button btnStart;
        private Button btnStop;
    }
}
