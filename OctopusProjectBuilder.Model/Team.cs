using System;
using System.Collections.Generic;
using System.Linq;

namespace OctopusProjectBuilder.Model
{
    public class Team
    {
        public ElementIdentifier Identifier { get; }
		  public string Description { get; }
        public IEnumerable<ElementReference> Users { get; }
        public IEnumerable<string> ExternalSecurityGroups { get; }
        public Team(
            ElementIdentifier identifier,
		  		string description,
            IEnumerable<ElementReference> users,
            IEnumerable<string> externalSecurityGroups)
        {
            if (identifier == null)
                throw new ArgumentNullException(nameof(identifier));
            Identifier = identifier;
				Description = description;
            Users = users.ToArray();
            ExternalSecurityGroups = externalSecurityGroups.ToArray();
        }
        
        public override string ToString()
        {
            return Identifier.ToString();
        }
    }
}
