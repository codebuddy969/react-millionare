using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    EventsManager eventsManager;
    PopupsEvents popupsEvents;
    DatabaseManager databaseManager;

    void Start()
    {
        eventsManager = EventsManager.current;
        popupsEvents = EventsManager.popupsEvents;
        databaseManager = DatabaseManager.manager;
        
        databaseManager.LoadSaving();
    }

    public void PlayGame()
    {
        GameDataConfig gameDataConfig = databaseManager.LoadSaving();

        gameDataConfig.level = 0;
        gameDataConfig.session_score = 0;

        databaseManager.CreateSaving(gameDataConfig);

        SceneManager.LoadScene("Game");
    }

    public void optionsPopup(bool status)
    {
        popupsEvents.optionsPopupAction(status);
    }

    public void shopPopup(bool status)
    {
        popupsEvents.shopPopupAction(status);
    }

    public void musicVolume(float value)
    { 
        eventsManager.changeMusicVolumeAction(value);

        // audioManager.MusicVolumeControl(value / 3);
    }

    public void fxVolume(float value)
    { 
        eventsManager.changeFxVolumeAction(value);
    }
}
