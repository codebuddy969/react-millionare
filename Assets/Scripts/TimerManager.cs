using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class TimerManager : MonoBehaviour
{
    float timer = 0.0f;

    int countdown = 0;

    bool active = true;

    Image o_circle = null;

    TextMeshProUGUI o_countdown = null;

    EventsManager eventsManager;
    PopupsEvents popupsEvents;

    // Start is called before the first frame update
    void Start()
    {
        eventsManager = EventsManager.current;

        eventsManager.onChangeTimerAction += modifyTimer;

        o_circle = gameObject.transform.Find("Circle").GetComponent<Image>();
        o_countdown = gameObject.transform.Find("Countdown").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {   
        if (countdown < 30 && active) {
            timer += Time.deltaTime;
            countdown = (int)(timer % 60);
            o_countdown.text = (30 - countdown).ToString();
            o_circle.fillAmount = 1 - 0.0333f * (timer % 60);
        }

        if (countdown == 30 && active) {
            active = false;
            popupsEvents.gameLostAction(true);
        }
    }

    void modifyTimer(bool status, bool refresh = false)
    {
        if (refresh) {
            timer = 0.0f;
            countdown = 0;
            o_circle.fillAmount = 1;
            o_countdown.text = (30).ToString();
        }

        active = status;
    }
}
