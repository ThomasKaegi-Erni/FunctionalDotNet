namespace Functional.Composition;

// This set of persistence components abstracts away the
// business rule that all persistence must be logged.

// How many reasons to change does any one of these components have?

public sealed class PersistenceLogger : IPersist
{
    private readonly ILog logger;
    private readonly IPersist persistence;
    public PersistenceLogger(IPersist persistence, ILog logger)
    {
        this.persistence = persistence;
        this.logger = logger;
    }
    public void Persist(String text)
    {
        this.logger.Log($"Persisting: {text}");
        try
        {
            this.persistence.Persist(text);
        }
        catch (Exception e)
        {
            this.logger.Log("Persisting failed with:");
            this.logger.Log(e);
            throw;
        }
        this.logger.Log("Done persisting.");
    }
}

// This is a pattern I have found to be immensely useful in many projects
// It harks back to the mathematical nature of functional programming.
// This impl. represents the Identity element (that does nothing).
public sealed class NoPersistence : IPersist
{
    public void Persist(String text) { /* This is the empty implementation */ }
}

public sealed class ToFile : IPersist
{
    private readonly FileInfo path;
    public ToFile(FileInfo path) => this.path = path;
    public void Persist(String text)
    {
        using var file = this.path.AppendText();
        file.Write(text);
    }
}

public sealed class ToDb : IPersist
{
    private readonly String connectionString;
    public ToDb(String connectionString) => this.connectionString = connectionString;
    public void Persist(String text)
    {
        // persist to DB.
    }
}

public sealed class Composition : IPersist
{
    private readonly IPersist first, second; // We could also choose to use a collection here...
    private Composition(IPersist first, IPersist second) => (this.first, this.second) = (first, second);
    public void Persist(String text)
    {
        this.first.Persist(text);
        this.second.Persist(text);
    }
    public static IPersist Chain(IPersist first, IPersist second) => new Composition(first, second);
    public static IPersist Chain(params IPersist[] chain) => chain.Length switch
    {
        0 => new NoPersistence(), // Although, one could argue this violates our business rule... (But does this matter here?)
        1 => chain[0],
        2 => new Composition(chain[0], chain[1]),
        // Default case using recursive composition :-)
        _ => chain.Aggregate((IPersist)new NoPersistence(), (first, second) => new Composition(first, second))
    };
}