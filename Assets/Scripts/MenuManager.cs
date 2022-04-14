using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    GameDataConfig config;
    AudioManager audioManager;
    DatabaseManager databaseManager;
    EventsManager eventsManager;

    // Start is called before the first frame update
    void Start()
    {
        eventsManager = new EventsManager();
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void optionsPopup(bool status)
    {
        Hashtable parameters = new Hashtable();

        parameters["opened"] = status;

        EventsManager.current.optionsPopupAction(parameters, () => {  });
    }

    public void shopPopup(bool status)
    {
        Hashtable parameters = new Hashtable();

        parameters["opened"] = status;

        EventsManager.current.shopPopupAction(parameters, () => { });
    }

    public void musicVolume(float value)
    {
        Debug.Log(value);

        eventsManager.musicVolumeAction(value);

        // config.musicLevel = value;

        // audioManager.MusicVolumeControl(value / 3);

        // databaseManager.CreateSaving(config);
    }
}
