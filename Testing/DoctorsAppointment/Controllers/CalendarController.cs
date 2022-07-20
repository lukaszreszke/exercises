using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestSharp;

namespace DoctorsAppointment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalendarController : ControllerBase
    {
        // GET: api/Calendar
        [HttpGet]
        public async Task<IEnumerable<TimeSchedule>> Get()
        {
            var request = new RestRequest().AddHeader("Content-Type", "application/json")
                .AddHeader("Accept", "application/json");
            var nfz = await new RestClient("https://nfz.pl?doctor=jakies_id").GetAsync<NfzResponse>(request);
            var luxMed = await new RestClient("https://luxmed.pl?doctor=jakies_id").GetAsync<LuxMedResponse>(request);
            var doctorsPrivateClinic =
                await new RestClient("https://luxmed.pl?doctor=jakies_id").GetAsync<PrivateClinicResponse>(request);

            var privateClinicTimeSchedule = doctorsPrivateClinic.TimeSchedules.Select(x =>
                    new TimeSchedule(x.Day,
                        x.Appointments.Select(y => new Appointment(y.When, y.PatientId, y.PatientName))))
                .ToList();
            var luxmedTimeSchedule = luxMed.TimeSchedules.Select(luxmedTimeSchedule => new Appointment(luxmedTimeSchedule.Key,
                    Guid.Parse(luxmedTimeSchedule.Value.Id), luxmedTimeSchedule.Value.FullName))
                .GroupBy(x => DateOnly.FromDateTime(x.When),
                    (date, appointment) => new TimeSchedule(date, appointment))
                .ToList();
            var nfzTimeSchedule = nfz.Calendar.Select(nfzSchedule => new Appointment(nfzSchedule.DateTime,
                    nfzSchedule.ClientId, $"{nfzSchedule.FirstName} {nfzSchedule.LastName}"))
                .GroupBy(x => DateOnly.FromDateTime(x.When),
                    (date, appointment) => new TimeSchedule(date, appointment))
                .ToList();

            return privateClinicTimeSchedule.Concat(luxmedTimeSchedule).Concat(nfzTimeSchedule).OrderBy(x => x.Day);
        }
    }

    public record PrivateClinicResponse(List<PrivateClinicTimeSchedule> TimeSchedules);

    public record PrivateClinicTimeSchedule(DateOnly Day, List<PrivateClinicAppointment> Appointments);

    public record PrivateClinicAppointment(DateTime When, Guid PatientId, string PatientName);

    public class LuxMedResponse
    {
        public Dictionary<DateTime, LuxMedPatient> TimeSchedules;
    }

    public class LuxMedPatient
    {
        public string Id { get; set; }
        public string FullName { get; set; }
    }

    public record NfzResponse(NfzSchedule[] Calendar);

    public record NfzSchedule(DateTime DateTime, Guid ClientId, string FirstName, string LastName);
}