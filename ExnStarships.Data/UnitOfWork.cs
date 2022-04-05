namespace ExnStarships.Data;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    MainContext context;

    public UnitOfWork(MainContext context)
    {
        this.context = context;
    }

    public void Dispose()
    {
        context.Dispose();
    }

    public void SaveChanges()
    {
        context.SaveChanges();
    }
}
