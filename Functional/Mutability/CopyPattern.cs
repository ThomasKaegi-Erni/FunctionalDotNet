using System.Collections;
using Microsoft.VisualBasic;

namespace Functional.Mutability;

public static class CopyPattern
{
    public static IEnumerable<Int32> Create()
    {
        var numbers = new ImmutableCollection<Int32>();
        do
        {
            Console.Write("Please type some number: ");
            String possibleNumber = Console.ReadLine()?.Trim() ?? String.Empty;
            if (Int32.TryParse(possibleNumber, out var number))
            {
                // notice the re-assignment of numbers!
                // This makes the change visible (even though it may be easy to miss)
                // Also: This may be prone to programming errors when used to mutable types.
                numbers = numbers.Add(number);
                Console.Write("Add more numbers? [y/n]: ");
            }

        } while (numbers.Count == 0 || Console.ReadLine()?.Trim().ToUpperInvariant() == "Y");

        return numbers;
    }

    public static void Use(IEnumerable<Int32> numbers)
    {
        Console.WriteLine($"You entered the numbers: [{String.Join(", ", numbers)}]");
    }
}

// These exist in System.Collections.Immutable ;-)
file sealed class ImmutableCollection<T> : IEnumerable<T>
{
    private readonly List<T> collection;
    public Int32 Count => this.collection.Count;
    public ImmutableCollection() => this.collection = new List<T>();
    public ImmutableCollection(IEnumerable<T> collection) => this.collection = new List<T>(collection);

    // Add an item by creating an immutable copy of the original collection.
    public ImmutableCollection<T> Add(T item) => new(this.Append(item));
    public IEnumerator<T> GetEnumerator() => this.collection.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
