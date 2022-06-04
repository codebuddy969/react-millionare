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
    AudioManager audioManager;

    void Start()
    {
        eventsManager = EventsManager.current;
        popupsEvents = EventsManager.popupsEvents;
        databaseManager = DatabaseManager.manager;
        audioManager = AudioManager.manager;
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

        audioManager.Stop("countdown");

        SceneManager.LoadScene("Menu");
    }

    public void reloadGameSession()
    {
        GameDataConfig gameDataConfig = databaseManager.LoadSaving();

        if (gameDataConfig.session_score >= 1000 && gameDataConfig.session_score < 32000) {
            gameDataConfig.score = gameDataConfig.score + 1000;
        }

        if (gameDataConfig.session_score >= 32000) {
            gameDataConfig.score = gameDataConfig.score + 32000;
        }

        gameDataConfig.level = 0;
        gameDataConfig.session_score = 0;

        databaseManager.CreateSaving(gameDataConfig);

        audioManager.Stop("countdown");

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
        audioManager.musicVolume(value);
    }

    public void fxVolume(float value)
    { 
        audioManager.effectsVolume(value);
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
        audioManager.Play("countdown");
        
        popupsEvents.audiencePopupAction(false);
    }

    public void achievementPopup()
    {
        popupsEvents.achievementPopupAction(false, "");
    }
}
