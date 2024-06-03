using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextTrigger : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;

    // Whether the player first needs to jump to trigger or only needs to be there
    [SerializeField] bool hasJumpTrigger = false;
    [SerializeField] bool hasTimer = false;

    bool playerInField = false;
    bool textActive = false;
    float textTimer = 5;

    void Start()
    {
        text.gameObject.SetActive(false);
    }

    void Update()
    {
        if(playerInField && Input.GetKeyDown(KeyCode.Space))
        {
            text.gameObject.SetActive(true);
            textActive = true;
        }
        if (textActive && hasTimer)
        {
            if(textTimer < 0)
            {
                text.gameObject.SetActive(false);
                Destroy(this.gameObject);
            }
            else
                textTimer -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(hasJumpTrigger)
            playerInField = true;
        else
        {
            text.gameObject.SetActive(true);
            textActive = true;
        }
    }
}
