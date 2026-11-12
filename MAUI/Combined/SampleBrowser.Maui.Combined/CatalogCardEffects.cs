using Syncfusion.Maui.Core;
using Syncfusion.Maui.Core.Internals;

namespace SampleBrowser.Maui.Combined;

// Catalog cards are the internal SfEffectsViewAdv. Its OnTouch method calls ApplyEffects
// directly on press, so TouchDownEffects has no effect and a swipe still animates.
// The listener is removed and a tap recognizer plays one ripple. Navigation stays on
// the recognizer the sample browser already added. Sample demos use SfEffectsView.
static class CatalogCardEffects
{
	static readonly HashSet<Element> Hooked = new();
	static readonly HashSet<SfEffectsView> Tuned = new();

	public static void Hook(Element element)
	{
		if (!Hooked.Add(element))
		{
			return;
		}

		if (element.GetType().Name == "SfEffectsViewAdv" && element is SfEffectsView effects)
		{
			Tune(effects);
		}

		element.ChildAdded += (_, args) => Hook(args.Element);

		foreach (var child in Children(element))
		{
			Hook(child);
		}
	}

	static void Tune(SfEffectsView effects)
	{
		if (!Tuned.Add(effects))
		{
			return;
		}

		effects.TouchDownEffects = SfEffects.None;
		effects.TouchUpEffects = SfEffects.None;
		DetachPressHandler(effects);
		effects.HandlerChanged += (_, _) => DetachPressHandler(effects);

		var tap = new TapGestureRecognizer();
		tap.Tapped += (_, _) =>
			effects.ApplyEffects(SfEffects.Ripple, RippleStartPosition.Default, null, false);
		effects.GestureRecognizers.Add(tap);
	}

	static void DetachPressHandler(SfEffectsView effects)
	{
		if (effects is ITouchListener touchListener)
		{
			effects.RemoveTouchListener(touchListener);
		}

		if (effects is IGestureListener gestureListener)
		{
			effects.RemoveGestureListener(gestureListener);
		}
	}

	static IEnumerable<Element> Children(Element element)
	{
		if (element is Layout layout)
		{
			foreach (var child in layout.Children)
			{
				if (child is Element childElement)
				{
					yield return childElement;
				}
			}
		}

		if (element is ContentView contentView && contentView.Content is Element content)
		{
			yield return content;
		}

		if (element is ScrollView scrollView && scrollView.Content is Element scrollContent)
		{
			yield return scrollContent;
		}

		if (element is Border border && border.Content is Element borderContent)
		{
			yield return borderContent;
		}

		if (element is NavigationPage navigationPage && navigationPage.CurrentPage is Element currentPage)
		{
			yield return currentPage;
		}

		if (element is IVisualTreeElement visual)
		{
			foreach (var child in visual.GetVisualChildren())
			{
				if (child is Element visualChild)
				{
					yield return visualChild;
				}
			}
		}
	}
}
