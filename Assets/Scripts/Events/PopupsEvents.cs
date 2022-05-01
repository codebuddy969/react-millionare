using System;
using UnityEngine;
using System.Collections;

public class PopupsEvents
{
    public void Test()
    {
        Debug.Log("Fuck");
    }

    public event Action<bool> onGameLostAction;
    public void gameLostAction(bool parameter)
    {
        if (onGameLostAction != null)
        {
            onGameLostAction(parameter);
        }
    }

    //----------------------------------------------------

    public event Action<Action> onChangeLevelAction;
    public void changeLevelAction(Action parameter)
    {
        if (onChangeLevelAction != null)
        {
            onChangeLevelAction(parameter);
        }
    }

    //----------------------------------------------------

    public event Action<bool> onConfirmationPopupAction;
    public void confirmationPopupAction(bool parameter)
    {
        if (onConfirmationPopupAction != null)
        {
            onConfirmationPopupAction(parameter);
        }
    }

    //----------------------------------------------------

    public event Action<bool> onOptionsPopupAction;
    public void optionsPopupAction(bool parameter)
    {
        if (onOptionsPopupAction != null)
        {
            onOptionsPopupAction(parameter);
        }
    }

    //----------------------------------------------------

    public event Action<bool> onShopPopupAction;
    public void shopPopupAction(bool parameter)
    {
        if (onShopPopupAction != null)
        {
            onShopPopupAction(parameter);
        }
    }
}
