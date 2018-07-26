﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using OctopusProjectBuilder.Model;
using OctopusProjectBuilder.YamlReader.Helpers;

namespace OctopusProjectBuilder.YamlReader.Model
{
    [Description(@"Variable scope definition. It can limit variable visibility to specific context. 
The variable scope should be understood as `(role1 OR ...roleN) AND (machine1 OR ...machineN) AND (env1 OR envN) AND...` where if none resource references are defined of specific type \(like role or machine etc.\) then variable is available to all the resources of that type.")]
    [Serializable]
    public class YamlVariableScope
    {
        [Description("List of Role references (based on the name) where variable is applicable to. The roles correspond to roles that Machines have specified. If none are specified, then variable is available to all of them.")]
        public string[] RoleRefs { get; set; }

        [Description("List of Machine references (based on the name) where variable is applicable to. If none are specified, then variable is available to all of them.")]
        public string[] MachineRefs { get; set; }

        [Description("List of Environment references (based on the name) where variable is applicable to. If none are specified, then variable is available to all of them.")]
        public string[] EnvironmentRefs { get; set; }

        [Description("List of Channel references (based on the name) where variable is applicable to. If none are specified, then variable is available to all of them.")]
        public string[] ChannelRefs { get; set; }

        [Description("List of Action references (based on the name) where variable is applicable to. If none are specified, then variable is available to all of them. The Action references can be only specified in Project variables (LibraryVariableSets does not support them).")]
        public string[] ActionRefs { get; set; }

        [Description("List of TenantTags references (based on the name) where variable is applicable to. If none are specified, then variable is available to all of them.")]
        public string[] TenantTagRefs { get; set; }

        public IReadOnlyDictionary<VariableScopeType, IEnumerable<ElementReference>> ToModel()
        {
            var result = new Dictionary<VariableScopeType, IEnumerable<ElementReference>>();
            Add(result, VariableScopeType.Role, RoleRefs);
            Add(result, VariableScopeType.Machine, MachineRefs);
            Add(result, VariableScopeType.Environment, EnvironmentRefs);
            Add(result, VariableScopeType.Channel, ChannelRefs);
            Add(result, VariableScopeType.Action, ActionRefs);
            Add(result, VariableScopeType.TenantTag, TenantTagRefs);
            return result;
        }

        private void Add(Dictionary<VariableScopeType, IEnumerable<ElementReference>> result, VariableScopeType type, string[] values)
        {
            if (values == null || values.Length == 0)
                return;
            result.Add(type, values.Select(name => new ElementReference(name)).ToArray());
        }

        public static YamlVariableScope FromModel(IReadOnlyDictionary<VariableScopeType, IEnumerable<ElementReference>> model)
        {
            if (!model.Any())
                return null;
            return new YamlVariableScope
            {
                ActionRefs = GetScopeRefs(model, VariableScopeType.Action),
                ChannelRefs = GetScopeRefs(model, VariableScopeType.Channel),
                EnvironmentRefs = GetScopeRefs(model, VariableScopeType.Environment),
                MachineRefs = GetScopeRefs(model, VariableScopeType.Machine),
                RoleRefs = GetScopeRefs(model, VariableScopeType.Role),
                TenantTagRefs = GetScopeRefs(model, VariableScopeType.TenantTag)
            };
        }

		private static string[] GetScopeRefs(IReadOnlyDictionary<VariableScopeType, IEnumerable<ElementReference>> model, VariableScopeType type)
		{
			return model.Where(kv => kv.Key == type).SelectMany(kv => kv.Value).Select(r => r.Name).OrderBy(v => v).ToArray().NullIfEmpty();
		}
    }
}