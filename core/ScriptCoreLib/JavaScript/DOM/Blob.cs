﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.DOM
{
    [Script(HasNoPrototype = true, ExternalTarget = "Blob")]
    public class Blob
    {
        public readonly ulong size;

        public Blob()
        {

        }


        // http://stackoverflow.com/questions/19327749/javascript-blob-filename-without-link

        public Blob(string[] e)
        {

        }

        public Blob(string[] e, object args)
        {

        }
    }

    [Script]
    public static class BlobExtensions
    {
        public static string ToObjectURL(this Blob e)
        {
            // tested by
            // X:\jsc.svn\examples\javascript\ScriptDynamicSourceBuilder\ScriptDynamicSourceBuilder\Application.cs

            Console.WriteLine("ToObjectURL");

            // is jsc trying to inline without the line above?
            return URL.createObjectURL(e);
        }
    }
}
