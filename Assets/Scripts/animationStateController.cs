using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationStateController : MonoBehaviour
{

    Animator animator;
    int isWalkingHash;
    int isSprintingHash;
    int isDashingHash;
    int isSprintDashHash;
    int isIdleDashHash;
    int isSillyHash;
    int isAbilityOneHash;
    int isAbilityOneWalkHash;
    int isAbilityOneSprintHash;
    public float dashTimer;
    public float abilityOneTimer;

    public GameObject playerTarget;
    private AudioSource audioSource;
    public AudioClip sillyBilly;
    public AudioClip[] clips;
    public GameObject sillyCamsAdder;
    public GameObject sillyCamsRemover;
    private System.Random rand = new System.Random();
    public GameObject removeLights;
    //public GameObject addLights;

    void Start()
    {
        animator = GetComponent<Animator>();
        isWalkingHash = Animator.StringToHash("isWalking");
        isSprintingHash = Animator.StringToHash("isSprinting");
        isDashingHash = Animator.StringToHash("isDashing");
        isSprintDashHash = Animator.StringToHash("isDashingFromSprint");
        isIdleDashHash = Animator.StringToHash("isDashingFromIdle");
        isSillyHash = Animator.StringToHash("isSilly");
        isAbilityOneHash = Animator.StringToHash("isAbilityOne");
        isAbilityOneWalkHash = Animator.StringToHash("isAbilWalk");
        isAbilityOneSprintHash = Animator.StringToHash("isAbilRun");

        audioSource = playerTarget.GetComponent<AudioSource>();
    }

    private AudioClip GetRandomClip()
    {
        //Random.InitState((int)System.DateTime.Now.Ticks);
        return clips[rand.Next(0, clips.Length)];
    }

    // Update is called once per frame
    void Update()
    {
        bool isWalking = animator.GetBool(isWalkingHash);
        bool isSprinting = animator.GetBool(isSprintingHash);
        bool isDashing = animator.GetBool(isDashingHash);
        bool isSprintDashing = animator.GetBool(isSprintDashHash);
        bool isIdleDashing = animator.GetBool(isIdleDashHash);
        bool isBeingSilly = animator.GetBool(isSillyHash);
        bool isAbilityOne = animator.GetBool(isAbilityOneHash);
        bool isAbilityOneWalk = animator.GetBool(isAbilityOneWalkHash);
        bool isAbilityOneRun = animator.GetBool(isAbilityOneSprintHash);

        //walking
        if (!isWalking && Input.GetButton("Vertical") || !isWalking && Input.GetButton("Horizontal"))
        {
            animator.SetBool(isWalkingHash, true);
        }
        if ( isWalking && !Input.GetButton("Vertical") && !Input.GetButton("Horizontal"))
        {
            animator.SetBool(isWalkingHash, false);
        }

        //sprinting
        if (!isSprinting && Input.GetButton("Vertical") && Input.GetButton("Fire3") || !isSprinting && Input.GetButton("Horizontal") && Input.GetButton("Fire3"))
        {
            animator.SetBool(isSprintingHash, true);
        }
        if (isSprinting && !Input.GetButton("Fire3") || isSprinting && !Input.GetButton("Vertical") && !Input.GetButton("Horizontal"))
        {
            animator.SetBool(isSprintingHash, false);
        }

        //dashing from run
        if (!isDashing && Input.GetButtonDown("Jump"))
        {
            StartCoroutine(DashRun());
        }
        else
        {
            animator.SetBool(isDashingHash, false);
        }

        //dashing from sprint
        if (!isSprintDashing && Input.GetButtonDown("Jump") && isSprinting)
        {
            StartCoroutine(DashSprint());
        }
        else
        {
            animator.SetBool(isSprintDashHash, false);
        }

        //dashing from idle
        if (!isIdleDashing && Input.GetButtonDown("Jump") && !isSprinting && !isWalking)
        {
            StartCoroutine(DashIdle());
        }
        else
        {
            animator.SetBool(isIdleDashHash, false);
        }

        //ability one from run
        if (!isAbilityOneWalk && Input.GetButtonDown("Fire1"))
        {
            StartCoroutine(AbilityOneWalker());
        }
        else
        {
            animator.SetBool(isAbilityOneWalkHash, false);
        }

        //ability one from sprint
        if (!isAbilityOneRun && Input.GetButtonDown("Fire1") && isSprinting)
        {
            StartCoroutine(AbilityOneRunner());
        }
        else
        {
            animator.SetBool(isAbilityOneSprintHash, false);
        }

        //ability one from idle
        if (!isAbilityOne && Input.GetButtonDown("Fire1") && !isSprinting && !isWalking )
        {
            StartCoroutine(AbilityOne());
        }
        else
        {
            animator.SetBool(isAbilityOneHash, false);
        }

        //sillyBilly
        if (Input.GetButtonDown("Fire2") && !isBeingSilly && !isSprinting && !isWalking && !isIdleDashing && !isSprintDashing && !isDashing)
        {
            
            audioSource.clip = sillyBilly;
            audioSource.loop = true;
            audioSource.Play();
            animator.SetBool(isSillyHash, true);
            sillyCamsAdder.SetActive(true);
            sillyCamsRemover.SetActive(false);
            removeLights.SetActive(false);
            //addLights.SetActive(true);
        }
        if (Input.GetButtonDown("Fire2") && isBeingSilly)
        {
            sillyCamsAdder.SetActive(false);
            sillyCamsRemover.SetActive(true);
            removeLights.SetActive(true);
            //addLights.SetActive(false);
            animator.SetBool(isSillyHash, false);
            audioSource.loop = false;
            audioSource.clip = GetRandomClip();
            audioSource.Play();
            //audioSource.Stop();
        }
    }

    IEnumerator DashRun()
    {
        float startTime = Time.time;

        while (Time.time < startTime + dashTimer)
        {
            animator.SetBool(isDashingHash, true);
            yield return null;
        }
    }
    IEnumerator DashSprint()
    {
        float startTime = Time.time;

        while (Time.time < startTime + dashTimer)
        {
            animator.SetBool(isSprintDashHash, true);
            yield return null;
        }
    }

    IEnumerator DashIdle()
    {
        float startTime = Time.time;

        while (Time.time < startTime + dashTimer)
        {
            animator.SetBool(isIdleDashHash, true);
            yield return null;
        }
    }

    IEnumerator AbilityOne()
    {
        float startTime = Time.time;

        while (Time.time < startTime + abilityOneTimer)
        {
            animator.SetBool(isAbilityOneHash, true);
            yield return null;
        }
    }

    IEnumerator AbilityOneWalker()
    {
        float startTime = Time.time;

        while (Time.time < startTime + abilityOneTimer)
        {
            animator.SetBool(isAbilityOneWalkHash, true);
            yield return null;
        }
    }

    IEnumerator AbilityOneRunner()
    {
        float startTime = Time.time;

        while (Time.time < startTime + abilityOneTimer)
        {
            animator.SetBool(isAbilityOneSprintHash, true);
            yield return null;
        }
    }
}
