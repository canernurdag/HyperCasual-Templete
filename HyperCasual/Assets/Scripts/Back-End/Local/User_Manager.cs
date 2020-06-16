using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class User_Manager : MonoBehaviour
{
    //Static Instance
    public static User_Manager _Instance;

    #region VARIABLES

    public List<bool> _userLevelsList{get; set;}
    public List<bool> _tempLevelList { get; set; }//For internal calculate
    public int _currentLevel { get; set; }

    public bool _IsSoundOn;
    public bool _IsVibrateOn;

    GameObject _null;
    #endregion

    private void OnEnable()
    {
        Event_Manager._Instance._onNextLevel1 += RefreshLevelArrayWithSucceedCurrentLevel;
        Event_Manager._Instance._onNextLevel1 += SaveUserLocal;
    }

    private void Awake()
    {
        SingletonPatern();

        UserData _myUserData = UserSave.LoadUser();
        CreateANewUserIfNecessary(_myUserData);
    }

    private void SingletonPatern()
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
    private void CreateANewUserIfNecessary(UserData _myUserData)
    {
        if (_myUserData == null)
        {
            //Give Defult Setting Here

            _userLevelsList = new List<bool>
            {
                true,
                false,
                false

            };
            _IsVibrateOn = true;
            _IsSoundOn = true;

            SaveUserLocal(_null);
        }
    }

    public void Start()
    {
        _userLevelsList = LoadUserLevelListLocal();
        _IsVibrateOn = LoadIsVibrateOnLocal();
        _IsSoundOn = LoadIsSoundOnLocal();

        _tempLevelList = _userLevelsList;

    }

    public void SaveUserLocal(GameObject _null) 
    {
        UserSave.SaveUser(_Instance);
    }

    public void RefreshLevelArrayWithSucceedCurrentLevel(GameObject _null)
    {
        _userLevelsList = new List<bool>();
        for (int i = 0; i < _tempLevelList.Count; i++)
        {
            _userLevelsList.Add(_tempLevelList[i]);
        }
        
        
        if (_currentLevel <= _tempLevelList.Count - 1) // Check the level is not the last level.
        {
            _userLevelsList[_currentLevel] = true;
        }
        else if (_currentLevel == _tempLevelList.Count) // If last level
        {
            // Game Finished Actions are here.
            Debug.Log("Game Completed Successfully");
        }
        _tempLevelList = _userLevelsList;
    }


    public List<bool> LoadUserLevelListLocal()
    {
        UserData _myUserData = UserSave.LoadUser();
        _Instance._userLevelsList = _myUserData._userLevelsListData;

        return _Instance._userLevelsList;
    }

    public bool LoadIsVibrateOnLocal()
    {
        UserData _myUserData = UserSave.LoadUser();
        _Instance._IsVibrateOn = _myUserData._IsVibrateOnData;

        return _Instance._IsVibrateOn;
    }

    public bool LoadIsSoundOnLocal()
    {
        UserData _myUserData = UserSave.LoadUser();
        _Instance._IsSoundOn = _myUserData._IsSoundOnData;

        return _Instance._IsSoundOn;
    }

}
