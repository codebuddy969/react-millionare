using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PopupsList
{
    public string Name;
    public GameObject Element;
}

[System.Serializable]
public class ConfigPopup
{
    public Transform level;
    public Transform score;
    public Transform clue_50on50;
    public Transform clue_auditory;
} 