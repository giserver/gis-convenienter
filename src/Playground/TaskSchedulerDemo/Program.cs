// See https://aka.ms/new-console-template for more information

using System.Collections.Concurrent;

LimitedConcurrencyLevelTaskScheduler2 scheduler = new LimitedConcurrencyLevelTaskScheduler2();
scheduler.Start();

for (int i = 0; i < 10; i++)
{
    // Task.Factory.StartNew(() =>
    //     DoWork(i).Wait(), CancellationToken.None, TaskCreationOptions.LongRunning, scheduler);
    var t = i;
    scheduler.AddTask(()=>DoWork(t));
}

Console.WriteLine("Press any key to exit...");
Console.ReadKey();

static async Task DoWork(int workItemId)
{
    Console.WriteLine($"Work item {workItemId} is running on thread {Thread.CurrentThread.ManagedThreadId}");
    await Task.Delay( 3000);
}

public class LimitedConcurrencyLevelTaskScheduler2
{
    private ConcurrentQueue<Func<Task>> _tasks = new ConcurrentQueue<Func<Task>>();
    private int _maxTaskCount = 3;
    private int _currentTaskCount = 0;

    public void AddTask(Func<Task> task)
    {
        _tasks.Enqueue(task);
    }

    public void Start()
    {
        Task.Run(() =>
        {
            while (true)
            {
                if (_currentTaskCount < _maxTaskCount)
                {
                    if (_tasks.TryDequeue(out var task))
                    {
                        Interlocked.Increment(ref _currentTaskCount);
                        Task.Run( () =>
                        {
                             task.Invoke().ContinueWith((_, __) => { Interlocked.Decrement(ref _currentTaskCount); },
                                null);
                        });
                    }
                }
            }
        });
    }
}

public class LimitedConcurrencyLevelTaskScheduler : TaskScheduler
{
    private readonly int _maxDegreeOfParallelism;
    private readonly ConcurrentQueue<Task> _tasks = new ConcurrentQueue<Task>();
    private readonly CancellationTokenSource _cts = new CancellationTokenSource();
    private readonly object _lockObject = new object();
    private int _currentActiveTasks;

    public LimitedConcurrencyLevelTaskScheduler(int maxDegreeOfParallelism)
    {
        if (maxDegreeOfParallelism <= 0) throw new ArgumentOutOfRangeException("maxDegreeOfParallelism");
        _maxDegreeOfParallelism = maxDegreeOfParallelism;
    }

    public void AddTask(Task task)
    {
        QueueTask(task);
    }

    protected override void QueueTask(Task task)
    {
        _tasks.Enqueue(task);
    }

    protected override bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued)
    {
        return false;
    }

    protected override IEnumerable<Task> GetScheduledTasks()
    {
        return _tasks;
    }

    public void Start()
    {
        for (int i = 0; i < _maxDegreeOfParallelism; i++)
        {
            Thread thread = new Thread(async () =>
            {
                try
                {
                    while (!_tasks.IsEmpty || !_cts.Token.IsCancellationRequested)
                    {
                        Task task;
                        if (_tasks.TryDequeue(out task))
                        {
                            base.TryExecuteTask(task);
                        }
                        else
                        {
                            Thread.Yield();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Thread encountered an exception: " + ex.Message);
                }
            });

            thread.IsBackground = true;
            thread.Start();
        }
    }

    public void Stop()
    {
        _cts.Cancel();
    }
}