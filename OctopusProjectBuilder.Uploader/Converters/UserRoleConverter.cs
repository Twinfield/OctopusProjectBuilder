using System.Linq;
using Octopus.Client.Model;
using OctopusProjectBuilder.Model;

namespace OctopusProjectBuilder.Uploader.Converters
{
    public static class UserRoleConverter
    {
        public static UserRoleResource UpdateWith(this UserRoleResource resource, UserRole model)
        {
            resource.Name = model.Identifier.Name;
            resource.Description = model.Description;
            resource.GrantedSpacePermissions = model.SpacePermissions.ToList();
				resource.GrantedSystemPermissions = model.SystemPermissions.ToList();

            return resource;
        }

        public static UserRole ToModel(this UserRoleResource resource)
        {
				return new UserRole(new ElementIdentifier(resource.Name), resource.Description,
               resource.GrantedSystemPermissions, resource.GrantedSpacePermissions);
        }
    }
}
