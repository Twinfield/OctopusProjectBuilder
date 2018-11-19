using System.Collections.Generic;
using System.Linq;

namespace OctopusProjectBuilder.Model
{
	public class SystemModel
	{

		public MachinePolicy[] MachinePolicies => machinePolicies.Values.ToArray();
		public Environment[] Environments => environments.Values.ToArray();
		public LibraryVariableSet[] LibraryVariableSets => libraryVariableSets.Values.ToArray();
		public Lifecycle[] Lifecycles => lifecycles.Values.ToArray();
		public ProjectGroup[] ProjectGroups => projectGroups.Values.ToArray();
		public Project[] Projects => projects.Values.ToArray();
		public UserRole[] UserRoles => userRoles.Values.ToArray();
		public Team[] Teams => teams.Values.ToArray();
		public Tenant[] Tenants => tenants.Values.ToArray();
		public TagSet[] TagSets => tagSets.Values.ToArray();


		readonly SortedList<string, MachinePolicy> machinePolicies;
		readonly SortedList<string, Environment> environments;

		readonly SortedList<string, LibraryVariableSet> libraryVariableSets;

		readonly SortedList<string, Lifecycle> lifecycles;
		readonly SortedList<string, ProjectGroup> projectGroups;
		readonly SortedList<string, Project> projects;
		readonly SortedList<string, UserRole> userRoles;
		readonly SortedList<string, Team> teams;
		readonly SortedList<string, Tenant> tenants;
		readonly SortedList<string, TagSet> tagSets;


		public SystemModel()
		{
			machinePolicies = new SortedList<string, MachinePolicy>();
			environments = new SortedList<string, Environment>();
			libraryVariableSets = new SortedList<string, LibraryVariableSet>();
			lifecycles = new SortedList<string, Lifecycle>();
			projectGroups = new SortedList<string, ProjectGroup>();
			projects = new SortedList<string, Project>();
			userRoles = new SortedList<string, UserRole>();
			teams = new SortedList<string, Team>();
			tenants = new SortedList<string, Tenant>();
			tagSets = new SortedList<string, TagSet>();
		}

		public SystemModel(IEnumerable<MachinePolicy> machinePolicies, IEnumerable<Lifecycle> lifecycles, IEnumerable<ProjectGroup> projectGroups, IEnumerable<LibraryVariableSet> libraryVariableSets, IEnumerable<Project> projects, IEnumerable<Environment> environments, IEnumerable<UserRole> userRoles, IEnumerable<Team> teams, IEnumerable<Tenant> tenants, IEnumerable<TagSet> tagSets) : this()
		{
			AddMachinePolicies(machinePolicies.ToArray());
			AddEnvironments(environments.ToArray());
			AddLibraryVariableSets(libraryVariableSets.ToArray());
			AddLifecycles(lifecycles.ToArray());
			AddProjectGroups(projectGroups.ToArray());
			AddProjects(projects.ToArray());
			AddUserRoles(userRoles.ToArray());
			AddTeams(teams.ToArray());
			AddTenants(tenants.ToArray());
			AddTagSets(tagSets.ToArray());
		}

