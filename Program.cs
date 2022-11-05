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
			
			// get the window's location
			Console.WriteLine("Get the location of the window...");
			Rect location = new Rect();
			GetWindowRect(p.MainWindowHandle, ref location);
			// Console.WriteLine(location.Left);
			// Console.WriteLine(location.Top);

			// set the mouse position
			Console.WriteLine("Set the mouse over the File Menu Item...");
			SetCursorPos(location.Left + 25, location.Top + 40);
			
			// click on File
			Console.WriteLine("And click File.");
			mouse_event(LEFTDOWN, 0, 0, 0, 0);
			mouse_event(LEFTUP, 0, 0, 0, 0);
			
			// click on New
			Console.WriteLine("Set the mouse over the New Menu Item...");
			SetCursorPos(location.Left, location.Top + 60);
			Console.WriteLine("And click New.");
			mouse_event(LEFTDOWN, 0, 0, 0, 0);
			mouse_event(LEFTUP, 0, 0, 0, 0);
			
			
			// these weren't mentioned in the challenge, but were mentioned by Ali Bakhta
			// type 'hello' in the window
			
			
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
		
	}
}
