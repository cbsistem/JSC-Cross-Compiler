﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.PHP.BCLImplementation.System.Collections
{
    [Script(Implements = typeof(global::System.Collections.IComparer))]
    internal interface __IComparer
    {
        int Compare(object x, object y);

    }
}
