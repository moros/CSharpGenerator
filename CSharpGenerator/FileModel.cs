using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CSharpGenerator
{
    public class FileModel
    {
        public FileModel()
        {
        }

        public FileModel(string name)
        {
            Name = name;
        }

        public List<string> UsingDirectives { get; set; } = new List<string>();

        public List<string> TopLevelComments { get; set; } = new List<string>();

        public string Namespace { get; set; }

        public string Name { get; set; }

        public string Extension { get; set; } = Util.CsExtension;

        public string OutputDirectory { get; set; } = string.Empty;

        public string FullName => !string.IsNullOrEmpty(OutputDirectory) 
            ? Path.Combine(OutputDirectory, NameWithExtension)
            : NameWithExtension;

        public string NameWithExtension => Name + "." + Extension;

        public List<EnumModel> Enums { get; set; } = new List<EnumModel>();

        public List<ClassModel> Classes { get; set; } = new List<ClassModel>();

        public List<InterfaceModel> Interfaces { get; set; } = new List<InterfaceModel>();

        public void LoadUsingDirectives(List<string> usingDirectives)
        {
            foreach (var usingDirective in usingDirectives)
            {
                UsingDirectives.Add(usingDirective);
            }
        }

        public void AddTopLevelComments(params string[] comments)
        {
            TopLevelComments.AddRange(comments);
        }

        public override string ToString()
        {
            var topLevelComments = TopLevelComments.Count == 0 ? "" : string.Join(Util.NewLine, TopLevelComments.Select(str => "//" + str)) + Util.NewLine;
            var usingText = UsingDirectives.Count > 0 ? Util.Using + " " : "";
            var result = topLevelComments + usingText + string.Join(Util.NewLine + usingText, UsingDirectives);
            result += !string.IsNullOrEmpty(usingText) ? Util.NewLineDouble : "";
            result += Util.Namespace + " " + Namespace;
            result += Util.NewLine + "{";
            result += string.Join(Util.NewLine, Enums);
            result += Enums.Count <= 0 || Classes.Count <= 0 ? "" : Util.NewLine;
            result += string.Join(Util.NewLine, Classes);
            result += Interfaces.Count <= 0 ? "" : Util.NewLine;
            result += string.Join(Util.NewLine, Interfaces);
            result += Util.NewLine + "}";
            result += Util.NewLine;
            return result;
        }
    }
}
