﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
[assembly: Obfuscation(Feature = "script")]

namespace TestStructInit
{
    public  class Class1
    {
        void foo(System.Windows.Rect r, int x = 0, int y = 0, int width = 0, int height = 0)
        {
            var rr = new System.Windows.Rect();

            this.foo(
                new System.Windows.Rect
                {
                    X = x,
                    Y = y,
                    Width = width,
                    Height = height
                }
            );


            this.foo(
              new System.Windows.Rect
              {
                  X = x,
                  Y = y,
                  Width = width,
                  Height = height
              }
          );
        }
    }
}

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows
{
    [Script(Implements = typeof(global::System.Windows.Rect))]
    internal class __Rect
    {
        public double Height { get; set; }
        public double Width { get; set; }
        public double X { get; set; }
        public double Y { get; set; }

        public double Right { get { return this.X + this.Width; } }
        public double Bottom { get { return this.Y + this.Height; } }

        public __Rect()
        {

        }
    }
}