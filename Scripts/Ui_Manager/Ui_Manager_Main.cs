using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Ui_Manager_Main : MonoBehaviour
{
    public RectTransform _mainCanvas;
    public RectTransform _settingsCanvas;

    public Button _soundButton;
    public Button _vibrateButton;

    public Sprite _soundOnSprite;
    public Sprite _soundOffSprite;
    public Sprite _vibrateOnSprite;
    public Sprite _vibrateOffSprite;

    GameObject _null;

    public Camera_Audio _myCameraAudio;
    private void Start()
    {
        DOTween.Init();
        _myCameraAudio = FindObjectOfType<Camera_Audio>();

        MainCanvasEntrence();

        Invoke("InitialIconSetup", 0.4f);

    }

    private void InitialIconSetup()
    {
        if (User_Manager._Instance._IsVibrateOn)
        {
            _vibrateButton.GetComponent<Image>().sprite = _vibrateOnSprite;
        }
        else if (!User_Manager._Instance._IsVibrateOn)
        {
            _vibrateButton.GetComponent<Image>().sprite = _vibrateOffSprite;
        }

        if (User_Manager._Instance._IsSoundOn)
        {
            _soundButton.GetComponent<Image>().sprite = _soundOnSprite;
        }
        else if (!User_Manager._Instance._IsSoundOn)
        {
            _soundButton.GetComponent<Image>().sprite = _soundOffSprite;
        }
    }

    private void MainCanvasEntrence()
    {
        _mainCanvas.DOAnchorPos(Vector2.zero, 1.5f);
    }

    public void PlayButton() //on click
    {
        Event_Manager._Instance.UiButtonDownSequence();
        SceneManager.LoadScene(1); //load level scene
    }

    public void SettingsButton() // on click
    {
        _settingsCanvas.DOScale(Vector3.one, 0.25f);
    }

    public void ExitButton() //on click
    {
        _settingsCanvas.DOScale(Vector3.zero, 0.25f);
    }

    public void SoundButton() // on click
    {
     
        if(User_Manager._Instance._IsSoundOn)
        {
            User_Manager._Instance._IsSoundOn = false;
            User_Manager._Instance.SaveUserLocal(_null);
            _soundButton.GetComponent<Image>().sprite = _soundOffSprite;

        }
        else if(!User_Manager._Instance._IsSoundOn)
        {
            User_Manager._Instance._IsSoundOn = true;
            User_Manager._Instance.SaveUserLocal(_null);
            _soundButton.GetComponent<Image>().sprite = _soundOnSprite;
        }
        Event_Manager._Instance.HitSoundButton(_null);
    }

    public void VibrateButton() //on click
    {
        if(User_Manager._Instance._IsVibrateOn)
        {
            User_Manager._Instance._IsVibrateOn = false;
            User_Manager._Instance.SaveUserLocal(_null);
            _vibrateButton.GetComponent<Image>().sprite = _vibrateOffSprite;
        }
        else if(!User_Manager._Instance._IsVibrateOn)
        {
            User_Manager._Instance._IsVibrateOn = true;
            User_Manager._Instance.SaveUserLocal(_null);
            _vibrateButton.GetComponent<Image>().sprite = _vibrateOnSprite;
        }
    }

 }
