using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;

namespace Hello_Notepad {
	
	class Program {
	
		// FLAGS
		// These are defined here in this way because this is a simple script. 
		static int SW_SHOWNORMAL = 1;
		static uint KEYEVENTF_KEYUP = 0x0002;
		static byte VK_RIGHT = 0x27;
		static byte VK_LEFT = 0x25;
		static byte VK_RETURN = 0x0D;
		static byte VK_BACK = 0x08;
		static byte VK_LSHIFT = 0xA0;
		static byte VK_CONTROL = 0x11;
		static byte VK_MENU = 0x12;	// ALT key
		static byte VK_OEM_MINUS = 0xBD;
		static byte VK_A = 0x41;
		static byte VK_E = 0x45;
		static byte VK_F = 0x46;
		static byte VK_H = 0x48;
		static byte VK_I = 0x49;
		static byte VK_L = 0x4C;
		static byte VK_M = 0x4D;
		static byte VK_N = 0x4E;
		static byte VK_O = 0x4F;
		
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
			
			// a new window might pop up that asks whether you want to save your data
			// we need to handle that situation
			// type RIGHT ENTER BACKSPACE
			// -> this will close the dialog box if it's open, and make no permanent changes if it isn't open

			// Close without saving (or do nothing significant)
			Console.WriteLine("Type RIGHT -> ENTER -> BACKSPACE");
			// Tap RIGHT
			keybd_event(VK_RIGHT, 0x45, 0, 0);
			// Tap ENTER
			keybd_event(VK_RETURN, 0x45, 0, 0);
			// Tap BACKSPACE
			keybd_event(VK_BACK, 0x45, 0, 0);

			// these weren't mentioned in the challenge, but were mentioned by Ali Bakhta
			// type 'Hello' in the window
			// this could be done with an array, but I'm just being explicit here
			Console.WriteLine("Type 'Hello'");
			keybd_event(VK_LSHIFT, 0x45, 0, 0);
			keybd_event(VK_H, 0x45, 0, 0);
			keybd_event(VK_LSHIFT, 0x45, KEYEVENTF_KEYUP, 0);
			keybd_event(VK_E, 0x45, 0, 0);
			keybd_event(VK_L, 0x45, 0, 0);
			keybd_event(VK_L, 0x45, 0, 0);
			keybd_event(VK_O, 0x45, 0, 0);
			
			// save to desktop
			// Click on File -> Save As, using the keyboard to type Alt+F -> Alt+A
			Console.WriteLine("Type Alt+F to open the File Menu.");
			keybd_event(VK_MENU, 0x45, 0, 0);
			keybd_event(VK_F, 0x45, 0, 0);
			// need to hold down the ALT key for this to work
			
			// wait
			Thread.Sleep(1000);

			Console.WriteLine("Type Alt+A to click Save As.");
			keybd_event(VK_A, 0x45, 0, 0);
			keybd_event(VK_MENU, 0x45, KEYEVENTF_KEYUP, 0);
			
			// wait
			Thread.Sleep(1000);

			// type 'Hello_IanM' -> ENTER
			Console.WriteLine("Type 'Hello_IanM'...");
			keybd_event(VK_LSHIFT, 0x45, 0, 0);
			keybd_event(VK_H, 0x45, 0, 0);
			keybd_event(VK_LSHIFT, 0x45, KEYEVENTF_KEYUP, 0);
			keybd_event(VK_E, 0x45, 0, 0);
			keybd_event(VK_L, 0x45, 0, 0);
			keybd_event(VK_L, 0x45, 0, 0);
			keybd_event(VK_O, 0x45, 0, 0);
			keybd_event(VK_LSHIFT, 0x45, 0, 0);
			keybd_event(VK_OEM_MINUS, 0x45, 0, 0);
			keybd_event(VK_I, 0x45, 0, 0);
			keybd_event(VK_LSHIFT, 0x45, KEYEVENTF_KEYUP, 0);
			keybd_event(VK_A, 0x45, 0, 0);
			keybd_event(VK_N, 0x45, 0, 0);
			keybd_event(VK_LSHIFT, 0x45, 0, 0);
			keybd_event(VK_M, 0x45, 0, 0);
			keybd_event(VK_LSHIFT, 0x45, KEYEVENTF_KEYUP, 0);
			Console.WriteLine("And type ENTER");
			keybd_event(VK_RETURN, 0x45, 0, 0);
			
			// sometimes, a dialog box appears asking if we want to overwrite
			// -> we need to handle it
			// -> we'll input LEFT ENTER
			// -> this has the side affect of modifying our text, but we'll close without saving
			Console.WriteLine("Close the dialog box that might appear.");
			keybd_event(VK_LEFT, 0x45, 0, 0);
			keybd_event(VK_RETURN, 0x45, 0, 0);
			
			// wait
			Thread.Sleep(1000);

			// close the window
			Console.WriteLine("Close the window.");
			p.Kill();
			
			Console.WriteLine("Finished.");
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
