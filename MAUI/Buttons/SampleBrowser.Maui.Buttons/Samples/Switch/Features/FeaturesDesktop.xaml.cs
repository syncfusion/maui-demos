#region Copyright Syncfusion Inc. 2001-2023.
// Copyright Syncfusion Inc. 2001-2023. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.Base;
using Syncfusion.Maui.Buttons;

namespace SampleBrowser.Maui.Buttons.Switch;
public partial class FeaturesDesktop : SampleView
{
	public FeaturesDesktop()
	{
		InitializeComponent();
	}


    bool setState = false;
    bool setSolidState = false;

    private void SfSwitch_StateChanged(object sender, SwitchStateChangedEventArgs e)
    {
        if (this.OnStateLabel != null && this.OnStateSwitch != null)
        {
            this.OnStateLabel.Text = GetLabel(this.OnStateSwitch.IsOn);
            setStates(true, this.OnStateSwitch.IsOn);
        }
    }

    private void SfSwitch_StateChanged_1(object sender, SwitchStateChangedEventArgs e)
    {
        if (this.IndeterminateStateLabel != null && this.IndeterminateStateSwitch != null)
        {

            this.IndeterminateStateLabel.Text = GetLabel(this.IndeterminateStateSwitch.IsOn);
            setStates(null, this.IndeterminateStateSwitch.IsOn);
        }
    }

    private void SfSwitch_StateChanged_2(object sender, SwitchStateChangedEventArgs e)
    {
        if (this.OffStateLabel != null && this.OffStateSwitch != null)
        {
            this.OffStateLabel.Text = GetLabel(this.OffStateSwitch.IsOn);
            setStates(false, this.OffStateSwitch.IsOn);
        }
    }

    private string GetLabel(bool? value)
    {
        if (value == true)
            return "On";
        else if (value == false)
            return "Off";
        else
            return "Indeterminate";
    }

    private async void setStates(bool? value, bool? state)
    {
        if (setState)
            return;
        setState = true;

        if (value == true)
        {
            if (state == true)
            {
                this.IndeterminateStateSwitch.IsOn = null;
                this.OffStateSwitch.IsOn = false;
            }
            else if (state == false)
            {
                this.IndeterminateStateSwitch.IsOn = null;
                this.OffStateSwitch.IsOn = true;
            }
            else
            {
                this.IndeterminateStateSwitch.IsOn = false;
                this.OffStateSwitch.IsOn = true;
            }

        }
        else if (value == false)
        {
            if (state == true)
            {
                this.IndeterminateStateSwitch.IsOn = null;
                this.OnStateSwitch.IsOn = false;
            }
            else if (state == false)
            {
                this.IndeterminateStateSwitch.IsOn = null;
                this.OnStateSwitch.IsOn = true;
            }
            else
            {
                this.IndeterminateStateSwitch.IsOn = false;
                this.OnStateSwitch.IsOn = true;
            }
        }
        else
        {
            if (state == true)
            {
                this.OnStateSwitch.IsOn = null;
                this.OffStateSwitch.IsOn = false;
            }
            else if (state == false)
            {
                this.OnStateSwitch.IsOn = true;
                this.OffStateSwitch.IsOn = null;
            }
            else
            {
                this.OnStateSwitch.IsOn = true;
                this.OffStateSwitch.IsOn = false;
            }
        }

        await Task.Delay(100);
        setState = false;
    }

    private void SolidSfSwitch_StateChanged(object sender, SwitchStateChangedEventArgs e)
    {
        if (this.solidOnLabel != null && this.solidOnSwitch != null)
        {
            this.solidOnLabel.Text = GetSolidLabel(this.solidOnSwitch.IsOn);
            setSolidStates(true, this.solidOnSwitch.IsOn);
        }
    }

    private void SolidSfSwitch_StateChanged_1(object sender, SwitchStateChangedEventArgs e)
    {
        if (this.solidIndeterminateLabel != null && this.solidIndeterminateSwitch != null)
        {

            this.solidIndeterminateLabel.Text = GetSolidLabel(this.solidIndeterminateSwitch.IsOn);
            setSolidStates(null, this.solidIndeterminateSwitch.IsOn);
        }
    }

    private void SolidSfSwitch_StateChanged_2(object sender, SwitchStateChangedEventArgs e)
    {
        if (this.solidOffLabel != null && this.solidOffSwitch != null)
        {
            this.solidOffLabel.Text = GetSolidLabel(this.solidOffSwitch.IsOn);
            setSolidStates(false, this.solidOffSwitch.IsOn);
        }
    }

    private string GetSolidLabel(bool? value)
    {
        if (value == true)
            return "On";
        else if (value == false)
            return "Off";
        else
            return "Indeterminate";
    }

    private async void setSolidStates(bool? value, bool? state)
    {
        if (setSolidState)
            return;
        setSolidState = true;

        if (value == true)
        {
            if (state == true)
            {
                this.solidIndeterminateSwitch.IsOn = null;
                this.solidOffSwitch.IsOn = false;
            }
            else if (state == false)
            {
                this.solidIndeterminateSwitch.IsOn = null;
                this.solidOffSwitch.IsOn = true;
            }
            else
            {
                this.solidIndeterminateSwitch.IsOn = false;
                this.solidOffSwitch.IsOn = true;
            }

        }
        else if (value == false)
        {
            if (state == true)
            {
                this.solidIndeterminateSwitch.IsOn = null;
                this.solidOnSwitch.IsOn = false;
            }
            else if (state == false)
            {
                this.solidIndeterminateSwitch.IsOn = null;
                this.solidOnSwitch.IsOn = true;
            }
            else
            {
                this.solidIndeterminateSwitch.IsOn = false;
                this.solidOnSwitch.IsOn = true;
            }
        }
        else
        {
            if (state == true)
            {
                this.solidOnSwitch.IsOn = null;
                this.solidOffSwitch.IsOn = false;
            }
            else if (state == false)
            {
                this.solidOnSwitch.IsOn = true;
                this.solidOffSwitch.IsOn = null;
            }
            else
            {
                this.solidOnSwitch.IsOn = true;
                this.solidOffSwitch.IsOn = false;
            }
        }

        await Task.Delay(100);
        setSolidState = false;
    }
}