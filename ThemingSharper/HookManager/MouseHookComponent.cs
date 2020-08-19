using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static AdvancedThemeManager.HookManager.Structure;

namespace AdvancedThemeManager.HookManager
{
    public partial class MouseHookComponent : Component
    {

        public static event EventHandler<MouseHookEventArgs> MouseClick;

        private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

        const int WH_MOUSE_LL = 14; 
        private LowLevelKeyboardProc _proc = hookProc;
        private static IntPtr hhook = IntPtr.Zero;

        /// <summary>
        /// Constructor
        /// </summary>
        public MouseHookComponent()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public MouseHookComponent(IContainer container)
        {
            container.Add(this);
            InitializeComponent();
        }

        /// <summary>
        /// Enable Hook
        /// </summary>
        public void EnableHook()
        {
            IntPtr hInstance = LoadLibrary("User32");
            hhook = SetWindowsHookEx(WH_MOUSE_LL, _proc, hInstance, 0);
        }

        /// <summary>
        /// Disable Hook
        /// </summary>
        public void DisableHook()
        {
            UnhookWindowsHookEx(hhook);
        }

        /// <summary>
        /// Hook procedur
        /// </summary>
        public static IntPtr hookProc(int code, IntPtr wParam, IntPtr lParam)
        {
            if (code >= 0 && (MouseMessages)wParam == MouseMessages.WM_LBUTTONDOWN)
            {
                MSLLHOOKSTRUCT hookStruct = (MSLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(MSLLHOOKSTRUCT));
                MouseClick?.Invoke(null,new MouseHookEventArgs(hookStruct.pt));
            }
            return CallNextHookEx(hhook, code, (int)wParam, lParam);
        }


        // ... { GLOBAL HOOK }
        [DllImport("user32.dll")]
        static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc callback, IntPtr hInstance, uint threadId);

        [DllImport("user32.dll")]
        static extern bool UnhookWindowsHookEx(IntPtr hInstance);

        [DllImport("user32.dll")]
        static extern IntPtr CallNextHookEx(IntPtr idHook, int nCode, int wParam, IntPtr lParam);

        [DllImport("kernel32.dll")]
        static extern IntPtr LoadLibrary(string lpFileName);


        public class MouseHookEventArgs: EventArgs
        {
            public POINT Point;

            public MouseHookEventArgs(POINT point)
            {
                this.Point = point;
            }
        }

    }
}
