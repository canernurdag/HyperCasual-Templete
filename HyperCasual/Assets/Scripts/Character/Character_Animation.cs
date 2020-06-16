using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This is the main object's animation, not chracter mesh.
//For the purpose of using Cinemachine State-driven system

public class Character_Animation : MonoBehaviour
{
    public Animator _myAnimator;

    private void OnEnable()
    {
        Event_Manager._Instance._onCharacterHitFailRight1 += SetFailRightAnimation;
        Event_Manager._Instance._onCharacterHitFailLeft1 += SetFailLeftAnimation;
    }

    private void Start()
    {
        _myAnimator = GetComponent<Animator>();
    }

    public void SetFailRightAnimation(GameObject _null)
    {
        _myAnimator.SetBool("FailRight", true);
    }

    public void SetFailLeftAnimation(GameObject _null)
    {
        _myAnimator.SetBool("FailLeft", true);
    }


    private void OnDisable()
    {
        Event_Manager._Instance._onCharacterHitFailRight1 -= SetFailRightAnimation;
        Event_Manager._Instance._onCharacterHitFailLeft1 -= SetFailLeftAnimation;
    }

}
