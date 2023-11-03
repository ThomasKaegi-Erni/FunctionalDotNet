using Functional.Mutability;

namespace Functional.Test;

public class ImmutabilityTest
{
    [Fact]
    public void SyntacticallyImmutable_IsImmutable()
    {
        IsImmutable(c => new SyntacticallyImmutable<Int32>(c));
    }

    [Fact]
    public void SyntacticallyImmutableButAccidentallyMutable_IsImmutable()
    {
        IsImmutable(c => new SyntacticallyImmutableButAccidentallyMutable<Int32>(c));
    }

    [Fact]
    public void OpenForMaliciousMutability_IsImmutable()
    {
        IsImmutable(c => new OpenForMaliciousMutability<Int32>(c).Items);
    }

    [Fact]
    public void ThePospsicle_DoesNotThrow()
    {
        Immutability.Popsicle(new List<Int32>(), 16);
        Assert.True(true);
    }

    private static void IsImmutable(Func<List<Int32>, IEnumerable<Int32>> create)
    {
        var nonEmptyCollection = new List<Int32> { 3, 4, 1, -5, 9 };
        var immutableCollection = create(nonEmptyCollection);
        var initialCount = immutableCollection.Count();

        nonEmptyCollection.Add(300);

        var countAfterInitMutation = immutableCollection.Count();
        Assert.Equal(initialCount, countAfterInitMutation);
    }
}