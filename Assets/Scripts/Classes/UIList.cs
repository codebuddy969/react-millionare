using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class UIList
{
    public TextMeshProUGUI question;
    public Transform[] buttonsList;
    public Transform first_button;
    public Transform second_button;
    public Transform third_button;
    public Transform fourth_button;
    public TextMeshProUGUI first_text;
    public TextMeshProUGUI second_text;
    public TextMeshProUGUI third_text;
    public TextMeshProUGUI fourth_text;
    public Transform[] cluesList;
    public Transform menu;
    public Transform clue50on50;
    public Transform audience;
    public Transform ads;
    public Transform[] slidersList;
    public Transform first_slider;
    public Transform second_slider;
    public Transform third_slider;
    public Transform fourth_slider;

    public UIList(GameObject document){
        question = document.transform.Find("Question/Panel/Text").GetComponent<TextMeshProUGUI>();
        first_button = document.transform.Find("Answers/First/Button");
        second_button = document.transform.Find("Answers/Second/Button");
        third_button = document.transform.Find("Answers/Third/Button");
        fourth_button = document.transform.Find("Answers/Fourth/Button");
        buttonsList = new Transform[] {first_button, second_button, third_button, fourth_button};
        first_text = document.transform.Find("Answers/First/Button/Text").GetComponent<TextMeshProUGUI>();
        second_text = document.transform.Find("Answers/Second/Button/Text").GetComponent<TextMeshProUGUI>();
        third_text = document.transform.Find("Answers/Third/Button/Text").GetComponent<TextMeshProUGUI>();
        fourth_text = document.transform.Find("Answers/Fourth/Button/Text").GetComponent<TextMeshProUGUI>();
        menu = document.transform.Find("Clues/Menu");
        clue50on50 = document.transform.Find("Clues/50on50");
        audience = document.transform.Find("Clues/Audience");
        ads = document.transform.Find("Clues/Ads");
        cluesList = new Transform[] {menu, clue50on50, audience, ads};
        slidersList = new Transform[] {
            document.transform.Find("AudiencePopup/Sliders/First/Slider"),
            document.transform.Find("AudiencePopup/Sliders/Second/Slider"),
            document.transform.Find("AudiencePopup/Sliders/Third/Slider"),
            document.transform.Find("AudiencePopup/Sliders/Fourth/Slider"),
        };
    }
}