using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Schedule : MonoBehaviour
{
    public class Task
    {
        public string name { get; private set; }
        public DateTime date { get; private set; }
        public TimeSpan duration { get; private set; }
        Delegate method, cancel;
        public bool started { get; private set; }
        public Task(string name, DateTime date, TimeSpan duration, Delegate method, Delegate cancel)
        {
            this.name = name;
            this.date = date;
            this.duration = duration;
            this.method = method;
            this.cancel = cancel;
            started = false;
        }
        public Task(string name, DateTime date, Delegate method)
        {
            this.name = name;
            this.date = date;
            this.method = method;
        }
        public void Invoke()
        {
            method.DynamicInvoke();
            started = true;
        }
        public void Finish() => cancel?.DynamicInvoke();

    }

    public static UnityEvent OnNewMonth = new();

    static List<Task> schedule = new();
    int mounth;

    private void Start()
    {
        TimeManager.OnChangeTime.AddListener(checkSchedule);
        mounth = TimeManager.GetDateTime.Month;
    }

    public static void AddTask(Task task) => schedule.Add(task);

    void checkSchedule()
    {
        if (TimeManager.GetDateTime.Month != mounth)
        {
            OnNewMonth.Invoke();
            mounth = TimeManager.GetDateTime.Month;
        }

        List<Task> completed = new();

        foreach (var task in schedule)
        {
            TimeSpan diff = task.date - TimeManager.GetDateTime;
            if (diff < TimeSpan.Zero)
            {
                if (diff.Negate() < task.duration)
                {
                    if (task.started) continue;
                    Debug.Log("Start");
                    task.Invoke();
                }
                else completed.Add(task);
            }
        }
        foreach (var task in completed)
        {
            Debug.Log("Complete");
            task.Finish();
            schedule.Remove(task);
        }
    }
}
