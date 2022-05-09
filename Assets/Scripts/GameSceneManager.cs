using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    EventsManager eventsManager;
    PopupsEvents popupsEvents;
    DatabaseManager databaseManager;

    // Start is called before the first frame update
    void Start()
    {
        eventsManager = EventsManager.current;
        popupsEvents = EventsManager.popupsEvents;
        databaseManager = DatabaseManager.manager;
    }

    public void leaveGameSession(bool saving)
    {
        GameDataConfig gameDataConfig = databaseManager.LoadSaving();

        if (saving) {
            gameDataConfig.score = gameDataConfig.score + gameDataConfig.session_score;
        }

        gameDataConfig.session_score = 0;
        gameDataConfig.level = 0;

        databaseManager.CreateSaving(gameDataConfig);

        SceneManager.LoadScene("Menu");
    }

    public void reloadGameSession()
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

    public void confirmationPopup(bool status)
    {
        popupsEvents.confirmationPopupAction(status);
    }

    public void onLevelsOpen(bool status)
    {
        popupsEvents.levelsSlideAction(status);
    }

    public void musicVolume(float value)
    { 
        eventsManager.changeMusicVolumeAction(value);
    }

    public void fxVolume(float value)
    { 
        eventsManager.changeFxVolumeAction(value);
    }

    public void selectAnswer(string name)
    {
        eventsManager.selectAnswerAction(name);
    }

    public void onClueClick(string name)
    {
        eventsManager.clueSelectionAction(name);
    }

    public void audiencePopup()
    {
        popupsEvents.audiencePopupAction(false);
    }
}
