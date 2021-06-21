using Moros.CSharpGenerator.Enums;

// ReSharper disable once IdentifierTypo
namespace Moros.CSharpGenerator
{
    public class Property : Field
    {
        public Property() { }

        public Property(BuiltInDataType builtInDataType, string name) : base(builtInDataType, name) { }

        public Property(string customDataType, string name) : base(customDataType, name) { }

        public override bool HasAttributes => true;

        public override AccessModifier AccessModifier { get; set; } = AccessModifier.Public;

        public bool IsGetOnly { get; set; } = false;

        public bool IsAutoImplemented { get; set; } = true;

        public string GetterBody { get; set; }

        public string SetterBody { get; set; }

        protected override string Ending => DefaultValue != null ? ";" : "";

        public override string Body
        {
            get
            {
                if (IsAutoImplemented)
                {
                    return IsGetOnly ? " { get; }" : " { get; set; }";
                }
                else
                {
                    var result = Util.NewLine + Indent + "{";
                    var currentIndent = Util.NewLine + Indent + FileGenerator.IndentSingle;

                    result += currentIndent + "get { return " + GetterBody + "; }";
                    if (!IsGetOnly && SetterBody != null)
                    {
                        result += currentIndent + "set { " + SetterBody + "; }";
                    }
                    result += Util.NewLine + Indent + "}";

                    return result;
                }
            }
        }
    }
}
