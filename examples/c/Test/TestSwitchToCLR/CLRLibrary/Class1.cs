﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLRLibrary
{
    public class Class1
    {
        // https://msdn.microsoft.com/en-us/library/fdy23fx6.aspx

        // .export [1] as 'Export2'

        // dllExport?

        // http://recxltd.blogspot.com/2012/05/forwarded-dll-exports-and-interesting.html
        // http://stackoverflow.com/questions/4060143/forwarding-data-in-a-dll
        // http://blogs.msdn.com/b/oldnewthing/archive/2012/11/16/10369061.aspx
        // https://msdn.microsoft.com/en-us/library/f5xe74x8(v=vs.110).aspx


        public static void Export2() => CLRLibraryCSharp.Class1.Export2();
        public static long Export4(long u) => CLRLibraryCSharp.Class1.Export4(u);
        public unsafe static long ExportPointer(global::CLRLibraryDllExportDefinition.uvec3ptr* p) => CLRLibraryCSharp.Class1.ExportPointer(p);
        public unsafe static long Export196(CLRLibraryDllExportDefinition.uvec3* u) => CLRLibraryCSharp.Class1.Export196(u);
        //public unsafe static long Export196(ref CLRLibraryDllExportDefinition.uvec3 u) => CLRLibraryCSharp.Class1.Export196(ref u);

        //Additional information: The runtime has encountered a fatal error.The address of the error was at 0x6f94af0f, on thread 0x23e4. The error code is 0xc0000005. This error may be a bug in the CLR or in the unsafe or non-verifiable portions of user code.Common sources of this bug include user marshaling errors for COM-interop or PInvoke, which may corrupt the stack.


        //Additional information: Could not load file or assembly 'CLRLibraryDllExportDefinition, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null' or one of its dependencies.The system cannot find the file specified.

    }
}