Builder is creational pattern that allows creating complex objects step by step.
It is useful when you need to create an object with a lot of configuration options.
Builder is a good alternative to telescoping constructor antipattern.
Telescoping constructor antipattern is a pattern where you have a class with multiple constructors,
each with a different number of parameters.

Builder pattern often provides fluent interface, which allows you to chain method calls.

## Exercise 

Add possibility to set the logging verbosity to the builder example.