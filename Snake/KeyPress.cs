using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Snake
{
    static class KeyPress
    {
        [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto, ExactSpelling = true)]
        public static extern short GetAsyncKeyState(int vkey);

        public enum Key
        { //Enter,
          Space
        };
        public delegate void keyPress(Key Key);
        public static event keyPress OnKeyPressed;
        static Thread th = new Thread(x =>
        {
            while (true)
            {
                if (OnKeyPressed != null)
                {
                    //if (GetAsyncKeyState(0x0D) != 0)
                    //    OnKeyPressed(Key.Enter);

                    if (GetAsyncKeyState(0x20) != 0)
                        OnKeyPressed(Key.Space);
                }

                Thread.Sleep(150);
            }
        });

        public static void Start()
        {
            th.Start();
        }

        public static void Stop()
        {
            th.Abort();
        }
    }
}
