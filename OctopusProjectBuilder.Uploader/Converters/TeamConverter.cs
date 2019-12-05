using System.Linq;
using Octopus.Client;
using Octopus.Client.Model;
using OctopusProjectBuilder.Model;

namespace OctopusProjectBuilder.Uploader.Converters
{
    public static class TeamConverter
    {
        public static TeamResource UpdateWith(this TeamResource resource, Team model, IOctopusRepository repository)
        {
            resource.Name = model.Identifier.Name;
            resource.MemberUserIds = new ReferenceCollection(model.Users.Select(u => repository.Users.ResolveResourceId(u)));
            resource.ExternalSecurityGroups = new NamedReferenceItemCollection();
            foreach (var esg in model.ExternalSecurityGroups)
                resource.ExternalSecurityGroups.Add(new NamedReferenceItem { Id = esg });
				resource.Description = model.Description;
            return resource;
        }

        public static Team ToModel(this TeamResource resource, IOctopusRepository repository)
        {
            return new Team(
                new ElementIdentifier(resource.Name),
					 resource.Description,
                resource.MemberUserIds.Select(mui => new ElementReference(repository.Users.Get(mui).Username)),
                resource.ExternalSecurityGroups.Select(esg => esg.Id));
        }
    }
}
