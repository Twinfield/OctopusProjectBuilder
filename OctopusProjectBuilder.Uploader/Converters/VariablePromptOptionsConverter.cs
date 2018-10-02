using System;
using System.Collections.Generic;
using Octopus.Client.Model;
using OctopusProjectBuilder.Model;
using ControlType = OctopusProjectBuilder.Model.ControlType;

namespace OctopusProjectBuilder.Uploader.Converters
{
    public static class VariablePromptOptionsConverter
    {
        const string ControlTypeKey = "Octopus.ControlType";
        const string SelectOptionsKey = "Octopus.SelectOptions";

        public static VariablePrompt ToModel(this VariablePromptOptions resource)
        {
            var controlType = GetControlType(resource);
            var selectOptions = GetSelectOptions(resource);
            return new VariablePrompt(resource.Required, resource.Label, resource.Description, controlType, selectOptions);
        }

        public static VariablePromptOptions FromModel(this VariablePrompt model)
        {
            return new VariablePromptOptions
            {
                Description = model.Description,
                Label = model.Label,
                Required = model.Required,
                DisplaySettings = GetDisplaySettings(model)
            };
        }

        static ControlType GetControlType(VariablePromptOptions resource)
        {
            if (resource.DisplaySettings.ContainsKey(ControlTypeKey) &&
                Enum.TryParse<ControlType>(resource.DisplaySettings[ControlTypeKey], out var type))
                return type;

            return ControlType.None;
        }

        static string GetSelectOptions(VariablePromptOptions resource)
        {
            if (resource.DisplaySettings.ContainsKey(SelectOptionsKey))
                return resource.DisplaySettings[SelectOptionsKey];

            return null;
        }

        static Dictionary<string, string> GetDisplaySettings(VariablePrompt model)
        {
            var displaySettings = new Dictionary<string, string>();

            if (model.ControlType != ControlType.None)
                displaySettings.Add(ControlTypeKey, model.ControlType.ToString());

            if (!string.IsNullOrEmpty(model.SelectOptions))
                displaySettings.Add(SelectOptionsKey, model.SelectOptions);

            return displaySettings;
        }
    }
}