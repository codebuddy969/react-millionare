using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

[System.Serializable]
public class Question
{
    public string question;
    public string answer;
    public string first;
    public string second;
    public string third;
    public string fourth;

    public Question(
        string _question, 
        string _answer, 
        string _first, 
        string _second, 
        string _third, 
        string _fourth
    ){
        question = _question;
        answer = _answer;
        first = _first;
        second = _second;
        third = _third;
        fourth = _fourth;
    }
}

[System.Serializable]
public class EasyQuestions
{
    public Question[] easy;
}

[System.Serializable]
public class MediumQuestions
{
    public Question[] medium;
}

[System.Serializable]
public class HardQuestions
{
    public Question[] hard;
}

public class QuestionsManager : MonoBehaviour
{
    string answer = null;

    EventsManager eventsManager;
    PopupsEvents popupsEvents;
    DatabaseManager databaseManager;
    AudioManager audioManager;
    HelperEvents helperEvents;

    public TextAsset questionsFile;

    public Sprite[] backgroundSprites;

    UIList interfaces;

    Question[] question = new Question[16];

    int [] reward = new int[] {100, 200, 300, 500, 1000, 2000, 4000, 8000, 16000, 32000, 64000, 125000, 250000, 500000, 1000000};

    // Start is called before the first frame update
    void Start()
    {
        eventsManager = EventsManager.current;
        databaseManager = DatabaseManager.manager;
        popupsEvents = EventsManager.popupsEvents;
        helperEvents = EventsManager.helperEvents;
        audioManager = AudioManager.manager;

        interfaces = helperEvents.interfaceList;

        eventsManager.onSelectAnswerAction += selectAnswer;
        eventsManager.onClueSelectionAction += clueClick;

        GameDataConfig gameDataConfig = databaseManager.LoadSaving();

        gameDataConfig.level = 0;

        databaseManager.CreateSaving(gameDataConfig);

        storeQuestionsInMemory();

        setQuestionInfoToUI(gameDataConfig);
    }

    void storeQuestionsInMemory()
    {
         for (int i = 0; i <= 14; i++) {

            if (i >= 11 && i <= 14) {
                HardQuestions questions = JsonUtility.FromJson<HardQuestions>(questionsFile.text);

                question[i] = questions.hard[Random.Range(0, questions.hard.Length)];
            }

            if (i >= 6 && i <= 10) {
                MediumQuestions questions = JsonUtility.FromJson<MediumQuestions>(questionsFile.text);

                question[i] = questions.medium[Random.Range(0, questions.medium.Length)];
            }

            if (i >= 0 && i <= 5) {
                EasyQuestions questions = JsonUtility.FromJson<EasyQuestions>(questionsFile.text);

                question[i] = questions.easy[Random.Range(0, questions.easy.Length)];
            }
        }
    }

    void selectAnswer(string name) 
    {
        string selectedAnswer = null;

        Transform correctButton = null;

        eventsManager.changeTimerAction(false, false);

        audioManager.StopAndPlay(new string[] {"countdown"}, new string[] {"select-answer"});

        for (int i = 0; i <= interfaces.buttonsList.Length - 1; i++) 
        {
            interfaces.buttonsList[i].GetComponent<Button>().interactable = false;
        }

        for (int i = 0; i <= interfaces.cluesList.Length - 1; i++) 
        {
            interfaces.cluesList[i].GetComponent<Button>().interactable = false;
        }

        switch (name)
        {
            case "first":
                selectedAnswer = "1";
                interfaces.first_button.GetComponent<Image>().sprite = backgroundSprites[2];
                break;
            case "second":
                selectedAnswer = "2"; 
                interfaces.second_button.GetComponent<Image>().sprite = backgroundSprites[2];
                break;
            case "third":
                selectedAnswer = "3"; 
                interfaces.third_button.GetComponent<Image>().sprite = backgroundSprites[2];
                break;
            case "fourth":
                selectedAnswer = "4"; 
                interfaces.fourth_button.GetComponent<Image>().sprite = backgroundSprites[2];
                break;
        }

        switch (answer)
        {
            case "1":
                correctButton = interfaces.first_button;
                break;
            case "2":
                correctButton = interfaces.second_button;
                break;
            case "3":
                correctButton = interfaces.third_button;
                break;
            case "4":
                correctButton = interfaces.fourth_button;
                break;
        }

        StartCoroutine(awaitOnSelection(correctButton, selectedAnswer == answer));
    }

    void restoreButtonsState()
    {
        for (int i = 0; i <= interfaces.buttonsList.Length - 1; i++) 
        {
            interfaces.buttonsList[i].GetComponent<Button>().interactable = true;
            interfaces.buttonsList[i].GetComponent<Image>().sprite = backgroundSprites[0];
        }

        for (int i = 0; i <= interfaces.cluesList.Length - 1; i++) 
        {
            interfaces.cluesList[i].GetComponent<Button>().interactable = true;
        }
    }

