using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class TimerManager : MonoBehaviour
{
    float timer = 0.0f;
    int countdown = 0;

    Image o_circle = null;
    TextMeshProUGUI o_countdown = null;

    // Start is called before the first frame update
    void Start()
    {
        o_circle = gameObject.transform.Find("Circle").GetComponent<Image>();
        o_countdown = gameObject.transform.Find("Countdown").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {   
        if (countdown < 30) {
            timer += Time.deltaTime;
            countdown = (int)(timer % 60);
            o_countdown.text = (30 - countdown).ToString();
            o_circle.fillAmount = 1 - 0.0333f * (timer % 60);
        }
    }
}
