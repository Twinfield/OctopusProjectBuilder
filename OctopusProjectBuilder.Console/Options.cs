﻿namespace OctopusProjectBuilder.Console
{
    internal class Options
    {
        public enum Verb { Upload, Download, CleanupConfig }
        public string OctopusUrl { get; set; }
        public string OctopusApiKey { get; set; }
        public string DefinitionsDir { get; set; }
		public string ProjectGroup { get; set; }
        public Verb Action { get; set; }
    }
}