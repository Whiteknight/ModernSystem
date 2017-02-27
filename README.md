# ModernSystem
A modern re-imagining of the .NET standard library with improved interfaces and patterns.

## Premise

The problem with the .NET standard library is that many of the classes, interfaces, methods, data structures and algorithms it defines were written over 15 years ago in C# 1.0. The .NET feature set has changed dramatically since the `System` namespace was first authored. Consider this partial list of features which have been added to the .NET runtime or the C# language in that time:

* Generics
* Lambdas and Expression trees
* Anonymous types
* async/await
* Extension methods
* Nullable
* `var` and `dynamic`
* Named and Optional arguments
* String Iterpolation

Besides a few orthogonal additions at each step, the core methods of the .NET standard library haven't been updated or modernized at all. This is largely because of concerns of backwards-compatibility and developer momentum, and these concerns are not trivial for Microsoft or the .NET Foundation to ignore. The only way to solve these problems is to create a wrapper library, a modernization layer over the standard library, which developers can opt-in to and upgrade or downgrade as required by their own individual needs.

ModernSystem is this layer. It contains wrappers, improvements, niceties and abstractions to provide access to standard-library features in a way that is modern and idiosyncratic to C# 6.0 and later. Modern System will provide better ways to access features which are old, crufty, ugly or strange (of which the .NET standard library has many features in each group).

By definition, what represents a "nicer" or "more modern" interface is open to subjective opinion, and ModernSystem won't pretend that there is a single way to wrap standard library features which will suit all needs or use-cases. 

It's also worth considering the fact that what is considered "modern" and "idiosyncratic" today won't be when the next version of C# is released, or when new design patterns are adopted. ModernSystem will make aggressive use of semantic versioning so that it has flexibility to change as necessary.

## Specific Goals

ModernSystem will do some of the following:

* Provide nicer abstractions and interfaces for features which are ugly or difficult to use
* Turn many static classes and methods, which are difficult to use in unit-testing and other scenarios, with concrete classes and instance methods.
* Provide abstractions and interfaces to allow the user to override and extend built-in behaviors
* Use established, modern design patterns and developer best practices where appropriate
* Provide some utility features and feature extensions which don't exist in the standard library but really should, because developers frequently re-implement them over and over again.

## Contributing

If you have some ideas for how to improve ModernSystem, or have suggestions for new features to add, please create a fork and open a pull request. I'm always looking for new comments, suggestions and contributed code.




