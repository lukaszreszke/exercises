namespace DocumentExample;

public class DocumentAlreadyAcceptedBySide : Exception
{
    public Side Side { get; }

    public DocumentAlreadyAcceptedBySide(Side side)
    {
        Side = side;
    }
}