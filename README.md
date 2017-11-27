# `HttpMessageHandler` usage example.

A minimal example showing a a `HttpMessageHandler` can be used to intercept and modify HTTP requests done by `HttpClient`.

[FancyService](Services/FancyService.cs) has an dependency on `HttpClient` which it gets through constructor injection. We initialize the actual instance of `HttpClient` in [Startup.cs](Startup.cs#L33) in a way that it uses the [FancyMessageHandler](MessageHandlers/FancyMessageHandler.cs). All it does is to increment an `int` on each request done and adds it to the HTTP header.

All `FancyService` is doing is it's making requests to <https://httpbin.org/anything> - An API echoing the incoming HTTP request in its response payload. This response is pretty printed in order to show how `FancyMessageHandler` is able to interact on the payload without `FancyService` knowing about it.