using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;
using TMPro;
using Random = UnityEngine.Random;

public class PopupsManager : MonoBehaviour
{
    EventsManager eventsManager;
    PopupsEvents popupsEvents;
    DatabaseManager databaseManager;
    AudioManager audioManager;

    public PopupsList[] popupsList;
 
    void Start()
    {
        eventsManager = EventsManager.current;
        popupsEvents = EventsManager.popupsEvents;
        databaseManager = DatabaseManager.manager;
        audioManager = AudioManager.manager;

        foreach (PopupsList popup in popupsList)
        {
            switch (popup.Name)
            {
                case "Options":
                    popupsEvents.onOptionsPopupAction += (p) => onOptionsOpen(p, popup.Element);
                    break;
                case "Shop":
                    popupsEvents.onShopPopupAction += (p) => onShopOpen(p, popup.Element);
                    break;
                case "Confirmation":
                    popupsEvents.onConfirmationPopupAction += (p) => onConfirmationOpen(p, popup.Element);
                    break;
                case "Game Lost":
                    popupsEvents.onGameLostAction += (p) => onGameLost(p, popup.Element);
                    break;
                case "Level Change":
                    popupsEvents.onChangeLevelAction += (p) => onChangeLevel(p, popup.Element);
                    break;
                case "Levels Slide":
                    popupsEvents.onLevelsSlideAction += (p) => onLevelsSlide(p, popup.Element);
                    break;
                case "Audience":
                    popupsEvents.onAudiencePopupAction += (p, callback) => onAudienceOpen(p, callback, popup.Element);
                    break;
                case "Achievement":
                    popupsEvents.onAchievementPopupAction += (p, amount) => onAchievementOpen(p, amount, popup.Element);
                    break;
            }
        }
    }

    void onGameLost(bool status, GameObject element)
    {
        Image circle = element.transform.Find("Popup/Icon/Circle").GetComponent<Image>(); 

        if (status) {
            element.SetActive(true);
            element.transform
                        .DOScale(new Vector3(1, 1, 1), 0.2f)
                        .SetEase(Ease.OutBack)
                        .OnComplete(() => {
                            StartCoroutine(CircleFill(circle, 300, () => { SceneManager.LoadScene("Menu"); }));
                        });
        } else {
            element.transform.DOScale(new Vector3(0, 0, 0), 0.2f).OnComplete(() => { 
                element.SetActive(false); 
            });
        }
    }

    void onChangeLevel(Action callback, GameObject element)
    {
        GameDataConfig gameDataConfig = DatabaseManager.manager.LoadSaving();
        Image circle = element.transform.Find("Icon/Circle").GetComponent<Image>();

        element.SetActive(true);
        element.transform
                     .DOScale(new Vector3(1, 1, 1), 0.2f)
                     .SetEase(Ease.OutBack)
                     .SetDelay(1)
                     .OnComplete(() => {
                         audioManager.Play("switch-level");
                         StartCoroutine(CircleFill(circle, 5, () => {
                             callback?.Invoke();
                             element.transform.DOScale(new Vector3(0, 0, 0), 0.2f).SetDelay(4).OnComplete(() => {
                                eventsManager.changeTimerAction(true, true);
                                element.SetActive(false);
                                audioManager.Play("countdown");
                                if (gameDataConfig.level == 4 || gameDataConfig.level == 9) {
                                    popupsEvents.achievementPopupAction(true, gameDataConfig.level == 4 ? "1000" : "32000");
                                }
                             });
                         }));
                    });
    }

    void onConfirmationOpen(bool status, GameObject element)
    {
        if (status) {
            element.SetActive(true);
            element.transform.DOScale(new Vector3(1, 1, 1), 0.2f).SetEase(Ease.OutBack);
        } else {
            element.transform.DOScale(new Vector3(0, 0, 0), 0.2f).OnComplete(() => { element.SetActive(false); });
        }
    }

