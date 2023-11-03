using System.Diagnostics;

namespace Functional.Overriding;

public sealed class Animal : IAnimal
{
    // Note, in Java we could do this example much more elegantly
    // using "functional interfaces".
    private readonly Action makeSound;
    private Animal(Action makeSound) => this.makeSound = makeSound;
    public void MakeSound() => this.makeSound();

    public static IAnimal Cat(String name)
    {
        return new Animal(() => Console.WriteLine($"The cat '{name}' is meowing."));
    }
    public static IAnimal PersianCat(String name)
    {
        return new Animal(() => Console.WriteLine($"The cat '{name}' is purring."));
    }
    public static IAnimal Dog(String name)
    {
        return new Animal(() => Console.WriteLine($"The dog '{name}' is barking."));
    }
    public static IAnimal Koala(String name)
    {
        return new Animal(() => Debug.WriteLine($"I don't know if koalas make sounds. If they do, the koala '{name}' would be making a sound now."));
    }
    public static IAnimal Wolpertinger(String name)
    {
        // (https://en.wikipedia.org/wiki/Wolpertinger)
        return new Animal(() => MakeWolpertingerSound(name));

        static void MakeWolpertingerSound(String name)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"The bavarian wolpertinger '{name}' says hi!");
            Console.ResetColor();
        }
    }
}

// This is conceptually equivalent to the above, just a bit more verbose to implement...
public abstract class AnimalAlternative : IAnimal
{
    private readonly String name;

    // Notice the private constructor...
    private AnimalAlternative(String name) => this.name = name;
    public abstract void MakeSound();

    public static IAnimal Cat(String name) => new CatImpl(name);
    public static IAnimal Dog(String name) => new DogImpl(name);
    public static IAnimal Koala(String name) => new KoalaImpl(name);

    private sealed class CatImpl : AnimalAlternative
    {
        public CatImpl(String name) : base(name) { }
        public override void MakeSound() => Console.WriteLine($"The cat '{this.name}' is meowing.");
    }

    private sealed class DogImpl : AnimalAlternative
    {
        public DogImpl(String name) : base(name) { }
        public override void MakeSound() => Console.WriteLine($"The dog '{this.name}' is barking.");
    }

    private sealed class KoalaImpl : AnimalAlternative
    {
        public KoalaImpl(String name) : base(name) { }
        public override void MakeSound() => Debug.WriteLine($"I don't know if koalas make sounds. If they do, the koala '{this.name}' would be making a sound now.");
    }
}
