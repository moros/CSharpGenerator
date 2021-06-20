using System.Collections.Generic;
using System.Linq;
using CSharpGenerator.Enums;
using static System.String;

namespace CSharpGenerator
{
    public sealed class ClassModel : BaseElement
    {
        public ClassModel(string name = null)
        {
            base.CustomDataType = Util.Class;
            base.Name = name;
            Constructors.Add(new Constructor(Name) { IsVisible = false, BracesInNewLine = false });
        }

        public override int IndentSize { get; set; } = (int)IndentType.Single * FileGenerator.DefaultTabSize;

        public bool HasPropertiesSpacing { get; set; } = true;

        public new BuiltInDataType? BuiltInDataType { get; }

        public new string CustomDataType => Util.Class;

        public new string Name => base.Name;

        public string BaseClass { get; set; }

        public List<string> Interfaces { get; set; } = new List<string>();

        public List<Field> Fields { get; set; } = new List<Field>();

        public List<Constructor> Constructors { get; set; } = new List<Constructor>();

        public Constructor DefaultConstructor
        {
            get => Constructors[0];
            set => Constructors[0] = value;
        }

        public List<Property> Properties { get; set; } = new List<Property>();

        public List<Method> Methods { get; set; } = new List<Method>();

        public List<ClassModel> NestedClasses { get; set; } = new List<ClassModel>();

        public override string ToString()
        {
            var result = base.ToString();
            result += (BaseClass != null || Interfaces?.Count > 0) ? " : " : "";
            result += BaseClass ?? "";
            result += (BaseClass != null && Interfaces?.Count > 0) ? ", " : "";
            result += Interfaces?.Count > 0 ? Join(", ", Interfaces) : "";
            result += Util.NewLine + Indent + "{";

            result += Join("", Fields);

            var visibleConstructors = Constructors.Where(a => a.IsVisible);
            var hasFieldsBeforeConstructor = visibleConstructors.Any() && Fields.Any();
            result += hasFieldsBeforeConstructor ? Util.NewLine : "";
            result += Join(Util.NewLine, visibleConstructors);
            var hasMembersAfterConstructor = (visibleConstructors.Any() || Fields.Any()) && (Properties.Any() || Methods.Any());
            result += hasMembersAfterConstructor ? Util.NewLine : "";

            result += Join(HasPropertiesSpacing ? Util.NewLine : "", Properties);

            result += hasMembersAfterConstructor ? Util.NewLine : "";
            result += Join(Util.NewLine, Methods);

            result += NestedClasses.Count > 0 ? Util.NewLine : "";
            result += Join(Util.NewLine, NestedClasses);

            result += Util.NewLine + Indent + "}";
            
            return result;
        }
    }
}
