namespace Acme.TemplateMethod._3;

public class EmployeeOvertimeReportGenerator
{
    private readonly IOvertimeRepository _overtimeRepository;
    private readonly IUploadResultToWebPage _uploader;
    private readonly IHTMLSprinkler _htmlSprinkler;
    private readonly IMailTheReportToHR _mailer;

    public EmployeeOvertimeReportGenerator(
        IOvertimeRepository overtimeRepository,
        IUploadResultToWebPage uploader,
        IHTMLSprinkler htmlSprinkler,
        IMailTheReportToHR mailer)
    {
        _overtimeRepository = overtimeRepository;
        _uploader = uploader;
        _htmlSprinkler = htmlSprinkler;
        _mailer = mailer;
    }
    
    public string GenerateReport(string output)
    {
        var data = _overtimeRepository.EmployeeOvertimeData();
        
        if (output == "HTML")
        {
            var result = "<table><tr><th>Employee Name</th><th>Overtime Hours</th></tr>";
            result += data.Select(d => $"<tr><td>{d.EmployeeName}</td><td>{d.OvertimeHours}</td></tr>")
                .Aggregate((current, next) => current + next);
            
            _htmlSprinkler.MakeItPretty(ref result);
            
            _uploader.Upload(result);

            return result;
        } else
        {
            // csv output
            var result = "Employee Name,Overtime Hours";
            result += Environment.NewLine;
            result += data.Select(d => $"{d.EmployeeName},{d.OvertimeHours}")
                .Aggregate((current, next) => current + next);
            
            _mailer.Send(result);
            
            return result;
        }
    }
}

public interface IHTMLSprinkler
{
    void MakeItPretty(ref string result);
}

public interface IUploadResultToWebPage
{
    void Upload(string result);
}

public interface IMailTheReportToHR
{
    void Send(string result);
}

public class EmployeeOvertimeData
{
    public string EmployeeName { get; set; }
    public int OvertimeHours { get; set; }
}

public interface IOvertimeRepository
{
    public List<EmployeeOvertimeData> EmployeeOvertimeData();
}