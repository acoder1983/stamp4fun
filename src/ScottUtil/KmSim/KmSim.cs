using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Windows.Forms;

namespace KmSim
{
    
    public class Win32Api{
        [DllImport("user32.dll")]
        public static extern bool SetCursorPos(int X, int Y);

        [DllImport("user32.dll", EntryPoint = "mouse_event")]
        public static extern void mouse_event(
             int dwFlags,
             int dx,
             int dy,
             int cButtons,
             int dwExtraInfo
             );

        public const int MOUSEEVENTF_LEFTDOWN = 0x2;
        public const int MOUSEEVENTF_LEFTUP = 0x4;
    }

    public class IKmSim{
        public virtual void Do(int waitMilliSecs){
            if(waitMilliSecs>0){
                System.Threading.Thread.Sleep(waitMilliSecs);
            }
        }
    }

    public class Delay : IKmSim{
    }

    public class MouseMoveTo:IKmSim{
        private int x;
        private int y;

        public MouseMoveTo(int x,int y){
            this.x=x;
            this.y=y;
        }

        public override void Do(int waitMilliSecs){
            Win32Api.SetCursorPos(this.x,this.y);
            base.Do(waitMilliSecs);
        }
    }

    public class MouseLeftClick:IKmSim{
        public override void Do(int waitMilliSecs){
            Win32Api.mouse_event(Win32Api.MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
            Win32Api.mouse_event(Win32Api.MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
            base.Do(waitMilliSecs);
        }
    }

    public class MouseLeftUp:IKmSim{
        public override void Do(int waitMilliSecs){
            Win32Api.mouse_event(Win32Api.MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
            base.Do(waitMilliSecs);
        }
    }

    public class MouseLeftDown:IKmSim{
        public override void Do(int waitMilliSecs){
            Win32Api.mouse_event(Win32Api.MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
            base.Do(waitMilliSecs);
        }
    }

    public class MouseLeftDoubleClick:IKmSim{
        public override void Do(int waitMilliSecs){
            new MouseLeftClick().Do(0);
            new MouseLeftClick().Do(0);
            base.Do(waitMilliSecs);
        }
    }

    public class KeyPress:IKmSim{
        private string keys;
        public KeyPress(string keys){
            this.keys=keys;
        }

        public override void Do(int waitMilliSecs){
            SendKeys.SendWait(this.keys);
            base.Do(waitMilliSecs);
        }
    }
}
