using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Threading;
using System.IO.Ports;
using System.Windows.Forms;
using Microsoft.DirectX.DirectInput;


namespace JoyForX.UI
{
    public class MsgData
    {
        public byte Len;
        public byte Code;
        public List<byte> Data;
        public byte CheckSum;

        public void SetMSPCmd(byte code,List<byte> data)
        {
            byte c = 0;
            if (data != null)
            {
                c ^= (byte)data.Count(); //数据长度也参与校验计算
                this.Len = (byte)data.Count();
            }
            else
            {
                c ^= 0;
                this.Len = 0;
            }
            

            c ^= code; //命令位
            if (data!=null)
            for (int i = 0; i < data.Count(); i++) c ^= data[i];   //计算数据位校验

            this.Code = code;
            if (data == null)
                this.Data = new List<byte>();
            else
                this.Data = data;
            
            this.CheckSum = c;
        }

        public bool Check()
        {
            byte c = 0;
            c^=Len; //数据长度也参与校验计算
            c^= Code; //命令位
            for (int i = 0; i < Data.Count; i++) c ^= Data[i];   //计算数据位校验
            if (CheckSum == c) //判断校验位是否正确   
            {
                return true;
            }
            else return false;
        }

        public void SetCustomData(byte code,string msg)
        {
            Code = code;
            Data=Encoding.UTF8.GetBytes(msg).ToList<byte>();
        }

        public string GetCustomData()
        {
            return Encoding.UTF8.GetString(Data.ToArray());
        }

