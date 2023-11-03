using System.Diagnostics;

namespace Functional.Overriding;

// The superclass pattern...
public class AnimalBase : IAnimal // Only using base here as the functional example is called "Animal", too.
{
    private readonly String name;
    protected AnimalBase(String name) => this.name = name; // now "all" animals are expected to have names.
    public void MakeSound() => MakeSound(this.name);

    // I chose not to use an abstract class here, even though it'd be better suited outside this example's context.
    protected virtual void MakeSound(String name)
    {
        throw new NotImplementedException("A subclass must implement this.");
    }
}

public class Cat : AnimalBase
{
    public Cat(String name) : base(name) { }
    protected override void MakeSound(String name) => Console.WriteLine($"The cat '{name}' is meowing.");
}

// Modelling cladistic relationships, too..
public class PersianCat : Cat
{
    public PersianCat(String name) : base(name) { }
    protected override void MakeSound(String name) => Console.WriteLine($"The cat '{name}' is purring.");
}

public class Dog : AnimalBase
{
    public Dog(String name) : base(name) { }
    protected override void MakeSound(String name) => Console.WriteLine($"The dog '{name}' is barking.");
}

public class Koala : AnimalBase
{
    public Koala(String name) : base(name) { }
    protected override void MakeSound(String name) => Debug.WriteLine($"I don't know if koalas make sounds. If they do, the koala '{name}' would be making a sound now.");
}

// What do we do if we need to implement the Wolpertinger? (https://en.wikipedia.org/wiki/Wolpertinger)
// From which class would we inherit: Rabbit, Deer, Bird or Squirrel?
// Probably we should inherit from all, but C# can't do that...
// Or do we break the pattern and just implement IAnimal?
// Or do we take that as the reason to switch to C++ that allows multiple inheritance?
// This can get arbitrarily absurd...

// A slightly more realistic example is the Hyena.
// It used to be classified as a Canine (Dogs), but recent evidence suggests it's more closely related
// to cats... But why would our software design need to model the cladistic relationships of animals
// if all our software needs is the ability for your animals to make different sounds?
