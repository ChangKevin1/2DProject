using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        camera.position = new Vector3(Mathf.Lerp(camera.position.x, followCube.position.x + offset.x, smoothSpeed ), Mathf.Lerp(camera.position.y, followCube.position.y+offset.y, smoothSpeed ) ,-10);
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
            if (depth > -1 && camera.GetComponent<Camera>().fieldOfView <= zoomMin[depth])
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
