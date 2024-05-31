namespace ExampleCore.ZookeperSemafor.Interfaces;

public interface IDistrebutedSemafor
{
    Task<LockHandler> AcquireAsync(TimeOutValue timeOut, CancellationToken cancellationToken = default);
    Task ReleaseAsync(string nodePath);
}