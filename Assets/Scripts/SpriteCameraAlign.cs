using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteCameraAlign : MonoBehaviour
{
    Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.LookRotation(mainCamera.transform.forward);
    }
}
