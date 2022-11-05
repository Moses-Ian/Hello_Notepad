using System;
using System.Diagnostics;

namespace Hello_Notepad {
	
	class Program {
	
		static void Main(string[] args) {
			
			string processName = "Notepad";
			
			// Check whether notepad is already open
			if (Process.GetProcessesByName(processName).Length > 0) {
				
				Console.WriteLine("Notepad is running.");
				
			} else {
				
				Console.WriteLine("Notepad is NOT running.");
				// if it's not, open it
				Process.Start(processName);
				
			}
			
			// click on File
			
			
			// click on New
			
			
			// these weren't mentioned in the challenge, but were mentioned by Ali Bakhta
			// type 'hello' in the window
			
			
			// save to desktop
			
			
		}
	}
}
