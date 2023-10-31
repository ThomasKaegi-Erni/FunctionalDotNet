using System.Diagnostics;

namespace Functional.Overriding;

public sealed class Animal : IAnimal
{
    // Note, in Java we could do this example much more elegantly
    // using "functional interfaces".
    private readonly Action makeSound;
    private Animal(Action makeSound) => this.makeSound = makeSound;
    public void MakeSound() => this.makeSound();

    public static Animal Cat(String name)
    {
        return new(() => Console.WriteLine($"The cat '{name}' is meowing."));
    }
    public static Animal Dog(String name)
    {
        return new(() => Console.WriteLine($"The dog '{name}' is barking."));
    }
    public static Animal Koala(String name)
    {
        return new(() => Debug.WriteLine($"I don't know if koalas make sounds. If so, the koala '{name}' would be making a sound now."));
    }
}