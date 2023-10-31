namespace Functional.Composition;

// i) Let's assume we started off with one persistence layer and
//    the business rule to log all persistence operations.

// ii) Let's further assume we've just received the change request (CR)
//     to not only log to DB, but also to the file system.
public static class Usage
{
    public static IPersist Functional(ILog logger)
    {
        var file = new ToFile(new FileInfo("myPath")); // CR!
        var db = new ToDb("my connection string"); // unchanged
        var persistenceComposition = Composition.Chain(file, db); // CR!
        // note how we have clean separation of concerns until now.

        // What do you think, does the persistence logger violate SoC?
        var loggingComposition = new PersistenceLogger(persistenceComposition, logger); // minor change
        return loggingComposition;

        // Challenge: What if we added the business rule that we must log to which destination we persist?
        // Critique:  But what problem do we have here with many DI IoC implementations?
    }

    public static IPersist ObjectOriented(ILog logger)
    {
        var file = new FilePersistence(new FileInfo("myPath"), logger); // CR!
        var db = new DbPersistence("my connection string", logger); // unchanged

        // But OO now poses a dilemma: how do I combine these?
        // Do I use the same design pattern, or start a new pattern?

        // let's just fall back to the functional pattern for now...
        return Composition.Chain(file, db); // CR!

        // But what problem do we now have? How would we fix it?
    }
}