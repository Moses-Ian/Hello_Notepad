using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Hello_Notepad {
	
	class Program {
	
		static int LEFTDOWN = 0x00000002;
		static int LEFTUP =   0x00000004;
		
		static void Main(string[] args) {
			
			string processName = "Notepad";
			Process p = null;
			
			// Check whether notepad is already open
			if (Process.GetProcessesByName(processName).Length > 0) {
				
				Console.WriteLine("Notepad is running.");
				p = Process.GetProcessesByName(processName)[0];
				
				// should do something if it's minimized
				
			} else {
				
				Console.WriteLine("Notepad is NOT running.");
				// if it's not, open it
				p = Process.Start(processName);
				
			}
			
			// wait
			p.WaitForInputIdle();
			
			// get the window's location
			Rect location = new Rect();
			GetWindowRect(p.MainWindowHandle, ref location);
			// Console.WriteLine(location.Left);
			// Console.WriteLine(location.Top);

			// set the mouse position
			SetCursorPos(location.Left + 25, location.Top + 40);
			
			// click on File
			mouse_event(LEFTDOWN, 0, 0, 0, 0);
			mouse_event(LEFTUP, 0, 0, 0, 0);
			
			// click on New
			
			
			// these weren't mentioned in the challenge, but were mentioned by Ali Bakhta
			// type 'hello' in the window
			
			
			// save to desktop
			
			
		}
		
		public struct Rect {
			public int Left { get; set; }
			public int Top { get; set; }
			public int Right { get; set; }
			public int Bottom { get; set; }
		}
		
		[DllImport("user32.dll")]
		public static extern bool GetWindowRect(IntPtr hwnd, ref Rect rectangle);

		[System.Runtime.InteropServices.DllImport("user32.dll")]
		static extern bool SetCursorPos(int x, int y);

		[System.Runtime.InteropServices.DllImport("user32.dll")]
		public static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);
		
	}
}
