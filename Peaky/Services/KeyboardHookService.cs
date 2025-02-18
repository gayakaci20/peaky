using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Input;

namespace Peaky.Services
{
    /// <summary>
    /// Provides low-level keyboard hook functionality to monitor keyboard events system-wide.
    /// </summary>
    public class KeyboardHookService : IDisposable
    {
        private const int WH_KEYBOARD_LL = 13;
        private const int WM_KEYDOWN = 0x0100;
        private const int WM_KEYUP = 0x0101;

        private readonly LowLevelKeyboardProc _proc;
        private IntPtr _hookId = IntPtr.Zero;
        private bool _isSpacebarDown;

        public event EventHandler? SpacebarPressed;

        public KeyboardHookService()
        {
            _proc = HookCallback;
            _hookId = SetHook(_proc);
        }

        /// <summary>
        /// Sets up the keyboard hook using the Windows API.
        /// </summary>
        /// <param name="proc">The callback procedure to handle keyboard events.</param>
        /// <returns>A handle to the keyboard hook.</returns>
        /// <exception cref="Win32Exception">Thrown when the hook cannot be set.</exception>
        private IntPtr SetHook(LowLevelKeyboardProc proc)
        {
            using var curProcess = Process.GetCurrentProcess();
            using var curModule = curProcess.MainModule;
            var hookHandle = SetWindowsHookEx(WH_KEYBOARD_LL, proc,
                GetModuleHandle(curModule?.ModuleName), 0);

            if (hookHandle == IntPtr.Zero)
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }

            return hookHandle;
        }

        private IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0)
            {
                var vkCode = Marshal.ReadInt32(lParam);
                var key = KeyInterop.KeyFromVirtualKey(vkCode);

                if (key == Key.Space)
                {
                    if (wParam == (IntPtr)WM_KEYDOWN && !_isSpacebarDown)
                    {
                        _isSpacebarDown = true;
                        SpacebarPressed?.Invoke(this, EventArgs.Empty);
                    }
                    else if (wParam == (IntPtr)WM_KEYUP)
                    {
                        _isSpacebarDown = false;
                    }
                }
            }

            return CallNextHookEx(_hookId, nCode, wParam, lParam);
        }

        public void Dispose()
        {
            if (_hookId != IntPtr.Zero)
            {
                UnhookWindowsHookEx(_hookId);
                _hookId = IntPtr.Zero;
            }
        }

        /// <summary>
        /// Delegate for the low-level keyboard hook callback function.
        /// </summary>
        private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

        /// <summary>
        /// Sets a hook procedure to monitor low-level keyboard input events.
        /// </summary>
        /// <param name="idHook">The type of hook procedure to install.</param>
        /// <param name="lpfn">The hook procedure.</param>
        /// <param name="hMod">A handle to the DLL containing the hook procedure.</param>
        /// <param name="dwThreadId">The identifier of the thread to associate with the hook.</param>
        /// <returns>A handle to the hook procedure if successful, or IntPtr.Zero otherwise.</returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn,
            IntPtr hMod, uint dwThreadId);

        /// <summary>
        /// Removes a previously installed hook procedure.
        /// </summary>
        /// <param name="hhk">A handle to the hook to be removed.</param>
        /// <returns>True if successful, false otherwise.</returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        /// <summary>
        /// Passes the hook information to the next hook procedure in the current hook chain.
        /// </summary>
        /// <param name="hhk">A handle to the current hook.</param>
        /// <param name="nCode">The hook code passed to the current hook procedure.</param>
        /// <param name="wParam">The wParam value passed to the current hook procedure.</param>
        /// <param name="lParam">The lParam value passed to the current hook procedure.</param>
        /// <returns>The value returned by the next hook procedure in the chain.</returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode,
            IntPtr wParam, IntPtr lParam);

        /// <summary>
        /// Retrieves a module handle for the specified module.
        /// </summary>
        /// <param name="lpModuleName">The name of the loaded module (*.dll or *.exe).</param>
        /// <returns>A handle to the specified module, or IntPtr.Zero if the module is not found.</returns>
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string? lpModuleName);
    }
}