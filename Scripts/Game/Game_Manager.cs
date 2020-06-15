using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Manager : MonoBehaviour
{
    public Character_Movement _myCharacterMovement;
    public Stage_Manager _myStageManager;

    public List<GameObject> _characterStackList;
    public List<GameObject> _tempList;
    public GameObject _characterPrefab;
    public GameObject _levelMalePrefab;
    public GameObject _stackInHierarchy;
    public bool _CanInteract;
    public int _stackCountPre;

    public GameObject _null;

    private void OnEnable()
    {
        Event_Manager._Instance._onCharacterHitRewardMale += AddRewardMaleToStack;
        Event_Manager._Instance._onCharacterHitRewardFemale += SubstractARewardMaleFromStack;
        Event_Manager._Instance._onCharacterHitNextStageObject1 += SubstaractCalculatedRewardMalesFromStackToGetNextStage;
        Event_Manager._Instance._onCharacterHitFailRight1 += FailFunctionRight;
        Event_Manager._Instance._onCharacterHitFailLeft1 += FailFunctionLeft;
        Event_Manager._Instance._onCharacterHitToBeDestroyed += CharacterDestroy;
    }

    private void Start()
    {
        //Cache
        _myCharacterMovement = FindObjectOfType<Character_Movement>();
        _myStageManager = FindObjectOfType<Stage_Manager>();

        InstantiateTheChracterMesh();
        InstantiateTheFirstStackOfCharacter();
        _CanInteract = true;
    }

    private void InstantiateTheChracterMesh()
    {
        GameObject _tempChar = Instantiate(_characterPrefab,transform.position, Quaternion.Euler(0, 180, 0));
        _characterStackList.Add(_tempChar);
        _characterStackList[0].transform.SetParent(_stackInHierarchy.transform);
        _tempChar.transform.position = transform.position + Vector3.up;
    }

    private void InstantiateTheFirstStackOfCharacter()
    {
        GameObject _firstStack = Instantiate(_levelMalePrefab, this.transform.position, Quaternion.Euler(-90, 0, 0));
        _characterStackList.Add(_firstStack);
        _characterStackList[1].transform.SetParent(_stackInHierarchy.transform);
    }
    public void AddRewardMaleToStack(GameObject _collisionGameobject)
    {
        for(int i=0; i<_characterStackList.Count ;i++)
        {
            _characterStackList[i].transform.position = new Vector3(_characterStackList[i].transform.position.x,
                                                                _characterStackList[i].transform.position.y + _myCharacterMovement._characterSize, 
                                                                _characterStackList[i].transform.position.z);
        }
        _collisionGameobject.transform.position = transform.position;
        _collisionGameobject.transform.SetParent(_stackInHierarchy.transform);
        _characterStackList.Add(_collisionGameobject);
    }
    public void SubstractARewardMaleFromStack(GameObject _collision)
    {
        for (int i = 0; i < _characterStackList.Count; i++)
        {
            _characterStackList[i].transform.position = new Vector3(_characterStackList[i].transform.position.x,
                                                                _characterStackList[i].transform.position.y - _myCharacterMovement._characterSize,
                                                                _characterStackList[i].transform.position.z);
        }

        _collision.gameObject.transform.GetChild(0).gameObject.SetActive(true);
        _collision.gameObject.GetComponent<BoxCollider>().enabled = false;
        Destroy(_characterStackList[_characterStackList.Count - 1]);
        _tempList.RemoveAt(_tempList.Count - 1);
        _characterStackList = new List<GameObject>();
        _characterStackList = _tempList;

        _myStageManager._childList.RemoveAt(0);
      
 
    }
    public void SubstaractCalculatedRewardMalesFromStackToGetNextStage(GameObject other)
    {
        switch (other.gameObject.GetComponent<Next_Stage>()._numberOfNextStageObjectInLevel)
        {
            case 1:
                SubstractMainFunction(1);
                break;
            case 2:
                SubstractMainFunction(2);
                break;
            case 3:
                SubstractMainFunction(3);
                break;

        }
    }
    public void SubstractMainFunction(int _var)
    {
        _stackCountPre = _characterStackList.Count;

        if (_characterStackList[1].transform.position.y < _myStageManager._jumpPosesArray[_var-1].transform.position.y) //Check if the the top object is at lower position.y
        {
            return;
        }
        else
        {
            _tempList = new List<GameObject>();

            for (int i = 0; i < _characterStackList.Count; i++)
            {
                //Seperate objects cannot pass to next stage
                if (_characterStackList[i].transform.position.y < _myStageManager._jumpPosesArray[_var-1].transform.position.y)
                {
                    _characterStackList[i].layer = LayerMask.NameToLayer("Empty");
                    _characterStackList[i].transform.SetParent(_myStageManager.transform);
                }

                else if (_characterStackList[i].transform.position.y >= _myStageManager._jumpPosesArray[_var - 1].transform.position.y)
                {
                    _tempList.Add(_characterStackList[i]);
                }
            }

            _characterStackList = new List<GameObject>();
            _characterStackList = _tempList;
    
            //Wait For Dotween's finish
            StartCoroutine(WaitFunctionStage(_var));

        }
    }
    public IEnumerator WaitFunctionStage(int _var)
    {
        yield return new WaitForSeconds(0.05f);
        for (int i = 0; i < _characterStackList.Count; i++)
        {
            _characterStackList[i].transform.position = _characterStackList[i].transform.position + Vector3.down * (_stackCountPre - _characterStackList.Count);
        }
        this.gameObject.transform.position = _myStageManager._jumpPosesArray[_var - 1].transform.position;
        SFX_Manager._Instance.NextStage(_null);
        
    }
    public void FailFunctionRight(GameObject _null)
    {
        StartCoroutine(FailRightHelpFunction());
    }
    public void FailFunctionLeft(GameObject _null)
    {
       StartCoroutine(FailLeftHelpFunction());
    }
    private IEnumerator FailRightHelpFunction()
    {
        yield return new WaitForSeconds(1.5f); // To avoid character fall asap
        for (int i = 0; i < _characterStackList.Count; i++)
        {
            _characterStackList[i].AddComponent<Rigidbody>();
            _characterStackList[i].GetComponent<Rigidbody>().AddForce(new Vector3(UnityEngine.Random.Range(50, 100), -2, UnityEngine.Random.Range(-20, 20)));
        }
    }
    private IEnumerator FailLeftHelpFunction()
    {
        yield return new WaitForSeconds(1.5f); // To avoid character fall asap
        for (int i = 0; i < _characterStackList.Count; i++)
        {
            _characterStackList[i].AddComponent<Rigidbody>();
            _characterStackList[i].GetComponent<Rigidbody>().AddForce(new Vector3(UnityEngine.Random.Range(-30, -10), -2, UnityEngine.Random.Range(-5, 5)));
        }
    }
    public void CharacterDestroy(GameObject _gameObject)
    {
        Destroy(_gameObject);
    }

    private void OnDisable()
    {
        Event_Manager._Instance._onCharacterHitRewardMale -= AddRewardMaleToStack;
        Event_Manager._Instance._onCharacterHitRewardFemale -= SubstractARewardMaleFromStack;
        Event_Manager._Instance._onCharacterHitNextStageObject1 -= SubstaractCalculatedRewardMalesFromStackToGetNextStage;
        Event_Manager._Instance._onCharacterHitFailRight1 -= FailFunctionRight;
        Event_Manager._Instance._onCharacterHitFailLeft1 -= FailFunctionLeft;
        Event_Manager._Instance._onCharacterHitToBeDestroyed -= CharacterDestroy;
    }




 

}

