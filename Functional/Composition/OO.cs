namespace Functional.Composition;

// How many reasons to change does any one of these components have?

// This superclass abstracts away the business rule
// that all persistence must be logged.
public abstract class PersistenceBase : IPersist
{
    private readonly ILog logger;
    private readonly String persistenceType;
    protected PersistenceBase(ILog logger) // Constructor dependency on ILog :-(
    {
        this.logger = logger;
        this.persistenceType = this.GetType().Name.Replace("Persistence", String.Empty); // not a clean design...
    }
    public void Persist(String text)
    {
        this.logger.Log($"Persisting to {this.persistenceType}: {text}");
        try
        {
            PersistImplementation(text);
        }
        catch (Exception e)
        {
            this.logger.Log("Persisting failed with:");
            this.logger.Log(e);
            throw;
        }
        this.logger.Log("Done persisting.");
    }

    protected abstract void PersistImplementation(String text);
}

public class FilePersistence : PersistenceBase
{
    private readonly FileInfo path;
    public FilePersistence(FileInfo path, ILog logger)
        : base(logger)
    {
        this.path = path;
    }
    protected override void PersistImplementation(String text)
    {
        using var file = this.path.AppendText();
        file.Write(text);
    }
}

public class DbPersistence : PersistenceBase
{
    private readonly String connectionString;
    public DbPersistence(String connectionString, ILog logger)
        : base(logger)
    {
        this.connectionString = connectionString;
    }
    protected override void PersistImplementation(String text)
    {
        // persist to DB...
    }
}

// How would you go about designing persistence to db and(!) file?
