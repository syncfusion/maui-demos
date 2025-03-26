#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Microsoft.Maui.Controls;
using SampleBrowser.Maui.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleBrowser.Maui.Expander.SfExpander
{
	internal class SampleViewBehavior : Behavior<SampleView>
	{
		#region Fields

		private ScrollView? scrollView;

		private Grid? Layout;

		#endregion

		#region Overrides
		protected override void OnAttachedTo(SampleView bindable)
		{
			this.Layout = bindable.FindByName<Grid>("layout");
			this.scrollView = bindable.FindByName<ScrollView>("scrollView");
#if __MACCATALYST__
			this.Layout!.SizeChanged += this.StackLayout_SizeChanged;
#endif
			base.OnAttachedTo(bindable);
		}

		private void StackLayout_SizeChanged(object? sender, EventArgs e)
		{
			// ScrollView does not scroll unless window is resized.
			// Measure scroll view to update scroll view height when children's height changes dynamically.
			(this.scrollView as IView)!.InvalidateMeasure();
		}

		protected override void OnDetachingFrom(BindableObject bindable)
		{
#if __MACCATALYST__
			this.Layout!.SizeChanged -= this.StackLayout_SizeChanged;
#endif
			this.Layout = null;
			this.scrollView = null;
			base.OnDetachingFrom(bindable);
		}
		#endregion
	}
}
