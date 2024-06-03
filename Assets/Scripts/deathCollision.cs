using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterTeleporter : MonoBehaviour
{
    public GameObject teleporterSubject;
    public Transform tellieTarget;
    public float teleporterGoalX;
    public float teleporterGoalY;
    public float teleporterGoalZ;
    public AudioSource audioSource;
    public AudioClip clipDeath;
    public GameObject kennergetic;
    private float deathCount = 0;


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {

            deathCount++;
            StartCoroutine(WowKennergetic());
        }

       // if (deathCount >= 3)
       // {
       //SceneManager.LoadScene("endMenuScene");
       // }
    }





    private IEnumerator WowKennergetic()
    {
        //audioSource = FindObjectOfType<AudioSource>();
        audioSource.loop = false;

        teleporterSubject.transform.position = tellieTarget.transform.position;
        Debug.Log("peepeepooPoo");
        audioSource.clip = clipDeath;
        audioSource.Play();

        kennergetic.SetActive(true);
        yield return new WaitForSeconds(clipDeath.length);
        Debug.Log(clipDeath.length.ToString());
        kennergetic.SetActive(false);

    }
}
