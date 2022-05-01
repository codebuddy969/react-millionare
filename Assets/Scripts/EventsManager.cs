using System;
using UnityEngine;
using System.Collections;

public class EventsManager : MonoBehaviour
{
    public static EventsManager current;
    public static PopupsEvents popupsEvents;

    private void Awake()
    {
        current = this;
        popupsEvents = new PopupsEvents();
    }

    //----------------------------------------------------

    // public event Action<bool> onOptionsPopupAction;
    // public void optionsPopupAction(bool parameter)
    // {
    //     if (onOptionsPopupAction != null)
    //     {
    //         onOptionsPopupAction(parameter);
    //     }
    // }

    //----------------------------------------------------

    // public event Action<Hashtable, Action> onShopPopupAction;
    // public void shopPopupAction(Hashtable parameters, Action callback)
    // {
    //     if (onShopPopupAction != null)
    //     {
    //         onShopPopupAction(parameters, callback);
    //     }
    // }

    //----------------------------------------------------

    // public event Action<bool> onConfirmationPopupAction;
    // public void confirmationPopupAction(bool parameter)
    // {
    //     if (onConfirmationPopupAction != null)
    //     {
    //         onConfirmationPopupAction(parameter);
    //     }
    // }

    //----------------------------------------------------

    public event Action<Hashtable, Action> onLevelsAction;
    public void levelsAction(Hashtable parameters, Action callback)
    {
        if (onLevelsAction != null)
        {
            onLevelsAction(parameters, callback);
        }
    }

    //----------------------------------------------------

    public event Action<Hashtable, Action> onPopupAction;
    public void popupAction(Hashtable parameters, Action callback)
    {
        if (onPopupAction != null)
        {
            onPopupAction(parameters, callback);
        }
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

    // public event Action<Action> onChangeLevelAction;
    // public void changeLevelAction(Action parameter)
    // {
    //     if (onChangeLevelAction != null)
    //     {
    //         onChangeLevelAction(parameter);
    //     }
    // }

    //----------------------------------------------------

    public event Action<GameObject> onSelectAnswerAction;
    public void selectAnswerAction(GameObject parameter)
    {
        if (onSelectAnswerAction != null)
        {
            onSelectAnswerAction(parameter);
        }
    }

    //----------------------------------------------------

    // public event Action<bool> onGameLostAction;
    // public void gameLostAction(bool parameter)
    // {
    //     if (onGameLostAction != null)
    //     {
    //         onGameLostAction(parameter);
    //     }
    // }

    //----------------------------------------------------

    public event Action<bool, bool> onChangeTimerAction;
    public void changeTimerAction(bool parameter, bool refresh)
    {
        if (onChangeTimerAction != null)
        {
            onChangeTimerAction(parameter, refresh);
        }
    }
}
