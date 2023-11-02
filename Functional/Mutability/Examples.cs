using System.Collections;

namespace Functional.Mutability;

public sealed class SyntacticallyImmutable<T> : IEnumerable<T>
{
    // A mutable backing value is ok, as long as it is fully decoupled from the outside world.
    private readonly List<T> backingType;
    public SyntacticallyImmutable(IEnumerable<T> collection) => this.backingType = new List<T>(collection);
    public IEnumerator<T> GetEnumerator() => this.backingType.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

public sealed class SyntacticallyImmutableButAccidentallyMutable<T> : IEnumerable<T>
{
    private readonly List<T> backingType;
    public SyntacticallyImmutableButAccidentallyMutable(List<T> collection) => this.backingType = collection;
    public IEnumerator<T> GetEnumerator() => this.backingType.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

public sealed class SemanticAmbiguity<T>
{
    public List<T> Items { get; } // Am I "allowed" to add items to this "readonly" property?
    public IReadOnlyList<T> UnambiguouslyReadonly => Items;

    // Why a set accessor that takes an explicitly readonly(!) type as value?
    public IReadOnlyCollection<T> UtterlyConfusingSetter
    {
        set => Items.AddRange(value); // or similar.
    }
    public SemanticAmbiguity(IEnumerable<T> collection) => Items = new List<T>(collection);

    // likely other members...
}

public class OpenForMaliciousMutability<T>
{
    public IReadOnlyCollection<T> Items { get; }
    public OpenForMaliciousMutability(IReadOnlyCollection<T> supposedlyImmutable) => Items = supposedlyImmutable;
}

public static class Immutability
{
    public static void AccidentalMutability<T>(T item)
    {
        var mutable = new List<T>();
        // holding references to mutable arguments
        var notImmutable = new SyntacticallyImmutableButAccidentallyMutable<T>(mutable); // 0 elements!
        mutable.Add(item);
        Console.WriteLine($"Count: {notImmutable.Count()}"); // Count: 1

        // ambiguous interface
        var ambiguous = new SemanticAmbiguity<T>(mutable);
        ambiguous.Items.Add(item); // Is this possible by accident?
        ambiguous.UtterlyConfusingSetter = mutable;
    }

    // Name coined by Jon Skeet...
    public static void Popsicle<T>(ICollection<T> collection, T item)
    {
        collection.Add(item); // Will this throw?

        // it depends on the property "IsReadOnly".
        Console.WriteLine($"Will it throw? {(collection.IsReadOnly ? "Yes, it will." : "No, it won't.")}");

        // Yes, it will throw when an array is passed as argument:
        Popsicle(new T[] { item }, item);
    }

    public static void MaliciousMutability<T>(T item)
    {
        IReadOnlyCollection<T> someReadonlyCollection = new List<T>();

        // i) Type casting:
        var immutable = new OpenForMaliciousMutability<T>(someReadonlyCollection);
        // Accidentally works:
        ((List<T>)immutable.Items).Add(item); // no compile or runtime error.

        // ii) Type sub-classing:
        var mutableByDefinition = new MaliciousInheritance<T>();
        ThisReliesOnImmutability(mutableByDefinition);
        mutableByDefinition.Add(item);
        mutableByDefinition.Add(item);

        static void ThisReliesOnImmutability(OpenForMaliciousMutability<T> immutable)
        {
            // stuff that relies on "immutable" never to change as per api...
        }
    }
}

file class MaliciousInheritance<T> : OpenForMaliciousMutability<T>
{
    private readonly List<T> items;
    private MaliciousInheritance(List<T> items) // First malicious entry point if made public
        : base(items)
    {
        this.items = items;
    }

    public MaliciousInheritance() : this(new List<T>()) { } // Forces users to use the Add method below...

    // Second blatantly malicious entry point.
    public void Add(T item) => this.items.Add(item);
}