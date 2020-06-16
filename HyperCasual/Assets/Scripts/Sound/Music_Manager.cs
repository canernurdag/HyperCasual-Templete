using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music_Manager : MonoBehaviour
{
    public static Music_Manager _Instance;

    public AudioClip[] _audioClipArray;

    public AudioSource _myAudioSource;
    GameObject _null;

    private void OnEnable()
    {
        Event_Manager._Instance._onNextLevel1 += LevelCompleteSFX;
        Event_Manager._Instance._onHitSoundButton += CheckAudioIsAvailable;
    }

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

    private void Start()
    {
        _myAudioSource = GetComponent<AudioSource>();
        StartCoroutine(CheckAudioHelper());
    }
    public void PlayMusic(AudioClip _tempAudioClip)
    {
        _myAudioSource.clip = _tempAudioClip;
        _myAudioSource.Play();
    }
    public void StopMusic(AudioClip _tempAudioClip)
    {
        _myAudioSource.Stop();
    }

    public void LevelCompleteSFX(GameObject _null)
    {
        PlayMusic(_audioClipArray[0]);
    }

    public void CheckAudioIsAvailable(GameObject _null)
    {
        if (User_Manager._Instance._IsSoundOn)
        {
            _myAudioSource.volume = 1;
        }
        else if (!User_Manager._Instance._IsSoundOn)
        {
            _myAudioSource.volume = 0;
        }
    }

    public IEnumerator CheckAudioHelper()
    {
        yield return new WaitForSeconds(0.5f);
        CheckAudioIsAvailable(_null);
    }
    private void OnDisable()
    {
        Event_Manager._Instance._onNextLevel1 -= LevelCompleteSFX;
        Event_Manager._Instance._onHitSoundButton -= CheckAudioIsAvailable;
    }

}
