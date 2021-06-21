using System.Collections.Generic;
using Moros.CSharpGenerator.Enums;

// ReSharper disable once IdentifierTypo
namespace Moros.CSharpGenerator
{
    public class EnumModel : BaseElement
    {
        public EnumModel(string name = null)
        {
            base.CustomDataType = Util.Enum;
            base.Name = name;
        }

        public override int IndentSize { get; set; } = (int)IndentType.Single * FileGenerator.DefaultTabSize;

        public new BuiltInDataType? BuiltInDataType { get; }

        public new string CustomDataType => base.CustomDataType;

        public new string Name => base.Name;

        public List<EnumValue> EnumValues { get; set; } = new List<EnumValue>();

        public override string ToString()
        {
            var result = base.ToString();
            result += Util.NewLine + Indent + "{";

            result += EnumValues.Count > 0 ? Util.NewLine : "";
            result += string.Join("," + Util.NewLine, EnumValues);
            result += Util.NewLine + Indent + "}";
            return result;
        }
    }

    public class EnumValue
    {
        public EnumValue(string name = null, int? value = null)
        {
            Name = name;
            Value = value;
        }

        public virtual int IndentSize { get; set; } = (int)IndentType.Double * FileGenerator.DefaultTabSize;
        public string Indent => new string(' ', IndentSize);

        public string Name { get; set; }

        public int? Value { get; set; }
        public string ValuFormated => Value != null ? " = " + Value : "";

        public override string ToString()
        {
            var result = Indent + Name + ValuFormated;
            return result;
        }
    }
}
