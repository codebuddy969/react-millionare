using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using System.Collections;

public class LevelsManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        EventsManager.current.onLevelsAction += slide;
    }

    public void slide(Hashtable parameters, Action callback) 
    {
        if((bool)parameters["opened"]) {
            gameObject.SetActive(true);
            gameObject.transform
                      .DOLocalMoveX(-178, parameters["time"] != null ? (float)parameters["time"] : 0.2f)
                      .OnComplete(() => { callback?.Invoke(); });
        } else {
            gameObject.transform.DOLocalMoveX(400, 0.2f).OnComplete(() => { gameObject.SetActive(false); });
        }
    }
}
