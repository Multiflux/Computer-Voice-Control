using System;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.ComponentModel;
using System.Diagnostics;
using System.IO.IsolatedStorage;
using System.Runtime.CompilerServices;

namespace Plugin1
{
    /// <summary>
    /// Dies ist das Erste implementierte Plugin. Es Enthällt basis Funktionen wie sag "Hallo" und "Öffne google". 
    /// Die dazugehörigen Sprachbefehle befinden sich in der Plugin1 xml.
    /// </summary>


    public class Plugin : iPlugin.iPlugin
    {
        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(uint dwFlags, uint dx, uint dy, int cButtons, uint dwExtraInfo);

        [DllImport("user32.dll", SetLastError = true)]
        static extern void keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);

        private const int MOUSEEVENTF_LEFTDOWN = 0x02;
        private const int MOUSEEVENTF_LEFTUP = 0x04;
        private const int MOUSEEVENTF_RIGHTDOWN = 0x08;
        private const int MOUSEEVENTF_RIGHTUP = 0x10;
        private const int MOUSEEVENTF_WHEEL = 0x800;
        private const int MOUSEEVENTF_MIDDLEDOWN = 0x0020;
        private const int MOUSEEVENTF_MIDDLEUP = 0x0040;

        public const int KEYEVENTF_EXTENDEDKEY = 0x0001; //Key down flag
        public const int KEYEVENTF_KEYUP = 0x0002; //Key up flag
        public const int VK_LCONTROL = 0xA2; //Left Control key code
        public const int A = 0x41; //A key code
        public const int C = 0x43; //C key code
        public const int V = 0x56; //V key code
        public const int F4 = 0x73; //F4 key code
        public const int VK_RETURN = 0x0D;

        private const int SW_MAXIMIZE = 3;
        private const int SW_MINIMIZE = 6;
        private const int SW_RESTORE = 1;

        public string Name
        {
            get
            {
                return "Erste_Klasse";
            }
            set
            {
                ;
            }
        }

        public string Description
        {
            get
            {
                return "Das erste entwickelte Plugin!";
            }
            set
            {
                ;
            }
        }

        public override string ToString()
        {
            return "Plugin1.dll";
        }


        public void f1()
        {
            //Thread.Sleep(1000);
            Console.WriteLine("Hi there!");
            //MessageBox.Show("Hi there!");
        }

        public void currentTime()
        {
            //Thread.Sleep(1000);
            Console.WriteLine("The current time is: " + DateTime.Now.ToString());
            //MessageBox.Show("The current time is: " + DateTime.Now.ToString());
        }
        public void open_google()
        {
            System.Diagnostics.Process.Start("https://www.google.de/");
        }

        public void centerIt()
        {
            Cursor.Position = new System.Drawing.Point(Screen.PrimaryScreen.Bounds.Width / 2,
                            Screen.PrimaryScreen.Bounds.Height / 2);
            keybd_event(F4, 0, KEYEVENTF_EXTENDEDKEY, 0);
            keybd_event(F4, 0, KEYEVENTF_KEYUP, 0);
        }

