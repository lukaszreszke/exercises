namespace Tests.Documents;

public class PrinterFacade : IPrinterFacade
{
    public void Print(Document document)
    {
        throw new NotImplementedException();
    }
}

public interface IPrinterFacade
{
    void Print(Document document);
}