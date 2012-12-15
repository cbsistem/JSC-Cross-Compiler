﻿using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System
{
    [Script]
    public static class CoreStringExtensions
    {
        public static IEnumerable<int> GetIndecies(this string e, string f)
        {
            var a = new List<int>();

            var p = 0;
            var i = e.IndexOf(f, p);
            while (i >= 0)
            {
                p = i + 1;

                a.Add(i);

                i = e.IndexOf(f, p);
            }

            return a;
        }

        internal static string InternalToString(this object e)
        {
            // dumb workaround for __ivec
            // Parse error: syntax error, unexpected '->' (T_OBJECT_OPERATOR) in
            // jsc should be the one to apply a workaround 

            return e.ToString();
        }
    }
}
