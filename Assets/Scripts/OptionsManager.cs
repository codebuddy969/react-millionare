using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OptionsManager : MonoBehaviour
{
    DatabaseManager databaseManager;
    Slider musicSlider;
    Slider effectsSlider;

    // Start is called before the first frame update
    void Start()
    {
        EventsManager.current.onChangeMusicVolumeAction += musicVolume;
        EventsManager.current.onChangeFxVolumeAction += fxVolume;

        databaseManager = DatabaseManager.manager;

        musicSlider = GameObject.Find("MusicSlider").GetComponent<Slider>();
        effectsSlider = GameObject.Find("EffectsSlider").GetComponent<Slider>();
    }

    void setSoundVolumeOnLoad()
    {
        GameDataConfig gameDataConfig = databaseManager.LoadSaving();
        
        musicSlider.value = gameDataConfig.musicVolume;
        effectsSlider.value = gameDataConfig.effectsVolume;
    }

    public void popup(Hashtable parameters, Action callback) 
    {
        if((bool)parameters["opened"]) {
            setSoundVolumeOnLoad();
            gameObject.SetActive(true);
            gameObject.transform
                      .DOScale(new Vector3(1, 1, 1), parameters["time"] != null ? (float)parameters["time"] : 0.2f)
                      .SetEase(Ease.OutBack)
                      .OnComplete(() => { callback?.Invoke(); });
        } else {
            gameObject.transform.DOScale(new Vector3(0, 0, 0), 0.2f).OnComplete(() => { gameObject.SetActive(false); });
        }
    }

    public void musicVolume(float parameter)
    {
        GameDataConfig gameDataConfig = databaseManager.LoadSaving();

        gameDataConfig.musicVolume = parameter;

        // audioManager.MusicVolumeControl(musicSlider.value / 3);

        databaseManager.CreateSaving(gameDataConfig);
    }

    public void fxVolume(float parameter)
    {
        GameDataConfig gameDataConfig = DatabaseManager.manager.LoadSaving();
        
        gameDataConfig.effectsVolume = parameter;

        databaseManager.CreateSaving(gameDataConfig);
    }
}
