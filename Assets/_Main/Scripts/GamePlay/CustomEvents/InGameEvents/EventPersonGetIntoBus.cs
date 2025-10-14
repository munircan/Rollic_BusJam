using _Main.Scripts.GamePlay.PersonSystem.Components;
using _Main.Scripts.Patterns.EventSystem;

namespace _Main.Scripts.GamePlay.CustomEvents.InGameEvents
{
    // WE CAN ADD AN EXTRA VARIABLE FOR LAST PERSON GET INTO BUS
    public class EventPersonGetIntoBus : ICustomEvent
    {
        public Person Person { get; set; }
        public bool IsInstant {get; set;}
        public static EventPersonGetIntoBus Create(Person person,bool isInstant)
        {
            var eventLastPersonGetIntoBus = new EventPersonGetIntoBus();
            eventLastPersonGetIntoBus.Person = person;
            eventLastPersonGetIntoBus.IsInstant = isInstant;
            return eventLastPersonGetIntoBus;
        }
    }
}