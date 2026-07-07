using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Shapes;
using SampleBrowser.Maui.Base;

namespace SampleBrowser.Maui.AIAssistView.SfAIAssistView
{
    class DotAnimationBehavior : Behavior<Grid>
    {
        private Ellipse? dot1, dot2, dot3, dot4;
        private bool isAnimating = true;
        private readonly double animationDuration = 120;
        private Color DotDefaultColor = Application.Current!.UserAppTheme == AppTheme.Dark ? (Color)App.Current.Resources["FlyoutBackground"] : (Color)App.Current.Resources["FlyoutBackgroundLight"];
        private Color AnimationColor = Application.Current!.UserAppTheme == AppTheme.Dark ? (Color)App.Current.Resources["PrimaryBackgroundSelected"] : (Color)App.Current.Resources["PrimaryBackgroundSelectedLight"];

        protected override void OnAttachedTo(Grid bindable)
        {
            base.OnAttachedTo(bindable);

            // Initialize dots by finding them by their names
            dot1 = bindable.FindByName<Ellipse>("dot1");
            dot2 = bindable.FindByName<Ellipse>("dot2");
            dot3 = bindable.FindByName<Ellipse>("dot3");
            dot4 = bindable.FindByName<Ellipse>("dot4");

            this.StartAnimation();
        }

        protected override void OnDetachingFrom(Grid bindable)
        {
            // Stop animation and clear dot references
            isAnimating = false;
            dot1 = dot2 = dot3 = dot4 = null;
            base.OnDetachingFrom(bindable);
        }

        private async void StartAnimation()
        {
            while (isAnimating)
            {
                if (dot1 != null)
                {
                    await AnimateDot(dot1);
                }
                if (dot2 != null)
                {
                    await AnimateDot(dot2);
                }
                if (dot3 != null)
                {
                    await AnimateDot(dot3);
                }
                if (dot4 != null)
                {
                    await AnimateDot(dot4);
                }
            }
        }

        private async Task AnimateDot(Ellipse dot)
        {
            // Perform animation on the dot
            dot.Fill = new SolidColorBrush(AnimationColor);
            await dot.TranslateToAsync(0, -2, (uint)animationDuration);
            await dot.TranslateToAsync(0, 0, (uint)animationDuration);
            dot.Fill = new SolidColorBrush(DotDefaultColor);
        }
    }
}
