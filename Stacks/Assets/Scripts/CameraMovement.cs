using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public List<Transform> viewPoints;
    public Transform CamPos1;
    public Transform CamPos2;
    public Transform CamPos3;

    private int currentViewPoint = 0;


    void Start()
    {
        transform.position = CamPos1.position;
    }

    void Update()
    {


        if (Input.GetKeyDown(KeyCode.Space))
        {
            currentViewPoint++;

            if (currentViewPoint > 2)
            {
                currentViewPoint = 0;
            }

            if (currentViewPoint == 0)
            {
                transform.position = CamPos1.position;
            }
            else if (currentViewPoint == 1)
            {
                transform.position = CamPos2.position;
            }
            else
            {
                transform.position = CamPos3.position;
            }
        }


        if (Input.GetMouseButton(0))
        {
            if (currentViewPoint == 0)
            {
                transform.position = CamPos1.position;
            }
            else if (currentViewPoint == 1)
            {
                transform.position = CamPos2.position;
            }
            else
            {
                transform.position = CamPos3.position;
            }
            float angle = Mathf.Clamp(Input.mousePosition.x - (Screen.width / 2), 0, 80) - 40;

            transform.RotateAround(viewPoints[currentViewPoint].position, Vector3.up, angle);

        }



        transform.LookAt(viewPoints[currentViewPoint]);
    }
}
