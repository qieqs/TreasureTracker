using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class rotatelocked : MonoBehaviour
{
    PlayerInput playerinput;
    CharacterMovementControl charactermovement;
    bool pressright;
    bool pressleft;
    bool pressavailable = true;
    
    [HideInInspector] public float currangle = 45;
    [HideInInspector] public int side;

    private void Awake()
    {
        playerinput = new PlayerInput();
        playerinput.CharacterControls.turnleft.started += onpressedleft;
        playerinput.CharacterControls.turnleft.canceled += onpressedleft;
        playerinput.CharacterControls.turnright.started += onpressright;
        playerinput.CharacterControls.turnright.canceled += onpressright;
        charactermovement = FindObjectOfType<CharacterMovementControl>();
        if(charactermovement == null)
        {
            Debug.LogError("je hebt geen karakter in je scène");
        }
    }

    void onpressright(InputAction.CallbackContext context)
    {
        pressright = context.ReadValueAsButton();
    }

    void onpressedleft(InputAction.CallbackContext context)
    {
        pressleft = context.ReadValueAsButton();
    }

    void Update()
    {
        if (pressavailable)
        {
            if (pressleft)
            {
                Debug.Log("pressleft");
                StartCoroutine(lockroutine(90));
            }
            if (pressright)
            {
                Debug.Log("pressright");
                StartCoroutine(lockroutine(-90));
            }
        }
    }

    void checkrotation(float turnamount)
    {
        if(turnamount < 0)
        {
            side -= 1;
            if(side < 0)
            {
                side = 3;
            }
        }
        else
        {
            side += 1;
            if(side > 3)
            {
                side = 0;
                currangle = 45;
            }
        }
        charactermovement.i = side;
    }

    private IEnumerator lockroutine(float turnamount)
    {
        currangle += turnamount;
        checkrotation(turnamount);
        pressavailable = false;
        Quaternion targetrot = Quaternion.AngleAxis(currangle, Vector3.up);
        Quaternion startrot = transform.rotation;
        float timeElapsed = 0;
        float lerpduration = 1f;
        while (timeElapsed < lerpduration)
        {
            transform.rotation = Quaternion.Lerp(startrot, targetrot, timeElapsed / lerpduration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        pressavailable = true;
    }

    private void OnEnable()
    {
        playerinput.CharacterControls.Enable();
    }

    private void OnDisable()
    {
        playerinput.CharacterControls.Disable();
    }


}
