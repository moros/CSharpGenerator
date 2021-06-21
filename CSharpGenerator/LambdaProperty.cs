using Moros.CSharpGenerator.Enums;

// ReSharper disable once IdentifierTypo
namespace Moros.CSharpGenerator
{
    public class LambdaProperty : Property
    {
        public LambdaProperty()
        {
        }

        public LambdaProperty(BuiltInDataType builtInDataType, string name)
            : base(builtInDataType, name)
        {
        }

        public LambdaProperty(string customDataType, string name)
            : base(customDataType, name)
        {
        }

        public override string Body
        {
            get
            {
                if (IsGetOnly)
                    return $" => {GetterBody};";

                var str1 = Util.NewLine + Indent + "{";
                var str2 = Util.NewLine + Indent + FileGenerator.IndentSingle;
                var str3 = str1 + str2 + "get => " + GetterBody + ";";
                if (!IsGetOnly && SetterBody != null)
                    str3 = str3 + str2 + "set => " + SetterBody + ";";

                return str3 + Util.NewLine + Indent + "}";
            }
        }
    }
}
