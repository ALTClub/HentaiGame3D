using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rain
{
    public DateTime Date { get; private set; }
    public TimeSpan Duration { get; private set; }
    public bool Started = false;
    public Rain(DateTime date, TimeSpan duration)
    {
        Date = date;
        Duration = duration;
    }
    /// <summary>
    /// Deserialize
    /// </summary>
    public Rain(string serialized)
    {
        string[] splited = serialized.Split(',');
        Date = DateTime.ParseExact(splited[0], "dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
        Duration = TimeSpan.FromMinutes(int.Parse(splited[1]));
        Started = bool.Parse(splited[2]);
    }
    public string Serialize()
    {
        List<string> data = new();
        data.Add(Date.ToString("dd/MM/yyyy HH:mm:ss"));
        data.Add(Duration.TotalMinutes.ToString());
        data.Add(Started.ToString());
        return string.Join(",", data);
    }
}
