using System;
using System.Collections.Generic;
using System.Linq;

using OctopusProjectBuilder.Model;

namespace OctopusProjectBuilder.Uploader
{
	public static class ProjectExtensions
	{
		public static IEnumerable<ProjectReference> FindProjectReferences(this Project project)
		{
			var references =  project
				.DeploymentProcess
				.DeploymentSteps
				.SelectMany(step => step.Actions)
				.Where(a => a.ActionType == "Octopus.DeployRelease")
				.SelectMany(a => a.Properties)
				.Where(p => p.Key == "Octopus.Action.DeployRelease.ProjectId")
				.Where(p => (p.Value.Value?.StartsWith("@")).GetValueOrDefault())
				.Select(p => new ProjectReference(p.Value))
				.ToArray();

			return references;
		}
	}
}
