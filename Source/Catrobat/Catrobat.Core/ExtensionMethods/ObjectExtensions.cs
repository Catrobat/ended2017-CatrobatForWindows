namespace Catrobat.IDE.Core.ExtensionMethods
{
    public static class ObjectExtensions
    {
        public static bool TypeEquals(object first, object second)
        {
            return first == null || second == null
                ? first == null && second == null
                : first.GetType() == second.GetType();
        }
    }
}
