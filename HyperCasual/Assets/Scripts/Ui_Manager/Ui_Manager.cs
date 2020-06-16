using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class Ui_Manager : MonoBehaviour
{
    public RectTransform _preGameCanvas;
    public RectTransform _GameCanvas;
    public RectTransform _GameOverCanvas;
    public RectTransform _LevelCompletedCanvas;
  
    public GameOver_Button _tapToContinueButton;

    public TMP_Text _levelText;
    public TMP_Text _levelCompletedText;

    public RectTransform _settingsCanvas;

    public Button _soundButton;
    public Button _vibrateButton;

    public Sprite _soundOnSprite;
    public Sprite _soundOffSprite;
    public Sprite _vibrateOnSprite;
    public Sprite _vibrateOffSprite;

    public GameObject _null;

    public Camera_Audio _myCameraAudio;

    private void OnEnable()
    {
        Event_Manager._Instance._onCharacterHitFailLeft1 += GameOverCanvasEntrance;
        Event_Manager._Instance._onCharacterHitFailRight1 += GameOverCanvasEntrance;
        Event_Manager._Instance._onNextLevel1 += LevelCompletedCanvasEntrence;
        SwipeDetector.OnSwipe += PreGameCanvasDissappear;
    }

    private void Start()
    {
        DOTween.Init();
        _myCameraAudio = FindObjectOfType<Camera_Audio>();

        //_tapToContinueButton = FindObjectOfType<GameOver_Button>();
        //_tapToContinueButton.GetComponent<Button>().interactable = false;
        //_tapToContinueButton.GetComponent<Button>().onClick.AddListener((Scene_Manager._Instance.GetLevelSceneUnityEvents));

        GetCurrentLevelToText();
        GetLevelCompltedText();

        Invoke("InitialIconSetup",0.4f);
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

    public void PreGameCanvasDissappear(SwipeData _mySwipeData)
    {
        _preGameCanvas.gameObject.SetActive(false);
        GameCanvasEntrance();
        SwipeDetector.OnSwipe -= PreGameCanvasDissappear;
    }


    public void GameCanvasEntrance()
    {
        _GameCanvas.DOAnchorPos(Vector2.zero, 1.5f);
    }

    public void GameOverCanvasEntrance(GameObject _null)
    {
        _GameCanvas.DOAnchorPos(new Vector2(0, 1600), 0.25f);
        _GameOverCanvas.DOAnchorPos(Vector2.zero, 2.5f).OnComplete(()=> { _tapToContinueButton.GetComponent<Button>().interactable = true; });
    }


    public void LevelCompletedCanvasEntrence(GameObject _null)
    {
        _GameCanvas.DOAnchorPos(new Vector2(0, 1600), 0.25f);
        _LevelCompletedCanvas.DOAnchorPos(Vector2.zero, 2.5f);
    }
    public void RetryButtonFunctiın()//On click
    {
        Scene_Manager._Instance.GetTheSameLevelAgain();
    }

    public void GetCurrentLevelToText()
    {
        _levelText.text = "Level " + User_Manager._Instance._currentLevel.ToString();
    }

    public void GetLevelCompltedText()
    {
        _levelCompletedText.text = "Level " + User_Manager._Instance._currentLevel.ToString() + " Completed";
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

        if (User_Manager._Instance._IsSoundOn)
        {
            User_Manager._Instance._IsSoundOn = false;
            User_Manager._Instance.SaveUserLocal(_null);
            _soundButton.GetComponent<Image>().sprite = _soundOffSprite;

        }
        else if (!User_Manager._Instance._IsSoundOn)
        {
            User_Manager._Instance._IsSoundOn = true;
            User_Manager._Instance.SaveUserLocal(_null);
            _soundButton.GetComponent<Image>().sprite = _soundOnSprite;
        }
        Event_Manager._Instance.HitSoundButton(_null);
    }

    public void VibrateButton() //on click
    {
        if (User_Manager._Instance._IsVibrateOn)
        {
            User_Manager._Instance._IsVibrateOn = false;
            User_Manager._Instance.SaveUserLocal(_null);
            _vibrateButton.GetComponent<Image>().sprite = _vibrateOffSprite;
        }
        else if (!User_Manager._Instance._IsVibrateOn)
        {
            User_Manager._Instance._IsVibrateOn = true;
            User_Manager._Instance.SaveUserLocal(_null);
            _vibrateButton.GetComponent<Image>().sprite = _vibrateOnSprite;
        }
    }


    private void OnDisable()
    {
       // _tapToContinueButton.GetComponent<Button>().onClick.RemoveListener((Scene_Manager._Instance.GetLevelSceneUnityEvents));
        Event_Manager._Instance._onCharacterHitFailLeft1 -= GameOverCanvasEntrance;
        Event_Manager._Instance._onCharacterHitFailRight1 -= GameOverCanvasEntrance;
        Event_Manager._Instance._onNextLevel1 -= LevelCompletedCanvasEntrence;
        SwipeDetector.OnSwipe -= PreGameCanvasDissappear;
    }
}
