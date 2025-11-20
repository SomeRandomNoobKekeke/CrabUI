Pathetic attempts to keep track of versions and dependencies of libs

Coolstory: For a very long time i was making one small mod after another
When I needed some code that i had already written i was taking it from previous mod
But one day i was deving two mods at once and had created 2 versions of the same lib
I didn't know which one should i use in next mod so i decided to merge them in a separate mod
And i was merging such shared libs in a separate mod since then, but it turned out that very big libs are 
hard to merge frequently and thus it's super easy to make some small change and forget about it

So now when i change some lib i increment its version, and i keep track of the chain of versions it's based on
Let's see if it works

Also i can print only used libs because they register their version in static constructors, and i can check for incompatible libs 
