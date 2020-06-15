using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage_Manager : MonoBehaviour
{
    public RewardFemale[] _levelRewardFemalesArray;
    public List<GameObject> _childList;

    public bool _IsLevelFinished;

    public GameObject[] _jumpPosesArray;

    private void Start()
    {
        _levelRewardFemalesArray = FindObjectsOfType<RewardFemale>();

        for (int i = 0; i<_levelRewardFemalesArray.Length;i++)
        {
            _childList.Add(_levelRewardFemalesArray[i].transform.GetChild(0).gameObject);
        }

    }

    public void IsLevelFinished()
    {
        if (_childList.Count == 0)
        {
            _IsLevelFinished = true;
        }
        else
            _IsLevelFinished = false;
    }



}
  
