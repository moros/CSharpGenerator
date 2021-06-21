// ReSharper disable once IdentifierTypo
namespace Moros.CSharpGenerator.Enums
{
    public static class EnumExtensions
    {
        public static string ToTextLower<T>(this T value, string append = "")
        {
            var result = value.ToString().ToLower().Replace("_", " ") + append;
            return result;
        }
    }
}
