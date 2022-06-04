using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    EventsManager eventsManager;
    PopupsEvents popupsEvents;
    DatabaseManager databaseManager;
    AudioManager audioManager;

    void Start()
    {
        eventsManager = EventsManager.current;
        popupsEvents = EventsManager.popupsEvents;
        databaseManager = DatabaseManager.manager;
        audioManager = AudioManager.manager;
        
        databaseManager.LoadSaving();

        audioManager.PlayRandomTrack();
    }

    public void PlayGame()
    {
        GameDataConfig gameDataConfig = databaseManager.LoadSaving();

        gameDataConfig.level = 0;
        gameDataConfig.session_score = 0;

        databaseManager.CreateSaving(gameDataConfig);

        audioManager.StopMultiple(new string[] {"menu-1", "menu-2"});

        SceneManager.LoadScene("Game");
    }

    public void optionsPopup(bool status)
    {
        audioManager.Play("click");

        popupsEvents.optionsPopupAction(status);
    }

    public void shopPopup(bool status)
    {
        audioManager.Play("click");
        
        popupsEvents.shopPopupAction(status);
    }

    public void musicVolume(float value)
    { 
        audioManager.musicVolume(value);
    }

    public void fxVolume(float value)
    { 
        audioManager.effectsVolume(value);
    }
}
