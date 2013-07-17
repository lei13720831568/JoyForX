using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
using Microsoft.DirectX.DirectInput;

namespace JoyForX.UI
{


    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        public MsgProcess mp = new MsgProcess();

        System.Timers.Timer t =new System.Timers.Timer();

        //定时器定时读数
        public void TimeRead(object source,System.Timers.ElapsedEventArgs e)
        {
            t.Enabled = false;
            if (mp.sp.IsOpen)
            mp.SendMSPCmd(MsgProcess.MSP_RC, null);
            t.Enabled = true;
        }

        //打开端口
        private void btnOpenPort_Click(object sender, EventArgs e)
        {
            if (cbJoy.Items.Count == 0)
            {
                MessageBox.Show("先检查手柄");
                return;
            }

            cbPorts.Enabled = false;
            btnOpenPort.Enabled = false;
            cbRef.Enabled = false;
            this.lbResultCount.Text = "0";
            Application.DoEvents();
            mp.start(cbPorts.Text.Trim(), 115200, this, cbJoy.Text.Trim());

            //启动刷新
            t.Interval=int.Parse(this.cbRef.Text);
            t.AutoReset = true;
            
        }

        

        public delegate void UpdateUIDelegate(UIData data);

        public UpdateUIDelegate UpdateUId;

        //更新UI
        public void UpdateUI(UIData data)
        {
            this.txtLog.Text = data.LogBuf.ToString();

            if (data.IsOpened == false)
            {
                cbPorts.Enabled = true;
                btnOpenPort.Enabled = true;
                btnClosePort.Enabled = false;
                cbRef.Enabled = true;
                t.Stop();

                this.rc_Read.SetRCInputParameters(1000, 1500, 1500, 1500, 1500, 1500, 1500, 1500);
            }
            else
            {
                if (btnClosePort.Enabled == false) btnClosePort.Enabled = true;
                t.Start();
            }

            this.rc_Read.SetRCInputParameters(data.RCData.THR, data.RCData.PITCH, data.RCData.ROLL, data.RCData.YAW, data.RCData.AUX1, data.RCData.AUX2, data.RCData.AUX3, data.RCData.AUX4);
            this.lbResultCount.Text = data.ResultCount.ToString();

            if (mp.K_RC != null) 
            this.rc_keep.SetRCInputParameters(mp.K_RC.THR, mp.K_RC.PITCH, mp.K_RC.ROLL, mp.K_RC.YAW, mp.K_RC.AUX1, mp.K_RC.AUX2, mp.K_RC.AUX3, mp.K_RC.AUX4);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            mp.FromClose();
        }

        //关闭端口
        private void btnClosePort_Click(object sender, EventArgs e)
        {
            t.Enabled = false;
            mp.Close();
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            mp.SendMSPCmd(MsgProcess.MSP_RC, null);
        }

        public RCDataStruct rd = new RCDataStruct
        {
            ROLL = 1600,
            PITCH = 1600,
            THR = 1600,
            YAW = 1600,
            AUX1 = 1300,
            AUX2 = 1300,
            AUX3 = 1300,
            AUX4 = 1300
        };


        private void button4_Click(object sender, EventArgs e)
        {
            if (rd.ROLL < 1900)
                rd.ROLL = (short)(rd.ROLL + 100);
            if (rd.THR < 1900)
                rd.THR = (short)(rd.THR + 100);
            //lock (mp.K_RC)
            //{
            //    mp.K_RC.FromListBytes(rd.ToListBytes());
            //}
            mp.SendMSPCmd(MsgProcess.MSP_SET_RAW_RC, rd.ToListBytes());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (rd.ROLL > 1000)
                rd.ROLL = (short)(rd.ROLL - 100);
            if (rd.THR > 1000)
                rd.THR = (short)(rd.THR - 100);
            //lock (mp.K_RC)
            //{
            //    mp.K_RC.FromListBytes(rd.ToListBytes());
            //}
            mp.SendMSPCmd(MsgProcess.MSP_SET_RAW_RC, rd.ToListBytes());
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            string[] ports = SerialPort.GetPortNames();
          
            Array.Sort(ports);
            cbPorts.Items.AddRange(ports);
            if (cbPorts.Items.Count > 0) cbPorts.SelectedIndex = 0;
            UpdateUId = UpdateUI;
            t.Elapsed += new System.Timers.ElapsedEventHandler(TimeRead);
            this.cbRef.SelectedIndex = 0;
            this.lbResultCount.Text = "0";
            t.Enabled = false;



        }


        //选择手柄
        private void btnCheckJoy_Click(object sender, EventArgs e)
        {
            DeviceList dl = Manager.GetDevices(DeviceClass.GameControl, EnumDevicesFlags.AttachedOnly);
            if (dl != null)
            {
                cbJoy.Items.Clear();
                foreach (DeviceInstance info in dl)
                {
                    cbJoy.Items.Add(info.ProductName);
                }
                cbJoy.SelectedIndex = 0;
            }
        }


    }
}
