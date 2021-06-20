using System.Collections.Generic;
using CSharpGenerator.Enums;

namespace CSharpGenerator
{
    public class Method : BaseElement
    {
        public Method() { }

        public Method(BuiltInDataType builtInDataType, string name) : base(builtInDataType, name) { }

        public Method(string customDataType, string name) : base(customDataType, name) { }

        public Method(AccessModifier accessModifier, KeyWord singleKeyWord, BuiltInDataType builtInDataType, string name) : base(builtInDataType, name)
        {
            AccessModifier = accessModifier;
            KeyWords.Add(singleKeyWord);
        }

        public virtual bool IsVisible { get; set; } = true;

        public List<Parameter> Parameters { get; set; } = new List<Parameter>();

        public string BaseParameters { get; set; }
        public string BaseParametersFormated => BaseParameters != null ? $" : base({BaseParameters})" : "";

        public virtual bool BracesInNewLine { get; set; } = true;

        public List<string> BodyLines { get; set; } = new List<string>();

        public override string Signature => base.Signature + Parameters.ToStringList();

        public override string ToString()
        {
            if (!IsVisible)
                return "";

            var result = base.ToString() + BaseParametersFormated;
            var bracesPrefix = BracesInNewLine ? (Util.NewLine + Indent) : " ";
            var currentIndent = Util.NewLine + Indent + FileGenerator.IndentSingle;
            result += bracesPrefix + "{";
            result += BodyLines.Count == 0 ? "" : (BracesInNewLine ? currentIndent : " ") + string.Join(currentIndent, BodyLines);
            result += bracesPrefix + "}";
            return result;
        }
    }
}
