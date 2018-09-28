namespace OctopusProjectBuilder.Model
{
    public class VariablePrompt
    {
        public bool Required { get; }
        public string Label { get; }
        public string Description { get; }
	    public ControlType ControlType { get; }
		public string SelectOptions { get; }

        public VariablePrompt(bool required, string label, string description, ControlType controlType, string selectOptions)
        {
            Required = required;
            Label = label;
            Description = description;
	        ControlType = controlType;
	        SelectOptions = selectOptions;
        }
    }
}