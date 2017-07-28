using System;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using AR.Drone.Data;
using AR.Drone.Video;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Windows.Forms.DataVisualization.Charting;

namespace AR.Drone.WinApp
{
    public partial class PlayerForm : Form
    {
        private readonly VideoPacketDecoderWorker _videoPacketDecoderWorker;
        private string _fileName;
        private FilePlayer _filePlayer;
        private VideoFrame _frame;
        private Bitmap _frameBitmap;
        private decimal _frameNumber;
        private List<DataPID> dataPIDs = new List<DataPID>();
        private int dataPIDsI = 0;

        System.Windows.Forms.DataVisualization.Charting.Series seriesFnB = new System.Windows.Forms.DataVisualization.Charting.Series
        {
            Name = "SeriesFnB",
            Color = System.Drawing.Color.Green,
            IsVisibleInLegend = false,
            IsXValueIndexed = true,
            ChartType = SeriesChartType.Line
        };

        System.Windows.Forms.DataVisualization.Charting.Series seriesUnD = new System.Windows.Forms.DataVisualization.Charting.Series
        {
            Name = "SeriesUnD",
            Color = System.Drawing.Color.Green,
            IsVisibleInLegend = false,
            IsXValueIndexed = true,
            ChartType = SeriesChartType.Line
        };

        System.Windows.Forms.DataVisualization.Charting.Series seriesLnR = new System.Windows.Forms.DataVisualization.Charting.Series
        {
            Name = "SeriesLnR",
            Color = System.Drawing.Color.Green,
            IsVisibleInLegend = false,
            IsXValueIndexed = true,
            ChartType = SeriesChartType.Line
        };

        public PlayerForm()
        {
            InitializeComponent();
            chartFnB.Series.Clear();
            //chart1.ChartAreas[0].AxisX.Minimum = 5000;
            chartUnD.Series.Clear();
            //chart2.ChartAreas[0].AxisX.Minimum = 5000;
            chartLnR.Series.Clear();
            //chart3.ChartAreas[0].AxisX.Minimum = 5000;
           

            _videoPacketDecoderWorker = new VideoPacketDecoderWorker(PixelFormat.BGR24, true, OnVideoPacketDecoded);
            _videoPacketDecoderWorker.Start();

            tmrVideoUpdate.Enabled = true;

            _videoPacketDecoderWorker.UnhandledException += UnhandledException;
        }

        public string FileName
        {
            get { return _fileName; }
            set
            {
                _fileName = value;
                Text = Path.GetFileName(_fileName);
                // Read the file as one string.
                System.IO.StreamReader jsonFile = new System.IO.StreamReader(_fileName + ".json");
                string json = jsonFile.ReadToEnd();
                dataPIDs = JsonConvert.DeserializeObject<List<DataPID>>(json);
            }
        }

        private void UnhandledException(object sender, Exception exception)
        {
            MessageBox.Show(exception.ToString(), "Unhandled Exception (Ctrl+C)", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void StartPlaying()
        {
            StopPlaying();
            if (_filePlayer == null) _filePlayer = new FilePlayer(_fileName, OnNavigationPacketAcquired, OnVideoPacketAcquired);
            _filePlayer.UnhandledException += UnhandledException;
            _filePlayer.Start();
        }

        private void OnNavigationPacketAcquired(NavigationPacket obj)
        {
            // do nothing
        }

        private void OnVideoPacketAcquired(VideoPacket packet)
        {
            _videoPacketDecoderWorker.EnqueuePacket(packet);
            Thread.Sleep(20);
        }

        private void StopPlaying()
        {
            if (_filePlayer != null)
            {
                _filePlayer.Stop();
                _filePlayer.Join();
            }
        }

        private void PausePlaying()
        {
            if (_filePlayer != null)
            {
                _filePlayer.Stop();
                _filePlayer.Join();
            }
        }


        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            StartPlaying();
        }

        private void OnVideoPacketDecoded(VideoFrame frame)
        {
            _frame = frame;
        }

        private void tmrVideoUpdate_Tick(object sender, EventArgs e)
        {
            if (_frame == null || _frameNumber == _frame.Number)
                return;
            _frameNumber = _frame.Number;

            Console.WriteLine(_frameNumber);

            if (_frameBitmap == null)
                _frameBitmap = VideoHelper.CreateBitmap(ref _frame);
            else
                VideoHelper.UpdateBitmap(ref _frameBitmap, ref _frame);

            pbVideo.Image = _frameBitmap;
            
            while (dataPIDsI < dataPIDs.Count)
            {


               


                if (_frameNumber == dataPIDs[dataPIDsI]._number_of_frame)
                {

                   

                chartFnB.Series.Clear();
                seriesFnB.Points.AddXY(dataPIDs[dataPIDsI]._time, dataPIDs[dataPIDsI]._error_forwardnback);
                chartFnB.Series.Add(seriesFnB);

                chartUnD.Series.Clear();
                seriesUnD.Points.AddXY(dataPIDs[dataPIDsI]._time, dataPIDs[dataPIDsI]._error_upndown);
                chartUnD.Series.Add(seriesUnD);

                chartLnR.Series.Clear();
                seriesLnR.Points.AddXY(dataPIDs[dataPIDsI]._time, dataPIDs[dataPIDsI]._error_leftnright);
                chartLnR.Series.Add(seriesLnR);
                    dataPIDsI = dataPIDs.Count;
                }else{
                    dataPIDsI++;
                    }

                
                


            }
                            
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnReplay_Click(object sender, EventArgs e)
        {
            StopPlaying();
            StartPlaying();
        }

        protected override void OnClosed(EventArgs e)
        {
            StopPlaying();

            _videoPacketDecoderWorker.Dispose();

            base.OnClosed(e);
        }

        private void btnPause_Click(object sender, EventArgs e)
        {

        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            StopPlaying();
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            StartPlaying();
        }
    }
}