using System;
using System.Collections.Generic;
using System.Linq;

namespace OctopusProjectBuilder.Model
{
    public class UserRole
    {
        public UserRole(ElementIdentifier identifier, string description, IEnumerable<Permission> systemPermissions, IEnumerable<Permission> spacePermissions)
        {
            if (identifier == null)
                throw new ArgumentNullException(nameof(identifier));
            Identifier = identifier;
            Description = description;
				SystemPermissions = systemPermissions.ToArray();
				SpacePermissions = spacePermissions.ToArray();
		}

        public ElementIdentifier Identifier { get; }
        public string Description { get; }
        public IEnumerable<Permission> SystemPermissions { get; }
		  public IEnumerable<Permission> SpacePermissions { get; }

		public override string ToString()
        {
            return Identifier.ToString();
        }
    }
}
