namespace Functional.InterfaceDesign;

// These interfaces model reading and writing in terms of IO.
// - wire communication (serial, TCP, Bluetooth, etc)
// - storage devices
// - intermediates (e.g. caching)

public interface IRead<T>
{
    IAsyncEnumerable<T> Read(CancellationToken token);
}

public interface IWrite<T>
{
    Task Write(IEnumerable<T> data, CancellationToken token);
}

// Three methods: Read, Write & Dispose
// - Read and Write are symmetric
// - Dispose is the only "allowed" third method.
public interface IConnection<T> : IRead<T>, IWrite<T>, IDisposable
{ }