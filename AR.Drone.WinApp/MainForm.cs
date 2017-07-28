using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using AR.Drone.Client;
using AR.Drone.Client.Command;
using AR.Drone.Client.Configuration;
using AR.Drone.Data;
using AR.Drone.Data.Navigation;
using AR.Drone.Data.Navigation.Native;
using AR.Drone.Media;
using AR.Drone.Video;
using AR.Drone.Avionics;
using AR.Drone.Avionics.Objectives;
using AR.Drone.Avionics.Objectives.IntentObtainers;
//EMGU
using Emgu.CV;

using Emgu.CV.Structure;
using Emgu.Util;
using System.Threading;

using System.Diagnostics;
using System.Runtime.InteropServices;
using Emgu.CV.Util;
using System.Globalization;
using System.Windows.Forms.DataVisualization.Charting;
using System.Drawing.Drawing2D;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using System.Text;
using System.Xml.Serialization;

namespace AR.Drone.WinApp
{
    public partial class MainForm : Form
    {
        #region Ar.Drone's variables
        private const string ARDroneTrackFileExt = ".ardrone";
        private const string ARDroneTrackFilesFilter = "AR.Drone track files (*.ardrone)|*.ardrone";
        private string path_json;
        private List<DataPID> _dataPIDs = new List<DataPID>();
        private readonly DroneClient _droneClient;
        private readonly List<PlayerForm> _playerForms;
        private readonly VideoPacketDecoderWorker _videoPacketDecoderWorker;
        private Settings _settings;
        private VideoFrame _frame;
        private Bitmap _frameBitmap;
        private uint _frameNumber;
        private uint _StartframeNumber;
        private NavigationData _navigationData;
        private NavigationPacket _navigationPacket;
        private PacketRecorder _packetRecorderWorker;
        private FileStream _recorderStream;
        private Autopilot _autopilot;
        private Boolean _takedOff = false;
        #endregion

        #region PID controller's variables
        Stopwatch _sw_previus = new Stopwatch();
        private float _integral_forwardnback = 0;
        private float _integral_upndown = 0;
        private float _integral_leftnright = 0;
        private float[,] _integrals = new float[4, 10];
        private int _number_integral = 10;
        private float _k_forwardnback_p = -0.4f;
        private float _k_forwardnback_d = -1.1f;
        private float _k_forwardnback_i = -0.005f;
        private float _k_leftnright_p = -0.4f;
        private float _k_leftnright_d = -1.1f;
        private float _k_leftnright_i = -0.005f;
        private float _k_upndown_p = 0.4f;
        private float _k_upndown_d = 1.1f;
        private float _k_upndown_i = 0.005f;
        private float _pitch = 0, _roll = 0, _gaz = 0;

        #region PID mission
        private static MCvPoint3D32f drone_point;
        private static MCvPoint3D32f drone_last_errors;
        private static MCvPoint3D32f drone_destination_point = new MCvPoint3D32f(0.0f, 0.0f, 200.0f);

        #endregion
        #endregion

        private static int _sample_length = 15;
        private float _sample_error = 5;

        private Boolean _calibration = false;
        

        #region Display and aquaring chess board info

        Image<Gray, Byte> Gray_Frame; // image for processing
        const int width = 9;//9 //width of chessboard no. squares in width - 1
        const int height = 8;//6 // heght of chess board no. squares in heigth - 1
        Size patternSize = new Size(width, height); //size of chess board to be detected
        PointF[] corners; //corners found from chessboard
        Bgr[] line_colour_array = new Bgr[width * height]; // just for displaying coloured lines of detected chessboard
        static Image<Gray, Byte>[] Frame_array_buffer = new Image<Gray,byte>[30]; //number of images to calibrate camera over
        int frame_buffer_savepoint = 0;
        bool take_foto_points = false;
        #endregion

        #region Current mode variables
        public enum Mode
        {
            Caluculating_Intrinsics,
            Calibrated,
            SavingFrames
        }
        Mode currentMode = Mode.Calibrated;
        #endregion

        #region Getting the camera calibration
        MCvPoint3D32f[][] corners_object_list = new MCvPoint3D32f[Frame_array_buffer.Length][];
        PointF[][] corners_points_list = new PointF[Frame_array_buffer.Length][];

        IntrinsicCameraParameters IC;
        ExtrinsicCameraParameters[] EX_Param;

        Matrix<float> Map1 = null , Map2 = null;

        #endregion

        


        public MainForm()
        {
            InitializeComponent();

            _videoPacketDecoderWorker = new VideoPacketDecoderWorker(PixelFormat.BGR24, true, OnVideoPacketDecoded);
            _videoPacketDecoderWorker.Start();

            _droneClient = new DroneClient("192.168.1.1");
            _droneClient.NavigationPacketAcquired += OnNavigationPacketAcquired;
            _droneClient.VideoPacketAcquired += OnVideoPacketAcquired;
            _droneClient.NavigationDataAcquired += data => _navigationData = data;

            tmrStateUpdate.Enabled = true;
            tmrVideoUpdate.Enabled = true;

            _playerForms = new List<PlayerForm>();

            _videoPacketDecoderWorker.UnhandledException += UnhandledException;


            try
            {
                IC = ReadXML("ICP_drone.xml");
                
            }
            catch(Exception e)
            {
                WriteInConsole("Невозможно найти внутренние параметры камеры. Если запуск производится впервые -- откалируйте камеру!");
                currentMode = Mode.SavingFrames;
            }
                
                
            
            

          
        }

