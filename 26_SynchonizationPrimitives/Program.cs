using _26_SynchonizationPrimitives;

object syncObj = new();
bool lockTaken = false;
try
{
    Monitor.Enter(syncObj, ref lockTaken);
    await Task.Delay(1000);
}
finally
{
    if (lockTaken)
    {
        Monitor.Exit(syncObj);
    }
}

return;


SemaphoreExample.Main();
