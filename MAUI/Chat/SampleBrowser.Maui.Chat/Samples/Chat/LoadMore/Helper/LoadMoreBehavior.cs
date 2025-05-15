#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.Base;
using Syncfusion.Maui.Chat;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleBrowser.Maui.Chat.SfChat
{
	public class LoadMoreBehavior : Behavior<SampleView>
	{
		#region Fields

		private LoadMoreViewModel? viewModel;
		private Syncfusion.Maui.Inputs.SfComboBox? comboBox;

		#endregion

		/// <summary>
		/// Method will be called when the view is attached to window
		/// </summary>
		/// <param name="bindable">SampleView type of bindable parameter</param>
		protected override void OnAttachedTo(SampleView bindable)
		{
			base.OnAttachedTo(bindable);
			this.viewModel = bindable.FindByName<LoadMoreViewModel>("viewModel");
			comboBox = bindable.FindByName<Syncfusion.Maui.Inputs.SfComboBox>("comboBox");
			comboBox.SelectionChanged += PositionChangedPicker_SelectionChanged;
		}


		/// <summary>
		/// Triggers when selected Index was changed, used to set a Load More Behavior
		/// </summary>
		/// <param name="sender">OnSelectionChanged event sender</param>
		/// <param name="e">EventArgs e</param>
		private void PositionChangedPicker_SelectionChanged(object? sender, Syncfusion.Maui.Inputs.SelectionChangedEventArgs e)
		{
			switch (this.comboBox!.SelectedIndex)
			{
				case 0:
					this.viewModel!.LoadMoreBehavior = Syncfusion.Maui.ListView.LoadMoreOption.Manual;
					break;
				case 1:
					this.viewModel!.LoadMoreBehavior = Syncfusion.Maui.ListView.LoadMoreOption.Auto;
					break;
			}
		}


		/// <summary>
		/// Method will be called when the view is detached from window
		/// </summary>
		/// <param name="bindable">SampleView type of bindable parameter</param>
		protected override void OnDetachingFrom(SampleView bindable)
		{
			this.viewModel = null;
			comboBox!.SelectionChanged -= PositionChangedPicker_SelectionChanged;
			comboBox = null;
			base.OnDetachingFrom(bindable);
		}
	}
}
