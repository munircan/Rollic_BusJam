using _Main.Patterns.EventSystem;
using _Main.Scripts.GamePlay.PersonSystem;

namespace _Main.Scripts.GamePlay.CustomEvents
{
    // TODO: CHANGE THIS EVENT LIKE LAST PERSON GET INTO BUS
    public class EventPersonGetIntoBus : ICustomEvent
    {
        public Person Person { get; set; }
        public static EventPersonGetIntoBus Create(Person person)
        {
            var eventLastPersonGetIntoBus = new EventPersonGetIntoBus();
            eventLastPersonGetIntoBus.Person = person;
            return eventLastPersonGetIntoBus;
        }
    }
}