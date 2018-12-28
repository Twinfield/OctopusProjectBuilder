using System.Collections.Generic;
using System.Linq;

using Common.Logging;

using Octopus.Client.Exceptions;
using Octopus.Client.Extensibility;
using Octopus.Client.Model;
using Octopus.Client.Repositories;
using OctopusProjectBuilder.Model;

namespace OctopusProjectBuilder.Uploader.Converters
{
    public static class ReferenceCollectionConverter
    {
		static readonly ILog Logger = LogManager.GetLogger(typeof(ScopeSpecificationConverter));

		public static void UpdateWith(this ReferenceCollection collection, IEnumerable<string> ids)
        {
            collection.Clear();
            foreach (var id in ids)
                collection.Add(id);
        }

        public static IEnumerable<ElementReference> ToModel<TResource>(this ReferenceCollection collection, IGet<TResource> repository) where TResource : INamedResource
		{
            return collection.Select(id => TryGetReference(repository, id)).Where(r => r != null);
        }

		private static ElementReference TryGetReference<TResource>(IGet<TResource> repository, string id) where TResource : INamedResource
		{
			var name = TryGetReferenceName(repository, id);
			return name != null ? new ElementReference(name) : null;
		}

		private static string TryGetReferenceName<TResource>(IGet<TResource> repository, string id) where TResource : INamedResource
		{
			try
			{
				return repository.Get(id).Name;
			}
			catch (OctopusResourceNotFoundException ex)
			{
				Logger.Warn(ex.Message);
				return null;
			}
		}

		public static void UpdateWith(this IDictionary<string, ReferenceCollection> resource, IReadOnlyDictionary<string, IEnumerable<ElementReference>> model)
        {
            resource.Clear();
            foreach (var keyValuePair in model)
                resource.Add(keyValuePair.Key, new ReferenceCollection(keyValuePair.Value.Select(x => x.Name)));
        }
    }
}