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

        public MotorsStruct MotorsData;

        public StringBuilder LogBuf ;

        public long ResultCount = 0;
        public bool IsOpened=false;

        public UIData()
        {
            LogBuf = new StringBuilder();
            MotorsData = new MotorsStruct();
            MotorsData.motors=new int[8];
            init();
        }

        private void init()
        {
             RCData = new RCDataStruct();

             LogBuf.Length = 0;

             ResultCount = 0;
             IsOpened=false;
        }

        public void Clear()
        {
            init();
        }
    }
}
