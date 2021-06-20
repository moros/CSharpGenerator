﻿using System.Collections.Generic;

namespace CSharpGenerator
{
    public class FileModel
    {
        public FileModel() { }
        public FileModel(string name)
        {
            Name = name;
        }

        public List<string> UsingDirectives { get; set; } = new List<string>();

        public string Namespace { get; set; }

        public string Name { get; set; }

        public string Extension { get; set; } = Util.CsExtension;

        public string FullName => Name + "." + Extension;

        public List<EnumModel> Enums { get; set; } = new List<EnumModel>();

        public List<ClassModel> Classes { get; set; } = new List<ClassModel>();

        public void LoadUsingDirectives(List<string> usingDirectives)
        {
            foreach (var usingDirective in usingDirectives)
            {
                UsingDirectives.Add(usingDirective);
            }
        }

        public override string ToString()
        {
            var usingText = UsingDirectives.Count > 0 ? Util.Using + " " : "";
            var result = usingText + string.Join(Util.NewLine + usingText, UsingDirectives);
            result += Util.NewLineDouble + Util.Namespace + " " + Namespace;
            result += Util.NewLine + "{";
            result += string.Join(Util.NewLine, Enums);
            result += (Enums.Count > 0 && Classes.Count > 0) ? Util.NewLine : "";
            result += string.Join(Util.NewLine, Classes);
            result += Util.NewLine + "}";
            result += Util.NewLine;
            return result;
        }
    }
}
