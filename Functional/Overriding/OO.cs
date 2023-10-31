using System.Diagnostics;

namespace Functional.Overriding;

public class AnimalBase : IAnimal // Only using base here as the functional example is called "Animal", too.
{
    // I chose not to use an abstract class here, even though it'd be better suited outside this example's context.
    public virtual void MakeSound()
    {
        throw new NotImplementedException("A subclass must implement this.");
    }
}

public class Cat : AnimalBase
{
    private readonly String name;
    public Cat(String name) => this.name = name;
    public override void MakeSound() => Console.WriteLine($"The cat '{this.name}' is meowing.");
}

public class Dog : AnimalBase
{
    private readonly String name;
    public Dog(String name) => this.name = name;
    public override void MakeSound() => Console.WriteLine($"The dog '{this.name}' is barking.");
}

public class Koa : AnimalBase
{
    private readonly String name;
    public Koa(String name) => this.name = name;
    public override void MakeSound() => Debug.WriteLine($"I don't know if koalas make sounds. If so, the koala '{this.name}' would be making a sound now.");
}