using System;
using UnityEngine;
using System.Collections;

public class EventsManager : MonoBehaviour
{
    public GameObject document;

    public static EventsManager current;
    public static PopupsEvents popupsEvents;
    public static HelperEvents helperEvents;

    private void Awake()
    {
        current = this;
        popupsEvents = new PopupsEvents();
        helperEvents = new HelperEvents(new UIList(document));
    }

    //----------------------------------------------------

    public event Action<float> onChangeMusicVolumeAction;
    public void changeMusicVolumeAction(float parameter)
    {
        if (onChangeMusicVolumeAction != null)
        {
            onChangeMusicVolumeAction(parameter);
        }
    }

    //----------------------------------------------------

    public event Action<float> onChangeFxVolumeAction;
    public void changeFxVolumeAction(float parameter)
    {
        if (onChangeFxVolumeAction != null)
        {
            onChangeFxVolumeAction(parameter);
        }
    }

    //----------------------------------------------------

    public event Action<string> onSelectAnswerAction;
    public void selectAnswerAction(string parameter)
    {
        if (onSelectAnswerAction != null)
        {
            onSelectAnswerAction(parameter);
        }
    }

    //----------------------------------------------------

    public event Action<bool, bool> onChangeTimerAction;
    public void changeTimerAction(bool parameter, bool refresh)
    {
        if (onChangeTimerAction != null)
        {
            onChangeTimerAction(parameter, refresh);
        }
    }
    
    //----------------------------------------------------

    public event Action<string> onClueSelectionAction;
    public void clueSelectionAction(string parameter)
    {
        if (onClueSelectionAction != null)
        {
            onClueSelectionAction(parameter);
        }
    }
}
