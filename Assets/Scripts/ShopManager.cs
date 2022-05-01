using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using System.Collections;

public class ShopManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameDataConfig gameDataConfig = DatabaseManager.manager.LoadSaving();

        gameObject.transform.Find("Title/Score").GetComponent<TextMeshProUGUI>().text = (gameDataConfig.score).ToString();
    }

    public void popup(Hashtable parameters, Action callback) 
    {
        if((bool)parameters["opened"]) {
            gameObject.SetActive(true);
            gameObject.transform
                      .DOScale(new Vector3(1, 1, 1), parameters["time"] != null ? (float)parameters["time"] : 0.2f)
                      .SetEase(Ease.OutBack)
                      .OnComplete(() => { callback?.Invoke(); });
        } else {
            gameObject.transform.DOScale(new Vector3(0, 0, 0), 0.2f).OnComplete(() => { gameObject.SetActive(false); });
        }
    }
}
