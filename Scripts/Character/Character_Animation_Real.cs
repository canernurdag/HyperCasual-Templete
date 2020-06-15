using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This is for character mesh animations

public class Character_Animation_Real : MonoBehaviour
{
    public Animator _myAnimator;

    private void OnEnable()
    {
        Event_Manager._Instance._onNextLevel1 += WinAnimation;
    }

    private void Start()
    {
        _myAnimator = GetComponent<Animator>();
    }

    public void WinAnimation(GameObject _null)
    {
        _myAnimator.SetBool("Win", true);
    }

    private void OnDisable()
    {
        Event_Manager._Instance._onNextLevel1 -= WinAnimation;
    }
}
