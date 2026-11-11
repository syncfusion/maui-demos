using System.Reflection;
using System.Xml.Linq;
using SampleBrowser.Maui.Base;

namespace SampleBrowser.Maui.Combined;

// Loads every sample list into BaseConfig before MainPageInit.
// MainPageInit tags every sample with the single assembly it is passed, so AssignAssemblies
// retargets each model afterward. Assemblies are loaded from the app directory because the
// host project never mentions their types, so they are not assembly references.
// SmartPdfViewer and PdfViewer both use the control key "PdfViewer"; the second key is the assembly name.
static class SampleCatalog
{
	static readonly Dictionary<string, Assembly> Assemblies = new(StringComparer.Ordinal);

	public static void Load()
	{
		var directory = Path.GetDirectoryName(typeof(SampleCatalog).Assembly.Location);
		if (string.IsNullOrEmpty(directory))
		{
			return;
		}

		foreach (var file in Directory.GetFiles(directory, "SampleBrowser.Maui.*.dll"))
		{
			var fileName = Path.GetFileName(file);
			if (fileName is "SampleBrowser.Maui.Base.dll" or "SampleBrowser.Maui.Combined.dll")
			{
				continue;
			}

			var assembly = Assembly.LoadFrom(file);
			using var controlList = OpenResource(assembly, ".ControlList.xml");
			if (controlList == null)
			{
				continue;
			}

			var document = XDocument.Load(controlList);
			foreach (var element in document.Descendants("Assembly"))
			{
				var shortName = element.Attribute("Name")?.Value;
				if (string.IsNullOrEmpty(shortName))
				{
					continue;
				}

				var samples = OpenResource(assembly, "." + shortName + "SamplesList.xml")
					?? OpenSingleResource(assembly, "SamplesList.xml");
				if (samples == null)
				{
					continue;
				}

				var key = shortName;
				if (BaseConfig.AvailableControlStreamList.ContainsKey(key))
				{
					key = assembly.GetName().Name?.Replace("SampleBrowser.Maui.", "", StringComparison.Ordinal) ?? shortName;
				}

				BaseConfig.AvailableControlStreamList[key] = samples;
				Assemblies[key] = assembly;
			}
		}
	}

	public static void AssignAssemblies(object? node)
	{
		var seen = new HashSet<object>(ReferenceEqualityComparer.Instance);
		Visit(node, seen);
	}

	static void Visit(object? node, HashSet<object> seen)
	{
		if (node == null || node is string || !seen.Add(node))
		{
			return;
		}

		var type = node.GetType();
		var shortName = type.GetProperty("ControlShortName")?.GetValue(node) as string;
		var assemblyProperty = type.GetProperty("AssemblyName");
		if (shortName != null
			&& assemblyProperty?.PropertyType == typeof(Assembly)
			&& assemblyProperty.CanWrite
			&& Assemblies.TryGetValue(shortName, out var assembly))
		{
			assemblyProperty.SetValue(node, assembly);
		}

		foreach (var property in type.GetProperties())
		{
			if (property.GetIndexParameters().Length > 0
				|| property.GetMethod == null
				|| property.DeclaringType?.Namespace?.StartsWith("SampleBrowser.Maui.Base", StringComparison.Ordinal) != true)
			{
				continue;
			}

			object? value;
			try
			{
				value = property.GetValue(node);
			}
			catch (TargetInvocationException)
			{
				continue;
			}

			if (value is System.Collections.IEnumerable items && value is not string)
			{
				foreach (var item in items)
				{
					Visit(item, seen);
				}
			}
			else if (value?.GetType().Namespace?.StartsWith("SampleBrowser.Maui.Base", StringComparison.Ordinal) == true)
			{
				Visit(value, seen);
			}
		}
	}

	static Stream? OpenSingleResource(Assembly assembly, string suffix)
	{
		var matches = assembly.GetManifestResourceNames().Where(name => name.EndsWith(suffix, StringComparison.Ordinal)).ToList();
		if (matches.Count != 1)
		{
			return null;
		}

		return CopyResource(assembly, matches[0]);
	}

	static Stream? OpenResource(Assembly assembly, string suffix)
	{
		var resourceName = assembly.GetManifestResourceNames().FirstOrDefault(name => name.EndsWith(suffix, StringComparison.Ordinal));
		return resourceName == null ? null : CopyResource(assembly, resourceName);
	}

	static Stream? CopyResource(Assembly assembly, string resourceName)
	{
		var stream = assembly.GetManifestResourceStream(resourceName);
		if (stream == null)
		{
			return null;
		}

		var copy = new MemoryStream();
		stream.CopyTo(copy);
		stream.Dispose();
		copy.Position = 0;
		return copy;
	}
}
