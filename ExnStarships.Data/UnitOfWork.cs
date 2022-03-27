namespace ExnStarships.Data;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    ExnStarshipContext context;

    public UnitOfWork(ExnStarshipContext context)
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
