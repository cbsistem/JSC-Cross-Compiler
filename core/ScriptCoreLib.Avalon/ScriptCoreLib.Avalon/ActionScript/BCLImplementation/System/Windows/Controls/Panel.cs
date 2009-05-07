﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using ScriptCoreLib.ActionScript.BCLImplementation.System.Windows.Markup;
using ScriptCoreLib.ActionScript.flash.display;
using System.Windows.Media;
using ScriptCoreLib.ActionScript.BCLImplementation.System.Windows.Media;
using System.Windows;
using System.Windows.Input;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System.Windows.Controls
{
	[Script(Implements = typeof(global::System.Windows.Controls.Panel))]
	internal class __Panel : __FrameworkElement, __IAddChild
	{
		public readonly Sprite InternalSprite = new Sprite();

		public override InteractiveObject InternalGetDisplayObject()
		{
			return InternalSprite;
		}

		

		public __Panel()
		{
			_Children = new __UIElementCollection(this) { InternalSprite = InternalSprite };
		}

		public double InternalWidthValue = 200;
		public double InternalHeightValue = 200;

		public override double InternalGetHeight()
		{
			return InternalHeightValue;
		}

		public override double InternalGetWidth()
		{
			return InternalWidthValue;
		}

		public override void InternalSetHeight(double value)
		{
			InternalHeightValue = value;
			//InternalSprite.width = value;
			InternalUpdateBackground();
		}

		public override void InternalSetWidth(double value)
		{
			InternalWidthValue = value;
			//InternalSprite.height = value;
			InternalUpdateBackground();
		}

		public Brush InternalBackgroundValue;

		public Brush Background
		{
			get { throw new NotImplementedException(); }
			set
			{
				InternalBackgroundValue = value;

				InternalUpdateBackground();
			}
		}

		private void InternalUpdateBackground()
		{
			InternalSprite.graphics.clear();

			var AsSolidColorBrush = InternalBackgroundValue as SolidColorBrush;

			if (AsSolidColorBrush != null)
			{
				var _SolidColorBrush = (__SolidColorBrush)AsSolidColorBrush;
				uint _Color = (__Color)_SolidColorBrush.Color;

				InternalSprite.graphics.beginFill(_Color);
				InternalSprite.graphics.drawRect(0, 0, InternalWidthValue, InternalHeightValue);
			}
		}
		UIElementCollection _Children;

		public UIElementCollection Children
		{
			get
			{
				return _Children;
			}
		}


		public static implicit operator __Panel(Panel e)
		{
			return (__Panel)(object)e;
		}

		#region __IAddChild Members

		public void AddChild(object value)
		{
			var e = value as UIElement;

			if (e == null)
			{
				throw new NotSupportedException("AddChild supports UIElement");
			}

			this.Children.Add(e);
		}

		#endregion

		public static void SetZIndex(UIElement element, int value)
		{
			__UIElement _element = element;
			// this will only apply for 3d...
			// we need to emulate zindex
			_element.InternalGetDisplayObject().z = value;
		}
	}
}
