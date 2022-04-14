using System;
using UnityEngine;
using System.Collections;
public class EventsManager
{
    public static EventsManager current;

    private void Awake()
    {
        current = this;
    }

    //----------------------------------------------------

    public event Action<Hashtable, Action> onOptionsPopupAction;
    public void optionsPopupAction(Hashtable parameters, Action callback)
    {
        if (onOptionsPopupAction != null)
        {
            onOptionsPopupAction(parameters, callback);
        }
    }

    //----------------------------------------------------

    public event Action<Hashtable, Action> onShopPopupAction;
    public void shopPopupAction(Hashtable parameters, Action callback)
    {
        if (onShopPopupAction != null)
        {
            onShopPopupAction(parameters, callback);
        }
    }

    //----------------------------------------------------

    public event Action<Hashtable, Action> onConfirmationPopupAction;
    public void confirmationPopupAction(Hashtable parameters, Action callback)
    {
        if (onConfirmationPopupAction != null)
        {
            onConfirmationPopupAction(parameters, callback);
        }
    }

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

    public event Action<float> onMusicVolumeAction;
    public void musicVolumeAction(float parameter)
    {
        if (onMusicVolumeAction != null)
        {
            musicVolumeAction(parameter);
        }
    }


}
