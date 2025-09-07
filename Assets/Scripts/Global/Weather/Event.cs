using System;

public class Event
{
    public DateTime Start { get; private set; }
    public TimeSpan Duration { get; private set; }
    public DateTime End => Start + Duration;
    public bool Started = false;
    public Event(DateTime date, TimeSpan duration)
    {
        Start = date;
        Duration = duration;
    }
}
