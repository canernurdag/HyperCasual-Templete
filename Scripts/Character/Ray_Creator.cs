using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ray_Creator : MonoBehaviour
{
    public float _rayDistance;
    public Vector3 _nextCollisionPosition;
    public GameObject _nextCollidingObject;

    public Game_Manager _myGameManager;

    private void Start()
    {
        _myGameManager = FindObjectOfType<Game_Manager>();

        _rayDistance = 100;
    }

    public void GetNextCollidingPoint(Vector3 _currentDirection)
    { 
        RaycastHit _hit;

        if(Physics.Raycast(transform.position, _currentDirection,out _hit, _rayDistance, ~LayerMask.NameToLayer("RewardMale"))) 
        {
            _nextCollisionPosition = _hit.point;
            _nextCollidingObject = _hit.collider.gameObject;
        }
    }

}
