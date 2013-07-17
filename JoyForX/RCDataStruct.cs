using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JoyForX.UI
{
    public class RCDataStruct
    {
        public short ROLL=1000;
        public short PITCH = 1000;
        public short YAW = 1000;
        public short THR = 1000;
        public short AUX1 = 1000;
        public short AUX2 = 1000;
        public short AUX3 = 1000;
        public short AUX4 = 1000;

        public List<byte> ToListBytes()
        {
            List<byte> bs=new List<byte>();
            bs.AddRange(BitConverter.GetBytes(ROLL));
            bs.AddRange(BitConverter.GetBytes(PITCH));
            bs.AddRange(BitConverter.GetBytes(YAW));
            bs.AddRange(BitConverter.GetBytes(THR));
            bs.AddRange(BitConverter.GetBytes(AUX1));
            bs.AddRange(BitConverter.GetBytes(AUX2));
            bs.AddRange(BitConverter.GetBytes(AUX3));
            bs.AddRange(BitConverter.GetBytes(AUX4));
            return bs;
        }

        public void FromListBytes(List<byte> d)
        {
            byte[] data = d.ToArray();
            if (data != null && data.Length == 16)
            {
                ROLL = BitConverter.ToInt16(data, 0);
                PITCH = BitConverter.ToInt16(data, 2);
                YAW = BitConverter.ToInt16(data, 4);
                THR = BitConverter.ToInt16(data, 6);
                AUX1 = BitConverter.ToInt16(data, 8);
                AUX2 = BitConverter.ToInt16(data, 10);
                AUX3 = BitConverter.ToInt16(data, 12);
                AUX4 = BitConverter.ToInt16(data, 14);
            }
            else throw new Exception("rc数据没有16个字节");

        }

        public static bool Compare(RCDataStruct a, RCDataStruct b)
        {
            if (a.PITCH == b.PITCH 
                && a.ROLL == b.ROLL 
                && a.THR == b.THR 
                && a.YAW == b.YAW 
                && a.AUX1 == b.AUX1 
                && a.AUX2 == b.AUX2
                && a.AUX3 == b.AUX3
                && a.AUX4 == b.AUX4)
                return true;

            else return false;

        }

    }

}