        private void UnhandledException(object sender, Exception exception)
        {
            MessageBox.Show(exception.ToString(), "Unhandled Exception (Ctrl+C)", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Text += Environment.Is64BitProcess ? " [64-bit]" : " [32-bit]";
        }

        protected override void OnClosed(EventArgs e)
        {
            if (_autopilot != null)
            {
                _autopilot.UnbindFromClient();
                _autopilot.Stop();
            }
                       
            StopRecording();

            _droneClient.Dispose();
            _videoPacketDecoderWorker.Dispose();

            base.OnClosed(e);
        }

        private void OnNavigationPacketAcquired(NavigationPacket packet)
        {
            if (_packetRecorderWorker != null && _packetRecorderWorker.IsAlive)
                _packetRecorderWorker.EnqueuePacket(packet);

            _navigationPacket = packet;
        }

        private void OnVideoPacketAcquired(VideoPacket packet)
        {
            if (_packetRecorderWorker != null && _packetRecorderWorker.IsAlive)
                _packetRecorderWorker.EnqueuePacket(packet);
            if (_videoPacketDecoderWorker.IsAlive)
                _videoPacketDecoderWorker.EnqueuePacket(packet);
        }

        private void OnVideoPacketDecoded(VideoFrame frame)
        {
            _frame = frame;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            _droneClient.Start();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            _droneClient.Stop();
        }

        private void tmrVideoUpdate_Tick(object sender, EventArgs e)
        {
            #region Some flags
            if (_takedOff)
            {
                if (!_droneClient.NavigationData.State.HasFlag(NavigationState.Flying)) _takedOff = false;
            }

            if (!_takedOff)
            {
                if (_droneClient.NavigationData.State.HasFlag(NavigationState.Hovering)) _takedOff = true;
            }
            #endregion

            #region Take frame from drone
            if (_frame == null || _frameNumber == _frame.Number)
                return;
            _frameNumber = _frame.Number;

            if (_frameBitmap == null)
                _frameBitmap = VideoHelper.CreateBitmap(ref _frame);
            else VideoHelper.UpdateBitmap(ref _frameBitmap, ref _frame);
            Image<Bgr, Byte> imgOriginal = new Image<Bgr, Byte>(_frameBitmap);  // We are using imgOriginal how original image, or frame from drone
            #endregion

            #region Calibration mode
            if (_calibration)
            {
                Gray_Frame = imgOriginal.Convert<Gray, Byte>();
                //apply chess board detection
                if (currentMode == Mode.SavingFrames)
                {
                    corners = CameraCalibration.FindChessboardCorners(Gray_Frame, patternSize, Emgu.CV.CvEnum.CALIB_CB_TYPE.ADAPTIVE_THRESH);
                    //we use this loop so we can show a colour image rather than a gray: //CameraCalibration.DrawChessboardCorners(Gray_Frame, patternSize, corners);

                    if (corners != null) //chess board found
                    {
                        //make mesurments more accurate by using FindCornerSubPixel
                        Gray_Frame.FindCornerSubPix(new PointF[1][] { corners }, new Size(11, 11), new Size(-1, -1), new MCvTermCriteria(30, 0.1));

                        //if go button has been pressed start aquiring frames else we will just display the points
                        if (_calibration && take_foto_points)
                        {
                            Frame_array_buffer[frame_buffer_savepoint] = Gray_Frame.Copy(); //store the image
                            frame_buffer_savepoint++;//increase buffer positon
                            take_foto_points = false;
                            //check the state of buffer
                            if (frame_buffer_savepoint == Frame_array_buffer.Length) currentMode = Mode.Caluculating_Intrinsics; //buffer full
                        }

                        //dram the results
                        imgOriginal.Draw(new CircleF(corners[0], 3), new Bgr(Color.Yellow), 1);
                        for (int i = 1; i < corners.Length; i++)
                        {
                            imgOriginal.Draw(new LineSegment2DF(corners[i - 1], corners[i]), line_colour_array[i], 2);
                            imgOriginal.Draw(new CircleF(corners[i], 3), new Bgr(Color.Yellow), 1);
                        }
                        pbVideo1.Image = imgOriginal.ToBitmap(640, 360);//Display the image
                        //calibrate the delay bassed on size of buffer
                        //if buffer small you want a big delay if big small delay

                    }
                    corners = null;
                }
                if (currentMode == Mode.Caluculating_Intrinsics)
                {
                    //we can do this in the loop above to increase speed
                    for (int k = 0; k < Frame_array_buffer.Length; k++)
                    {

                        corners_points_list[k] = CameraCalibration.FindChessboardCorners(Frame_array_buffer[k], patternSize, Emgu.CV.CvEnum.CALIB_CB_TYPE.ADAPTIVE_THRESH);
                        //for accuracy
                        Gray_Frame.FindCornerSubPix(corners_points_list, new Size(11, 11), new Size(-1, -1), new MCvTermCriteria(30, 0.1));

                        //Fill our objects list with the real world mesurments for the intrinsic calculations
                        List<MCvPoint3D32f> object_list = new List<MCvPoint3D32f>();
                        for (int i = 0; i < height; i++)
                        {
                            for (int j = 0; j < width; j++)
                            {
                                object_list.Add(new MCvPoint3D32f(j * 60.0F, i * 60.0F, 0.0F));
                            }
                        }
                        corners_object_list[k] = object_list.ToArray();
                    }

                    //our error should be as close to 0 as possible
                    IC = new IntrinsicCameraParameters();
                    WriteInConsole(IC.IntrinsicMatrix.ToString());
                    double error = CameraCalibration.CalibrateCamera(corners_object_list, corners_points_list, Gray_Frame.Size, IC, Emgu.CV.CvEnum.CALIB_TYPE.CV_CALIB_RATIONAL_MODEL, out EX_Param);
                    WriteInConsole(IC.IntrinsicMatrix.Data.ToString());
                    //If Emgu.CV.CvEnum.CALIB_TYPE == CV_CALIB_USE_INTRINSIC_GUESS and/or CV_CALIB_FIX_ASPECT_RATIO are specified, some or all of fx, fy, cx, cy must be initialized before calling the function
                    //if you use FIX_ASPECT_RATIO and FIX_FOCAL_LEGNTH options, these values needs to be set in the intrinsic parameters before the CalibrateCamera function is called. Otherwise 0 values are used as default.
                    WriteInConsole("Intrinsci Calculation Error: " + error.ToString() + " Results"); //display the results to the user
                    _calibration = false;
                    button5.Visible = false;
                    checkBox1.Checked = false;
                    currentMode = Mode.Calibrated;
                    IC.InitUndistortMap(imgOriginal.Width, imgOriginal.Height, out Map1, out Map2);
                    WriteXML(IC, "ICP_drone.xml");

                }
            }

            #endregion

            #region Normal use. Calibrated.
            if (!_calibration && currentMode == Mode.Calibrated)
            {
                if (Map1 == null) IC.InitUndistortMap(imgOriginal.Width, imgOriginal.Height, out Map1, out Map2); // If we don't have map's for undistortion, we make it
                Stopwatch _first_watch = new Stopwatch();                              // Create new stopwatch for FPS counter
                _first_watch.Start();                                                  // Begin timing

                if (Map1 != null && Map2 != null && IC != null)
                {
                    #region Undistortion, finding the ChessBoard
                    
                    Image<Bgr, Byte> temp = imgOriginal.CopyBlank();
                    CvInvoke.cvRemap(imgOriginal, temp, Map1, Map2, 0, new MCvScalar(0));
                    //pbVideo2.Image = temp.ToBitmap(320, 180);
                    imgOriginal = temp.Copy();
                    //pbVideo1.Image = imgOriginal.ToBitmap(640, 360);
                    Int32 width = 9;                                                    // define the chess board size: Width! Count of rectangles - 1
                    Int32 height = 8;                                                   // define the chess board size: Height! Count of rectangles - 1
                    Size patternSize = new Size(width, height);                         // define the chess board size: Creating a variable of Size from width and height!
                    Image<Gray, Byte> InputImage = imgOriginal.Convert<Gray, Byte>();   // define new gray image variable for detecting a chessboard

                    // create a array of PointF to store the chess board corner locations
                    PointF[] corners = CameraCalibration.FindChessboardCorners(InputImage,      // Input image in gray scale 
                                                                               patternSize,     // Input size of the chessboard
                                           Emgu.CV.CvEnum.CALIB_CB_TYPE.ADAPTIVE_THRESH | Emgu.CV.CvEnum.CALIB_CB_TYPE.FILTER_QUADS);   // Some filters. Read more in Internet
                    #endregion

                    if (corners != null)
                    {
                        #region Get extrinsic camera parametrs
                        List<MCvPoint3D32f> object_list = new List<MCvPoint3D32f>();
                        for (int i = 0; i < height; i++)
                        {
                            for (int j = 0; j < width; j++)
                            {
                                object_list.Add(new MCvPoint3D32f(j * 6.0F, i * 6.0F, 0.0F));
                            }
                        }
                        ExtrinsicCameraParameters EX_Param_now = CameraCalibration.FindExtrinsicCameraParams2(object_list.ToArray(), corners, IC);
                        #endregion

                        #region Paint all extras on imgOriginal
                        //Create the font
                        MCvFont f = new MCvFont(Emgu.CV.CvEnum.FONT.CV_FONT_HERSHEY_COMPLEX, 0.3, 0.3);
                        for (int point_number = 0; point_number < corners.Length; point_number++)
                        {
                            PointF point = corners[point_number];
                            imgOriginal.Draw(point_number.ToString(), ref f, new Point((int)point.X, (int)point.Y), new Bgr(0, 255, 0));
                            //CircleF circle1 = new CircleF(new Point((int)point.X, (int)point.Y), 3);
                            //imgOriginal.Draw(circle1, new Bgr(Color.Red), 3);
                        }

                        PointF centr_chessboard = new PointF((corners[31].X + corners[40].X) / 2, (corners[31].Y + corners[40].Y) / 2);
                        PointF centr_frame = new PointF(imgOriginal.Width / 2, imgOriginal.Height / 2);
                        CircleF circle = new CircleF(centr_chessboard, 4);
                        imgOriginal.Draw(circle, new Bgr(Color.Blue), -1);

                        #endregion Paint all extras on imgOriginal

                        #region Findig the drone location
                        Double Yd = RadianToDegree(EX_Param_now.RotationVector.Data[0, 0]);
                        Double Xd = RadianToDegree(EX_Param_now.RotationVector.Data[1, 0]);
                        Double Zd = RadianToDegree(EX_Param_now.RotationVector.Data[2, 0]);
                        WriteInConsole("X°=" + ((int)Xd).ToString() + ",Y°=" + ((int)Yd).ToString());


                        Double Y = EX_Param_now.RotationVector.Data[0, 0];
                        Double X = EX_Param_now.RotationVector.Data[1, 0];
                        Double Z = EX_Param_now.RotationVector.Data[2, 0];
                        double wpx, Wpx, fx, wsm, Z1, Z2;

                        wpx = new LineSegment2DF(new PointF((corners[0].X + corners[63].X) / 2, (corners[0].Y + corners[63].Y) / 2), new PointF((corners[8].X + corners[71].X) / 2, (corners[8].Y + corners[71].Y) / 2)).Length;
                        Wpx = wpx / Math.Cos(X);
                        fx = IC.IntrinsicMatrix[0, 0];
                        wsm = width * 6;
                        Z1 = (fx * wsm) / Wpx;
                        Z2 = Z1 + wsm * Math.Sin(X);
                        drone_point.z = (float)((Z1 + Z2) / 2 * 0.86);
                        drone_point.x = (float)(((centr_frame.X - centr_chessboard.X) * wsm) / Wpx * 0.85);

                        double hpx, Hpx, fy, hsm;
                        hsm = height * 6;
                        hpx = new LineSegment2DF(new PointF((corners[0].X + corners[8].X) / 2, (corners[0].Y + corners[8].Y) / 2), new PointF((corners[63].X + corners[71].X) / 2, (corners[63].Y + corners[71].Y) / 2)).Length;
                        Hpx = hpx / Math.Cos(Y);
                        fy = IC.IntrinsicMatrix[0, 0];
                        drone_point.y = (float)(-1 * ((centr_frame.Y - centr_chessboard.Y) * hsm) / Hpx);
                        WriteInConsole("X(sm)=" + ((int)drone_point.x).ToString() + ",Z(sm)=" + ((int)drone_point.z).ToString() + ",Y(sm)=" + ((int)drone_point.y).ToString());
                        #endregion

                        #region Pseudocode for PID controller
                        /*
                     * Pseudocode for PID controller. Is got from wikipedia
                     * Web page is <see href="http://en.wikipedia.org/wiki/PID_controller">HERE</see>
                     * 
                     * previous_error = 0
                     * integral = 0      
                     * 
                     * start:                    
                     * error = setpoint - measured_value                    
                     * integral = integral + error*dt                    
                     * derivative = (error - previous_error)/dt                    
                     * output = Kp*error + Ki*integral + Kd*derivative                    
                     * previous_error = error                    
                     * wait(dt)                    
                     * goto start
                     */
                        #endregion

                        _first_watch.Stop();               // Stop timing FPS
                        _sw_previus.Start();

                        int dt = (int)_first_watch.Elapsed.TotalMilliseconds + (int)_sw_previus.Elapsed.TotalMilliseconds;

                        label_FPS.Text = (Int32)(new TimeSpan(0, 0, 1).TotalMilliseconds / dt) + " FPS";       // Write FPS on label_FPS

                        if (followChessBoard.Checked && _takedOff)
                        {


                            _pitch = 0;
                            _roll = 0;
                            _gaz = 0;

                            #region Moving Forward and Back

                            float _error_forwardnback = drone_destination_point.z - drone_point.z;
                            float _previus_error_forwardnback = drone_last_errors.z;
                            if (Math.Abs(_error_forwardnback) <= _sample_error) { _error_forwardnback = 0; }
                            else
                            {
                                float _derivative_forwardnback = (_error_forwardnback - _previus_error_forwardnback) / dt;
                                _integral_forwardnback += dt * _error_forwardnback;

                                _pitch = _k_forwardnback_p * _error_forwardnback +
                                         _k_forwardnback_d * _derivative_forwardnback +
                                         _k_forwardnback_i * _integral_forwardnback;
                                drone_last_errors.z = _error_forwardnback;
                            }
                            #endregion

                            #region Moving Left and Right

                            float _error_leftnright = drone_destination_point.x - drone_point.x;
                            float _previus_error_leftnright = drone_last_errors.x;
                            if (Math.Abs(_error_leftnright) <= _sample_error) { _error_leftnright = 0; }
                            else
                            {
                                float _derivative_leftnright = (_error_leftnright - _previus_error_leftnright) / dt;
                                _integral_leftnright += dt * _error_leftnright;

                                _roll = _k_leftnright_p * _error_leftnright +
                                        _k_leftnright_d * _derivative_leftnright +
                                        _k_leftnright_i * _integral_leftnright;
                                drone_last_errors.x = _error_leftnright;
                            }
                            #endregion

                            #region Moving Up and Down

                            float _error_upndown = drone_destination_point.y - drone_point.y;
                            float _previus_error_upndown = drone_last_errors.y;
                            if (Math.Abs(_error_upndown) <= _sample_error) { _error_upndown = 0; }
                            else
                            {
                                float _derivative_upndown = (_error_upndown - _previus_error_upndown) / dt;
                                _integral_upndown += dt * _error_upndown;

                                _gaz = _k_upndown_p * _error_upndown +
                                       _k_upndown_d * _derivative_upndown +
                                       _k_upndown_i * _integral_upndown;
                                drone_last_errors.y = _error_upndown;
                            }
                            #endregion

                            

                            #region Make a packet, if recording
                            /*
                            if (_packetRecorderWorker != null)
                            {

                                DataPID dataPID = new DataPID();

                                dataPID._k_forwardnback_d = _k_forwardnback_d;
                                dataPID._k_forwardnback_i = _k_forwardnback_i;
                                dataPID._k_forwardnback_p = _k_forwardnback_p;

                                dataPID._k_leftnright_d = _k_leftnright_d;
                                dataPID._k_leftnright_i = _k_leftnright_i;
                                dataPID._k_leftnright_p = _k_leftnright_p;

                                dataPID._k_upndown_d = _k_upndown_d;
                                dataPID._k_upndown_i = _k_upndown_i;
                                dataPID._k_upndown_i = _k_upndown_p;

                                dataPID._sample_error = _sample_error;
                                dataPID._sample_length = _sample_length;


                                dataPID._integral_forwardnback = _integral_forwardnback;
                                dataPID._error_forwardnback = _error_forwardnback;
                                dataPID._derivative_forwardnback = _derivative_forwardnback;

                                dataPID._integral_leftnright = _integral_leftnright;
                                dataPID._error_leftnright = _error_leftnright;
                                dataPID._derivative_leftnright = _derivative_leftnright;

                                dataPID._integral_upndown = _integral_upndown;
                                dataPID._error_upndown = _error_upndown;
                                dataPID._derivative_upndown = _derivative_upndown;

                                dataPID._number_of_frame = _frameNumber;


                                _dataPIDs.Add(dataPID);
                            }
                             * */
                            #endregion

                            WriteInConsole("dT: " + dt + " LnR: " + _roll + " FnB: " + _pitch + " UnD: " + _gaz);

                            #region Send control, if it exist's, otherwise hover
                            if (_droneClient.NavigationData.State.HasFlag(NavigationState.Flying))
                            {
                                if ((_roll == 0) && (_pitch == 0) && (_gaz == 0))
                                {
                                    _droneClient.Hover();
                                }
                                else
                                {
                                    _droneClient.Progress(FlightMode.Progressive, roll: _roll, pitch: _pitch, yaw: 0.0f, gaz: _gaz);
                                }
                            }
                            #endregion
                        }
                    }

                }
                _sw_previus.Stop();

            }
            #endregion
            pbVideo1.Image = imgOriginal.ToBitmap(640, 360);
        }

        private void tmrStateUpdate_Tick(object sender, EventArgs e)
        {
            tvInfo.BeginUpdate();

            TreeNode node = tvInfo.Nodes.GetOrCreate("ClientActive");
            node.Text = string.Format("Client Active: {0}", _droneClient.IsActive);

            node = tvInfo.Nodes.GetOrCreate("Navigation Data");
            if (_navigationData != null) DumpBranch(node.Nodes, _navigationData);

            node = tvInfo.Nodes.GetOrCreate("Configuration");
            if (_settings != null) DumpBranch(node.Nodes, _settings);

            TreeNode vativeNode = tvInfo.Nodes.GetOrCreate("Native");

            NavdataBag navdataBag;
            if (_navigationPacket.Data != null && NavdataBagParser.TryParse(ref _navigationPacket, out navdataBag))
            {
                var ctrl_state = (CTRL_STATES)(navdataBag.demo.ctrl_state >> 0x10);
                node = vativeNode.Nodes.GetOrCreate("ctrl_state");
                node.Text = string.Format("Ctrl State: {0}", ctrl_state);

                var flying_state = (FLYING_STATES)(navdataBag.demo.ctrl_state & 0xffff);
                node = vativeNode.Nodes.GetOrCreate("flying_state");
                node.Text = string.Format("Ctrl State: {0}", flying_state);

                DumpBranch(vativeNode.Nodes, navdataBag);
            }
            tvInfo.EndUpdate();

            if (!_droneClient.IsActive && !label_BateryLevel.Text.Equals("Drone isn't connected!"))
            {
                label_BateryLevel.Text = "Drone isn't connected!";
                label_BateryLevel.ForeColor = Color.Black;
            }
            else
            {
                label_BateryLevel.Text = "Battery: " + _droneClient.NavigationData.Battery.Percentage+"%";
            }

            if (_droneClient.IsConnected && _droneClient.NavigationData.Battery.Percentage <= 20)
            {
                label_BateryLevel.ForeColor = Color.Red;
            }
            else
            {
                label_BateryLevel.ForeColor = Color.Black;
            }

            if (_autopilot != null && !_autopilot.Active && btnAutopilot.ForeColor != Color.Black)
                btnAutopilot.ForeColor = Color.Black;

         //   if (_follow_autopilot != null && !_follow_autopilot.Active && followChessBoard.Checked)
         //       followChessBoard.Checked = false;

            
        }

        private void DumpBranch(TreeNodeCollection nodes, object o)
        {
            Type type = o.GetType();

            foreach (FieldInfo fieldInfo in type.GetFields())
            {
                TreeNode node = nodes.GetOrCreate(fieldInfo.Name);
                object value = fieldInfo.GetValue(o);

                DumpValue(fieldInfo.FieldType, node, value);
            }

            foreach (PropertyInfo propertyInfo in type.GetProperties())
            {
                TreeNode node = nodes.GetOrCreate(propertyInfo.Name);
                object value = propertyInfo.GetValue(o, null);

                DumpValue(propertyInfo.PropertyType, node, value);
            }
        }

        private void DumpValue(Type type, TreeNode node, object value)
        {
            if (value == null)
                node.Text = node.Name + ": null";
            else
            {
                if (type.Namespace.StartsWith("System") || type.IsEnum)
                    node.Text = node.Name + ": " + value;
                else
                    DumpBranch(node.Nodes, value);
            }
        }

        private void btnFlatTrim_Click(object sender, EventArgs e)
        {
            _droneClient.FlatTrim();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _droneClient.Takeoff();
            _integral_forwardnback = 0;
            _integral_upndown = 0;
            _integral_leftnright = 0;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            _droneClient.Hover();
            _droneClient.Land();
            _takedOff = false;
        }

        private void btnEmergency_Click(object sender, EventArgs e)
        {
            _droneClient.Emergency();
            _takedOff = false;

        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            drone_last_errors = new MCvPoint3D32f(0, 0, 0);
        
            _integral_forwardnback = 0;
            _integral_upndown = 0;
            _integral_leftnright = 0;
        
            _integrals = new float[4, 10];
            _takedOff = false;
            _droneClient.ResetEmergency();
            

        }

        private void btnSwitchCam_Click(object sender, EventArgs e)
        {
            var configuration = new Settings();
            configuration.Video.Channel = VideoChannelType.Next;
            _droneClient.Send(configuration);
        }

        private void btnHover_Click(object sender, EventArgs e)
        {
            _droneClient.Hover();
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            _droneClient.Progress(FlightMode.Progressive, gaz: 0.25f);
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            _droneClient.Progress(FlightMode.Progressive, gaz: -0.25f);
        }

        private void btnTurnLeft_Click(object sender, EventArgs e)
        {
            _droneClient.Progress(FlightMode.Progressive, yaw: 0.25f);
        }

        private void btnTurnRight_Click(object sender, EventArgs e)
        {
            _droneClient.Progress(FlightMode.Progressive, yaw: -0.25f);
        }

        private void btnLeft_Click(object sender, EventArgs e)
        {
            _droneClient.Progress(FlightMode.Progressive, roll: -0.05f);
        }

        private void btnRight_Click(object sender, EventArgs e)
        {
            _droneClient.Progress(FlightMode.Progressive, roll: 0.05f);
        }

        private void btnForward_Click(object sender, EventArgs e)
        {
            _droneClient.Progress(FlightMode.Progressive, pitch: -0.05f);
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            _droneClient.Progress(FlightMode.Progressive, pitch: 0.05f);
        }

        private void btnReadConfig_Click(object sender, EventArgs e)
        {
            Task<Settings> configurationTask = _droneClient.GetConfigurationTask();
            configurationTask.ContinueWith(delegate(Task<Settings> task)
                {
                    if (task.Exception != null)
                    {
                        Trace.TraceWarning("Get configuration task is faulted with exception: {0}", task.Exception.InnerException.Message);
                        return;
                    }

                    _settings = task.Result;
                });
            configurationTask.Start();
        }

        private void btnSendConfig_Click(object sender, EventArgs e)
        {
            var sendConfigTask = new Task(() =>
                {
                    if (_settings == null) _settings = new Settings();
                    Settings settings = _settings;

                    if (string.IsNullOrEmpty(settings.Custom.SessionId) ||
                        settings.Custom.SessionId == "00000000")
                    {
                        // set new session, application and profile
                        _droneClient.AckControlAndWaitForConfirmation(); // wait for the control confirmation

                        settings.Custom.SessionId = Settings.NewId();
                        _droneClient.Send(settings);

                        _droneClient.AckControlAndWaitForConfirmation();

                        settings.Custom.ProfileId = Settings.NewId();
                        _droneClient.Send(settings);

                        _droneClient.AckControlAndWaitForConfirmation();

                        settings.Custom.ApplicationId = Settings.NewId();
                        _droneClient.Send(settings);

                        _droneClient.AckControlAndWaitForConfirmation();
                    }

                    settings.General.NavdataDemo = false;
                    settings.General.NavdataOptions = NavdataOptions.All;

                    settings.Video.BitrateCtrlMode = VideoBitrateControlMode.Dynamic;
                    settings.Video.Bitrate = 1000;
                    settings.Video.MaxBitrate = 2000;

                    //settings.Leds.LedAnimation = new LedAnimation(LedAnimationType.BlinkGreenRed, 2.0f, 2);
                    //settings.Control.FlightAnimation = new FlightAnimation(FlightAnimationType.Wave);

                    // record video to usb
                    //settings.Video.OnUsb = true;
                    // usage of MP4_360P_H264_720P codec is a requirement for video recording to usb
                    //settings.Video.Codec = VideoCodecType.MP4_360P_H264_720P;
                    // start
                    //settings.Userbox.Command = new UserboxCommand(UserboxCommandType.Start);
                    // stop
                    //settings.Userbox.Command = new UserboxCommand(UserboxCommandType.Stop);


                    //send all changes in one pice
                    _droneClient.Send(settings);
                });
            sendConfigTask.Start();
        }

        private void StopRecording()
        {
            if (_packetRecorderWorker != null)
            {
                _packetRecorderWorker.Stop();
                _packetRecorderWorker.Join();
                _packetRecorderWorker = null;

                string json = JsonConvert.SerializeObject(_dataPIDs);
                //WriteInConsole(json);
                StreamWriter sw = new StreamWriter(path_json); //located in /bin/debug
                sw.WriteLine(json);

                sw.Close();
            }
            if (_recorderStream != null)
            {
                _recorderStream.Dispose();
                _recorderStream = null;
            }
        }

        private void btnStartRecording_Click(object sender, EventArgs e)
        {
            string path = string.Format("flight_{0:yyyy_MM_dd_HH_mm}" + ARDroneTrackFileExt, DateTime.Now);

            using (var dialog = new SaveFileDialog { DefaultExt = ARDroneTrackFileExt, Filter = ARDroneTrackFilesFilter, FileName = path })
            {
                if (dialog.ShowDialog(this) == DialogResult.OK)
                {
                    StopRecording();
                    path_json = dialog.FileName + ".json";
                    _recorderStream = new FileStream(dialog.FileName, FileMode.OpenOrCreate);
                    _packetRecorderWorker = new PacketRecorder(_recorderStream);
                    _packetRecorderWorker.Start();
                    _StartframeNumber = _frameNumber;
                }
            }
        }

        private void btnStopRecording_Click(object sender, EventArgs e)
        {
            StopRecording();
        }

        private void btnReplay_Click(object sender, EventArgs e)
        {
            using (var dialog = new OpenFileDialog { DefaultExt = ARDroneTrackFileExt, Filter = ARDroneTrackFilesFilter })
            {
                if (dialog.ShowDialog(this) == DialogResult.OK)
                {
                    StopRecording();

                    var playerForm = new PlayerForm { FileName = dialog.FileName };
                    playerForm.Closed += (o, args) => _playerForms.Remove(o as PlayerForm);
                    _playerForms.Add(playerForm);
                    playerForm.Show(this);
                }
            }
        }

        // Make sure '_autopilot' variable is initialized with an object
        private void CreateAutopilot()
        {
            if (_autopilot != null) return;

            _autopilot = new Autopilot(_droneClient);
            _autopilot.OnOutOfObjectives += Autopilot_OnOutOfObjectives;
            _autopilot.BindToClient();
            _autopilot.Start();
        }
        
        // Event that occurs when no objectives are waiting in the autopilot queue
        private void Autopilot_OnOutOfObjectives()
        {
            _autopilot.Active = false;
        }
                    
        // Create a simple mission for autopilot
        private void CreateAutopilotMission()
        {
            _autopilot.ClearObjectives();

            // Do two 36 degrees turns left and right if the drone is already flying
            if (_droneClient.NavigationData.State.HasFlag(NavigationState.Flying))
            {
                const float turn = (float)(Math.PI / 5);
                float heading = _droneClient.NavigationData.Yaw;

                _autopilot.EnqueueObjective(Objective.Create(2000, new Heading(heading + turn, aCanBeObtained: true)));
                _autopilot.EnqueueObjective(Objective.Create(2000, new Heading(heading - turn, aCanBeObtained: true)));
                _autopilot.EnqueueObjective(Objective.Create(2000, new Heading(heading, aCanBeObtained: true)));
            }
            else // Just take off if the drone is on the ground
            {
                _autopilot.EnqueueObjective(new FlatTrim(1000));
                _autopilot.EnqueueObjective(new Takeoff(3500));
            }

            // One could use hover, but the method below, allows to gain/lose/maintain desired altitude
            _autopilot.EnqueueObjective(
                Objective.Create(3000,
                    new VelocityX(0.0f),
                    new VelocityY(0.0f),
                    new Altitude(1.0f)
                )
            );



            //_autopilot.EnqueueObjective(new Land(5000));
        }

        // Activate/deactive autopilot
        private void btnAutopilot_Click(object sender, EventArgs e)
        {
            if (!_droneClient.IsActive) return;

            CreateAutopilot();
            if (_autopilot.Active) _autopilot.Active = false;
            else
            {
                CreateAutopilotMission();
                _autopilot.Active = true;
                btnAutopilot.ForeColor = Color.Red;
            }
        }

        //  Checking status in CheckBox of Chessboard following changed
        private void followChessBoard_CheckedChanged(object sender, EventArgs e)
        {
                                                            
            if (!_droneClient.IsActive)                     // If we aren't connected (active)
            {
                followChessBoard.Checked = false;           // Uncheck the checkBox
                return;                                     // End function
            }
           
        }

        // This is the list of Hot-Keys in this program
        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
                                                                                    // This is the list of Hot-Keys in this program
                                                                                    // We use MainForm_KeyDown for listening button pushing
            if (e.KeyCode == Keys.Delete) btnEmergency_Click(sender, null);           // We use "DELETE" button for EMERGENCY
            if (e.KeyCode == Keys.NumPad8) btnForward_Click(sender, null);            // We use "NUM8" button for FORWARD
            if (e.KeyCode == Keys.NumPad2) btnBack_Click(sender, null);               // We use "NUM2" button for BACK              
            if (e.KeyCode == Keys.NumPad4) btnLeft_Click(sender, null);               // We use "NUM4" button for LEFT
            if (e.KeyCode == Keys.NumPad6) btnRight_Click(sender, null);              // We use "NUM6" button for RIGHT
            if (e.KeyCode == Keys.NumPad5) btnHover_Click(sender, null);              // We use "NUM5" button for HOVER
            if (e.KeyCode == Keys.NumPad7) btnTurnLeft_Click(sender, null);           // We use "NUM7" button for TURN_LEFT
            if (e.KeyCode == Keys.NumPad9) btnTurnRight_Click(sender, null);          // We use "NUM9" button for TURN_RIGHT
            if (e.KeyCode == Keys.Multiply) btnUp_Click(sender, null);                // We use "NUM_MULTIPLY" (*) button for UP
            if (e.KeyCode == Keys.Subtract) btnDown_Click(sender, null);              // We use "NUM_SUBSTRACT" (-) button for BACK
            if (e.KeyCode == Keys.Enter) button2_Click(sender, null);                 // We use "ENTER" button for TAKE_OFF
            if (e.KeyCode == Keys.Back) button3_Click(sender, null);                  // We use "BACKSPACE" button for LAND
        }

