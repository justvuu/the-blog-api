using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
namespace TheBlogAPI.Services
{
    public class CreateEvent
    {
        string jsonFile;
        string calendar_id = "763246885186b7677c73ad1fa1ccd39d0ccdaf2ff264319182902f93905f8def@group.calendar.google.com";
        string[] Scopes = { CalendarService.Scope.Calendar };
        ServiceAccountCredential credential;

        public CreateEvent()
        {
            jsonFile = "wwwroot/credentials.json";
        }

        public void Start(string title, DateTime scheduleTime)
        {
            using (var stream = new FileStream(jsonFile, FileMode.Open, FileAccess.Read))
            {
                var confg = Google.Apis.Json.NewtonsoftJsonSerializer.Instance.Deserialize<JsonCredentialParameters>(stream);
                credential = new ServiceAccountCredential(
                   new ServiceAccountCredential.Initializer(confg.ClientEmail)
                   {
                       Scopes = Scopes
                   }.FromPrivateKey(confg.PrivateKey));
            }

            var service = new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "Google Calendar",
            });

            var calendar = service.Calendars.Get(calendar_id).Execute();
            try
            {
                TimePeriod timePeriod = new TimePeriod();
                timePeriod.Start = scheduleTime;
                timePeriod.End = scheduleTime.AddMinutes(30);
                Event myEvent = new Event
                {
                    Summary = title,
                    Location = "Hanoi",
                    Start = new EventDateTime()
                    {
                        DateTime = timePeriod.Start.Value,
                        TimeZone = "Asia/Ho_Chi_Minh"
                    },
                    End = new EventDateTime()
                    {
                        DateTime = timePeriod.End.Value,
                        TimeZone = "Asia/Ho_Chi_Minh"
                    },
                };
                Event recurringEvent = service.Events.Insert(myEvent, calendar_id).Execute();
                
                
                
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }

        }

        static List<Event> DB =
             new List<Event>() {
                new Event(){
                    Id = "eventid" + 1,
                    Summary = "Google I/O 2015",
                    Location = "800 Howard St., San Francisco, CA 94103",
                    Description = "A chance to hear more about Google's developer products.",
                    Start = new EventDateTime()
                    {
                        DateTime = new DateTime(2019, 01, 13, 15, 30, 0),
                        TimeZone = "America/Los_Angeles",
                    },
                    End = new EventDateTime()
                    {
                        DateTime = new DateTime(2019, 01, 14, 15, 30, 0),
                        TimeZone = "America/Los_Angeles",
                    },
                     Recurrence = new List<string> { "RRULE:FREQ=DAILY;COUNT=2" },
                    Attendees = new List<EventAttendee>
                    {
                        new EventAttendee() { Email = "vunt@example.com"},
                        new EventAttendee() { Email = "vunt2@example.com"}
                    }
                }
             };
    }
}

