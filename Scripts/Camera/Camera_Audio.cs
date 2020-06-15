using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Audio : MonoBehaviour
{
    public AudioListener _myAudioListener;
    public GameObject _null;

    private void OnEnable()
    {
        Event_Manager._Instance._onHitSoundButton += CheckAudioIsAvailable;
    }


    private void Start()
    {
        _myAudioListener = GetComponent<AudioListener>();
        CheckAudioIsAvailable(_null);
    }

    public void CheckAudioIsAvailable(GameObject _null)
    {
        if(User_Manager._Instance._IsSoundOn)
        {
            _myAudioListener.enabled = true;
        }
        else if(!User_Manager._Instance._IsSoundOn)
        {
            _myAudioListener.enabled = false;
        }
    }

    private void OnDisable()
    {
        Event_Manager._Instance._onHitSoundButton -= CheckAudioIsAvailable;
    }


}
