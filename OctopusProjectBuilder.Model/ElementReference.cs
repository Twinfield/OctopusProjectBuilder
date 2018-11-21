namespace OctopusProjectBuilder.Model
{
    public class ElementReference
    {
        public ElementReference(string name)
        {
            Name = name;
        }

        public string Name { get; }

        public override string ToString()
        {
            return $"@{Name}";
        }

	    public static ElementReference Extract(string reference)
	    {
		    return new ElementReference(reference.Replace("@", string.Empty));
	    }
    }
}