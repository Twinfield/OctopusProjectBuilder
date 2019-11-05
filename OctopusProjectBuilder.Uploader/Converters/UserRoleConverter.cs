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
				resource.GrantedSpacePermissions = model.SpacePermissions.Select(p => (Octopus.Client.Model.Permission)p).ToList(); ;
				resource.GrantedSystemPermissions = model.SystemPermissions.Select(p => (Octopus.Client.Model.Permission)p).ToList(); ;

            return resource;
        }

        public static UserRole ToModel(this UserRoleResource resource)
        {
            var systemPermissions = resource.GrantedSystemPermissions.Select(PermissionConverter.ToModel);
				var spacePermissions = resource.GrantedSpacePermissions.Select(PermissionConverter.ToModel);
				return new UserRole(new ElementIdentifier(resource.Name), resource.Description, systemPermissions, spacePermissions);
        }
    }
}
