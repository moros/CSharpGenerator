using System;
using System.Collections.Generic;
using System.IO;

namespace CSharpGenerator
{
    public class FileGenerator
    {
        public static int DefaultTabSize = 4;

        public static string IndentSingle => new string(' ', DefaultTabSize);

        public string OutputDirectory { get; set; } = "Output";

        public string DefaultPath { get; } = Directory.GetCurrentDirectory();

        public string RootPath { get; set; }

        public List<FileModel> Files { get; set; } = new List<FileModel>();

        public void CreateFiles()
        {
            var path = RootPath ?? DefaultPath;
            if (!Directory.Exists(path))
            {
                var message = "Path not valid: " + RootPath;
                Console.WriteLine(message);
                throw new InvalidOperationException(message);
            }

            if (OutputDirectory == null)
            {
                const string message = "Generator.OutputDirectory not set!";
                Console.WriteLine(message);
                throw new InvalidOperationException(message);
            }

            if (!Directory.Exists(Path.Combine(path, OutputDirectory)))
            {
                Directory.CreateDirectory(Path.Combine(path, OutputDirectory));
                Console.WriteLine("Created folder: " + OutputDirectory);
            }

            Console.WriteLine("Created files: ");
            var num = 1;
            foreach (var fileModel in Files)
            {
                var fullPath = Path.Combine(Path.Combine(path, OutputDirectory), fileModel.FullName);
                var dirPath = Path.GetDirectoryName(fullPath);
                if (dirPath != null && !Directory.Exists(dirPath))
                {
                    Directory.CreateDirectory(dirPath);
                }

                using (var text = File.CreateText(fullPath))
                {
                    text.Write(fileModel);
                    Console.WriteLine($"  {num}. {fileModel.FullName}");
                    ++num;
                }
            }
        }
    }
}
