using ScriptCoreLib.JavaScript.BCLImplementation.System.Reflection;
using ScriptCoreLib.JavaScript.BCLImplementation.System.Runtime.CompilerServices;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.Shared.BCLImplementation.System;
using ScriptCoreLib.Shared.BCLImplementation.System.Runtime.CompilerServices;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Threading.Tasks
{
	public partial class __Task
	{
		public bool IsFaulted { get; set; }

		public AggregateException Exception { get; set; }

		public static Task FromException(Exception exception)
		{
			return FromException<object>(exception);
		}

		public static Task<TResult> FromException<TResult>(Exception exception)
		{
			// X:\jsc.svn\examples\java\async\test\TestFromException\TestFromException\Application.cs

			var x = new TaskCompletionSource<TResult>();
			x.SetException(exception);
			return x.Task;
		}


	}
}