﻿using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptCoreLibJava.BCLImplementation.System.Threading.Tasks
{
    // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Threading\Tasks\TaskCompletionSource.cs
    // https://github.com/dot42/api/blob/master/System/Threading/Tasks/TaskCompletionSource.cs

    [Script(Implements = typeof(global::System.Threading.Tasks.TaskCompletionSource<>))]
    internal class __TaskCompletionSource<TResult>
    {
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201402/20140216/task




        public Task<TResult> Task { get; set; }

        public __TaskCompletionSource()
        {
            this.Task = new __Task<TResult>();
        }

        public void SetResult(TResult result)
        {
            //Console.WriteLine("enter TaskCompletionSource SetResult " + new { __Environment.CurrentManagedThreadId });
            var t = ((__Task<TResult>)this.Task);


            t.SetResult(result);
        }


        public override string ToString()
        {
            return new { this.Task.IsCompleted }.ToString();
        }
    }
}
