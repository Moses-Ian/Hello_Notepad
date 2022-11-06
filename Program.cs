using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;

namespace Hello_Notepad {
	
	class Program {
	
		// FLAGS
		static int LEFTDOWN = 0x00000002;
		static int LEFTUP =   0x00000004;
		static int SW_SHOWNORMAL = 1;
		static byte VK_RIGHT = 0x27;
		static byte VK_RETURN = 0x0D;
		static byte VK_BACK = 0x08;
		static byte VK_H = 0x48;
		static byte VK_E = 0x45;
		static byte VK_L = 0x4C;
		static byte VK_O = 0x4F;
		static byte VK_LSHIFT = 0xA0;
		static uint KEYEVENTF_KEYUP = 0x0002;
		
		// MAIN PROGRAM
		static void Main(string[] args) {
			
			string processName = "Notepad";
			Process p = null;
			
			// Check whether notepad is already open
			// Console.WriteLine("Check whether Notepad is already open...");
			if (Process.GetProcessesByName(processName).Length > 0) {
				// Console.WriteLine("Notepad is running -> Bring it to the front.");
				
				p = Process.GetProcessesByName(processName)[0];
				
				// Bring up the window if it's minimized
				ShowWindow(p.MainWindowHandle, SW_SHOWNORMAL);
				// Bring it to the front
				SetForegroundWindow(p.MainWindowHandle.ToInt32());
				
			} else {
				// Console.WriteLine("Notepad is NOT running. -> Launch it.");
				
				// if it's not, open it
				p = Process.Start(processName);
				
			}
			
			// wait
			p.WaitForInputIdle();
			
			// get the window's location
			// Console.WriteLine("Get the location of the window...");
			Rect location = new Rect();
			GetWindowRect(p.MainWindowHandle, ref location);
			// Console.WriteLine(location.Left);
			// Console.WriteLine(location.Top);

			// set the mouse position
			// Console.WriteLine("Set the mouse over the File Menu Item...");
			SetCursorPos(location.Left + 25, location.Top + 40);
			
			// click on File
			// Console.WriteLine("And click File.");
			mouse_event(LEFTDOWN, 0, 0, 0, 0);
			mouse_event(LEFTUP, 0, 0, 0, 0);
			
			// click on New
			// Console.WriteLine("Set the mouse over the New Menu Item...");
			SetCursorPos(location.Left, location.Top + 60);
			// Console.WriteLine("And click New.");
			mouse_event(LEFTDOWN, 0, 0, 0, 0);
			mouse_event(LEFTUP, 0, 0, 0, 0);
			
			// a new window might pop up that asks whether you want to save your data
			// we need to handle that situation
			// type RIGHT ENTER BACKSPACE
			// -> this will close the dialog box if it's open, and make no permanent changes if it isn't open

			// Close without saving (or do nothing significant)
			// Tap RIGHT
			keybd_event(VK_RIGHT, 0x45, 0, 0);
			// Tap ENTER
			keybd_event(VK_RETURN, 0x45, 0, 0);
			// Tap BACKSPACE
			keybd_event(VK_BACK, 0x45, 0, 0);

			// these weren't mentioned in the challenge, but were mentioned by Ali Bakhta
			// type 'hello' in the window
			// this could be done with an array, but I'm just being explicit here
			keybd_event(VK_LSHIFT, 0x45, 0, 0);
			keybd_event(VK_H, 0x45, 0, 0);
			keybd_event(VK_LSHIFT, 0x45, KEYEVENTF_KEYUP, 0);
			keybd_event(VK_E, 0x45, 0, 0);
			keybd_event(VK_L, 0x45, 0, 0);
			keybd_event(VK_L, 0x45, 0, 0);
			keybd_event(VK_O, 0x45, 0, 0);
			
			
			// save to desktop
			
			
		}
		
		// HELPFUL OBJECTS
		public struct Rect {
			public int Left { get; set; }
			public int Top { get; set; }
			public int Right { get; set; }
			public int Bottom { get; set; }
		}
		
		// DLL IMPORTS
		[DllImport("user32.dll")]
		public static extern bool GetWindowRect(IntPtr hwnd, ref Rect rectangle);

		[System.Runtime.InteropServices.DllImport("user32.dll")]
		static extern bool SetCursorPos(int x, int y);

		[System.Runtime.InteropServices.DllImport("user32.dll")]
		public static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);
		
		[DllImport("user32.dll")]
		static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
		
		[DllImport("User32.dll")]
		public static extern Int32 SetForegroundWindow(int hWnd);
		
		[DllImport("user32.dll")]
		internal static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, uint dwExtraInfo);		
	}
}
