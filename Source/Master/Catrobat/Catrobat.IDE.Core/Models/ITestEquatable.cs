namespace Catrobat.IDE.Core.Models
{
    /// <summary>Provides test-specific equality. </summary>
    /// <remarks>See <see cref="http://stackoverflow.com/a/2047576"/> and compare <see cref="System.IEquatable{T}"/>. </remarks>
    public interface ITestEquatable<in T>
    {
        bool TestEquals(T other);
    }
}
