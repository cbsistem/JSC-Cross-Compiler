﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.JavaScript.DOM.HTML;
using jsc.meta.Library;
using System.Reflection.Emit;
using System.Reflection;

namespace jsc.meta.Commands.Reference.ReferenceUltraSource
{
	partial class ReferenceUltraSource
	{
		// this should be part of the ScriptCoreLib

		// todo: we should actually scan the html elements for InternalConstructos and infer the type names!

		Dictionary<string, Type> InternalElementTypes;

		public Dictionary<string, Type> ElementTypes
		{
			get
			{

				if (InternalElementTypes == null)
				{
					var q =
						from t in typeof(IHTMLElement).GetSubTypesFromAssembly()
						let m = t.GetMethod("InternalConstructor", BindingFlags.Static | BindingFlags.NonPublic, null, new Type[0], null)
						where m != null
						let i = new ILBlock(m)
						from si in i.Instructrions

						from parameter in
							(si.OpCode == OpCodes.Newobj ? si.TargetConstructor.GetParameters() :
							 si.OpCode == OpCodes.Call ? si.TargetMethod.GetParameters() :
							 new ParameterInfo[0]
							)

						where parameter.ParameterType == typeof(IHTMLElement.HTMLElementEnum)

						// can we read out what param is being used?
						let si_ldc = si.StackBeforeStrict[parameter.Position]
						let iname = si_ldc.SingleStackInstruction.TargetInteger
						where iname != null
						let name = ((IHTMLElement.HTMLElementEnum)iname.Value).ToString()
						select new { t, name };

					InternalElementTypes = new Dictionary<string, Type>();
					foreach (var item in q)
					{
						// duplicates? probably a mistake at ScriptCoreLib!
						InternalElementTypes.Add(item.name, item.t);
					}

				}

				return InternalElementTypes;

			}
		}
		
	}
}
