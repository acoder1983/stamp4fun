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

    public interface IKmSim{
        void Do();
    }

    public class Delay : IKmSim{
        private int millisecs;
        public Delay(int millisecs){
            this.millisecs=millisecs;
        }
        public void Do(){
            System.Threading.Thread.Sleep(this.millisecs);
        }
    }

    public class MouseMoveTo:IKmSim{
        private int x;
        private int y;

        public MouseMoveTo(int x,int y){
            this.x=x;
            this.y=y;
        }

        public void Do(){
            Win32Api.SetCursorPos(this.x,this.y);
        }
    }

    public class MouseLeftClick:IKmSim{
        public void Do(){
            Win32Api.mouse_event(Win32Api.MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
            Win32Api.mouse_event(Win32Api.MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
        }
    }

    public class MouseLeftUp:IKmSim{
        public void Do(){
            Win32Api.mouse_event(Win32Api.MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
        }
    }

    public class MouseLeftDown:IKmSim{
        public void Do(){
            Win32Api.mouse_event(Win32Api.MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
        }
    }

    public class MouseLeftDoubleClick:IKmSim{
        public void Do(){
            new MouseLeftClick().Do();
            new MouseLeftClick().Do();
        }
    }

    public class KeyPress:IKmSim{
        private string keys;
        public KeyPress(string keys){
            this.keys=keys;
        }

        public void Do(){
            SendKeys.SendWait(this.keys);
        }
    }
}
