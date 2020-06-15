using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vibrate_Manager : MonoBehaviour
{
    public int _durationLong;
    public int _durationMid;
    public int _durationShort;

    private void OnEnable()
    {
        Event_Manager._Instance._onCharacterHitRewardMale += VibrateShort;
        Event_Manager._Instance._onCharacterHitRewardFemale += VibrateShort;
        Event_Manager._Instance._onCharacterHitFailLeft1 += VibrateMid;
        Event_Manager._Instance._onCharacterHitFailRight1 += VibrateMid;
        Event_Manager._Instance._onCharacterHitNextStageObject1 += VibrateShort;
        Event_Manager._Instance._onNextLevel1 += VibrateLong;
    }

    private void Start()
    {
        _durationLong = 150;
        _durationMid = 80;
        _durationShort = 30;
    }

    public void VibrateLong(GameObject _null)
    {
        if(User_Manager._Instance._IsVibrateOn)
        { 
            Vibration.Vibrate(_durationLong);
        }
    }

    public void VibrateMid(GameObject _null)
    {
        if (User_Manager._Instance._IsVibrateOn)
        {
            Vibration.Vibrate(_durationMid);
        }
    }

    public void VibrateShort(GameObject _null)
    {
        if (User_Manager._Instance._IsVibrateOn)
        {
            Vibration.Vibrate(_durationShort);
        }
    }

    public void OnDisable()
    {
        Event_Manager._Instance._onCharacterHitRewardMale -= VibrateShort;
        Event_Manager._Instance._onCharacterHitRewardFemale -= VibrateShort;
        Event_Manager._Instance._onCharacterHitFailLeft1 -= VibrateMid;
        Event_Manager._Instance._onCharacterHitFailRight1 -= VibrateMid;
        Event_Manager._Instance._onCharacterHitNextStageObject1 -= VibrateShort;
        Event_Manager._Instance._onNextLevel1 -= VibrateLong;
    }

}
