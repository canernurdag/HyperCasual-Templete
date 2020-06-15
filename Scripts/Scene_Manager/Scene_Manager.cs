using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_Manager : MonoBehaviour
{
    public static Scene_Manager _Instance;

    public int _currentSceneIndex { get; set; }


    #region SINGLETON Pattern
    private void Awake()
    {
        if (_Instance == null)
        {
            _Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    private void OnEnable()
    {
        Event_Manager._Instance._onNextLevel2 += GetLevelScene;
        Event_Manager._Instance._onCharacterHitFailLeft2 += GetLevelScene;
        Event_Manager._Instance._onCharacterHitFailRight2 += GetLevelScene;
    }
  
    public void GetCurrentSceneIndex(Scene _current, Scene _next)
    {

    }

    public void GetLevelScene(GameObject _null) // to use in Events
    {
        SceneManager.LoadScene(1);
    }
    public void GetLevelSceneUnityEvents() // To use in UnityEvents
    {
        SceneManager.LoadScene(1);
    }

    public void GetTheSameLevelAgain()
    {
        SceneManager.LoadScene(User_Manager._Instance._currentLevel + 1); 
    }

 
    private void OnDisable()
    {
        Event_Manager._Instance._onNextLevel2 -= GetLevelScene;
        Event_Manager._Instance._onCharacterHitFailLeft2 -= GetLevelScene;
        Event_Manager._Instance._onCharacterHitFailRight2 -= GetLevelScene;
    }



}
