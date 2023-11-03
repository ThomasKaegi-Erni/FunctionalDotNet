# Functional C\#

There are two aspects of functional programming with C\#.

1. Using lambda expressions everywhere.
1. Designing C\# components with functional aspects in mind.

This repo is all about the second aspect: taking the best of both worlds into account in your C\# designs.

As per the first aspect: please don't do that, as that's what [F\#](https://en.wikipedia.org/wiki/F_Sharp_(programming_language)) is for :-)

## Characteristics of Functional Programming

- Strive for immutability
  - If there is state, it does not change.
  - Related:
    - the lack of side effects.
    - pure implementations (idempotency).
- Composable by nature
- Functions are first class citizens
  - e.g: Linq is an extension on top of `IEnumerable` using aspects of functional paradigms.
  - Some even as "higher order functions"
    - see also Eric Lippert's awesome series on [Monads](https://ericlippert.com/2013/02/21/monads-part-one/).

## Applied to C\#

Functional programming in C\# boils down to taking the SOLID principles to the extreme.

- __S, I__: Keep interfaces extremely narrow.
  - _one_ member only, _two_ at most.
  - _three_ only when the third member inherits from `IDisposable`.
  - This enables composability.
- __O, L__: Only allow for one inheritance level.
  - seal all classes by default.
  - only use class inheritance as an implementation detail.
    - i.e. classes that inherit from classes ought to be private.
  - Don't allow for hidden state changes.
  - This enables easy reasoning about your code. (immutability, etc...)
- __D__: Depend on interfaces only.
  - This also enables composability.

With these principles in place, it becomes "trivial" to:

- Extend or change functionality
  - Just create new composable implementations of the very narrow interfaces
  - As the interfaces are narrow, they are easy to implement and reason about
    - Liskov's substitution principle ought to trivially hold
  - New implementations can easily be injected, as "everything" depends on abstractions.
- Reason about correctness
  - Existing components don't need to be touched
  - There is no hidden state one needs to _worry_ about
    - This is in stark contrast to pure OO that is all about encapsulation (of state & state changes)
    - State changes happen in the open, via obviously changing components
  - Without mutable state, there are simply _no threading issues_ to worry about.
- Testing is simple, precisely because of above reasons.

Naturally, in big systems the ideal case won't apply. But if the principles are followed, the system will tend to be more easy to reason about and hence also change.

### Caveats

The biggest caveat is instantiation. Injection via IoC quickly becomes unwieldy as most IoC Frameworks don't know how to handle injection of multiple instances of different implementations of the same interface...

Also, the instantiation tree can become very big and complex quite quickly. However, with dependencies that resolve at compile time (as opposed to IoC runtime resolution), it should remain manageable.

An (anti) pattern that may emerge quite quickly is the appearance of many factories or even factory-factories. Keep these in check early on.

## References

- [Wikipedia article](https://en.wikipedia.org/wiki/Functional_programming)
- [Functional elements of C#](https://en.wikipedia.org/wiki/C_Sharp_(programming_language)#Functional_programming)
- [Monads](https://ericlippert.com/2013/02/21/monads-part-one/)
