using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMovementControl : MonoBehaviour
{
    PlayerInput playerinput;
    CharacterController charactercontroller;
    Animator animator;

    Vector2 currentMovementinput;
    Vector3 currentmovement;
    Vector3 currentrunmovement;
    bool ismovementpressed;
    public float rotationfactorperframe = 1;
    bool isrunpressed;

    public float runmultiplier = 3f;
    public float walkmultiplier = 3f;
    [HideInInspector]public int i = 0;
    void Awake()
    {
        playerinput = new PlayerInput();
        charactercontroller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        playerinput.CharacterControls.Move.started += onmovementinput;
        playerinput.CharacterControls.Move.canceled += onmovementinput;
        playerinput.CharacterControls.Move.performed += onmovementinput;
        playerinput.CharacterControls.Run.started += onrun;
        playerinput.CharacterControls.Run.canceled += onrun;
    }

    void onmovementinput (InputAction.CallbackContext context) {
        currentMovementinput = context.ReadValue<Vector2>();
        if(i == 0)
        {
            currentmovement.x = currentMovementinput.y * walkmultiplier;
            currentmovement.z = -currentMovementinput.x * walkmultiplier;
            currentrunmovement.x = currentMovementinput.y * runmultiplier;
            currentrunmovement.z = -currentMovementinput.x * runmultiplier;
        }
        else if (i == 1)
        {
            currentmovement.x = -currentMovementinput.x * walkmultiplier;
            currentmovement.z = -currentMovementinput.y * walkmultiplier;
            currentrunmovement.x = -currentMovementinput.x * runmultiplier;
            currentrunmovement.z = -currentMovementinput.y * runmultiplier;
        }
        else if (i == 2)
        {
            currentmovement.x = -currentMovementinput.y * walkmultiplier;
            currentmovement.z = currentMovementinput.x * walkmultiplier;
            currentrunmovement.x = -currentMovementinput.y * runmultiplier;
            currentrunmovement.z = currentMovementinput.x * runmultiplier;
        }
        else if(i == 3)
        {
            currentmovement.x = currentMovementinput.x * walkmultiplier;
            currentmovement.z = currentMovementinput.y * walkmultiplier; 
            currentrunmovement.x = currentMovementinput.x * runmultiplier;
            currentrunmovement.z = currentMovementinput.y * runmultiplier;
        }
        ismovementpressed = currentMovementinput.x != 0 || currentMovementinput.y != 0;
    }

    void onrun(InputAction.CallbackContext context)
    {
        isrunpressed = context.ReadValueAsButton();
    } 

    private void OnEnable()
    {
        playerinput.CharacterControls.Enable();
    }

    private void OnDisable()
    {
        playerinput.CharacterControls.Disable();
    }

    void handleanimation()
    {
        bool iswalking = animator.GetBool("iswalking");
        bool isrunning = animator.GetBool("isrunning");
        if(ismovementpressed && !iswalking)
        {
            animator.SetBool("iswalking", true);
        }
        else if (!ismovementpressed && iswalking)
        {
            animator.SetBool("iswalking", false);
        }

        if((ismovementpressed && isrunpressed) && !isrunning)
        {
            animator.SetBool("isrunning", true);
        }
        else if((!ismovementpressed || !isrunpressed) && isrunning)
        {
            animator.SetBool("isrunning", false);
        }
    }

    void handlerotation()
    {
        Vector3 positiontolook;

        positiontolook.x = -currentmovement.x;
        positiontolook.y = 0.0f;
        positiontolook.z = -currentmovement.z;

        Quaternion currentRotation = transform.rotation;

        if(ismovementpressed )
        {
            Quaternion targetrotation = Quaternion.LookRotation(positiontolook);
            transform.rotation = Quaternion.Slerp(currentRotation, targetrotation, rotationfactorperframe * Time.deltaTime);
        }


    }

    void handlegravity()
    {
        if (charactercontroller.isGrounded)
        {
            float groundedgravity = -0.5f;
            currentmovement.y  = groundedgravity;
            currentrunmovement.y = groundedgravity;
        }
        else
        {
            float gravity = -9.8f;
            currentmovement.y = gravity;
            currentrunmovement.y = gravity;
        }
    }

    void Update()
    {
        handlerotation();
        handleanimation();
        handlegravity();
        if (isrunpressed)
        {
            charactercontroller.Move(currentrunmovement * Time.deltaTime);
        }
        else
        {
            charactercontroller.Move(currentmovement * Time.deltaTime);
        }
    }
}