		public IEnumerable<SystemModel> SplitModel()
		{
			return machinePolicies.Values.Select(t => new SystemModel(Enumerable.Repeat(t, 1), Enumerable.Empty<Lifecycle>(), Enumerable.Empty<ProjectGroup>(), Enumerable.Empty<LibraryVariableSet>(), Enumerable.Empty<Project>(), Enumerable.Empty<Environment>(), Enumerable.Empty<UserRole>(), Enumerable.Empty<Team>(), Enumerable.Empty<Tenant>(), Enumerable.Empty<TagSet>()))
				.Concat(environments.Values.Select(e => new SystemModel(Enumerable.Empty<MachinePolicy>(), Enumerable.Empty<Lifecycle>(), Enumerable.Empty<ProjectGroup>(), Enumerable.Empty<LibraryVariableSet>(), Enumerable.Empty<Project>(), Enumerable.Repeat(e, 1), Enumerable.Empty<UserRole>(), Enumerable.Empty<Team>(), Enumerable.Empty<Tenant>(), Enumerable.Empty<TagSet>())))
				.Concat(projectGroups.Values.Select(grp => new SystemModel(Enumerable.Empty<MachinePolicy>(), Enumerable.Empty<Lifecycle>(), Enumerable.Repeat(grp, 1), Enumerable.Empty<LibraryVariableSet>(), Enumerable.Empty<Project>(), Enumerable.Empty<Environment>(), Enumerable.Empty<UserRole>(), Enumerable.Empty<Team>(), Enumerable.Empty<Tenant>(), Enumerable.Empty<TagSet>())))
				.Concat(projects.Values.Select(prj => new SystemModel(Enumerable.Empty<MachinePolicy>(), Enumerable.Empty<Lifecycle>(), Enumerable.Empty<ProjectGroup>(), Enumerable.Empty<LibraryVariableSet>(), Enumerable.Repeat(prj, 1), Enumerable.Empty<Environment>(), Enumerable.Empty<UserRole>(), Enumerable.Empty<Team>(), Enumerable.Empty<Tenant>(), Enumerable.Empty<TagSet>())))
				.Concat(lifecycles.Values.Select(lf => new SystemModel(Enumerable.Empty<MachinePolicy>(), Enumerable.Repeat(lf, 1), Enumerable.Empty<ProjectGroup>(), Enumerable.Empty<LibraryVariableSet>(), Enumerable.Empty<Project>(), Enumerable.Empty<Environment>(), Enumerable.Empty<UserRole>(), Enumerable.Empty<Team>(), Enumerable.Empty<Tenant>(), Enumerable.Empty<TagSet>())))
				.Concat(libraryVariableSets.Values.Select(lvs => new SystemModel(Enumerable.Empty<MachinePolicy>(), Enumerable.Empty<Lifecycle>(), Enumerable.Empty<ProjectGroup>(), Enumerable.Repeat(lvs, 1), Enumerable.Empty<Project>(), Enumerable.Empty<Environment>(), Enumerable.Empty<UserRole>(), Enumerable.Empty<Team>(), Enumerable.Empty<Tenant>(), Enumerable.Empty<TagSet>())))
				.Concat(userRoles.Values.Select(ur => new SystemModel(Enumerable.Empty<MachinePolicy>(), Enumerable.Empty<Lifecycle>(), Enumerable.Empty<ProjectGroup>(), Enumerable.Empty<LibraryVariableSet>(), Enumerable.Empty<Project>(), Enumerable.Empty<Environment>(), Enumerable.Repeat(ur, 1), Enumerable.Empty<Team>(), Enumerable.Empty<Tenant>(), Enumerable.Empty<TagSet>())))
				.Concat(teams.Values.Select(t => new SystemModel(Enumerable.Empty<MachinePolicy>(), Enumerable.Empty<Lifecycle>(), Enumerable.Empty<ProjectGroup>(), Enumerable.Empty<LibraryVariableSet>(), Enumerable.Empty<Project>(), Enumerable.Empty<Environment>(), Enumerable.Empty<UserRole>(), Enumerable.Repeat(t, 1), Enumerable.Empty<Tenant>(), Enumerable.Empty<TagSet>())))
				.Concat(tenants.Values.Select(t => new SystemModel(Enumerable.Empty<MachinePolicy>(), Enumerable.Empty<Lifecycle>(), Enumerable.Empty<ProjectGroup>(), Enumerable.Empty<LibraryVariableSet>(), Enumerable.Empty<Project>(), Enumerable.Empty<Environment>(), Enumerable.Empty<UserRole>(), Enumerable.Empty<Team>(), Enumerable.Repeat(t, 1), Enumerable.Empty<TagSet>())))
				.Concat(tagSets.Values.Select(t => new SystemModel(Enumerable.Empty<MachinePolicy>(), Enumerable.Empty<Lifecycle>(), Enumerable.Empty<ProjectGroup>(), Enumerable.Empty<LibraryVariableSet>(), Enumerable.Empty<Project>(), Enumerable.Empty<Environment>(), Enumerable.Empty<UserRole>(), Enumerable.Empty<Team>(), Enumerable.Empty<Tenant>(), Enumerable.Repeat(t, 1))));
		}

		public SystemModel AddProjectGroups(params ProjectGroup[] groups)
		{
			foreach (ProjectGroup group in groups)
				this.projectGroups.Add(group.Identifier.Name, group);

			return this;
		}

		public SystemModel AddEnvironments(params Environment[] envs)
		{
			foreach (Environment environment in envs)
				this.environments.Add(environment.Identifier.Name, environment);
			
			return this;
		}

		public SystemModel AddProjects(params Project[] ps)
		{
			foreach (Project project in ps)
				this.projects.Add(project.Identifier.Name, project);

			return this;
		}

		public SystemModel AddLifecycles(params Lifecycle[] lcs)
		{
			foreach (Lifecycle lifecycle in lcs)
				this.lifecycles.Add(lifecycle.Identifier.Name, lifecycle);

			return this;
		}

		public SystemModel AddLibraryVariableSets(params LibraryVariableSet[] lvs)
		{
			foreach (LibraryVariableSet set in lvs)
				this.libraryVariableSets.Add(set.Identifier.Name, set);

			return this;
		}

		public SystemModel AddUserRoles(params UserRole[] roles)
		{
			foreach (UserRole role in roles)
				this.userRoles.Add(role.Identifier.Name, role);
			
			return this;
		}

		public SystemModel AddTeams(params Team[] userTeams)
		{
			foreach (Team team in userTeams)
				this.teams.Add(team.Identifier.Name, team);

			return this;
		}

		public SystemModel AddMachinePolicies(params MachinePolicy[] policies)
		{
			foreach (MachinePolicy policy in policies)
				this.machinePolicies.Add(policy.Identifier.Name, policy);

			return this;
		}

		public SystemModel AddTenants(params Tenant[] ts)
		{
			foreach (Tenant tenant in ts)
				this.tenants.Add(tenant.Identifier.Name, tenant);

			return this;
		}

		public SystemModel AddTagSets(params TagSet[] tss)
		{
			foreach (TagSet tagSet in tss)
				this.tagSets.Add(tagSet.Identifier.Name, tagSet);

			return this;
		}

		public SystemModel RemoveGroup(ElementIdentifier identifier)
		{
			if (projectGroups.ContainsKey(identifier.Name))
				projectGroups.Remove(identifier.Name);
			return this;
		}

		public void MergeIn(SystemModel model)
		{
			AddMachinePolicies(model.MachinePolicies.ToArray());
			AddEnvironments(model.Environments.ToArray());
			AddLibraryVariableSets(model.LibraryVariableSets.ToArray());
			AddLifecycles(model.Lifecycles.ToArray());
			AddProjectGroups(model.ProjectGroups.ToArray());
			AddProjects(model.Projects.ToArray());
			AddUserRoles(model.UserRoles.ToArray());
			AddTeams(model.Teams.ToArray());
			AddTenants(model.Tenants.ToArray());
			AddTagSets(model.TagSets.ToArray());
		}
	}
}
