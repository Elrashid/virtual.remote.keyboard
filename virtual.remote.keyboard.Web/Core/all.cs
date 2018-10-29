using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WindowsInput;
using WindowsInput.Native;

namespace remote.keyboard.Web.Core
{
    public class all
    {


        public static void typing(string txt)
        {
            simulateTypingText(txt);
        }
        public static void select(int id)
        {
            BringMainWindowToFront(id);
        }


        //https://ourcodeworld.com/articles/read/520/simulating-keypress-in-the-right-way-using-inputsimulator-with-csharp-in-winforms
        static void simulateTypingText(string Text, int typingDelay = 100, int startDelay = 0)
        {
            InputSimulator sim = new InputSimulator();

            // Wait the start delay time
            sim.Keyboard.Sleep(startDelay);

            // Split the text in lines in case it has
            string[] lines = Text.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);

            // Some flags to calculate the percentage
            int maximum = lines.Length;
            int current = 1;

            foreach (string line in lines)
            {
                // Split line into characters
                char[] words = line.ToCharArray();

                // Simulate typing of the char i.e: a, e , i ,o ,u etc
                // Apply immediately the typing delay
                foreach (char word in words)
                {
                    sim.Keyboard.TextEntry(word).Sleep(typingDelay);
                }

                float percentage = ((float)current / (float)maximum) * 100;

                current++;

                // Add a new line by pressing ENTER
                // Return to start of the line in your editor with HOME
                sim.Keyboard.KeyPress(VirtualKeyCode.RETURN);
                sim.Keyboard.KeyPress(VirtualKeyCode.HOME);

                // Show the percentage in the console
                Console.WriteLine("Percent : {0}", percentage.ToString());
            }
        }



        //https://stackoverflow.com/a/2315589/4879683

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.Bool)]
        private static extern bool ShowWindow(IntPtr hWnd, ShowWindowEnum flags);

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern int SetForegroundWindow(IntPtr hwnd);

        private enum ShowWindowEnum
        {
            Hide = 0,
            ShowNormal = 1, ShowMinimized = 2, ShowMaximized = 3,
            Maximize = 3, ShowNormalNoActivate = 4, Show = 5,
            Minimize = 6, ShowMinNoActivate = 7, ShowNoActivate = 8,
            Restore = 9, ShowDefault = 10, ForceMinimized = 11
        };


        static void BringMainWindowToFront(int procesid)
        {
            // get the process
            Process bProcess = Process.GetProcessById(procesid);

            // check if the process is running
            if (bProcess != null)
            {
                // check if the window is hidden / minimized
                if (bProcess.MainWindowHandle == IntPtr.Zero)
                {
                    // the window is hidden so try to restore it before setting focus.
                    ShowWindow(bProcess.Handle, ShowWindowEnum.Restore);
                }

                // set user the focus to the window
                SetForegroundWindow(bProcess.MainWindowHandle);
            }

        }
    }
}
