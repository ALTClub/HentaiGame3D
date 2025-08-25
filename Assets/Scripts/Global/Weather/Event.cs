using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event
{
    public DateTime StartDate { get; private set; }
    public TimeSpan Duration { get; private set; }
    public DateTime EndDate => StartDate + Duration;
    public bool Started = false;
    public Event(DateTime date, TimeSpan duration)
    {
        StartDate = date;
        Duration = duration;
    }
    /// <summary>
    /// Deserialize
    /// </summary>
    public Event(string serialized)
    {
        string[] splited = serialized.Split(',');
        StartDate = DateTime.Parse(splited[0]);
        Duration = TimeSpan.FromMinutes(int.Parse(splited[1]));
        Started = bool.Parse(splited[2]);
    }
    public string Serialize()
    {
        List<string> data = new();
        data.Add(StartDate.ToString("dd/MM/yyyy HH:mm:ss"));
        data.Add(Duration.TotalMinutes.ToString());
        data.Add(Started.ToString());
        return string.Join(",", data);
    }
}
