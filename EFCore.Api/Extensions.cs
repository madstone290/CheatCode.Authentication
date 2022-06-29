namespace EFCore.Api
{
    public static class Extensions
    {
        public static string JoinWithNewLine(this IEnumerable<object> enumerable)
        {
            return string.Join(Environment.NewLine, enumerable);
        }
    }
}
