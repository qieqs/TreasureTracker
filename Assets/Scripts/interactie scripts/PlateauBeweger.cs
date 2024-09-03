using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class PlateauBeweger : MonoBehaviour
{
    [HideInInspector] private Vector3 stablescale;
    public Transform position1;
    public Transform position2;
    [SerializeField] public float speed;
    [HideInInspector] public GameObject plateau;

    private bool switching;
    private Vector3 target;

    void FixedUpdate()
    {

        if(switching == false)
        {
            target = position1.position;
        }
        else if(switching == true)
        {
            target = position2.position;
        }


        if(plateau.transform.position == position1.position)
        {
            switching = true;
        }
        else if (plateau.transform.position == position2.position)
        {
            switching = false;
        }
        plateau.transform.position = Vector3.MoveTowards(plateau.transform.position, target, speed * Time.deltaTime);
    }

    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(position1.position, 0.4f);
        Gizmos.DrawWireSphere(position2.position, 0.4f);
        Handles.DrawLine(position1.position, position2.position);
    }
}
