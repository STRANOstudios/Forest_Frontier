public abstract class Node
{
    protected Status status;

    public Status Status
    {
        get { return status; }
    }

    public abstract Status Execute();
}

public enum Status
{
    Success,
    Failure,
    Running
}
