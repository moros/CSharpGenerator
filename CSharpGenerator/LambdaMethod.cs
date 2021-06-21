using Moros.CSharpGenerator.Enums;

// ReSharper disable once IdentifierTypo
namespace Moros.CSharpGenerator
{
    public class LambdaMethod : Method
    {
        public LambdaMethod() { }

        public LambdaMethod(BuiltInDataType builtInDataType, string name) : base(builtInDataType, name) { }

        public LambdaMethod(string customDataType, string name) : base(customDataType, name) { }

        public LambdaMethod(AccessModifier accessModifier, KeyWord singleKeyWord, BuiltInDataType builtInDataType, string name) : base(builtInDataType, name)
        {
            AccessModifier = accessModifier;
            KeyWords.Add(singleKeyWord);
        }

        public string Body { get; set; } = string.Empty;

        private string ConstructSignature() => (this.Comment != null ? Util.NewLine + this.Indent + "// " + this.Comment : "") + (this.HasAttributes ? this.Attributes.ToStringList(this.Indent) : "") + Util.NewLine + this.Signature;

        public override string ToString()
        {
            if (!IsVisible || string.IsNullOrEmpty(Body))
                return "";

            var result = ConstructSignature();
            result += " => " + Body;
            return result;
        }
    }
}
