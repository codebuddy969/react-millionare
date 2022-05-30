using System;
using UnityEngine;
using System.Collections;

public class PopupsEvents
{
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

    //----------------------------------------------------

    public event Action<bool> onLevelsSlideAction;
    public void levelsSlideAction(bool parameter)
    {
        if (onLevelsSlideAction != null)
        {
            onLevelsSlideAction(parameter);
        }
    }

    //----------------------------------------------------

    public event Action<bool, Action> onAudiencePopupAction;
    public void audiencePopupAction(bool parameter, Action callback = null)
    {
        if (onAudiencePopupAction != null)
        {
            onAudiencePopupAction(parameter, callback);
        }
    }

    //----------------------------------------------------

    public event Action<bool, string> onAchievementPopupAction;
    public void achievementPopupAction(bool parameter, string amount)
    {
        if (onAchievementPopupAction != null)
        {
            onAchievementPopupAction(parameter, amount);
        }
    }
}
