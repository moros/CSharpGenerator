using CSharpGenerator.Enums;

namespace CSharpGenerator
{
    public class Field : BaseElement
    {
        public Field()
        {
        }

        public Field(BuiltInDataType builtInDataType, string name) : base(builtInDataType, name)
        {
        }

        public Field(string customDataType, string name) : base(customDataType, name)
        {
        }

        public Field(AccessModifier modifier, BuiltInDataType builtInDataType, string name) : base(builtInDataType, name)
        {
            AccessModifier = modifier;
        }

        public Field(AccessModifier modifier, string customDataType, string name) : base(customDataType, name)
        {
            AccessModifier = modifier;
        }

        public override AccessModifier AccessModifier { get; set; } = AccessModifier.Private;

        public virtual string Body { get; }

        public virtual string DefaultValue { get; set; }
        protected string DefaultValueFormated => DefaultValue != null ? " = " + DefaultValue : "";

        public override bool HasAttributes => false;

        protected virtual string Ending => ";";

        public override string ToString() => base.ToString() + Body + DefaultValueFormated + Ending;
    }
}
