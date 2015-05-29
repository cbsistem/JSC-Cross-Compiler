﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Collections.Specialized;
using ScriptCoreLib.Shared.BCLImplementation.System.Collections.Specialized;

namespace ScriptCoreLibJava.BCLImplementation.System.Net
{
    // http://referencesource.microsoft.com/#System/net/System/Net/WebHeaderCollection.cs
    // https://github.com/mono/mono/blob/master/mcs/class/System/System.Net/WebHeaderCollection.cs
    // https://github.com/dot42/api/blob/master/System/Net/WebHeaderCollection.cs

    [Script(Implements = typeof(global::System.Net.WebHeaderCollection))]
    internal class __WebHeaderCollection : __NameValueCollection
    {

    }
}
