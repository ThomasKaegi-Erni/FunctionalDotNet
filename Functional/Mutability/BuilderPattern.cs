using System.Text;

namespace Functional.Mutability;

public static class BuilderPattern
{
    public static String Create()
    {
        var builder = new StringBuilder(); // a value that is mutable by design.
        // builders should never be assigned to a field or property. In other
        // words they only ever exist as variables that might be passed to
        // any number of pure functions (methods).
        do
        {
            Console.Write("Please type some characters: ");
            builder.Append(Console.ReadLine());
            Console.Write("Add more characters? [y/n]: ");

        } while (Console.ReadLine()?.Trim().ToUpperInvariant() == "Y");

        return builder.ToString(); // then use it to create the business object of interest
        // that may be kept as immutable state in a field or property.
    }

    public static void Use(String value)
    {
        Console.WriteLine($"This is my immutable value I can safely rely on: {value}");
    }

    public static void FullExample()
    {
        var immutableThing = Create();
        Use(immutableThing);
    }
}