using System.Text;

namespace Functional.Mutability;

public static class BuilderPattern
{
    public static String Create()
    {
        var builder = new StringBuilder(); // a value that is mutable by design.
        do
        {
            Console.Write("Please type some characters: ");
            builder.Append(Console.ReadLine());
            Console.Write("Add more characters? [y/n]: ");

        } while (Console.ReadLine()?.Trim().ToUpperInvariant() == "Y");

        return builder.ToString();
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