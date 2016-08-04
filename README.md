# Ways Of Using Task Lib

Nowadays, we are using multiple core systems. We must write our .NET applications in such way that we must utilize complete computing power of machine.

The task parallel library(TPL) allows you to write efficient code which is human readable, less error prone, and adjusts itself with the number of Cores available.

So your software would auto-upgrade with the upgrading environment. 
 
.Net framework provides System.Threading.Tasks.Task class to let you create threads and run them asynchronously.

Queuing a work item to a thread pool is useful, but there is no way to know when the operation has finished and what the return value is.
 
Task represents some work that should be done.

The parallel extensions and the task parallel library helps the developers to leverage the full potential of the available hardware capacity. The same code can adjust itself to give you the benefits across various hardware. It also improves the readability of the code and thus reduces the risk of introducing nasty bugs which drives developers crazy.
