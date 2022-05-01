using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class PopupsManager : MonoBehaviour
{
    EventsManager eventsManager;
    PopupsEvents popupsEvents;
    DatabaseManager databaseManager;

    public PopupsList[] popupsList;
 
    void Start()
    {
        eventsManager = EventsManager.current;
        popupsEvents = EventsManager.popupsEvents;
        databaseManager = DatabaseManager.manager;

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
                    //popupsEvents.onConfirmationPopupAction += (p) => onConfirmationOpen(p);
                    break;
                case "Game Lost":
                    //popupsEvents.onGameLostAction += (p) => onGameLost(p);
                    break;
                case "Level Change":
                    //popupsEvents.onChangeLevelAction += (p) => onChangeLevel(p);
                    break;
            }
        }
    }

    void onGameLost(bool status, GameObject element)
    {
        Image circle = popupsList[0].Element.transform.Find("Popup/Icon/Circle").GetComponent<Image>(); 

        if (status) {
            popupsList[0].Element.SetActive(true);
            popupsList[0].Element.transform
                        .DOScale(new Vector3(1, 1, 1), 0.2f)
                        .SetEase(Ease.OutBack)
                        .OnComplete(() => {
                            StartCoroutine(CircleFill(circle, 300, () => { SceneManager.LoadScene("Menu"); }));
                        });
        } else {
            popupsList[0].Element.transform.DOScale(new Vector3(0, 0, 0), 0.2f).OnComplete(() => { 
                popupsList[0].Element.SetActive(false); 
            });
        }
    }

    void onChangeLevel(Action callback)
    {
        GameDataConfig gameDataConfig = DatabaseManager.manager.LoadSaving();
        Image circle = popupsList[1].Element.transform.Find("Icon/Circle").GetComponent<Image>();

        popupsList[1].Element.SetActive(true);
        popupsList[1].Element.transform
                     .DOScale(new Vector3(1, 1, 1), 0.2f)
                     .SetEase(Ease.OutBack)
                     .SetDelay(2)
                     .OnComplete(() => {
                         StartCoroutine(CircleFill(circle, 5, () => {
                             callback?.Invoke();
                             popupsList[1].Element.transform.DOScale(new Vector3(0, 0, 0), 0.2f).SetDelay(2).OnComplete(() => {
                                eventsManager.changeTimerAction(true, true);
                                popupsList[1].Element.SetActive(false);
                             });
                         }));
                    });
    }

    void onConfirmationOpen(bool status)
    {
        if (status) {
            popupsList[2].Element.SetActive(true);
            popupsList[2].Element.transform
                         .DOScale(new Vector3(1, 1, 1), 0.2f)
                         .SetEase(Ease.OutBack);
        } else {
            popupsList[2].Element.transform.DOScale(new Vector3(0, 0, 0), 0.2f).OnComplete(() => { popupsList[2].Element.SetActive(false); });
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
}
