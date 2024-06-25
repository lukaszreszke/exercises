Consider the ResultServiceThatThrowsExceptions. It is a RestService that communicates with external resources to get important information.
However, it is designed in a way that it throws exceptions when it fails to communicate with the external resources.

The exceptions approach suited us well, but for new features the team decided to switch from using exceptions to using Result objects.

Your job is to decorate (use decorator pattern) to make the RestService return the Result object instead of throwing exceptions.

Make sure to write a test confirming that the RestService works as desired. 