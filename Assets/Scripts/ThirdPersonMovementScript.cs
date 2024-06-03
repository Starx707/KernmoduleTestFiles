using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.VFX;

public class ThirdPersonMovementScript : MonoBehaviour
{
    //hello :D Levi here, gonna quickly explain the controlls and some of the functions
    //"JUMP" = spacebar
    //"Fire3" = Shift
    //coyote time = adds a slight bit of time after the player isnt grounded, so there's slightly more time to jump even if shouldnt be able to normally
    //uhhh- idk, the script is super clear to me but that might also be cuz I spent way too much time in it
    //so if there be questions please do lemme know^^

    [Header("Movement")]
    public float coyoteTimer;
    private float cantCoyote;
    private bool pipoCoyote = false;
    private bool pipoCoyote2 = false;

    public CharacterController controller;
    public Transform cam;
    public float speed;
    private float speedMod;
    public float speedModUpdate;
    private bool speedModActive;

    private Vector3 horizontalVelocity;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    //jump stuff (used to be the dash, had to reuse so Im super sorry for it being uhh- messy :,)
    [Header("Jumping")]
    [SerializeField] private float jumpDistance;
    public float jumpTime;
    [SerializeField] private VisualEffect _dashEffect;
    [SerializeField] private VisualEffect _sprintEffect;
    public AudioSource dashSource;
    [SerializeField] private PlayerJumpLimiter playerJumpLimiter;

    //grav stuff
    private Vector3 playerVelocity;
    public float gravMultiplier;
    private float gravityFactor = -9.81f;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundLayer;
    private bool isGrounded;


    void Update()
    {
        _dashEffect.Stop();
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.2f, groundLayer);

        //coyoteTime
        if (!isGrounded && pipoCoyote == false)
        {
            cantCoyote = Time.time + coyoteTimer;
            pipoCoyote2 = true;
        }
        if (isGrounded)
        {
            pipoCoyote = false;
        }
        if (Time.time < cantCoyote && pipoCoyote2 == true)
        {
            playerVelocity.y = 0f;
            pipoCoyote = true;
        }

        horizontalVelocity = new Vector3(controller.velocity.x, 0, controller.velocity.z);
        float horizontalSpeed = horizontalVelocity.magnitude;

        if (controller.isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (Input.GetButton("Fire3"))
        {
            speedMod = speedModUpdate;
        }
        else
        {
            speedMod = 0f;
        }

        if (speedModActive == true && isGrounded)
        {
            PlaySprintParticle();
        }
        else
        {
            _sprintEffect.Stop();
        }

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f); ;

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * (speed + speedMod) * Time.deltaTime);
        }
        if (Input.GetButtonDown("Jump") && isGrounded || Input.GetButtonDown("Jump") && Time.time < cantCoyote )
        {
            if (playerJumpLimiter.CanJump())
            {
                pipoCoyote2 = false;

                dashSource.Play();

                StartCoroutine(Jump());
            }
            else
                FindObjectOfType<CantJumpText>().DisplayText();
        }
        playerVelocity.y += gravMultiplier * gravityFactor * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    IEnumerator Jump()
    {
        float startTime = Time.time;

        while (Time.time < startTime + jumpTime)
        {
            playerVelocity.y = 9.81f * jumpDistance; 
            PlayDashParticle();
            yield return null;
        }
    }

    void PlayDashParticle()
    {
        _dashEffect.Play();
    }

    void PlaySprintParticle()
    {
        _sprintEffect.Play();
    }
}