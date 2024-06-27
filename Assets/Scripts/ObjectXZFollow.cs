using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectXZFollow : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _followers;
    [SerializeField]
    private GameObject _leader;
 

    void FollowTheLeader()
    {
        foreach (GameObject _follower in _followers)
        {
            Vector3 newPosition = new Vector3(_leader.transform.position.x, _follower.transform.position.y, _leader.transform.position.z);
            _follower.transform.position = newPosition;
        }
    }

    void FixedUpdate()
    {
        FollowTheLeader();
    }
}
