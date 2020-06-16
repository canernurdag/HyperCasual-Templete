using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Event_Manager : MonoBehaviour
{
    public static Event_Manager _Instance; //Static Instance

    public event Action<GameObject> _onCharacterHitRewardMale;
    public event Action<GameObject> _onCharacterHitRewardFemale;
    public event Action<GameObject> _onCharacterHitFailRight1;
    public event Action<GameObject> _onCharacterHitFailRight2;
    public event Action<GameObject> _onCharacterHitFailLeft1;
    public event Action<GameObject> _onCharacterHitFailLeft2;
    public event Action<GameObject> _onCharacterHitNextStageObject1;
    public event Action<GameObject> _onCharacterHitNextStageObject2;
    public event Action<GameObject> _onNextLevel1;
    public event Action<GameObject> _onNextLevel2;
    public event Action _onUiButtonDown;
    public event Action<GameObject> _onCharacterHitToBeDestroyed;
    public event Action<GameObject> _onHitSoundButton;

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

    public void CharacterHitRewardMaleSequence(GameObject _gameObject)
    {
        if(_onCharacterHitRewardMale != null)
        {
            _onCharacterHitRewardMale(_gameObject);
        }
    }
    public void CharacterHitRewardFemaleSequence(GameObject _gameObject)
    {
       if(_onCharacterHitRewardFemale != null)
        {
            _onCharacterHitRewardFemale(_gameObject);
        }
    }
    public IEnumerator CharacterHitFailRightSequence(GameObject _gameObject)
    {
       
        if(_onCharacterHitFailRight1 != null)
        {
            _onCharacterHitFailRight1(_gameObject);
        }
        yield return new WaitForSeconds(4f);

        if(_onCharacterHitFailRight2 != null)
        {
            _onCharacterHitFailRight2(_gameObject);
        }
    }

    public IEnumerator CharacterHitFailLeftSequence(GameObject _gameObject)
    {
   
        if (_onCharacterHitFailLeft1 != null)
        {
            _onCharacterHitFailLeft1(_gameObject);
        }
        yield return new WaitForSeconds(4f);

        if (_onCharacterHitFailLeft2 != null)
        {
            _onCharacterHitFailLeft2(_gameObject);
        }
    }

    public void CharacterHitNextStageObjectSequence(GameObject _gameObject)
    {
        if(_onCharacterHitNextStageObject1 != null)
        {
            _onCharacterHitNextStageObject1(_gameObject);
        }
    }

    public IEnumerator NextLevelSequence(GameObject _null)
    {
        if (_onNextLevel1 != null)
        {
            _onNextLevel1(_null);
        }
        yield return new WaitForSeconds(4f);

        if (_onNextLevel2 != null)
        {
            _onNextLevel2(_null);
        }
    }

    public void UiButtonDownSequence()
    {
        if(_onUiButtonDown != null)
        {
            _onUiButtonDown();
        }
    }

    public void CharacterHitToBeDestroyedSequence(GameObject _gameObject)
    {
        if(_onCharacterHitToBeDestroyed != null)
        {
            _onCharacterHitToBeDestroyed(_gameObject);
        }
    }

    public void HitSoundButton(GameObject _null)
    {
        if(_onHitSoundButton != null)
        {
            _onHitSoundButton(_null);
        }
    }
}
