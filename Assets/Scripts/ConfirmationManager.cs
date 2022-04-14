using DG.Tweening;
using System;
using UnityEngine;
using System.Collections;

public class ConfirmationManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        EventsManager.current.onConfirmationPopupAction += popup;
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
