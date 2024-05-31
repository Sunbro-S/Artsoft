namespace ExampleCore.ZookeperSemafor;

public class LockHandler: IAsyncDisposable
{
    private readonly DistributedSemafor _semafore;
    private readonly string _nodePath;

    public LockHandler(DistributedSemafor semafore, string nodePath)
    {
        _semafore = semafore;
        _nodePath = nodePath;
    }

    public async ValueTask DisposeAsync()
    {
        await _semafore.ReleaseAsync(_nodePath)
            .ConfigureAwait(false);
    }
}