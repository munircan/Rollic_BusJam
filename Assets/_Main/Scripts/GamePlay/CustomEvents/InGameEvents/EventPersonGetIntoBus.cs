using _Main.Scripts.GamePlay.PersonSystem;
using _Main.Scripts.GamePlay.PersonSystem.Components;
using _Main.Scripts.Patterns.EventSystem;

namespace _Main.Scripts.GamePlay.CustomEvents.InGameEvents
{
    // WE CAN ADD AN EXTRA VARIABLE FOR LAST PERSON GET INTO BUS
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