using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JoyForX.UI
{
    public class UIData
    {
        public RCDataStruct RCData ;

        public RCDataStruct RC_KeepData;

        public short Front_Left ;
        public short Front_Right ;
        public short Rear_Left ;
        public short Rear_Right;

        public StringBuilder LogBuf ;

        public long ResultCount = 0;
        public bool IsOpened=false;

        public UIData()
        {
            LogBuf = new StringBuilder();
            init();
        }

        private void init()
        {
             RCData = new RCDataStruct();

             Front_Left = 1000;
             Front_Right = 1000;
             Rear_Left = 1000;
             Rear_Right = 1000;

             LogBuf = LogBuf.Clear();

             ResultCount = 0;
             IsOpened=false;
        }

        public void Clear()
        {
            init();
        }
    }
}
