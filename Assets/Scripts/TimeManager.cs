using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public int currentMinute;
    public int currentHour;
    public int currentDay = 1;
    public int currentMonth = 1;
    public int currentYear = 1;

    public event Action OnTimeChanged;

    [SerializeField] int numberOfMonths; // TODO: How many months?

    [SerializeField] int startOfDay;
    [SerializeField] int endOfDay;

    [SerializeField] float timeSpeed = 1.0f; // Speed for in-game time relative to real time
    // in-game time [minutes] = real-time [seconds] * timeSpeed
    // This means that for each x seconds, x*timeSpeed minutes occur in-game
    float timer = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        currentHour = startOfDay;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime; // Add the amount of seconds from one frame to another
        if (timer > timeSpeed)
        {
            AdvanceTime();
            timer = 0.0f;
        }
    }

    void AdvanceTime()
    {
        currentMinute++;

        if (currentMinute >= 60)
        {
            currentMinute = 0;
            currentHour++;
        }

        if (currentHour >= endOfDay && currentHour <= startOfDay)
        {
            currentHour = startOfDay;
            currentDay++;
            // TODO: What happens at the end of day
            // TODO: Transition into the next day
        }

        if (currentDay >= 28)
        {
            currentDay = 1;
            currentMonth++;
            // TODO: Change seasons
        }

        if (currentMonth >= numberOfMonths)
        {
            currentMonth = 1;
            currentYear++;
            // TODO: Event for end of year?
        }
        OnTimeChanged?.Invoke();
        // Debug.Log($"Time advanced to: {GetTimeString()}");
    }

    public string GetTimeString()
    {
        return $"{currentHour:D2}:{currentMinute:D2}";
    }
}
