using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableStatue : MonoBehaviour
{
    public GameObject leftBoundary, rightBoundary;
    GameObject player;

    bool following = false;
    bool leftOfPlayer = false;
    float xOffset = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            following = true;
            leftOfPlayer = other.gameObject.transform.position.x < transform.position.x;
            xOffset = transform.position.x - other.gameObject.transform.position.x;
            player = other.gameObject;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            following = false;
        }
    }

    void Update()
    {
        if(following)
        {
            if((leftOfPlayer && MovedToLeft()) || (!leftOfPlayer && !MovedToLeft()))
            {
                Vector3 pos = transform.position;
                pos.x = player.transform.position.x + xOffset;
                if(pos.x < leftBoundary.transform.position.x || pos.x > rightBoundary.transform.position.x)
                {
                    following = false;
                    return;
                }
                transform.position = pos;
            }
        }
    }

    bool MovedToLeft()
    {
        return player.transform.position.x > transform.position.x - xOffset;
    }
}
