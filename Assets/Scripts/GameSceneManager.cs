using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    EventsManager eventsManager;
    PopupsEvents popupsEvents;
    DatabaseManager databaseManager;
    AudioManager audioManager;
    HelperEvents helperEvents;

    void Start()
    {
        eventsManager = EventsManager.current;
        popupsEvents = EventsManager.popupsEvents;
        databaseManager = DatabaseManager.manager;
        audioManager = AudioManager.manager;
        helperEvents = EventsManager.helperEvents;

        this.onSessionLoaded();
    }

    public void onSessionLoaded() {

        UIList interfaces = helperEvents.interfaceList;


    }

    public void endGameSession(string scenario) {

        GameDataConfig gameDataConfig = databaseManager.LoadSaving();

        audioManager.Stop("countdown");

        if (scenario == "leave" || scenario == "lost & leave" || scenario == "lost & reload") {
            if (gameDataConfig.session_score >= 1000 && gameDataConfig.session_score < 32000) {
                gameDataConfig.score = gameDataConfig.score + 1000;
            }
            if (gameDataConfig.session_score >= 32000) {
                gameDataConfig.score = gameDataConfig.score + 32000;
            }
        }

        if (scenario == "won & leave" || scenario == "won & reload") {
            gameDataConfig.score = gameDataConfig.score + 1000000;
        }

        databaseManager.CreateSaving(gameDataConfig);

        if (scenario == "leave" || scenario == "lost & leave" || scenario == "won & leave") {
            SceneManager.LoadScene("Menu");
        }

        if (scenario == "lost & reload" || scenario == "won & reload") {
            SceneManager.LoadScene("Game");
        }
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

    public void configsPopup(bool status)
    {
        popupsEvents.configsPopupAction(status);
    }

    public void storeConfig()
    {
        UIList interfaces = helperEvents.interfaceList;

        GameDataConfig gameDataConfig = databaseManager.LoadSaving();

        gameDataConfig.level = interfaces.configPopup.level.GetComponent<TMP_Dropdown>().value;
        gameDataConfig.score = interfaces.configPopup.score.GetComponent<TMP_Dropdown>().value;
        gameDataConfig.clue_50on50 = int.Parse(interfaces.configPopup.clue_50on50.GetComponent<InputField>().text);
        gameDataConfig.clue_auditory = int.Parse(interfaces.configPopup.clue_auditory.GetComponent<InputField>().text);

        databaseManager.CreateSaving(gameDataConfig);

        this.configsPopup(false);
    }
}
