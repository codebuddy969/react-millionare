using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelsManager : MonoBehaviour
{
    EventsManager eventsManager;

    // Start is called before the first frame update
    void Start()
    {
        eventsManager = EventsManager.current;

        eventsManager.onLevelsAction += slide;
    }

    void slide(Hashtable parameters, Action callback) 
    {
        highlightLevel();

        if((bool)parameters["opened"]) {
            gameObject.SetActive(true);
            gameObject.transform
                      .DOLocalMoveX(-178, parameters["time"] != null ? (float)parameters["time"] : 0.2f)
                      .OnComplete(() => { callback?.Invoke(); });
        } else {
            gameObject.transform.DOLocalMoveX(400, 0.2f).OnComplete(() => { gameObject.SetActive(false); });
        }
    }

    void highlightLevel()
    {   
        for (int i = 0; i <= 14; i++) {
            gameObject.transform
                      .Find("Container")
                      .GetChild(i)
                      .Find("Numerotation")
                      .GetComponent<TextMeshProUGUI>()
                      .color = new Color32(255,205,3,255);
            gameObject.transform
                      .Find("Container")
                      .GetChild(i)
                      .Find("Reward")
                      .GetComponent<TextMeshProUGUI>()
                      .color = new Color32(255,205,3,255);
            gameObject.transform
                      .Find("Container")
                      .GetChild(i)
                      .GetComponent<Image>()
                      .color = new Color32(255,255,255,0);
        }

        GameDataConfig gameDataConfig = DatabaseManager.manager.LoadSaving();

        gameObject.transform
                  .Find("Container")
                  .GetChild(gameDataConfig.level)
                  .Find("Numerotation")
                  .GetComponent<TextMeshProUGUI>()
                  .color = new Color32(255,255,225,255);
        gameObject.transform
                  .Find("Container")
                  .GetChild(gameDataConfig.level)
                  .Find("Reward")
                  .GetComponent<TextMeshProUGUI>()
                  .color = new Color32(255,255,225,255);
        gameObject.transform
                  .Find("Container")
                  .GetChild(gameDataConfig.level)
                  .GetComponent<Image>()
                  .color = new Color32(255,255,225,255);
    }
}
