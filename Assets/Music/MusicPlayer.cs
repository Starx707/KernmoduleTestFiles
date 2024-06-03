using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{

    public AudioClip[] clips;
    private AudioSource musicSource;
    public GameObject playerSong;
    private System.Random rand = new System.Random();
   // public GameObject kennergetic;

    void Start()
    {
        musicSource = playerSong.GetComponent<AudioSource>();
        //musicSource = FindObjectOfType<AudioSource>();
        musicSource.loop=false;
    }

    private AudioClip GetRandomClip()
    {
        //Random.InitState((int)System.DateTime.Now.Ticks);
        return clips[rand.Next(0, clips.Length)];
    }

   
    void Update()
    {
        
        if (!musicSource.isPlaying)
        {
            //kennergetic.SetActive(false);
            musicSource.clip = GetRandomClip();
            musicSource.Play();
        }

    }
}
