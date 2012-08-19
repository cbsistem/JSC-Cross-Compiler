﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLibJava.BCLImplementation.System
{
	[Script(Implements = typeof(global::System.Int64),
		ImplementationType = typeof(java.lang.Long))]
	internal class __Int64
	{
		[Script(ExternalTarget = "parseLong")]
		public static long Parse(string e)
		{
			return default(long);
		}


        [Script(DefineAsStatic = true)]
        public string ToString(string format)
        {
            var value = (long)(object)this;


            return InternalToString(format, value);
        }

        internal static string InternalToString(string format, long value)
        {
            var s = new StringBuilder();

            if (format == "x8")
            {
                s.Append(ToHexString((byte)(value >> 0x18)));
                s.Append(ToHexString((byte)(value >> 0x10)));
                s.Append(ToHexString((byte)(value >> 8)));
                s.Append(ToHexString((byte)(value)));
            }
            else if (format == "x4")
            {
                s.Append(ToHexString((byte)(value >> 8)));
                s.Append(ToHexString((byte)(value)));
            }
            else if (format == "x2")
            {
                s.Append(ToHexString((byte)(value)));
            }

            return s.ToString();
        }

        public static string ToHexString(byte e)
        {
            const string u = "0123456789abcdef";

            return u.Substring((e >> 4) & 0xF, 1) + u.Substring((e >> 0) & 0xF, 1);
        }
	}
}