        public string GetMSPIntData()
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in Data)
                sb.Append(b.ToString() + " ");
            return sb.ToString();
        }
        
    }

    public class MsgProcess
    {

        public const int MSP_IDENT = 100;
        public const int MSP_STATUS = 101;
        public const int MSP_RAW_IMU = 102;
        public const int MSP_SERVO = 103;
        public const int MSP_MOTOR = 104;
        public const int MSP_RC = 105;
        public const int MSP_RAW_GPS = 106;
        public const int MSP_COMP_GPS = 107;
        public const int MSP_ATTITUDE = 108;
        public const int MSP_ALTITUDE = 109;
        public const int MSP_BAT = 110;
        public const int MSP_RC_TUNING = 111;
        public const int MSP_PID = 112;
        public const int MSP_BOX = 113;
        public const int MSP_MISC = 114;
        public const int MSP_MOTOR_PINS = 115;
        public const int MSP_BOXNAMES = 116;
        public const int MSP_PIDNAMES = 117;
        public const int MSP_WP = 118;


        public const int MSP_SET_RAW_RC = 200;
        public const int MSP_SET_RAW_GPS = 201;
        public const int MSP_SET_PID = 202;
        public const int MSP_SET_BOX = 203;
        public const int MSP_SET_RC_TUNING = 204;
        public const int MSP_ACC_CALIBRATION = 205;
        public const int MSP_MAG_CALIBRATION = 206;
        public const int MSP_SET_MISC = 207;
        public const int MSP_RESET_CONF = 208;
        public const int MSP_SET_WP = 209;

        public const int MSP_EEPROM_WRITE = 250;
        public const int MSP_DEBUG = 254;


        const int C_Opened = 90;
        const int C_Closed = 91;
        const int C_Error = 92;


        public UIData UI_data;

        public  Queue SendQueue;
        public  Queue ReceiveQueue;

        public Thread SerialThread ;
        public Thread RecevicerThread;
        public Thread JoyThread;

        public bool isSerialStart=false;
        public bool isRecevicerStart = false;

        public SerialPort sp;

        public Exception LastException = null;

        public string MyJoyName;


        public MsgProcess()
        {
            SendQueue = Queue.Synchronized(new Queue());
            ReceiveQueue = Queue.Synchronized(new Queue());
            UI_data = new UIData();
            sp = new SerialPort();
            K_RC = null;
        }

        public MainForm UIForm;

        public void start(string portName, int baudRate, MainForm uiobj,string joy)
        {
            MyJoyName = joy;
            
            this.UIForm = uiobj;
            this.UI_data.Clear();
            SerialThread = new Thread(SerialWork);
            isSerialStart=true;

            RecevicerThread = new Thread(ReceiveWork);
            isRecevicerStart = true;

            JoyThread = new Thread(JoyWork);
            IsJoyStart = true;
           
            if (sp.IsOpen) sp.Close();
            if (sp.PortName != portName||sp.BaudRate!=baudRate)
            {
                sp.BaudRate = baudRate;
                sp.PortName = portName;
            }
            try
            {
                sp.Open();
                var msg=new MsgData();
                msg.SetCustomData(C_Opened,string.Format("PortName:{0},BaudRate{1},Opened OK",sp.PortName,sp.BaudRate));
                ReceiveQueue.Enqueue(msg);
            }
            catch (Exception ex)
            {
                var msg = new MsgData();
                msg.SetCustomData(C_Error, string.Format("Open Error msg:{0}", ex.Message));
                ReceiveQueue.Enqueue(msg);   
            }

            RecevicerThread.Start();
            SerialThread.Start();
            JoyThread.Start();
            
        }

        public void FromClose()
        {
           
            UIForm = null;
            
            isSerialStart = false;
           
        }

        public void Close()
        {
            isSerialStart = false;
        }

        public void SerialWork()  //串口工作线程
        {
            while (isSerialStart)
            {
                if (sp.IsOpen == false)
                {
                    var msg = new MsgData();
                    msg.SetCustomData(C_Closed, string.Format("SerialPort:{0} is closed", sp.PortName));
                    ReceiveQueue.Enqueue(msg);
                    isSerialStart = false;
                }

                if (SendQueue.Count == 0)
                    Thread.Sleep(50);

                if (SendQueue.Count > 0)
                {
                    ProcessSend(SendQueue.Dequeue() as MsgData);
                }

                if (sp.IsOpen)
                ReadSerial();
            }

            IsJoyStart = false;
            Thread.Sleep(1000);
            isRecevicerStart = false;
            if (sp.IsOpen)
            {
                sp.Close();
            }
            var closemsg = new MsgData();
            closemsg.SetCustomData(C_Closed, string.Format("SerialPort:{0} is closed. Thread Out.", sp.PortName));
            ProcessRecv(closemsg);
            
        }

        private List<byte> ResBuf = new List<byte>();

        private void ReceiveWork() //接收工作线程
        {
            while (isRecevicerStart)
            {
                if (ReceiveQueue.Count > 0)
                {
                    ProcessRecv(ReceiveQueue.Dequeue() as MsgData);
                }
                else Thread.Sleep(50);
            }
        }


        public RCDataStruct K_RC;
        public RCDataStruct K_RCLast;
        private bool IsJoyStart=false;
        private Device MyJoy;
        private void JoyWork() //手柄读取线程
        {
            MyJoy=IniyJoy();
            while (IsJoyStart)
            {
                if (K_RC != null)
                {
                    int joyTHR =  -(MyJoy.CurrentJoystickState.Y);
                    int joyYAW = (MyJoy.CurrentJoystickState.X);
                    int joyPITCH = -(MyJoy.CurrentJoystickState.Rz);
                    int joyROLL = (MyJoy.CurrentJoystickState.Z);
                    K_RC.THR = Linear(K_RC.THR, joyTHR,false);
                    K_RC.YAW = Linear(K_RC.YAW, joyYAW,true);
                    K_RC.PITCH = Linear(K_RC.PITCH, joyPITCH,true);
                    K_RC.ROLL = Linear(K_RC.ROLL, joyROLL,true);

                    if (!RCDataStruct.Compare(K_RC, K_RCLast))
                    {
                        K_RCLast = K_RC;
                        this.SendMSPCmd(MsgProcess.MSP_SET_RAW_RC, K_RC.ToListBytes());
                    }
                }
                Thread.Sleep(100);
            }
        }

        //线性累加计算 
        private short Linear(short oldvalue, int joyvalue,bool isMid)
        {
            short minvalue = 1000;
            short maxvalue = 2000;
            short result = oldvalue;
            int add=0;

           /// y=x*(x/5)+10  公式
            if (joyvalue>0)
             add = (joyvalue / 100) *(joyvalue / 100/5)+10;  //缩小累计量
            else
             add =-( (joyvalue / 100) * (joyvalue / 100/5)+10);  //缩小累计量 

            short addedvalue=(short)(oldvalue + add);
            result = addedvalue;
            if (joyvalue == 0)
            {
                if (isMid) result = 1500; else result = oldvalue;
            }
            if (addedvalue >= maxvalue) result = maxvalue;  //约束行程
            if (addedvalue <= minvalue) result = minvalue;  //约束行程

            return result;
        }
        //初始化手柄
        private Device IniyJoy()
        {

            DeviceList dl = Manager.GetDevices(DeviceClass.GameControl, EnumDevicesFlags.AttachedOnly);
            Device joy = null;
            foreach (DeviceInstance info in dl)
            {
                if (info.ProductName == this.MyJoyName)
                {
                    joy = new Device(info.InstanceGuid);
                   // myJoy.SetCooperativeLevel(this.UIForm, CooperativeLevelFlags.Background | CooperativeLevelFlags.NonExclusive);//设置手柄执行级别？？好像是力反馈什么的执行级别，暂时设置成这样
                    foreach (DeviceObjectInstance doi in joy.Objects)    //设置行程
                    {
                        if ((doi.ObjectId & (int)DeviceObjectTypeFlags.Axis) != 0)
                        {
                            joy.Properties.SetRange(ParameterHow.ById, doi.ObjectId, new InputRange(-1000, 1000));
                        }
                    }
                    joy.Acquire();
                    break;
                }
            }

            
            return joy;
        }  

        private void ReadSerial()
        {
            try
            {

                
                while (sp.BytesToRead>0)
                {
                    int n = sp.BytesToRead;
                    byte[] buf = new byte[n];
                    sp.Read(buf, 0, n);
                    ResBuf.AddRange(buf);

                    while (ResBuf.Count > 6)
                    {
                        if (ResBuf[0] == 36 && ResBuf[1] == 77 && ResBuf[2] == 62) //可能匹配到
                        {
                            if (ResBuf.Count > ResBuf[3] + 5)    //缓冲区内数据满足最小情况
                            {
                                //可能匹配到,检查校验位
                                MsgData msg = new MsgData();
                                msg.Len ^= ResBuf[3]; //数据长度也参与校验计算
                                msg.Code = ResBuf[4]; //命令位
                                msg.Data = ResBuf.GetRange(5, ResBuf[3]); //取出数据
                                msg.CheckSum = ResBuf[4 + ResBuf[3] + 1];
                                if (msg.Check())
                                {
                                    ReceiveQueue.Enqueue(msg);
                                    ResBuf.RemoveRange(0, 6 + msg.Len);
                                }
                                else
                                {
                                    ResBuf.RemoveRange(0, 3); //移除head
                                    int i = ResBuf.FindIndex(delegate(byte b) { return b == 36 ? true : false; });
                                    if (i != -1) ResBuf.RemoveRange(0, i); else ResBuf.Clear(); //移除下一个$之前的数据，如果没有$则全部删除
                                }

                            }
                        }
                        else
                        {
                            int i = ResBuf.FindIndex(delegate(byte b) { return b == 36 ? true : false; });
                            if (i != -1) ResBuf.RemoveRange(0, i); else ResBuf.Clear(); //移除下一个$之前的数据，如果没有$则全部删除
                        }
                    }
                }
                //else
                //    Thread.Sleep(50);
            }
            finally
            {
                //IsReceiving = false;
            }
        }

        public  void ProcessSend(MsgData data)
        {
            
            byte[] SendByte = new byte[data.Len + 6];
            SendByte[0] = (byte)'$';
            SendByte[1] = (byte)'M';
            SendByte[2] = (byte)'<';
            SendByte[3] = data.Len;
            SendByte[4] = data.Code;
            data.Data.CopyTo(SendByte, 5);
            SendByte[5 + data.Len] = data.CheckSum;

            if (sp.IsOpen)
            sp.Write(SendByte, 0, SendByte.Length);
            Thread.Sleep(20);
        }

        public void ProcessRecv(MsgData data)
        {
            switch (data.Code)
            {
                case C_Opened:
                    UI_data.IsOpened = true;
                    UI_data.LogBuf.Insert(0,data.GetCustomData()+Environment.NewLine);
                    break;
                case C_Closed:
                    UI_data.IsOpened = false;
                    UI_data.LogBuf.Insert(0,data.GetCustomData()+Environment.NewLine);
                    K_RC = null;  //置空当前保持的rc数据
                    break;
                case C_Error:
                    UI_data.LogBuf.Insert(0,data.GetCustomData()+Environment.NewLine);
                    break;
                case MSP_RC:
                    UI_data.RCData.FromListBytes(data.Data);

                    UI_data.LogBuf.Insert(0,string.Format("GET_RC Result:{0}"+Environment.NewLine, data.GetMSPIntData()));
                    if (K_RC == null) //首次检查读数
                    {
                        K_RC = new RCDataStruct();
                        K_RCLast = K_RC;
                        K_RC.FromListBytes(data.Data);
                        
                    }
                    break;
                case MSP_SET_RAW_RC:
                   // UI_data.LogBuf.Insert(0,string.Format("SET_RC Result:{0}" + Environment.NewLine, data.GetMSPIntData()));
                    break;
            }

            UI_data.ResultCount++;

            if (UIForm!=null)
            UIForm.Invoke(UIForm.UpdateUId, this.UI_data);
        }

        public void SendMSPCmd(byte code, List<byte> data)
        {
            MsgData newdata = new MsgData();
            newdata.SetMSPCmd(code, data);
            SendQueue.Enqueue(newdata);
        }
    }

    

}
