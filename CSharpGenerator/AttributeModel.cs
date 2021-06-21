using System.Collections.Generic;

// ReSharper disable once IdentifierTypo
namespace Moros.CSharpGenerator
{
    public class AttributeModel
    {
        public AttributeModel(string name = null)
        {
            Name = name;
        }

        public string Name { get; set; }

        public List<Parameter> Parameters { get; set; } = new List<Parameter>();
        public Parameter SingleParameter { set => Parameters.Add(value); }

        public override string ToString()
        {
            var parametersString = Parameters.Count > 0 ? Parameters.ToStringList() : "";
            var result = $"[{Name}{parametersString}]";
            return result;
        }
    }

    public static class AttributeModelExtensions
    {
        public static string ToStringList(this List<AttributeModel> attributes, string indent)
        {
            return attributes.Count > 0 ? Util.NewLine + indent + string.Join(Util.NewLine + indent, attributes) : "";
        }
    }
}
