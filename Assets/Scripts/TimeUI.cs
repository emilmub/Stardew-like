using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeUI : MonoBehaviour
{
    [SerializeField] GameObject timeManagerObject;

    TimeManager timeManager;
    TextMeshProUGUI timeText;
    // Start is called before the first frame update
    void Start()
    {
        timeManager = timeManagerObject.GetComponent<TimeManager>();
        timeText = GetComponent<TextMeshProUGUI>();

        timeManager.OnTimeChanged += UpdateClock;
        UpdateClock();
    }

    void UpdateClock()
    {
        timeText.text = timeManager.GetTimeString();
    }
}
