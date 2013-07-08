namespace Catrobat.Core
{
    public interface IContextHolder
    {
        CatrobatContext GetContext();
    }
}