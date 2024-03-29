﻿/*
 * Created by SharpDevelop.
 * User: jgustafson
 * Date: 3/23/2017
 * Time: 4:48 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace DeepFileFind
{
	/// <summary>
	/// Class with program entry point.
	/// </summary>
	internal sealed class Program
	{
		/// <summary>
		/// Program entry point.
		/// </summary>
		[STAThread]
		private static void Main(string[] args)
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			var process = Process.GetCurrentProcess(); // Or whatever method you are using
			string fullPath = process.MainModule.FileName;
			bool version_then_exit = false;
			for (int i=0; i<args.Length; i++) {
				string s = args[i];
				Debug.WriteLine("args["+i.ToString()+"]="+s);
				
				bool handled = false;
				/*
				try {
				}
				catch (Exception exn) {
					Debug.WriteLine("Could not finish checking for startup path:");
					Debug.WriteLine(exn.ToString());
				}
				*/

				if (s == "--version") {
					version_then_exit = true;
				}
				else if (fullPath.ToLower().EndsWith(s) || fullPath.ToLower().EndsWith(s+".exe")) {
					// args[0] is NOT the application in the CLR.
					Debug.WriteLine("Warning: unexpected value arg["+i.ToString()+"]=\""+s+"\"");
				}
				else {  // add .exe in case was run with Windows
					MainForm.startup_path = s;
					Debug.WriteLine("MainForm.startup_path=\""+s+"\"");
					handled = true;
				}
			} 
			MainForm mainform = new MainForm();
			mainform.version_then_exit = version_then_exit;
			Application.Run(mainform);
		}
		
	}
}
