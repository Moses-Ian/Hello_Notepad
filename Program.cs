using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;

namespace Hello_Notepad {
	
	class Program {
	
		// FLAGS
		static int SW_SHOWNORMAL = 1;
		static uint KEYEVENTF_KEYUP = 0x0002;
		static byte VK_CONTROL = 0x11;
		static byte VK_N = 0x4E;
		
		// MAIN PROGRAM
		static void Main(string[] args) {
			
			string processName = "Notepad";
			Process p = null;
			
			// Check whether notepad is already open
			Console.WriteLine("Check whether Notepad is already open...");
			if (Process.GetProcessesByName(processName).Length > 0) {
				Console.WriteLine("Notepad is running -> Bring it to the front.");
				
				p = Process.GetProcessesByName(processName)[0];
				
				// Bring up the window if it's minimized
				ShowWindow(p.MainWindowHandle, SW_SHOWNORMAL);
				// Bring it to the front
				SetForegroundWindow(p.MainWindowHandle.ToInt32());
				
			} else {
				Console.WriteLine("Notepad is NOT running. -> Launch it.");
				
				// if it's not, open it
				p = Process.Start(processName);
				
			}
			
			// wait
			p.WaitForInputIdle();
			
			// Click on File -> New, using the keyboard to type Ctrl+N
			Console.WriteLine("Type Ctrl+N to open a new file.");
			keybd_event(VK_CONTROL, 0x45, 0, 0);
			keybd_event(VK_N, 0x45, 0, 0);
			keybd_event(VK_CONTROL, 0x45, KEYEVENTF_KEYUP, 0);
			
		}
		
		// DLL IMPORTS
		[DllImport("user32.dll")]
		static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
		
		[DllImport("User32.dll")]
		public static extern Int32 SetForegroundWindow(int hWnd);
		
		[DllImport("user32.dll")]
		internal static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, uint dwExtraInfo);	
		
	}
}
