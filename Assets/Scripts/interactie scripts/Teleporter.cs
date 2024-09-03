using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{

    public Teleporter target;
    public bool triggered;
    ParticleSystem particles;

    private void Start()
    {
        particles = GetComponentInChildren<ParticleSystem>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!triggered)
        {
            if (other.tag == "Player")
            {
                other.GetComponent<CharacterController>().enabled = false;
                Debug.Log("teleporter is geactiveerd");
                if (target == null)
                {
                    Debug.LogError("je moet een gameobject toevoegen waar de speler naar geteleporteerd wordt");
                }
                else
                {
                    target.activateteleporter(other.transform);
                }
                other.GetComponent<CharacterController>().enabled = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            triggered = false;
        }
    }

    public void activateteleporter(Transform character)
    {
        particles.Play();
        triggered = true;
        character.transform.position = transform.position + new Vector3(0, 2, 0);
    }
}
