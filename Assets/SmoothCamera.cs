using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SmoothCamera : MonoBehaviour
{
    public Transform followCube;
    public float smoothSpeed = 1;
    public Transform camera;
    float startMoveTime;
    public float duration;
    public Boolean moving = false;
    public Vector3 offset;
    public Boolean zoom = false;
    int zoomDir = -1;
    [SerializeField] float zoomDelta = 0.01f;
    [SerializeField] float zoomMax ;
    [SerializeField] List<float> zoomMin ;
    public int depth = -1;
    Vector2 offsetMax = new Vector2(5, 3);
    // Start is called before the first frame update
    public Vector2 inputRightStick = new Vector2(0,0);
    // Update is called once per frame
    void Update()
    {
        if(Gamepad.current != null)
            inputRightStick = Gamepad.all[0].rightStick.value;
        if (Input.GetKey(KeyCode.L))
        {
            inputRightStick = new Vector2(Mathf.Lerp(inputRightStick.x, offsetMax.x, 0.15f),inputRightStick.y);
        }
        else if (Input.GetKey(KeyCode.J))
        {
            inputRightStick = new Vector2(-Mathf.Lerp(-inputRightStick.x, offsetMax.x, 0.15f), inputRightStick.y);
        }
        else
        {
            inputRightStick = new Vector2(Mathf.Lerp(inputRightStick.x, 0, 0.15f), inputRightStick.y);
        }


        if (Input.GetKey(KeyCode.I))
        {
            inputRightStick = new Vector2(inputRightStick.x, Mathf.Lerp(inputRightStick.y, offsetMax.y, 0.15f));
        }
        else if (Input.GetKey(KeyCode.K))
        {
            inputRightStick = new Vector2(inputRightStick.x, -Mathf.Lerp(-inputRightStick.y, offsetMax.y, 0.15f));
        }
        else
        {
            inputRightStick = new Vector2(inputRightStick.x, Mathf.Lerp(inputRightStick.y, 0, 0.15f));
        }
        inputRightStick.x = Mathf.Clamp(inputRightStick.x  , -offsetMax.x, offsetMax.x);
        inputRightStick.y = Mathf.Clamp(inputRightStick.y , -offsetMax.y, offsetMax.y);
        camera.position = new Vector3(Mathf.Lerp(camera.position.x, followCube.position.x + offset.x + inputRightStick.x, smoothSpeed ), Mathf.Lerp(camera.position.y, followCube.position.y+offset.y + inputRightStick.y, smoothSpeed ) ,-10 + offset.z);
        if (zoom == true)
        {
            camera.GetComponent<Camera>().fieldOfView = camera.GetComponent<Camera>().fieldOfView +  zoomDir * zoomDelta;

            // zoom out
            if(camera.GetComponent<Camera>().fieldOfView >= zoomMax)
            {
                zoom = false;
                camera.GetComponent<Camera>().fieldOfView = zoomMax;
                depth = -1;
            }
            // when zoom out
            if (zoomDir == 1 && depth >-1)
            {
                for (int i = 0; i < zoomMin.Count; i++)
                {
                    if (camera.GetComponent<Camera>().fieldOfView >= zoomMin[depth])
                    {
                        depth = i;
                    }
                }
            }
            // zoom in
            if (depth > -1 && camera.GetComponent<Camera>().fieldOfView <= zoomMin[depth] && zoomDir == -1)
            {
                zoom = false;
                camera.GetComponent<Camera>().fieldOfView = zoomMin[depth];
            }
            
        }
    }


    public void zoomIn()
    {
        zoom = true;
        zoomDir = -1;
        if (depth < zoomMin.Count-1)
            depth++;
    }

    public void zoomOut()
    {
        zoom = true;
        zoomDir = 1;
    }
}
