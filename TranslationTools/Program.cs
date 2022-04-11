using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TranslationTools
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.Write("Name of project: ");
            var projectName = Console.ReadLine();

            var job = new CreateFileOrFolder();
            Console.WriteLine($"Processing {projectName}");
            await job.Create($"{projectName}-template.doc", projectName, false);
            await job.Create($"{projectName}-orc.doc", projectName, true);
        }
    }

    public class InputData
    {
        public string TitleJapanese { get;set; }
        public string TitleRomaji { get; set; }
    }
}
