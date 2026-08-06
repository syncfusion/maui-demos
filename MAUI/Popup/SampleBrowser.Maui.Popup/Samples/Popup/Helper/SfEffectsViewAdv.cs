using Syncfusion.Maui.Core.Internals;
using Syncfusion.Maui.Core;
using PointerEventArgs = Syncfusion.Maui.Core.Internals.PointerEventArgs;

namespace SampleBrowser.Maui.Popup.SfPopup
{
    internal class SfEffectsViewAdv : SfEffectsView, ITouchListener
    {
        public SfEffectsViewAdv()
        {

        }

        public new void OnTouch(PointerEventArgs e)
        {
            if(DeviceInfo.Platform == DevicePlatform.Android)
            {
                return;
            }

            if (e.Action == PointerActions.Entered)
            {
                this.ApplyEffects(SfEffects.Highlight, RippleStartPosition.Default, new System.Drawing.Point((int)e.TouchPoint.X, (int)e.TouchPoint.Y), false);
            }
            else if (e.Action == PointerActions.Pressed)
            {
                this.ApplyEffects(SfEffects.Ripple, RippleStartPosition.Default, new System.Drawing.Point((int)e.TouchPoint.X, (int)e.TouchPoint.Y), false);
            }
            else if (e.Action == PointerActions.Released || e.Action == PointerActions.Cancelled || e.Action == PointerActions.Exited)
            {
                this.Reset();
            }
        }
    }
}
