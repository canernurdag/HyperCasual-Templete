using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Character_Movement : MonoBehaviour
{
    [Header("Raycast")]
    public Ray_Creator _myRayCreator;
    public Vector3 _Up, _Right, _Down, _Left;
    public Vector3 _nextCollisionPosition;
    public GameObject _nextCollidingObject;
    public Vector3 _nextCharacterPosition;

    [Header("Movement")]
    public float _distanceCharacterWillMove;
    public float _characterSize;
    public int _stepQuantityCharacterWillMove;
    public float _durationCharacterToReach;
    public float _characterMoveSpeed;
    public bool _CanCharacterMove;

    [Header("Particle System")]
    public Character_Particles _myCharacterParticles;

    [Header("Other")]
    public GameObject _null; // Created to staisfy event type

    private void OnEnable()
    {
        SwipeDetector.OnSwipe += CharacterMove;
        Event_Manager._Instance._onCharacterHitFailLeft1 += MakeCharacterNonMove;
        Event_Manager._Instance._onCharacterHitFailRight1 += MakeCharacterNonMove;
        Event_Manager._Instance._onNextLevel1 += MakeCharacterNonMove;
    }

    private void Start()
    {
        DOTween.Init(); // Dotween Init

        //Cache
        _myRayCreator = FindObjectOfType<Ray_Creator>();
        _myCharacterParticles = GetComponent<Character_Particles>();
        _characterSize = GetComponent<BoxCollider>().bounds.size.x;

        //Declerations
        _Up = new Vector3(0, 0, 1);
        _Right = new Vector3(1, 0, 0);
        _Down = new Vector3(0, 0, -1);
        _Left = new Vector3(-1, 0, 0);

        _characterMoveSpeed = 10;
        _CanCharacterMove = true;
    }

    public void MakeCharacterNonMove(GameObject _null)
    {
        StartCoroutine(MakeCharacterNonMoveHelper());
    }
    public IEnumerator MakeCharacterNonMoveHelper()
    {
        yield return new WaitForSeconds(0.2f);
        SwipeDetector.OnSwipe -= CharacterMove;
    }
    public IEnumerator MakeCharacterMove(GameObject _null)
    {
        yield return new WaitForSeconds(0.1f);
        SwipeDetector.OnSwipe += CharacterMove;
    }
    public void CharacterMove(SwipeData _mySwipeData)
    {
        if(_CanCharacterMove)
        { 
            switch(_mySwipeData.Direction)
            {
                case SwipeDirection.Up:
                    GetNextCollidingPoint(_Up);
                    GetNextCharacterPosition(_Up);
                    GetDurationCharacterToReach();
                    CharacterMovement();

                    break;
                case SwipeDirection.Right:
                    GetNextCollidingPoint(_Right);
                    GetNextCharacterPosition(_Right);
                    GetDurationCharacterToReach();
                    CharacterMovement();

                    break;
                case SwipeDirection.Down:
                    GetNextCollidingPoint(_Down);
                    GetNextCharacterPosition(_Down);
                    GetDurationCharacterToReach();
                    CharacterMovement();

                    break;
                case SwipeDirection.Left:
                    GetNextCollidingPoint(_Left);
                    GetNextCharacterPosition(_Left);
                    GetDurationCharacterToReach();
                    CharacterMovement();

                    break;
            }
        }
    }

    public void GetNextCollidingPoint(Vector3 _currentDirection)
    {
        _myRayCreator.GetNextCollidingPoint(_currentDirection);
        _nextCollisionPosition = _myRayCreator._nextCollisionPosition;
        _nextCollidingObject = _myRayCreator._nextCollidingObject;
        
    }
    public void GetNextCharacterPosition(Vector3 _currentDirection)
    {
        _distanceCharacterWillMove = Vector3.Distance(_nextCollisionPosition, transform.position);
        _stepQuantityCharacterWillMove = Mathf.FloorToInt(_distanceCharacterWillMove / _characterSize);
        _nextCharacterPosition = transform.position + _currentDirection * _stepQuantityCharacterWillMove;
    }
    public void GetDurationCharacterToReach()
    {
        _durationCharacterToReach = Vector3.Distance(_nextCharacterPosition, transform.position) / _characterMoveSpeed;
    }
    public void CharacterMovement()
    {
        MakeCharacterNonMove(_null);
        _CanCharacterMove = false;

        SFX_Manager._Instance.NextStage(_null);  //Using the same SFX
        _myCharacterParticles.PlayDustParticles();
        transform.DOMove(_nextCharacterPosition, _durationCharacterToReach).SetEase(Ease.Linear).OnComplete(()=> 
        {
            _CanCharacterMove = true;
            _myCharacterParticles.StopDustParticles();
            StartCoroutine(MakeCharacterMove(_null));
        });
    }

    private void OnDisable()
    {
        SwipeDetector.OnSwipe -= CharacterMove;
        Event_Manager._Instance._onCharacterHitFailLeft1 -= MakeCharacterNonMove;
        Event_Manager._Instance._onCharacterHitFailRight1 -= MakeCharacterNonMove;
        Event_Manager._Instance._onNextLevel1 -= MakeCharacterNonMove;
    }
}
