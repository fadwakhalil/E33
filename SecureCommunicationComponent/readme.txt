Question 7:
I'm very much convinced that a user will most probably forget to dispose as much as I'm convinced that the user is a human being.
So from my readings and research and while working on this tricky assignmment, the following are points to concider to overcome
this issue:
1. I have seen several posts asking when to dispose and when to use a finalizer. From my understanding, both are important in a 
designed solution.
Implementing a finalizer along with the dispose can free resources when Dispose is not called to take care of those objects that the 
GC is unable to cleanup.
2. Since Finalizers can reduce performance, we need to prevent finalization to kick in anytime as it's unpredictable, 
by creating the instance with a using statement obviously it automatically calls Dispose on the object when it's not used anymore.

So basically, Finalize and Dispose do a cleanup from inside the Dispose(bool) which provides false to indicate that the finalizer 
kicked in versus a Dispose call. This is the best scenario in my opinion. 
It's worth mentioning here that such a scenario can vary depending on the resources of the object we are working with, mostly unmanaged resources.