        // Writing something in console. Console - textBox_console
        private void WriteInConsole(String s)
        {
            if (textBox_Console.Text != "") textBox_Console.AppendText(Environment.NewLine);            // Unless first line - create a new line
            textBox_Console.AppendText(s);                                                              // Appending the String, that we receive
            textBox_Console.ScrollToCaret();                                                            // Scrolling to the new line
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                _k_forwardnback_p = float.Parse(textBox1.Text, System.Globalization.CultureInfo.InvariantCulture);
                _k_forwardnback_d = float.Parse(textBox6.Text, System.Globalization.CultureInfo.InvariantCulture);
                _k_forwardnback_i = float.Parse(textBox10.Text, System.Globalization.CultureInfo.InvariantCulture);

                _k_leftnright_p = float.Parse(textBox2.Text, System.Globalization.CultureInfo.InvariantCulture);
                _k_leftnright_d = float.Parse(textBox5.Text, System.Globalization.CultureInfo.InvariantCulture);
                _k_leftnright_i = float.Parse(textBox8.Text, System.Globalization.CultureInfo.InvariantCulture);

                _k_upndown_p = float.Parse(textBox3.Text, System.Globalization.CultureInfo.InvariantCulture);
                _k_upndown_d = float.Parse(textBox4.Text, System.Globalization.CultureInfo.InvariantCulture);
                _k_upndown_i = float.Parse(textBox7.Text, System.Globalization.CultureInfo.InvariantCulture);

                _sample_error = int.Parse(textBox11.Text, System.Globalization.CultureInfo.InvariantCulture);

                drone_destination_point.x = float.Parse(textBox9.Text, System.Globalization.CultureInfo.InvariantCulture);
                drone_destination_point.y = float.Parse(textBox13.Text, System.Globalization.CultureInfo.InvariantCulture);
                drone_destination_point.z = float.Parse(textBox12.Text, System.Globalization.CultureInfo.InvariantCulture);
            } catch(Exception exception){
                WriteInConsole("You cann't change k's:");
                WriteInConsole(exception.ToString());

            }
        
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox_Console.Text = "";
        }

