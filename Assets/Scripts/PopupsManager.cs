using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;

public class PopupsManager : MonoBehaviour
{
    EventsManager eventsManager;
    public GameObject gameLostPopup, preloaderPopup, confirmationPopup;
    Image gameLostPopupCircle, preloaderPopupCircle;

    void Start()
    {
        eventsManager = EventsManager.current;

        eventsManager.onGameLostAction += gameLost;
        eventsManager.onChangeLevelAction += changeLevel;
        eventsManager.onConfirmationPopupAction += leaveGame;

        gameLostPopupCircle = gameLostPopup.transform.Find("Popup/Icon/Circle").GetComponent<Image>();
        preloaderPopupCircle = preloaderPopup.transform.Find("Icon/Circle").GetComponent<Image>();
    }

    void gameLost(bool status)
    {
        if(status) {
            gameLostPopup.SetActive(true);
            gameLostPopup.transform
                         .DOScale(new Vector3(1, 1, 1), 0.2f)
                         .SetEase(Ease.OutBack)
                         .OnComplete(() => {
                            StartCoroutine(CircleFill(gameLostPopupCircle, 300, () => { SceneManager.LoadScene("Menu"); }));
                         });
        } else {
            gameLostPopup.transform.DOScale(new Vector3(0, 0, 0), 0.2f).OnComplete(() => { 
                gameObject.SetActive(false); 
            });
        }
    }

    void changeLevel(Action callback)
    {
        GameDataConfig gameDataConfig = DatabaseManager.manager.LoadSaving();

        preloaderPopup.SetActive(true);
        preloaderPopup.transform
                     .DOScale(new Vector3(1, 1, 1), 0.2f)
                     .SetEase(Ease.OutBack)
                     .SetDelay(2)
                     .OnComplete(() => {
                         StartCoroutine(CircleFill(preloaderPopupCircle, 5, () => {
                             callback?.Invoke();
                             preloaderPopup.transform.DOScale(new Vector3(0, 0, 0), 0.2f).SetDelay(2).OnComplete(() => {
                                eventsManager.changeTimerAction(true, true);
                                preloaderPopup.SetActive(false);
                             });
                         }));
                    });
    }

    void leaveGame(Hashtable parameters)
    {
        if((bool)parameters["opened"]) {
            confirmationPopup.SetActive(true);
            confirmationPopup.transform
                      .DOScale(new Vector3(1, 1, 1), 0.2f)
                      .SetEase(Ease.OutBack);
        } else {
            confirmationPopup.transform.DOScale(new Vector3(0, 0, 0), 0.2f).OnComplete(() => { gameObject.SetActive(false); });
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