    void onOptionsOpen(bool status, GameObject element)
    {
        if (status) {
            element.SetActive(true);
            element.transform
                         .DOScale(new Vector3(1, 1, 1), 0.2f)
                         .SetEase(Ease.OutBack);
        } else {
            element.transform.DOScale(new Vector3(0, 0, 0), 0.2f).OnComplete(() => { element.SetActive(false); });
        }
    }

    void onShopOpen(bool status, GameObject element)
    {
        if (status) {
            GameDataConfig gameDataConfig = databaseManager.LoadSaving();
            element.SetActive(true);
            element.transform.Find("Title/Score").GetComponent<TextMeshProUGUI>().text = (gameDataConfig.score).ToString();
            element.transform.DOScale(new Vector3(1, 1, 1), 0.2f).SetEase(Ease.OutBack);
        } else {
            element.transform.DOScale(new Vector3(0, 0, 0), 0.2f).OnComplete(() => { element.SetActive(false); });
        }
    }

    void onLevelsSlide(bool status, GameObject element)
    {
        if(status) {
            highlightLevel(element);
            element.SetActive(true);
            element.transform.DOLocalMoveX(-178, 0.2f);
        } else {
            element.transform.DOLocalMoveX(400, 0.2f).OnComplete(() => { element.SetActive(false); });
        }
    }
    
    void onAudienceOpen(bool status, Action callback, GameObject element)
    {
        if (status) {
            element.SetActive(true);
            element.transform
                   .DOScale(new Vector3(1, 1, 1), 0.2f)
                   .SetEase(Ease.OutBack)
                   .OnComplete(() => {
                        callback?.Invoke();
                   });
        } else {
            element.transform.DOScale(new Vector3(0, 0, 0), 0.2f).OnComplete(() => { element.SetActive(false); });
        }
    }

    void onAchievementOpen(bool status, string amount, GameObject element)
    {
        if (status) {
            element.SetActive(true);
            element.transform.Find("Notification").GetComponent<TextMeshProUGUI>().text = $"Safety {amount} achieved";
            element.transform
                   .DOScale(new Vector3(1, 1, 1), 0.2f)
                   .SetEase(Ease.OutBack);
        } else {
            element.transform.DOScale(new Vector3(0, 0, 0), 0.2f).OnComplete(() => { element.SetActive(false); });
        }
    }
    
    public IEnumerator CircleFill(Image image, int loops, Action callback)
    {
        for (int i = 0; i <= loops; i++) {
            image.fillAmount = (float)i/loops;
            if (i == loops) {
                callback?.Invoke();
            }
            yield return new WaitForSeconds((float)loops/300);
        }
    }

    void highlightLevel(GameObject element)
    {   
        for (int i = 0; i <= 14; i++) {
            element.transform
                   .Find("Container")
                   .GetChild(i)
                   .Find("Numerotation")
                   .GetComponent<TextMeshProUGUI>()
                   .color = new Color32(255,205,3,255);
            element.transform
                   .Find("Container")
                   .GetChild(i)
                   .Find("Reward")
                   .GetComponent<TextMeshProUGUI>()
                   .color = new Color32(255,205,3,255);
            element.transform
                   .Find("Container")
                   .GetChild(i)
                   .GetComponent<Image>()
                   .color = new Color32(255,255,255,0);
        }

        GameDataConfig gameDataConfig = DatabaseManager.manager.LoadSaving();

        element.transform
                  .Find("Container")
                  .GetChild(gameDataConfig.level)
                  .Find("Numerotation")
                  .GetComponent<TextMeshProUGUI>()
                  .color = new Color32(255,255,225,255);
        element.transform
                  .Find("Container")
                  .GetChild(gameDataConfig.level)
                  .Find("Reward")
                  .GetComponent<TextMeshProUGUI>()
                  .color = new Color32(255,255,225,255);
        element.transform
                  .Find("Container")
                  .GetChild(gameDataConfig.level)
                  .GetComponent<Image>()
                  .color = new Color32(255,255,225,255);
    }
}
