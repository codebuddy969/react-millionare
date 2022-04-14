using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMenuManager : MonoBehaviour
{
    public void loadMenuScene()
    {
        SceneManager.LoadScene("Menu");
    }

    public void optionsPopup(bool status)
    {
        Hashtable parameters = new Hashtable();

        parameters["opened"] = status;

        EventsManager.current.optionsPopupAction(parameters, () => {  });
    }

    public void confirmationPopup(bool status)
    {
        Hashtable parameters = new Hashtable();

        parameters["opened"] = status;

        EventsManager.current.confirmationPopupAction(parameters, () => {  });
    }

    public void Levels(bool status)
    {
        Hashtable parameters = new Hashtable();

        parameters["opened"] = status;

        EventsManager.current.levelsAction(parameters, () => {  });
    }
}
