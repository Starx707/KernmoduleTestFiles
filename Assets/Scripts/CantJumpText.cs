using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CantJumpText : MonoBehaviour
{
    TextMeshProUGUI text;
    ThirdPersonMovementScript player;
    Vector3 offset;
    int pointer = 0;
    float timer = 0;
    bool displaying = false;

    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        player = FindObjectOfType<ThirdPersonMovementScript>();
        offset = this.transform.position - player.transform.position;

    }

    void Update()
    {
        if (displaying)
        {
            if(timer < 0)
                Clear();
            else
                timer -= Time.deltaTime;
        }
    }

    void Clear()
    {
        text.text = "";
        displaying = false;
    }

    public void DisplayText()
    {
        if(displaying) return;
        
        transform.position = player.transform.position + offset;

        switch (pointer)
        {
            case 0:
                text.text = "Did you see him trying to jump there?\r\n\t\tNo Way!";
                break;
            case 1:
                text.text = "Huh? That seems illegal.";
                break;
            case 2:
                text.text = "You there! Stop that!";
                break;
            case 3:
                text.text = "Did he really just try to jump?";
                break;
            case 4:
                text.text = "How dare you!";
                break;
        }
        pointer++;
        if(pointer > 4) pointer = 0;
        displaying = true;
        timer = 3;
    }
}
