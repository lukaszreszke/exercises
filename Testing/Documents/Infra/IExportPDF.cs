namespace Tests.Documents;

public interface IExportPDF
{
    double GetExportVersion();
    Uri Export(Document document);
}