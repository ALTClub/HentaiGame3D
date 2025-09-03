using System;
using System.Collections.Generic;
using UnityEngine;

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
    /// <summary>
    /// Deserialize
    /// </summary>
    public Event(List<string> serialized)
    {
        Start = DateTime.Parse(serialized[0]);
        Duration = TimeSpan.FromMinutes(int.Parse(serialized[1]));
        Started = bool.Parse(serialized[2]);
    }
    public List<string> Serialize => new()
    {
        Start.ToString(),
        Duration.TotalMinutes.ToString(),
        Started.ToString()
    };
}
