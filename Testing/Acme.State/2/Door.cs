namespace Acme.State._2;

public enum DoorState
{
    Open,
    Closed,
    Locked
}

public class Door
{
    public DoorState State { get; private set; }

    public Door()
    {
        State = DoorState.Closed;
    }

    public void Open()
    {
        if (State == DoorState.Closed)
        {
            State = DoorState.Open;
        }
        else if (State == DoorState.Locked)
        {
            throw new CannotOpenLockedDoorException();
        }
        else
        {
            throw new DoorAlreadyOpenException();
        }
    }

    public void Close()
    {
        if (State == DoorState.Open)
        {
            State = DoorState.Closed;
        }
        else if (State == DoorState.Closed)
        {
            throw new DoorAlreadyClosedException();
        }
        else
        {
            throw new CannotCloseLockedDoorException();
        }
    }

    public void Lock()
    {
        if (State == DoorState.Closed)
        {
            State = DoorState.Locked;
        }
        else if (State == DoorState.Locked)
        {
            throw new DoorAlreadyLockedException();
        }
        else
        {
            throw new CannotLockOpenDoorException();
        }
    }

    public void Unlock()
    {
        if (State == DoorState.Locked)
        {
            State = DoorState.Closed;
        }
        else
        {
            throw new DoorNotLockedException();
        }
    }
}

public class DoorAlreadyOpenException : Exception
{
}

public class DoorAlreadyClosedException : Exception
{
}

public class CannotCloseLockedDoorException : Exception
{
}

public class DoorAlreadyLockedException : Exception
{
}

public class CannotLockOpenDoorException : Exception
{
}

public class DoorNotLockedException : Exception
{
}

public class CannotOpenLockedDoorException : Exception
{
}
