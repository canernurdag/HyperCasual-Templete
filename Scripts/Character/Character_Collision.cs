using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Collision : MonoBehaviour
{
    Character_Movement _myCharacterMovement;
    Stage_Manager _myStageManager;

    GameObject _null;
 
    private void Start()
    {
        _myCharacterMovement = GetComponent<Character_Movement>();
        _myStageManager = FindObjectOfType<Stage_Manager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("RewardMale"))
        {
            if(!_myCharacterMovement._CanCharacterMove)
            { 
                Event_Manager._Instance.CharacterHitRewardMaleSequence(other.gameObject);
            }
        }

        else if(other.gameObject.layer == LayerMask.NameToLayer("NextStage"))
        {
            Event_Manager._Instance.CharacterHitNextStageObjectSequence(other.gameObject);
        }

        else if (other.gameObject.layer == LayerMask.NameToLayer("FailRight"))
        {
            StartCoroutine(Event_Manager._Instance.CharacterHitFailRightSequence(other.gameObject));
        }

        else if (other.gameObject.layer == LayerMask.NameToLayer("FailLeft"))
        {
            StartCoroutine(Event_Manager._Instance.CharacterHitFailLeftSequence(other.gameObject));
        }


        else if (other.gameObject.layer == LayerMask.NameToLayer("RewardFemale"))
        {

            Event_Manager._Instance.CharacterHitRewardFemaleSequence(other.gameObject);

            //CHECK _ISLEVELFINISHED
            _myStageManager.IsLevelFinished(); 
            if(_myStageManager._IsLevelFinished)
            {
                StartCoroutine(Event_Manager._Instance.NextLevelSequence(other.gameObject));
            }
        }

        else if(other.gameObject.layer == LayerMask.NameToLayer("Background"))
        {
            Event_Manager._Instance.CharacterHitToBeDestroyedSequence(other.gameObject);
        }
    }

   
}
