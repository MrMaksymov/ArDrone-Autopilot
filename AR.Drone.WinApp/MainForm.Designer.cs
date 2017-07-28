namespace AR.Drone.WinApp
{
    partial class MainForm
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
            this.btnStart = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnFlatTrim = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.btnEmergency = new System.Windows.Forms.Button();
            this.tmrStateUpdate = new System.Windows.Forms.Timer(this.components);
            this.btnSwitchCam = new System.Windows.Forms.Button();
            this.btnUp = new System.Windows.Forms.Button();
            this.btnDown = new System.Windows.Forms.Button();
            this.btnLeft = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.btnRight = new System.Windows.Forms.Button();
            this.btnForward = new System.Windows.Forms.Button();
            this.btnTurnLeft = new System.Windows.Forms.Button();
            this.btnTurnRight = new System.Windows.Forms.Button();
            this.btnHover = new System.Windows.Forms.Button();
            this.tvInfo = new System.Windows.Forms.TreeView();
            this.tmrVideoUpdate = new System.Windows.Forms.Timer(this.components);
            this.btnReset = new System.Windows.Forms.Button();
            this.btnReadConfig = new System.Windows.Forms.Button();
            this.btnSendConfig = new System.Windows.Forms.Button();
            this.btnStartRecording = new System.Windows.Forms.Button();
            this.btnStopRecording = new System.Windows.Forms.Button();
            this.btnReplay = new System.Windows.Forms.Button();
            this.btnAutopilot = new System.Windows.Forms.Button();
            this.followChessBoard = new System.Windows.Forms.CheckBox();
            this.label_BateryLevel = new System.Windows.Forms.Label();
            this.label_DirectionFound = new System.Windows.Forms.Label();
            this.label_FPS = new System.Windows.Forms.Label();
            this.textBox_Console = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.textBox8 = new System.Windows.Forms.TextBox();
            this.textBox10 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox11 = new System.Windows.Forms.TextBox();
            this.button4 = new System.Windows.Forms.Button();
            this.btnTakeFoto = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.button5 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.textBox9 = new System.Windows.Forms.TextBox();
            this.textBox12 = new System.Windows.Forms.TextBox();
            this.textBox13 = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.pbVideo1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbVideo1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(12, 9);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "Activate";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(93, 9);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 23);
            this.btnStop.TabIndex = 1;
            this.btnStop.Text = "Deactivate";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnFlatTrim
            // 
            this.btnFlatTrim.Location = new System.Drawing.Point(262, 9);
            this.btnFlatTrim.Name = "btnFlatTrim";
            this.btnFlatTrim.Size = new System.Drawing.Size(75, 23);
            this.btnFlatTrim.TabIndex = 3;
            this.btnFlatTrim.Text = "Flat Trim";
            this.btnFlatTrim.UseVisualStyleBackColor = true;
            this.btnFlatTrim.Click += new System.EventHandler(this.btnFlatTrim_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(12, 404);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 4;
            this.button2.Text = "Takeoff";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(93, 404);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 5;
            this.button3.Text = "Land";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // btnEmergency
            // 
            this.btnEmergency.ForeColor = System.Drawing.Color.Red;
            this.btnEmergency.Location = new System.Drawing.Point(432, 9);
            this.btnEmergency.Name = "btnEmergency";
            this.btnEmergency.Size = new System.Drawing.Size(83, 23);
            this.btnEmergency.TabIndex = 6;
            this.btnEmergency.Text = "Emergency";
            this.btnEmergency.UseVisualStyleBackColor = true;
            this.btnEmergency.Click += new System.EventHandler(this.btnEmergency_Click);
            // 
            // tmrStateUpdate
            // 
            this.tmrStateUpdate.Interval = 500;
            this.tmrStateUpdate.Tick += new System.EventHandler(this.tmrStateUpdate_Tick);
            // 
            // btnSwitchCam
            // 
            this.btnSwitchCam.Location = new System.Drawing.Point(564, 405);
            this.btnSwitchCam.Name = "btnSwitchCam";
            this.btnSwitchCam.Size = new System.Drawing.Size(89, 23);
            this.btnSwitchCam.TabIndex = 8;
            this.btnSwitchCam.Text = "Video Channel";
            this.btnSwitchCam.UseVisualStyleBackColor = true;
            this.btnSwitchCam.Click += new System.EventHandler(this.btnSwitchCam_Click);
            // 
            // btnUp
            // 
            this.btnUp.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.btnUp.Location = new System.Drawing.Point(227, 504);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(75, 23);
            this.btnUp.TabIndex = 9;
            this.btnUp.Text = "Up";
            this.btnUp.UseVisualStyleBackColor = true;
            this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
            // 
            // btnDown
            // 
            this.btnDown.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.btnDown.Location = new System.Drawing.Point(389, 504);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(75, 22);
            this.btnDown.TabIndex = 10;
            this.btnDown.Text = "Down";
            this.btnDown.UseVisualStyleBackColor = true;
            this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
            // 
            // btnLeft
            // 
            this.btnLeft.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.btnLeft.Location = new System.Drawing.Point(227, 475);
            this.btnLeft.Name = "btnLeft";
            this.btnLeft.Size = new System.Drawing.Size(75, 23);
            this.btnLeft.TabIndex = 11;
            this.btnLeft.Text = "Left";
            this.btnLeft.UseVisualStyleBackColor = true;
            this.btnLeft.Click += new System.EventHandler(this.btnLeft_Click);
            // 
            // btnBack
            // 
            this.btnBack.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.btnBack.Location = new System.Drawing.Point(308, 504);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(75, 23);
            this.btnBack.TabIndex = 12;
            this.btnBack.Text = "Back";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // btnRight
            // 
            this.btnRight.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.btnRight.Location = new System.Drawing.Point(389, 475);
            this.btnRight.Name = "btnRight";
            this.btnRight.Size = new System.Drawing.Size(75, 23);
            this.btnRight.TabIndex = 13;
            this.btnRight.Text = "Right";
            this.btnRight.UseVisualStyleBackColor = true;
            this.btnRight.Click += new System.EventHandler(this.btnRight_Click);
            // 
            // btnForward
            // 
            this.btnForward.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.btnForward.Location = new System.Drawing.Point(308, 446);
            this.btnForward.Name = "btnForward";
            this.btnForward.Size = new System.Drawing.Size(75, 23);
            this.btnForward.TabIndex = 14;
            this.btnForward.Text = "Forward";
            this.btnForward.UseVisualStyleBackColor = true;
            this.btnForward.Click += new System.EventHandler(this.btnForward_Click);
            // 
            // btnTurnLeft
            // 
            this.btnTurnLeft.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.btnTurnLeft.Location = new System.Drawing.Point(227, 446);
            this.btnTurnLeft.Name = "btnTurnLeft";
            this.btnTurnLeft.Size = new System.Drawing.Size(75, 23);
            this.btnTurnLeft.TabIndex = 15;
            this.btnTurnLeft.Text = "Turn Left";
            this.btnTurnLeft.UseVisualStyleBackColor = true;
            this.btnTurnLeft.Click += new System.EventHandler(this.btnTurnLeft_Click);
            // 
            // btnTurnRight
            // 
            this.btnTurnRight.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.btnTurnRight.Location = new System.Drawing.Point(389, 446);
            this.btnTurnRight.Name = "btnTurnRight";
            this.btnTurnRight.Size = new System.Drawing.Size(75, 23);
            this.btnTurnRight.TabIndex = 16;
            this.btnTurnRight.Text = "Turn Right";
            this.btnTurnRight.UseVisualStyleBackColor = true;
            this.btnTurnRight.Click += new System.EventHandler(this.btnTurnRight_Click);
            // 
            // btnHover
            // 
            this.btnHover.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.btnHover.Location = new System.Drawing.Point(308, 475);
            this.btnHover.Name = "btnHover";
            this.btnHover.Size = new System.Drawing.Size(75, 23);
            this.btnHover.TabIndex = 17;
            this.btnHover.Text = "Hover";
            this.btnHover.UseVisualStyleBackColor = true;
            this.btnHover.Click += new System.EventHandler(this.btnHover_Click);
            // 
            // tvInfo
            // 
            this.tvInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tvInfo.Location = new System.Drawing.Point(667, 312);
            this.tvInfo.Name = "tvInfo";
            this.tvInfo.Size = new System.Drawing.Size(201, 375);
            this.tvInfo.TabIndex = 18;
            // 
            // tmrVideoUpdate
            // 
            this.tmrVideoUpdate.Interval = 30;
            this.tmrVideoUpdate.Tick += new System.EventHandler(this.tmrVideoUpdate_Tick);
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(343, 9);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(83, 23);
            this.btnReset.TabIndex = 19;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnReadConfig
            // 
            this.btnReadConfig.Location = new System.Drawing.Point(564, 476);
            this.btnReadConfig.Name = "btnReadConfig";
            this.btnReadConfig.Size = new System.Drawing.Size(89, 23);
            this.btnReadConfig.TabIndex = 20;
            this.btnReadConfig.Text = "Read Config";
            this.btnReadConfig.UseVisualStyleBackColor = true;
            this.btnReadConfig.Click += new System.EventHandler(this.btnReadConfig_Click);
            // 
            // btnSendConfig
            // 
            this.btnSendConfig.Location = new System.Drawing.Point(564, 505);
            this.btnSendConfig.Name = "btnSendConfig";
            this.btnSendConfig.Size = new System.Drawing.Size(89, 23);
            this.btnSendConfig.TabIndex = 21;
            this.btnSendConfig.Text = "Send Config";
            this.btnSendConfig.UseVisualStyleBackColor = true;
            this.btnSendConfig.Click += new System.EventHandler(this.btnSendConfig_Click);
            // 
            // btnStartRecording
            // 
            this.btnStartRecording.Location = new System.Drawing.Point(628, 9);
            this.btnStartRecording.Name = "btnStartRecording";
            this.btnStartRecording.Size = new System.Drawing.Size(75, 23);
            this.btnStartRecording.TabIndex = 22;
            this.btnStartRecording.Text = "Start Rec.";
            this.btnStartRecording.UseVisualStyleBackColor = true;
            this.btnStartRecording.Click += new System.EventHandler(this.btnStartRecording_Click);
            // 
            // btnStopRecording
            // 
            this.btnStopRecording.Location = new System.Drawing.Point(709, 9);
            this.btnStopRecording.Name = "btnStopRecording";
            this.btnStopRecording.Size = new System.Drawing.Size(75, 23);
            this.btnStopRecording.TabIndex = 23;
            this.btnStopRecording.Text = "Stop Rec.";
            this.btnStopRecording.UseVisualStyleBackColor = true;
            this.btnStopRecording.Click += new System.EventHandler(this.btnStopRecording_Click);
            // 
            // btnReplay
            // 
            this.btnReplay.Location = new System.Drawing.Point(790, 9);
            this.btnReplay.Name = "btnReplay";
            this.btnReplay.Size = new System.Drawing.Size(75, 23);
            this.btnReplay.TabIndex = 24;
            this.btnReplay.Text = "Replay";
            this.btnReplay.UseVisualStyleBackColor = true;
            this.btnReplay.Click += new System.EventHandler(this.btnReplay_Click);
            // 
            // btnAutopilot
            // 
            this.btnAutopilot.Location = new System.Drawing.Point(13, 548);
            this.btnAutopilot.Name = "btnAutopilot";
            this.btnAutopilot.Size = new System.Drawing.Size(75, 23);
            this.btnAutopilot.TabIndex = 25;
            this.btnAutopilot.Text = "Auto&pilot";
            this.btnAutopilot.UseVisualStyleBackColor = true;
            this.btnAutopilot.Click += new System.EventHandler(this.btnAutopilot_Click);
            // 
            // followChessBoard
            // 
            this.followChessBoard.AutoSize = true;
            this.followChessBoard.Location = new System.Drawing.Point(94, 553);
            this.followChessBoard.Name = "followChessBoard";
            this.followChessBoard.Size = new System.Drawing.Size(114, 17);
            this.followChessBoard.TabIndex = 29;
            this.followChessBoard.Text = "Follow chessboard";
            this.followChessBoard.UseVisualStyleBackColor = true;
            this.followChessBoard.CheckedChanged += new System.EventHandler(this.followChessBoard_CheckedChanged);
            // 
            // label_BateryLevel
            // 
            this.label_BateryLevel.AutoSize = true;
            this.label_BateryLevel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_BateryLevel.Location = new System.Drawing.Point(12, 433);
            this.label_BateryLevel.Name = "label_BateryLevel";
            this.label_BateryLevel.Size = new System.Drawing.Size(140, 16);
            this.label_BateryLevel.TabIndex = 30;
            this.label_BateryLevel.Text = "Drone isn\'t connected!";
            // 
            // label_DirectionFound
            // 
            this.label_DirectionFound.AutoSize = true;
            this.label_DirectionFound.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_DirectionFound.Location = new System.Drawing.Point(12, 462);
            this.label_DirectionFound.Name = "label_DirectionFound";
            this.label_DirectionFound.Size = new System.Drawing.Size(140, 16);
            this.label_DirectionFound.TabIndex = 31;
            this.label_DirectionFound.Text = "Drone isn\'t connected!";
            // 
            // label_FPS
            // 
            this.label_FPS.AutoSize = true;
            this.label_FPS.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label_FPS.Location = new System.Drawing.Point(517, 408);
            this.label_FPS.Name = "label_FPS";
            this.label_FPS.Size = new System.Drawing.Size(46, 17);
            this.label_FPS.TabIndex = 32;
            this.label_FPS.Text = "0 FPS";
            // 
            // textBox_Console
            // 
            this.textBox_Console.Location = new System.Drawing.Point(12, 577);
            this.textBox_Console.Multiline = true;
            this.textBox_Console.Name = "textBox_Console";
            this.textBox_Console.ReadOnly = true;
            this.textBox_Console.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox_Console.Size = new System.Drawing.Size(641, 111);
            this.textBox_Console.TabIndex = 34;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(691, 55);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(51, 20);
            this.textBox1.TabIndex = 38;
            this.textBox1.Text = "0.0005";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(754, 55);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(51, 20);
            this.textBox2.TabIndex = 39;
            this.textBox2.Text = "0.0005";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(814, 55);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(51, 20);
            this.textBox3.TabIndex = 40;
            this.textBox3.Text = "0.001";
            this.textBox3.TextChanged += new System.EventHandler(this.textBox3_TextChanged);
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(814, 107);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(51, 20);
            this.textBox4.TabIndex = 43;
            this.textBox4.Text = "0.003";
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(754, 107);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(51, 20);
            this.textBox5.TabIndex = 42;
            this.textBox5.Text = "0.003";
            this.textBox5.TextChanged += new System.EventHandler(this.textBox5_TextChanged);
            // 
            // textBox6
            // 
            this.textBox6.Location = new System.Drawing.Point(691, 107);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(51, 20);
            this.textBox6.TabIndex = 41;
            this.textBox6.Text = "0.003";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(704, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(25, 13);
            this.label1.TabIndex = 47;
            this.label1.Text = "F/B";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(765, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 13);
            this.label2.TabIndex = 48;
            this.label2.Text = "L/R";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(825, 37);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(28, 13);
            this.label3.TabIndex = 49;
            this.label3.Text = "U/D";
            // 
            // textBox7
            // 
            this.textBox7.Location = new System.Drawing.Point(814, 81);
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new System.Drawing.Size(51, 20);
            this.textBox7.TabIndex = 53;
            this.textBox7.Text = "0";
            // 
            // textBox8
            // 
            this.textBox8.Location = new System.Drawing.Point(754, 81);
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new System.Drawing.Size(51, 20);
            this.textBox8.TabIndex = 52;
            this.textBox8.Text = "0";
            // 
            // textBox10
            // 
            this.textBox10.Location = new System.Drawing.Point(691, 81);
            this.textBox10.Name = "textBox10";
            this.textBox10.Size = new System.Drawing.Size(51, 20);
            this.textBox10.TabIndex = 51;
            this.textBox10.Text = "0";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(664, 223);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(203, 23);
            this.button1.TabIndex = 54;
            this.button1.Text = "Accept k\'s";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(661, 136);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 13);
            this.label5.TabIndex = 55;
            this.label5.Text = "Sample error +- (sm)";
            this.label5.Click += new System.EventHandler(this.label5_Click_1);
            // 
            // textBox11
            // 
            this.textBox11.Location = new System.Drawing.Point(767, 133);
            this.textBox11.Name = "textBox11";
            this.textBox11.Size = new System.Drawing.Size(98, 20);
            this.textBox11.TabIndex = 56;
            this.textBox11.Text = "20";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(547, 551);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(106, 23);
            this.button4.TabIndex = 57;
            this.button4.Text = "Clean Console";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // btnTakeFoto
            // 
            this.btnTakeFoto.Location = new System.Drawing.Point(564, 431);
            this.btnTakeFoto.Name = "btnTakeFoto";
            this.btnTakeFoto.Size = new System.Drawing.Size(89, 23);
            this.btnTakeFoto.TabIndex = 58;
            this.btnTakeFoto.Text = "Take Foto";
            this.btnTakeFoto.UseVisualStyleBackColor = true;
            this.btnTakeFoto.Click += new System.EventHandler(this.btnTakeFoto_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(665, 252);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(99, 17);
            this.checkBox1.TabIndex = 59;
            this.checkBox1.Text = "Start calibration";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(665, 275);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(203, 31);
            this.button5.TabIndex = 60;
            this.button5.Text = "Save chessboard corners";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Visible = false;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(661, 110);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(20, 13);
            this.label4.TabIndex = 61;
            this.label4.Text = "Kd";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(661, 84);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(16, 13);
            this.label6.TabIndex = 62;
            this.label6.Text = "Ki";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(661, 58);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(20, 13);
            this.label7.TabIndex = 63;
            this.label7.Text = "Kp";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(662, 157);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(111, 13);
            this.label8.TabIndex = 64;
            this.label8.Text = "Destination point (sm):";
            this.label8.Click += new System.EventHandler(this.label8_Click);
            // 
            // textBox9
            // 
            this.textBox9.Location = new System.Drawing.Point(682, 196);
            this.textBox9.Name = "textBox9";
            this.textBox9.Size = new System.Drawing.Size(51, 20);
            this.textBox9.TabIndex = 65;
            this.textBox9.Text = "30";
            // 
            // textBox12
            // 
            this.textBox12.Location = new System.Drawing.Point(796, 196);
            this.textBox12.Name = "textBox12";
            this.textBox12.Size = new System.Drawing.Size(51, 20);
            this.textBox12.TabIndex = 66;
            this.textBox12.Text = "200";
            // 
            // textBox13
            // 
            this.textBox13.Location = new System.Drawing.Point(739, 196);
            this.textBox13.Name = "textBox13";
            this.textBox13.Size = new System.Drawing.Size(51, 20);
            this.textBox13.TabIndex = 67;
            this.textBox13.Text = "0";
            this.textBox13.TextChanged += new System.EventHandler(this.textBox13_TextChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(687, 176);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(35, 13);
            this.label9.TabIndex = 68;
            this.label9.Text = "X ←→";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(744, 176);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(27, 13);
            this.label10.TabIndex = 69;
            this.label10.Text = "Y ↑↓";
            this.label10.Click += new System.EventHandler(this.label10_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(801, 176);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(33, 13);
            this.label11.TabIndex = 70;
            this.label11.Text = "Z ↙↗";
            // 
            // pbVideo1
            // 
            this.pbVideo1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.pbVideo1.Location = new System.Drawing.Point(12, 38);
            this.pbVideo1.Name = "pbVideo1";
            this.pbVideo1.Size = new System.Drawing.Size(640, 360);
            this.pbVideo1.TabIndex = 2;
            this.pbVideo1.TabStop = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(870, 692);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.textBox13);
            this.Controls.Add(this.textBox12);
            this.Controls.Add(this.textBox9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.btnTakeFoto);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.textBox11);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox7);
            this.Controls.Add(this.textBox8);
            this.Controls.Add(this.textBox10);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.textBox5);
            this.Controls.Add(this.textBox6);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.textBox_Console);
            this.Controls.Add(this.label_FPS);
            this.Controls.Add(this.label_DirectionFound);
            this.Controls.Add(this.label_BateryLevel);
            this.Controls.Add(this.followChessBoard);
            this.Controls.Add(this.btnAutopilot);
            this.Controls.Add(this.btnReplay);
            this.Controls.Add(this.btnStopRecording);
            this.Controls.Add(this.btnStartRecording);
            this.Controls.Add(this.btnSendConfig);
            this.Controls.Add(this.btnReadConfig);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.tvInfo);
            this.Controls.Add(this.btnHover);
            this.Controls.Add(this.btnTurnRight);
            this.Controls.Add(this.btnTurnLeft);
            this.Controls.Add(this.btnForward);
            this.Controls.Add(this.btnRight);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.btnLeft);
            this.Controls.Add(this.btnDown);
            this.Controls.Add(this.btnUp);
            this.Controls.Add(this.btnSwitchCam);
            this.Controls.Add(this.btnEmergency);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btnFlatTrim);
            this.Controls.Add(this.pbVideo1);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnStart);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.KeyPreview = true;
            this.Name = "MainForm";
            this.Text = " ";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbVideo1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.PictureBox pbVideo1;
        private System.Windows.Forms.Button btnFlatTrim;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button btnEmergency;
        private System.Windows.Forms.Timer tmrStateUpdate;
        private System.Windows.Forms.Button btnSwitchCam;
        private System.Windows.Forms.Button btnUp;
        private System.Windows.Forms.Button btnDown;
        private System.Windows.Forms.Button btnLeft;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Button btnRight;
        private System.Windows.Forms.Button btnForward;
        private System.Windows.Forms.Button btnTurnLeft;
        private System.Windows.Forms.Button btnTurnRight;
        private System.Windows.Forms.Button btnHover;
        private System.Windows.Forms.TreeView tvInfo;
        private System.Windows.Forms.Timer tmrVideoUpdate;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnReadConfig;
        private System.Windows.Forms.Button btnSendConfig;
        private System.Windows.Forms.Button btnStartRecording;
        private System.Windows.Forms.Button btnStopRecording;
        private System.Windows.Forms.Button btnReplay;
        private System.Windows.Forms.Button btnAutopilot;
        private System.Windows.Forms.CheckBox followChessBoard;
        private System.Windows.Forms.Label label_BateryLevel;
        private System.Windows.Forms.Label label_DirectionFound;
        private System.Windows.Forms.Label label_FPS;
        private System.Windows.Forms.TextBox textBox_Console;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.TextBox textBox8;
        private System.Windows.Forms.TextBox textBox10;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox11;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button btnTakeFoto;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBox9;
        private System.Windows.Forms.TextBox textBox12;
        private System.Windows.Forms.TextBox textBox13;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
    }
}

