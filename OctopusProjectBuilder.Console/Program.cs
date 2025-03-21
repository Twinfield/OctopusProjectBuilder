using System;
using System.Linq;
using Fclp;

namespace OctopusProjectBuilder.Console
{
	class Program
    {
        static int Main(string[] args)
        {
            return 0;
			
        }

        public static Options ReadOptions(string[] args)
        {
            var parser = new FluentCommandLineParser<Options>();
            parser.Setup(o => o.Action).As('a', "action").Required().WithDescription($"Action to perform: {string.Join(", ", Enum.GetValues(typeof(Options.Verb)).Cast<object>())}");
	        parser.Setup(o => o.ProjectGroup).As('g', "projectGroup").Required().WithDescription("Project group");
			parser.Setup(o => o.DefinitionsDir).As('d', "definitions").Required().WithDescription("Definitions directory");
            parser.Setup(o => o.OctopusUrl).As('u', "octopusUrl").Required().WithDescription("Octopus Url");
            parser.Setup(o => o.OctopusApiKey).As('k', "octopusApiKey").Required().WithDescription("Octopus API key");
            parser.SetupHelp("?", "help").Callback(text => System.Console.WriteLine(text));

            var result = parser.Parse(args);
            if (result.HasErrors)
            {
                System.Console.Error.WriteLine(result.ErrorText);
                parser.HelpOption.ShowHelp(parser.Options);
                return null;
            }
            return parser.Object;
        }
    }
}