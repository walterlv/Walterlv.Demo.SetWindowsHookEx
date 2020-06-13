using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

using Lsj.Util.Win32;
using Lsj.Util.Win32.BaseTypes;
using Lsj.Util.Win32.Enums;

namespace Walterlv.Demo
{
    class HookDemo
    {
        private readonly User32.HOOKPROC _mouseHook;
        private IntPtr _hMouseHook;

        public HookDemo()
        {
            _mouseHook = OnMouseHook;
        }

        public void AddHook()
        {
            Console.WriteLine("添加钩子……");
            var hModule = Kernel32.GetModuleHandle(null);
            _hMouseHook = User32.SetWindowsHookEx(
                (int)WindowHookTypes.WH_MOUSE_LL,
                _mouseHook,
                hModule,
                0);
            if (_hMouseHook == IntPtr.Zero)
            {
                int errorCode = Marshal.GetLastWin32Error();
                Console.WriteLine($"添加钩子发生错误 {errorCode}");
                throw new Win32Exception(errorCode);
            }
            Console.WriteLine("添加钩子完成。");
        }

        private LRESULT OnMouseHook(int code, WPARAM wParam, LPARAM lParam)
        {
            return User32.CallNextHookEx(new IntPtr(0), code, wParam, lParam);
        }
    }
}
