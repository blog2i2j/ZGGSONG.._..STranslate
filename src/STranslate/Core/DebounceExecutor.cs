namespace STranslate.Core;

public sealed class DebounceExecutor : IDisposable
{
    private CancellationTokenSource? _cts;
    private readonly object _lock = new();  // 避免并发调用时的竞态条件

    /// <summary>
    /// 同步动作防抖执行
    /// </summary>
    public void Execute(Action action, TimeSpan delay)
    {
        lock (_lock)
        {
            _cts?.Cancel();
            _cts?.Dispose();  // 释放旧的 CancellationTokenSource
            _cts = new CancellationTokenSource();
        }

        var token = _cts.Token;

        Task.Run(async () =>
        {
            try
            {
                await Task.Delay(delay, token);

                if (!token.IsCancellationRequested)
                {
                    action.Invoke();
                }
            }
            catch (TaskCanceledException)
            {
                // 忽略取消
            }
        }, token);
    }

    /// <summary>
    /// 异步动作防抖执行 (支持 async/await)
    /// </summary>
    public void ExecuteAsync(Func<Task> asyncAction, TimeSpan delay)
    {
        lock (_lock)
        {
            _cts?.Cancel();
            _cts?.Dispose();
            _cts = new CancellationTokenSource();
        }

        var token = _cts.Token;

        Task.Run(async () =>
        {
            try
            {
                await Task.Delay(delay, token);

                if (!token.IsCancellationRequested)
                {
                    await asyncAction();
                }
            }
            catch (TaskCanceledException)
            {
                // 忽略取消
            }
        }, token);
    }

    public void Dispose()
    {
        _cts?.Cancel();
        _cts?.Dispose();
    }
}