using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CluesManager : MonoBehaviour
{
    EventsManager eventsManager;

    PopupsEvents popupsEvents;

    UIList interfaces;

    public GameObject document;

    void Start()
    {
        eventsManager = EventsManager.current;
        popupsEvents = EventsManager.popupsEvents;

        interfaces = new UIList(document);
    }

    void onClueClick(string name)
    {
        for (int i = 0; i <= interfaces.buttonsList.Length - 1; i++) 
        {
            Debug.Log(interfaces.buttonsList[i].GetComponent<Button>());
        }

        switch (name)
        {
            case "50on50":

                break;
            case "audience":
                popupsEvents.audiencePopupAction(true);
                break;
            case "ad":
                popupsEvents.audiencePopupAction(true);
                break;
        }
    }

    void on50on50Selection()
    {
        int index = Random.Range(0, 3);
    }
}