        public void leftClick()
        {
            uint X = (uint)Cursor.Position.X;
            uint Y = (uint)Cursor.Position.Y;
            mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, X, Y, 0, 0);
        }

        public void rightClick()
        {
            uint X = (uint)Cursor.Position.X;
            uint Y = (uint)Cursor.Position.Y;
            mouse_event(MOUSEEVENTF_RIGHTDOWN | MOUSEEVENTF_RIGHTUP, X, Y, 0, 0);
        }

        public void doubleLeftClick()
        {
            uint X = (uint)Cursor.Position.X;
            uint Y = (uint)Cursor.Position.Y;
            mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, X, Y, 0, 0);
            Thread.Sleep(150);
            mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, X, Y, 0, 0);
        }

        public void startLeftClick()
        {
            uint X = (uint)Cursor.Position.X;
            uint Y = (uint)Cursor.Position.Y;
            mouse_event(MOUSEEVENTF_LEFTDOWN, X, Y, 0, 0);
        }

        public void endLeftClick()
        {
            uint X = (uint)Cursor.Position.X;
            uint Y = (uint)Cursor.Position.Y;
            mouse_event(MOUSEEVENTF_LEFTUP, X, Y, 0, 0);
        }


        public void scrollDown()
        {
            mouse_event(MOUSEEVENTF_WHEEL, 0, 0, -600, 0);
        }

        public void scrollUp()
        {
            mouse_event(MOUSEEVENTF_WHEEL, 0, 0, 600, 0);
        }

        public void startScrolling()
        {
            uint X = (uint)Cursor.Position.X;
            uint Y = (uint)Cursor.Position.Y;
            mouse_event(MOUSEEVENTF_MIDDLEDOWN, X, Y, 0, 0);
        }

        public void endScrolling()
        {
            uint X = (uint)Cursor.Position.X;
            uint Y = (uint)Cursor.Position.Y;
            mouse_event(MOUSEEVENTF_MIDDLEUP, X, Y, 0, 0);
        }

        public void copyToClip()
        {
            keybd_event(VK_LCONTROL, 0, KEYEVENTF_EXTENDEDKEY, 0);
            keybd_event(C, 0, KEYEVENTF_EXTENDEDKEY, 0);
            keybd_event(C, 0, KEYEVENTF_KEYUP, 0);
            keybd_event(VK_LCONTROL, 0, KEYEVENTF_KEYUP, 0);
        }

        //not sure how to implement this
        public void copyThatToClip()
        {

        }

        public void pasteHereFromClip()
        {
            leftClick();
            keybd_event(VK_LCONTROL, 0, KEYEVENTF_EXTENDEDKEY, 0);
            keybd_event(V, 0, KEYEVENTF_EXTENDEDKEY, 0);
            keybd_event(V, 0, KEYEVENTF_KEYUP, 0);
            keybd_event(VK_LCONTROL, 0, KEYEVENTF_KEYUP, 0);
        }

        public void enter()
        {
            keybd_event(VK_RETURN, 0, KEYEVENTF_EXTENDEDKEY, 0);
            keybd_event(VK_RETURN, 0, KEYEVENTF_KEYUP, 0);
        }

        public void minimizeCurrentWindow()
        {
            leftClick();
            Thread.Sleep(150);
            IntPtr handle = GetForegroundWindow();

            ShowWindow(handle, SW_MINIMIZE);
        }

        public void maximizeCurrentWindow()
        {
            leftClick();
            Thread.Sleep(150);
            IntPtr handle = GetForegroundWindow();

            ShowWindow(handle, SW_MAXIMIZE);
        }

        public void restoreDownCurrentWindow()
        {
            leftClick();
            Thread.Sleep(150);
            IntPtr handle = GetForegroundWindow();

            ShowWindow(handle, SW_RESTORE);
        }


        MouseWorker workerObject = null;
        Thread workerThread = null;

        public void startTracking()
        {
            workerObject = new MouseWorker();
            workerThread = new Thread(workerObject.DoWork);
            // Start the worker thread.
            workerThread.Start();
        }

        public void endTracking()
        {
            if (workerThread != null)
            {
                if (workerObject != null)
                {
                    workerObject.RequestStop();
                    workerThread.Join();
                    //workerThread = null;
                }
            }
        }

        private class MouseWorker
        {
            [DllImport(@"C:\Users\Chris\Desktop\Projekt_5.0\Plugin1\ReticleMMI.dll")]
            private static extern void Tracker();

            [DllImport(@"C:\Users\Chris\Desktop\Projekt_5.0\Plugin1\ReticleMMI.dll")]
            private static extern void Stop();

            // This method will be called when the thread is started.
            public void DoWork()
            {
                Tracker();

                ////SetCursorToCenter();

                //while (!_shouldStop)
                //{

                //}

                //Stop();

            }
            public void RequestStop()
            {
                Stop();
                _shouldStop = true;
            }
            // Volatile is used as hint to the compiler that this data
            // member will be accessed by multiple threads.
            private volatile bool _shouldStop;
        }
    }
}