    void setQuestionInfoToUI(GameDataConfig gameDataConfig)
    {
        answer = question[gameDataConfig.level].answer;

        interfaces.question.text = question[gameDataConfig.level].question;
        interfaces.first_text.text = question[gameDataConfig.level].first;
        interfaces.second_text.text = question[gameDataConfig.level].second;
        interfaces.third_text.text = question[gameDataConfig.level].third;
        interfaces.fourth_text.text = question[gameDataConfig.level].fourth;
    }

    GameDataConfig incrementLevel()
    {
        GameDataConfig gameDataConfig = databaseManager.LoadSaving();

        gameDataConfig.session_score = reward[gameDataConfig.level];

        gameDataConfig.level = gameDataConfig.level + 1;

        databaseManager.CreateSaving(gameDataConfig);

        return gameDataConfig;
    }

    void clue50on50()
    {
        restoreButtonsState();

        audioManager.Play("50on50");

        int[ ] indexes = {0, 1, 2, 3};

        indexes = indexes.Where((source, index) => index != (int.Parse(answer) - 1)).ToArray();

        int random = indexes[Random.Range(0, 3)];

        foreach(int i in indexes) {
            if (i != random) {
                interfaces.buttonsList[i].GetComponent<Button>().interactable = false;
                interfaces.buttonsList[i].GetComponent<Image>().sprite = backgroundSprites[3];
            }
        }
    }
    
    void clueClick(string name)
    {
        switch (name)
        {
            case "50on50":
                clue50on50();
                break;
            case "audience":
                popupsEvents.audiencePopupAction(true, () => {
                    StartCoroutine(audienceSimulation());
                });
                break;
            case "ad":

                break;
        }
    }
    
    public IEnumerator awaitOnSelection(Transform button, bool status)
    {
        yield return new WaitForSeconds(9.5f);
        StartCoroutine(HighlightCorrectAnswer(button, status));
    }

    public IEnumerator HighlightCorrectAnswer(Transform button, bool status)
    {
        audioManager.StopAndPlay(new string[] {"select-answer"}, new string[] {status ? "correct-answer" : "wrong-answer"});

        for (int i = 0; i <= 5; i++) {
            button.GetComponent<Image>().sprite = backgroundSprites[i % 2 == 0 ? 1 : 0];
            if (i == 5) {
                if (status) {
                    popupsEvents.changeLevelAction(() => {
                        GameDataConfig gameDataConfig = incrementLevel();

                        restoreButtonsState();
                        setQuestionInfoToUI(gameDataConfig);
                    });
                } else {
                    popupsEvents.gameLostAction(true);
                }
            }
            yield return new WaitForSeconds(0.5f);
        }
    }

    public IEnumerator audienceSimulation()
    {
        int iterations = Random.Range(4, 7);

        bool condition = (int)Random.Range(1, 5) == int.Parse(answer);

        audioManager.StopAndPlay(new string[] {"countdown"}, new string[] {"audience-thinking"});

        for (int i = 0; i <= iterations; i++) {

            float amount = 1.0f;

            float first = Random.Range(0, amount);

            amount = amount - first;

            float second = Random.Range(0, amount);

            amount = amount - second;

            float third = Random.Range(0, amount);

            amount = amount - third;

            if (condition && i == iterations) {
                switch (answer)
                {
                    case "1":
                        first = Random.Range(0.7f, 1.0f);
                        second = Random.Range(0, 0.3f);
                        third = Random.Range(0, 0.3f);
                        amount = Random.Range(0, 0.3f);
                        break;
                    case "2":
                        first = Random.Range(0, 0.3f);
                        second = Random.Range(0.7f, 1.0f);
                        third = Random.Range(0, 0.3f);
                        amount = Random.Range(0, 0.3f);
                        break;
                    case "3":
                        first = Random.Range(0, 0.3f);
                        second = Random.Range(0, 0.3f);
                        third = Random.Range(0.7f, 1.0f);
                        amount = Random.Range(0, 0.3f);
                        break;
                    case "4":
                        first = Random.Range(0, 0.3f);
                        second = Random.Range(0, 0.3f);
                        third = Random.Range(0, 0.3f);
                        amount = Random.Range(0.7f, 1.0f);
                        break;
                }
            }

            if (i == iterations) {
                audioManager.StopAndPlay(new string[] {"audience-thinking"}, new string[] {"audience-selected"});
            }

            float[ ] random = {first, second, third, amount};

            for (int j = 0; j <= interfaces.slidersList.Length - 1; j++) 
            {
                interfaces.slidersList[j].GetComponent<Slider>().DOValue(random[j], 0.5f);
            }

            yield return new WaitForSeconds(0.5f);
        }
    }
}