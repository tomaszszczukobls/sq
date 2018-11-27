# Simple Queue
For implementation SimpleQueue I use 'lock' statement. It is very simple mechanism for lock the block code.
Lock internaly uses 'Monitor' for synchronize access to object. Monitor is fast but it is blocking (therefore is not good for every solution).
I'm using lock statement in many application from years.

Documentation for 'lock':
https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/lock-statement
