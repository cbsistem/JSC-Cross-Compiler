﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using android.app;
using android.os;
using android.view;
using android.widget;
using ScriptCoreLib;
using ScriptCoreLib.Android.Extensions;

namespace HelloAndroidActivity.Activities
{
    public class ApplicationActivity : Activity
    {
        ScriptCoreLib.Android.IAssemblyReferenceToken ref1;


        protected override void onCreate(Bundle savedInstanceState)
        {
            base.onCreate(savedInstanceState);

            var sv = new ScrollView(this);
            var ll = new LinearLayout(this);
            //ll.setOrientation(LinearLayout.VERTICAL);
            sv.addView(ll);


            var b = new Button(this);
            b.setText((java.lang.CharSequence)(object)"I don't do anything, but I was added dynamically. :)");
            ll.addView(b);


            Action onclick = delegate
            {
                b.setText((java.lang.CharSequence)(object)"onclick");
            };

            b.setText((java.lang.CharSequence)(object)"before AtClick");
            b.AtClick(
                v =>
                {
                    b.setText((java.lang.CharSequence)(object)"AtClick");
                }
            );

            var b2 = new Button(this);
            b2.setText((java.lang.CharSequence)(object)"The other button!");
            ll.addView(b2);

            this.setContentView(sv);
        }

     
    }

   
}
