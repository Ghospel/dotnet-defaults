# Unit Tests

Unit Tests are best written before writing the code. While we don't force TDD it is a good idea to follow the following paradigm when implementing Unit Tests in your project.

When writing Unit tests always make sure they test the smallest scope possible. The smaller the scope, the easier it is to write the test. Making sure you have a good grasp of the requirements needed to satisfy the ticket you're working on also makes it easier to write tests and keep them simple.

## Red, Green, Refactor

**Red, green, refactor** is a method to write Unit Tests before writing any implementation code. Working this was enforces the use of good coding principles (SOLID) and makes the code more maintainable decreasing the changes of unexpected behaviour when the project becomes more complex.

The steps when doing **red, green, refactor** are the following:

**Red** - Think a about what you want to develop, what the input is and what the outour had to be and write a test for this. This test will always fail at this step as we have not written the code yet.

**Green** - Think about what you need to do to pass the test and write the minimal amount of code to achieve success. Keep in mind that it is not about the most efficient/performant way to implement the code, but it's about passing the test. Optimizing will be done in the next step.

**Refactor** - Think about how to improve your code, not just the implementation, but the test as well! This is the step where you refactor and optimize the code. Doing the same loop twice? Extract a method! having 10 if/else statments? Maybe a switch is better. Reusing the same value in a test? Create a (setup) variable for it. 

## Unit Tests a simple example:

For Heineken we sometimes get the question to add url rewrites. For this I created a tool that can read an excel provided by Heineken which is then transformed into a [rewriteMap](https://docs.microsoft.com/en-us/iis/extensions/url-rewrite-module/using-rewrite-maps-in-url-rewrite-module). 

This tool uses an ExcelHandler to load the excel and read that into a c# object. The LoadExcel method accepts a fileStream as a parameter:
```csharp
ExcelHandler.LoadExcel(fileStream)
```

**Red**: An example of a test for the function would be to check for ArgumentNullException when the fileStream provided is null:
```csharp
[Test]
public void WhenFileStreamInNull_ArgumentNullExceptionIsThrown()
{
    Assert.Catch<ArgumentNullException>(() => 
    new ExcelHandler.LoadExcel(null));
}
```

**Green**: The initial implementation for satisfying this test then could be:
```csharp
public class ExcelHandler
{
    public void LoadExcel(Stream fileStream)
    {
        if (fileStream == null)
        {
            throw new ArgumentNullException();
        }
    }
}
```

**Refactor**: Because we don't need an instance of the ExcelHandler and the class is not depending on other dependencies we can make the class and method static:

```csharp
public static class ExcelHandler
{
    public staic void LoadExcel(Stream fileStream)
    {
        if (fileStream == null)
        {
            throw new ArgumentNullException();
        }
    }
}
```

This means we can also refactor the test:
```csharp
[Test]
public void WhenFileStreamInNull_ArgumentNullExceptionIsThrown()
{
    Assert.Catch<ArgumentNullException>(() => ExcelHandler.LoadExcel(null));
}
```