using System;
using System.Collections.Generic;
using System.Linq;

using Octopus.Client;
using Octopus.Client.Model;
using Octopus.Client.Repositories;

using OctopusProjectBuilder.Model;

namespace OctopusProjectBuilder.Uploader.Converters
{
	public static class PropertyValueConverter
	{
		public static PropertyValue ToModel(this PropertyValueResource resource, IOctopusRepository repository)
		{
			string value = IsProjectReference(resource) ? ResolveProjectReference(resource, repository) : resource.Value;
			return new PropertyValue(resource.IsSensitive, value);
		}

		static bool IsProjectReference(PropertyValueResource resource)
		{
			return !string.IsNullOrEmpty(resource.Value) && resource.Value.StartsWith("Projects-");
		}

		static string ResolveProjectReference(PropertyValueResource resource, IOctopusRepository repository)
		{
			var collection = new ReferenceCollection(resource.Value);
			return collection.ToModel(repository.Projects).FirstOrDefault()?.ToString();
		}

		public static Dictionary<string, PropertyValue> ToModel(this IDictionary<string, PropertyValueResource> properties, IOctopusRepository repository)
		{
			return properties
				.Select(kv => Tuple.Create(kv.Key, ToModel(kv.Value, repository)))
				.ToDictionary(kv => kv.Item1, kv => kv.Item2);
		}

		public static void UpdateWith(this IDictionary<string, PropertyValueResource> resource, IReadOnlyDictionary<string, PropertyValue> model)
		{
			resource.Clear();
			foreach (var keyValuePair in model)
				resource.Add(keyValuePair.Key, new PropertyValueResource(keyValuePair.Value.Value, keyValuePair.Value.IsSensitive));
		}
	}
}