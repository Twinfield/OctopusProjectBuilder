using System;
using System.ComponentModel;
using System.Linq;
using OctopusProjectBuilder.Model;
using OctopusProjectBuilder.YamlReader.Helpers;
using YamlDotNet.Serialization;

namespace OctopusProjectBuilder.YamlReader.Model
{
    [Description("Team definition.")]
    [Serializable]
    public class YamlTeam : YamlNamedElement
    {
        [YamlMember(Order = 3)]
        [Description("List of user references.")]
        public string[] UserRefs { get; set; }

        [YamlMember(Order = 4)]
        [Description("List of external security group ids.")]
        public string[] ExternalSecurityGroupIds { get; set; }

		  [YamlMember(Order = 20)]
		  [Description("Team description.")]
		  public string Description { get; set; }

		public static YamlTeam FromModel(Team model)
        {
            return new YamlTeam
            {
                Name = model.Identifier.Name,
					 Description = model.Description,
                RenamedFrom = model.Identifier.RenamedFrom,
                UserRefs = model.Users.Select(u => u.Name).ToArray().NullIfEmpty(),
                ExternalSecurityGroupIds = model.ExternalSecurityGroups.ToArray().NullIfEmpty()
            };
        }

        public Team ToModel()
        {
            return new Team(
                ToModelName(),
					 Description,
                UserRefs.EnsureNotNull().Select(ur => new ElementReference(ur)),
                ExternalSecurityGroupIds.EnsureNotNull());
        }
    }
}
