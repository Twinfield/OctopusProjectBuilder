using System;
using System.Linq;

using OctopusProjectBuilder.Model;

namespace OctopusProjectBuilder.Uploader
{
	public class ProjectReference
	{
		readonly PropertyValue value;
		
		public ProjectReference(PropertyValue value)
		{
			this.value = value;
			this.ProjectName = ElementReference.Extract(value.Value).Name;
		}

		public string ProjectName { get; }

		public void Update(string id)
		{
			value.Value = id;
		}
	}
}