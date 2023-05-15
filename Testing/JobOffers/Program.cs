using System.Net;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

public class JobOffer
{
    public string CompanyName { get; set; }
    public string Position { get; set; }
    public decimal? MinBudget { get; set; }
    public decimal? MaxBudget { get; set; }
    public DateTime? DatePosted { get; set; }
    public bool PremiumOffer { get; set; }
}

public class InvalidJobOffer : JobOffer
{
}

public class JobContext : DbContext
{
    public DbSet<JobOffer> JobOffers { get; set; }
    public DbSet<InvalidJobOffer> InvalidJobOffers { get; set; }
}

public class Program
{
    static void Main(string[] args)
    {
        WebClient client = new WebClient();
        try
        {
            string xmlData = client.DownloadString("http://api.example.com/jobOffers");

            XDocument doc = XDocument.Parse(xmlData);
            var jobOffers = doc.Descendants("JobOffer")
                .Select(x => new JobOffer
                {
                    CompanyName = (string)x.Element("CompanyName"),
                    Position = (string)x.Element("Position"),
                    MinBudget = (int?)x.Element("MinBudget"),
                    MaxBudget = (int?)x.Element("MaxBudget"),
                    DatePosted = (DateTime?)x.Element("DatePosted")
                }).ToList();

            foreach (var jobOffer in jobOffers)
            {
                if (!jobOffer.MinBudget.HasValue && !jobOffer.MaxBudget.HasValue ||
                    jobOffer.PremiumOffer && jobOffer.MinBudget.HasValue && !jobOffer.MaxBudget.HasValue)
                {
                    try
                    {
                        string jsonBudgetData =
                            client.DownloadString(
                                $"http://anotherapi.example.com/jobOffers/{jobOffer.CompanyName}/budget");
                        dynamic budgetData = JsonConvert.DeserializeObject(jsonBudgetData);
                        jobOffer.MinBudget = budgetData.MinBudget;
                        jobOffer.MaxBudget = budgetData.MaxBudget;
                    }
                    catch (WebException ex) when ((ex.Response as HttpWebResponse)?.StatusCode >=
                                                  HttpStatusCode.BadRequest &&
                                                  (ex.Response as HttpWebResponse)?.StatusCode <
                                                  HttpStatusCode.InternalServerError)
                    {
                        throw new ApplicationException("Error retrieving budget data from the API", ex);
                    }
                }
            }

            using (JobContext context = new JobContext())
            {
                foreach (var jobOffer in jobOffers)
                {
                    if (jobOffer.MinBudget.HasValue && jobOffer.MaxBudget.HasValue ||
                        jobOffer.PremiumOffer && jobOffer.MinBudget.HasValue && !jobOffer.MaxBudget.HasValue)
                    {
                        context.JobOffers.Add(jobOffer);
                    }
                    else
                    {
                        context.InvalidJobOffers.Add(new InvalidJobOffer
                        {
                            CompanyName = jobOffer.CompanyName,
                            Position = jobOffer.Position,
                            MinBudget = jobOffer.MinBudget,
                            MaxBudget = jobOffer.MaxBudget,
                            DatePosted = jobOffer.DatePosted
                        });
                    }
                }

                context.SaveChanges();
            }
        }
        catch (WebException ex) when ((ex.Response as HttpWebResponse)?.StatusCode >= HttpStatusCode.BadRequest &&
                                      (ex.Response as HttpWebResponse)?.StatusCode < HttpStatusCode.InternalServerError)
        {
            throw new ApplicationException("Error retrieving job offers from the API", ex);
        }
    }
}