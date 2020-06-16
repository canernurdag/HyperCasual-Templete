using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class UserData
{
    public List<bool> _userLevelsListData;
    public bool _IsSoundOnData;
    public bool _IsVibrateOnData;

    public UserData()
    {
        _userLevelsListData = User_Manager._Instance._userLevelsList;
        _IsSoundOnData = User_Manager._Instance._IsSoundOn;
        _IsVibrateOnData = User_Manager._Instance._IsVibrateOn;
    }
}
