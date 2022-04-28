using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    EventsManager eventsManager;
    DatabaseManager databaseManager;

    void Start()
    {
        eventsManager = EventsManager.current;
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
        Hashtable parameters = new Hashtable();

        parameters["opened"] = status;

        eventsManager.optionsPopupAction(parameters, () => {  });
    }

    public void shopPopup(bool status)
    {
        Hashtable parameters = new Hashtable();

        parameters["opened"] = status;

        eventsManager.shopPopupAction(parameters, () => { });
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
