using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveStatueTrigger : MonoBehaviour
{
    [SerializeField] MovableStatue statue;
    float timer = 1;
    bool triggered = false;
    bool finished = false;
    bool playerPresent = false;

    void Start()
    {
        
    }

    void Update()
    {
        if (triggered && !finished)
        {
            if(timer < 0)
            {
                Vector3 pos = statue.transform.position;
                pos.x -= 5 * Time.deltaTime;
                if(pos.x < statue.leftBoundary.transform.position.x)
                {
                    pos.x = statue.leftBoundary.transform.position.x;
                    finished = true;
                }
                statue.transform.position = pos;
            }
            else
                timer -= Time.deltaTime;
        }
        else if(!triggered && playerPresent && Input.GetKeyDown(KeyCode.Space))
            triggered = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerPresent = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerPresent = false;
        }
    }
}
