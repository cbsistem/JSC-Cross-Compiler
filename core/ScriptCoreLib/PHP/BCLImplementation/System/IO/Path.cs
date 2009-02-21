﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.PHP.BCLImplementation.System.IO
{
	[Script(Implements = typeof(global::System.IO.Path))]
	internal class __Path
	{
		public static string Combine(string path1, string path2)
		{
			return path1 + "/" + path2;
		}
	}
}
