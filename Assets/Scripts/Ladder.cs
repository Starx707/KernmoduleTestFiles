using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    [SerializeField]
    private Transform _bottom;
    [SerializeField]
    private Transform _top;

    public Vector3 GetEndPosition(Vector3 playerPos)
    {
        float bottomDist = Vector3.Distance(playerPos, _bottom.position);
        float topDist = Vector3.Distance(playerPos, _top.position);
        return bottomDist <= topDist ? _top.position : _bottom.position;
    }
}
