namespace AR.Drone.WinApp
{
    partial class PlayerForm
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea10 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend10 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series10 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea11 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend11 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series11 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea12 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend12 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series12 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.pbVideo = new System.Windows.Forms.PictureBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnReplay = new System.Windows.Forms.Button();
            this.tmrVideoUpdate = new System.Windows.Forms.Timer(this.components);
            this.chartUnD = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chartLnR = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chartFnB = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tvInfo = new System.Windows.Forms.TreeView();
            this.btnPause = new System.Windows.Forms.Button();
            this.btnPlay = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pbVideo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartUnD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartLnR)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartFnB)).BeginInit();
            this.SuspendLayout();
            // 
            // pbVideo
            // 
            this.pbVideo.BackColor = System.Drawing.SystemColors.ControlDark;
            this.pbVideo.Location = new System.Drawing.Point(2, 0);
            this.pbVideo.Name = "pbVideo";
            this.pbVideo.Size = new System.Drawing.Size(640, 360);
            this.pbVideo.TabIndex = 3;
            this.pbVideo.TabStop = false;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(2, 482);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 4;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnReplay
            // 
            this.btnReplay.Location = new System.Drawing.Point(2, 453);
            this.btnReplay.Name = "btnReplay";
            this.btnReplay.Size = new System.Drawing.Size(75, 23);
            this.btnReplay.TabIndex = 5;
            this.btnReplay.Text = "Replay";
            this.btnReplay.UseVisualStyleBackColor = true;
            this.btnReplay.Click += new System.EventHandler(this.btnReplay_Click);
            // 
            // tmrVideoUpdate
            // 
            this.tmrVideoUpdate.Interval = 30;
            this.tmrVideoUpdate.Tick += new System.EventHandler(this.tmrVideoUpdate_Tick);
            // 
            // chartUnD
            // 
            chartArea10.Name = "ChartArea1";
            this.chartUnD.ChartAreas.Add(chartArea10);
            legend10.Name = "Legend1";
            this.chartUnD.Legends.Add(legend10);
            this.chartUnD.Location = new System.Drawing.Point(648, 0);
            this.chartUnD.Name = "chartUnD";
            this.chartUnD.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Bright;
            series10.ChartArea = "ChartArea1";
            series10.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series10.IsVisibleInLegend = false;
            series10.Legend = "Legend1";
            series10.Name = "Series1";
            this.chartUnD.Series.Add(series10);
            this.chartUnD.Size = new System.Drawing.Size(552, 180);
            this.chartUnD.TabIndex = 37;
            this.chartUnD.Text = "chart2";
            // 
            // chartLnR
            // 
            chartArea11.Name = "ChartArea1";
            this.chartLnR.ChartAreas.Add(chartArea11);
            legend11.Name = "Legend1";
            this.chartLnR.Legends.Add(legend11);
            this.chartLnR.Location = new System.Drawing.Point(648, 186);
            this.chartLnR.Name = "chartLnR";
            this.chartLnR.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Bright;
            series11.ChartArea = "ChartArea1";
            series11.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series11.IsVisibleInLegend = false;
            series11.Legend = "Legend1";
            series11.Name = "Series1";
            this.chartLnR.Series.Add(series11);
            this.chartLnR.Size = new System.Drawing.Size(552, 180);
            this.chartLnR.TabIndex = 38;
            this.chartLnR.Text = "chart2";
            // 
            // chartFnB
            // 
            chartArea12.Name = "ChartArea1";
            this.chartFnB.ChartAreas.Add(chartArea12);
            legend12.Name = "Legend1";
            this.chartFnB.Legends.Add(legend12);
            this.chartFnB.Location = new System.Drawing.Point(648, 372);
            this.chartFnB.Name = "chartFnB";
            this.chartFnB.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Bright;
            series12.ChartArea = "ChartArea1";
            series12.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series12.IsVisibleInLegend = false;
            series12.Legend = "Legend1";
            series12.Name = "Series1";
            this.chartFnB.Series.Add(series12);
            this.chartFnB.Size = new System.Drawing.Size(552, 180);
            this.chartFnB.TabIndex = 39;
            this.chartFnB.Text = "chart2";
            // 
            // tvInfo
            // 
            this.tvInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tvInfo.Location = new System.Drawing.Point(1206, -1);
            this.tvInfo.Name = "tvInfo";
            this.tvInfo.Size = new System.Drawing.Size(162, 556);
            this.tvInfo.TabIndex = 40;
            // 
            // btnPause
            // 
            this.btnPause.Location = new System.Drawing.Point(2, 395);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(75, 23);
            this.btnPause.TabIndex = 41;
            this.btnPause.Text = "Pause";
            this.btnPause.UseVisualStyleBackColor = true;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // btnPlay
            // 
            this.btnPlay.Location = new System.Drawing.Point(2, 366);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(75, 23);
            this.btnPlay.TabIndex = 42;
            this.btnPlay.Text = "Play";
            this.btnPlay.UseVisualStyleBackColor = true;
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(2, 424);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 23);
            this.btnStop.TabIndex = 43;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // PlayerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1369, 556);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnPlay);
            this.Controls.Add(this.btnPause);
            this.Controls.Add(this.tvInfo);
            this.Controls.Add(this.chartFnB);
            this.Controls.Add(this.chartLnR);
            this.Controls.Add(this.chartUnD);
            this.Controls.Add(this.btnReplay);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.pbVideo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "PlayerForm";
            this.Text = "PlayerForm";
            ((System.ComponentModel.ISupportInitialize)(this.pbVideo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartUnD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartLnR)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartFnB)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbVideo;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnReplay;
        private System.Windows.Forms.Timer tmrVideoUpdate;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartUnD;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartLnR;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartFnB;
        private System.Windows.Forms.TreeView tvInfo;
        private System.Windows.Forms.Button btnPause;
        private System.Windows.Forms.Button btnPlay;
        private System.Windows.Forms.Button btnStop;
    }
}