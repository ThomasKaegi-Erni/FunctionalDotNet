using Functional.Composition;

namespace Functional.Test;

public class PersistenceLoggingTest
{
    [Fact]
    public void PersistLogsTheSameTextThatWasPersisted_Functional()
    {
        const String text = "Some text";
        var logger = new FakeLogger();
        var persistence = new FakePersistence();
        var persistenceLogger = new PersistenceLogger(persistence, logger);

        persistenceLogger.Persist(text);

        Assert.Equal(new[] { text }, persistence.Data);
        Assert.Collection(logger.Statements, statement => Assert.EndsWith(text, statement));
        // Arguably easier to test, with zero (external) dependencies.
    }

    [Fact]
    public void PersistLogsTheSameTextThatWasPersisted_ObjectOriented()
    {
        // Often OO tests will look something like this:
        const String text = "Some text";
        var persistedText = "nothing was persisted";
        var tempPath = new FileInfo(Path.GetTempFileName()); // ugly...
        var logger = new FakeLogger();
        var persistence = new FilePersistence(tempPath, logger);
        try
        {
            persistence.Persist(text);
            persistedText = File.ReadAllText(tempPath.FullName); // ugly...
        }
        finally
        {
            // ugly and error prone...
            tempPath.Delete(); // Clever DevOps may make this redundant. Locally, still of value though...
        }

        // The final assertion here reads more clearly than in the functional example.
        Assert.Collection(logger.Statements, statement => Assert.EndsWith(persistedText, statement));
    }

    [Fact]
    public void PersistLogsTheSameTextThatWasPersisted_AlternativeObjectOriented()
    {
        // Luckily with this example and fakes, the OO version can just as easily be
        // tested as the functional example.
        // However, would we have found this solution without the functional example??

        const String text = "Some text";
        var logger = new FakeLogger();
        var persistence = new FakePersistenceOO(logger);

        persistence.Persist(text);

        Assert.Equal(new[] { text }, persistence.Data);
        Assert.Collection(logger.Statements, statement => Assert.EndsWith(text, statement));
    }
}

// Corollary: Fakes are almost trivial...
file class FakeLogger : ILog
{
    public List<String> Statements { get; } = new();
    public void Log(String message) => Statements.Add(message);
    public void Log(Exception exception) => Log(exception.Message);
}

file class FakePersistence : IPersist
{
    public List<String> Data { get; } = new();
    public void Persist(String text) => Data.Add(text);
}

file class FakePersistenceOO : PersistenceBase
{
    public List<String> Data { get; } = new();
    public FakePersistenceOO(ILog logger) : base(logger) { }
    protected override void PersistImplementation(String text) => Data.Add(text);
}