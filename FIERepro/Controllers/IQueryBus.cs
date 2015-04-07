namespace FIERepro.Controllers
{
    public interface IQueryBus
    {
        T Query<T>(IQuery<T> query);
    }

    public interface IQuery<T>
    {
    }
}