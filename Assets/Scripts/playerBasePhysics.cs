using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerBasePhysics : MonoBehaviour
{
    Vector3 gravMoveVector;
    public CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }


    void Update()
    {
        gravMoveVector = Vector3.zero;
        if (controller.isGrounded == false )
        {
            gravMoveVector += Physics.gravity;
        }
        controller.Move( gravMoveVector * Time.deltaTime);
    }
}
