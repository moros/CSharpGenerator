using System.Collections.Generic;
using Moros.CSharpGenerator.Enums;

// ReSharper disable once IdentifierTypo
namespace Moros.CSharpGenerator
{
    public class InterfaceModel : BaseElement
    {
        public InterfaceModel(string name = null)
        {
            base.CustomDataType = Util.Interface;
            base.Name = name;
        }

        public override int IndentSize { get; set; } = (int)IndentType.Single * FileGenerator.DefaultTabSize;

        public new BuiltInDataType? BuiltInDataType { get; }

        public new string CustomDataType => base.CustomDataType;

        public new string Name => base.Name;

        public virtual List<Property> Properties { get; set; } = new List<Property>();

        public virtual List<Method> Methods { get; set; } = new List<Method>();

        public override string ToString()
        {
            var result = base.ToString();
            result += Util.NewLine + Indent + "{";

            result += string.Join("", Properties);
            var hasPropertiesAndMethods = Properties.Count > 0 && Methods.Count > 0;
            result += hasPropertiesAndMethods ? Util.NewLine : "";
            result += string.Join(Util.NewLine, Methods);

            result += Util.NewLine + Indent + "}";
            result = result.Replace(AccessModifier.Public.ToTextLower() + " ", "");
            result = result.Replace("\r\n        {\r\n        }", ";");
            return result;
        }
    }
}
