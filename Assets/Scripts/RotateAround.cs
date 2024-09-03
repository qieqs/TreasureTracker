using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class RotateAround : MonoBehaviour
{
    /*public GameObject orb;
    public float radius;
    public float radiusSpeed;
    public float rotationSpeed;

    private Transform centre;
    private Vector3 desiredPos;

    void Start()
    {
        centre = orb.transform;
        transform.position = (transform.position - centre.position).normalized * radius + centre.position;
    }

    void Update()
    {
        float rotationX = Input.GetAxis("Mouse X") * -rotationSpeed;
        transform.RotateAround(centre.position, Vector3.up, 20 * Time.deltaTime);

        desiredPos = (transform.position - centre.position).normalized * radius + centre.position;
        transform.position = Vector3.MoveTowards(transform.position, desiredPos, radiusSpeed * Time.deltaTime);
    }*/
    /*public GameObject cameraObj;
    public GameObject myGameObj;
    public float speed = 2f;

    void Update()
    {
        RotateCamera();
    }

    void RotateCamera()
    {
        if (Input.GetMouseButton(0))
        {
            cameraObj.transform.RotateAround(myGameObj.transform.position, cameraObj.transform.up, Input.GetAxis("Mouse X") * speed);

            //cameraObj.transform.RotateAround(myGameObj.transform.position, cameraObj.transform.right, -Input.GetAxis("Mouse Y") * speed);
        }

    }*/

    public float rotationSpeed = 100f;
    public Camera mainCamera;
    public GameObject character; 
    public float radius;
    public float radiusSpeed;

    private Vector3 desiredPos;


    float ZoomAmount = 0;
    float MaxToClamp = 10;
    float rotspeed = 10;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) || Input.GetMouseButton(1))
        {
            float horizontalInput = Input.GetAxis("Mouse X");
            transform.Rotate(Vector3.up, horizontalInput * rotationSpeed * Time.deltaTime);
        }
        mainCamera.transform.LookAt(character.transform);
        mainCamera.fieldOfView -= Input.GetAxis("Mouse ScrollWheel") * radiusSpeed;

    }
}