        private void btnTakeFoto_Click(object sender, EventArgs e)
        {
            //imgOriginal.ToBitmap().Save(DateTime.Now.ToString("h:mm:ss tt") + ".bmp");
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                _calibration = true;
                button5.Visible = true;
                currentMode = Mode.SavingFrames;
            }
            else
            {
                _calibration = false;
                button5.Visible = false;
                currentMode = Mode.Calibrated;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            take_foto_points = true;
        }

        public static void WriteXML(IntrinsicCameraParameters ICP, String path)
        {
            System.Xml.Serialization.XmlSerializer writer = new System.Xml.Serialization.XmlSerializer(ICP.GetType());

            System.IO.StreamWriter file = new System.IO.StreamWriter(path);
            writer.Serialize(file, ICP);
            file.Close();
        }
        
        public IntrinsicCameraParameters ReadXML(String path)
        {
            System.Xml.Serialization.XmlSerializer reader =
                new System.Xml.Serialization.XmlSerializer(typeof(IntrinsicCameraParameters));
            System.IO.StreamReader file = new System.IO.StreamReader(path);
            IntrinsicCameraParameters ICP;
            ICP = (IntrinsicCameraParameters)reader.Deserialize(file);
            return ICP;
        }

        private double RadianToDegree(double angle)
        {
            return angle * (180.0 / Math.PI);
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click_1(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void textBox13_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
