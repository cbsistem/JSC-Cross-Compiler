﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection.Emit;
using System.Threading;
using System.Net.Sockets;
using System.Net;

namespace ScriptCoreLib.CompilerServices
{
	public class PrimitiveServerBuilder
	{
		// classes in this namespace are to be used to build
		// alternative versions additionally to user written assemblies

		// types built within this assembly can reference this assembly for previously 
		// written functionality

		// also not that this assembly depends on ScriptCoreLib but not on ScriptCoreLibJava. 
		// if we were to build a server with java in mind we should add a reference to ScriptCoreLibJava
		// to do that we should be referencing ScriptCoreLibJava at some point... but not within this assembly.

		// this assembly shall support flash network providers like nonoba and kongregate

		// the functionality from jsc.server could also be included within this assembly
		
		// some functionality shall be refactored to ScriptCoreLib.Net.Server assembly
		// to enable java support via [Optimization("script")]
		public static string GetVersionInformation()
		{
			return "powered by ScriptCoreLib.Net (server for .net)";
		}

		class RemoteEndpointIdentity
		{
			public int Index;
			public Action<byte> Write;
		}
		
		public static Action StartRouter(Type t)
		{
			// .net only touter
			// java router - should use only BCL classes and Obfuscate(feature = "script")
			// Nonoba router
			// ActionScript peer router/host

			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.WriteLine("will start router: " + t.FullName);
			Console.ForegroundColor = ConsoleColor.Gray;

			var Clients = new List<RemoteEndpointIdentity>();

			var thread = DefaultPort.ToListener(
				s =>
				{
					Console.WriteLine("router: client connected");

					// we need a lock here
					// java needs to support ThreadMonitor.Enter
					var Identity = new RemoteEndpointIdentity { Write = s.WriteByte, Index = Clients.Count + 1 };

					foreach (var r in Clients)
					{
						Identity.Write((byte)'X');
					}

					Clients.Add(Identity);
					
					Identity.Write((byte)'H');

					while (true)
					{
						Thread.Sleep(1000);

						Identity.Write((byte)'.');
					}
				}
			);
			

			return delegate
			{
				// we ought to kill the active connections too
				thread.Abort();
			};
		}

		public const int DefaultPort = 33333;

		public static void ConnectToRouter()
		{
			var c = new TcpClient();

			c.Connect(IPAddress.Loopback, DefaultPort);

			var s = c.GetStream();

			0.AtDelay(
				delegate
				{
					while (true)
					{
						var instruction = s.ReadByte();

						Console.ForegroundColor = ConsoleColor.Cyan;
						Console.Write((char)instruction);
						Console.ForegroundColor = ConsoleColor.Gray;
					}
				}
			);
		}

		public static void EmitVersionInformation(ILGenerator il)
		{
			// kind of funny as this assembly will have parts where the IL will be compiled to target languages like actionscript
			// and also this assemlby contains code to build some of the IL which might end up being retranslated to that language

			Func<string> _GetVersionInformation = GetVersionInformation;

			var _a = il.DeclareLocal(typeof(string));

			il.EmitCall(OpCodes.Call, _GetVersionInformation.Method, null);
			il.Emit(OpCodes.Stloc, _a);
			il.EmitWriteLine(_a);
		}
	}
}
