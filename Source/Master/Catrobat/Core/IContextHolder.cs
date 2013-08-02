namespace Catrobat.Core
{
    public interface IContextHolder
    {
        CatrobatContextBase GetContext();
    }
}