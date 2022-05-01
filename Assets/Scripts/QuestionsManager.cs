using System.Collections;
using System.Collections.Generic;
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

    public TextAsset questionsFile;

    public TextMeshProUGUI[] UITexts;

    public Sprite[] backgroundSprites;

    public Button[] buttons;

    Question[] question = new Question[16];

    int [] reward = new int[] {100, 200, 300, 500, 1000, 2000, 4000, 8000, 16000, 32000, 64000, 125000, 250000, 500000, 1000000};

    // Start is called before the first frame update
    void Start()
    {
        eventsManager = EventsManager.current;

        databaseManager = DatabaseManager.manager;

        eventsManager.onSelectAnswerAction += selectAnswer;

        storeQuestionsInMemory();
        setQuestionInfoToUI();
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

    void selectAnswer(GameObject button) 
    {
        button.GetComponent<Image>().sprite = backgroundSprites[1];

        eventsManager.changeTimerAction(false, false);

        for (int i = 0; i <= buttons.Length - 1; i++) 
        {
            buttons[i].GetComponent<Button>().interactable = false;

            if (buttons[i].name == answer) {
                StartCoroutine(awaitOnSelection(buttons[i].transform.gameObject, button.name == answer));
            }
        }
    }

    void restoreButtonsState()
    {
        for (int i = 0; i <= buttons.Length - 1; i++) 
        {
            buttons[i].GetComponent<Button>().interactable = true;
            buttons[i].GetComponent<Image>().sprite = backgroundSprites[2];
        }
    }

    void setQuestionInfoToUI()
    {
        GameDataConfig gameDataConfig = databaseManager.LoadSaving();

        answer = question[gameDataConfig.level].answer;
        
        UITexts[0].GetComponent<TextMeshProUGUI>().text = question[gameDataConfig.level].question;
        UITexts[1].GetComponent<TextMeshProUGUI>().text = question[gameDataConfig.level].first;
        UITexts[2].GetComponent<TextMeshProUGUI>().text = question[gameDataConfig.level].second;
        UITexts[3].GetComponent<TextMeshProUGUI>().text = question[gameDataConfig.level].third;
        UITexts[4].GetComponent<TextMeshProUGUI>().text = question[gameDataConfig.level].fourth;
    }

    void databaseOperations()
    {
        GameDataConfig gameDataConfig = databaseManager.LoadSaving();

        gameDataConfig.session_score = reward[gameDataConfig.level];
        gameDataConfig.level = gameDataConfig.level + 1;

        databaseManager.CreateSaving(gameDataConfig);
    }

    public IEnumerator awaitOnSelection(GameObject button, bool status)
    {
        yield return new WaitForSeconds(5.5f);
        StartCoroutine(HighlightCorrectAnswer(button, status));
    }

    public IEnumerator HighlightCorrectAnswer(GameObject button, bool status)
    {
        for (int i = 0; i <= 5; i++) {
            button.GetComponent<Image>().sprite = backgroundSprites[i % 2 == 0 ? 1 : 0];
            if (i == 5) {
                if (status) {
                    popupsEvents.changeLevelAction(() => {
                        setQuestionInfoToUI();
                        restoreButtonsState();
                        databaseOperations();
                    });
                } else {
                    popupsEvents.gameLostAction(true);
                }
            }
            yield return new WaitForSeconds(0.5f);
        }
    }
}