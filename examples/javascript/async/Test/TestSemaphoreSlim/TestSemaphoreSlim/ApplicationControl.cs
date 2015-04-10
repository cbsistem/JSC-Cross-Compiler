using TestSemaphoreSlim;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;

namespace TestSemaphoreSlim
{
	public partial class ApplicationControl : UserControl
	{
		public ApplicationControl()
		{
			this.InitializeComponent();
		}

		private void ApplicationControl_Load(object sender, System.EventArgs e)
		{



			Console.WriteLine("will spin up worker 1 and await for data");



			// https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201504/20150408
			var sInput = new SemaphoreSlim(0);
			var sOutput = new SemaphoreSlim(0);

			Task.Run(
				async delegate
				{
					Console.WriteLine("worker activated " + new { sInput, Thread.CurrentThread.ManagedThreadId });

					next:

					//830ms SemaphoreSlim.WaitAsync {{ InternalIsEntangled = true, ManagedThreadId = 10 }}
					//2015-04-08 16:33:44.579 :26849/view-source:50999 830ms worker  xSemaphoreSlim.InternalVirtualWaitAsync {{ Name = sInput }}

					await sInput.WaitAsync();

					Console.WriteLine("work complete " + new { Thread.CurrentThread.ManagedThreadId });

					//:29496/view-source:51197 2952ms work complete {{ ManagedThreadId = 10 }}
					//2015-04-08 17:25:48.849 :29496/view-source:51197 2952ms SemaphoreSlim.Release {{ InternalIsEntangled = true, ManagedThreadId = 10 }}
					//2015-04-08 17:25:48.849 :29496/view-source:51197 2952ms worker xSemaphoreSlim.InternalVirtualRelease, postMessage {{ Name = sOutput }}
					//2015-04-08 17:25:48.851 :29496/view-source:51197 2967ms worker Task Run ContinueWith {{ t = {{ IsCompleted = true, Result = null }} }}
					//2015-04-08 17:25:48.852 :29496/view-source:51197 2967ms Task ContinueWithResult {{ responseCounter = 20, ResultTypeIndex = null, Result = null }}
					//2015-04-08 17:25:48.853 :29496/view-source:51197 2967ms enter TaskExtensions.Unwrap Task<Task> ContinueWith

					sOutput.Release();
					// both threads continue working... unlike in a hop where only one logical thread continues..
					goto next;
				}
			);

			this.Click += async delegate
			{
				var sw = Stopwatch.StartNew();
				Console.WriteLine("will send data to the thread...");

				// signal the other thread
				sInput.Release();

				//view-source:50987 15989ms will send data to the thread...
				//2015-04-08 15:53:37.541 view-source:50987 15989ms SemaphoreSlim.Release {{ InternalIsEntangled = true, ManagedThreadId = 1 }}
				//2015-04-08 15:53:37.542 view-source:50987 15989ms xSemaphoreSlim.InternalVirtualRelease {{ MemberName = sInput }}
				//2015-04-08 15:53:37.544 view-source:50987 15989ms SemaphoreSlim.WaitAsync {{ InternalIsEntangled = true, ManagedThreadId = 1 }}
				//2015-04-08 15:53:37.545 view-source:50987 15989ms xSemaphoreSlim.InternalVirtualWaitAsync {{ MemberName = sOutput }}

				await sOutput.WaitAsync();

				Console.WriteLine("ui work complete... " + new { sw.ElapsedMilliseconds });

				// ui work complete... {{ ElapsedMilliseconds = 15 }}
			};
		}
	}
}

//---------------------------
//Microsoft Visual Studio
//---------------------------
//The following breakpoint cannot be set:

//At ApplicationControl.cs, line 42 character 6 ('TestSemaphoreSlim.ApplicationControl.ApplicationControl_Load(object sender, System.EventArgs e)')

//The breakpoint failed to bind.
//---------------------------
//OK   
//---------------------------