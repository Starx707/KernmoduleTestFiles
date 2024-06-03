using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerEventBox : MonoBehaviour
{
    public UnityEvent OnTriggerObject = new UnityEvent();

    private void OnTriggerEnter(Collider other)
    {
        //TODO: add check if the collision is the player
        OnTriggerObject.Invoke();
    }
}
