# Functional C\#

## Characteristics of Functional Programming

- Strive for immutability
  - If there is state, it does not change.
  - Related: the lack of side effects.
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
- __O, L__: Only allow for one inheritance level.
  - seal all classes by default.
  - only use class inheritance as an implementation detail.
    - i.e. classes that inherit from classes ought to be private.
- __D__: Depend on interfaces only.

## References

- [Wikipedia article](https://en.wikipedia.org/wiki/Functional_programming)
- [Functional elements of C#](https://en.wikipedia.org/wiki/C_Sharp_(programming_language)#Functional_programming)
- [Monads](https://ericlippert.com/2013/02/21/monads-part-one/)
