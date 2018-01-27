using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    Transform mainCameraTransform;
    void Start()
    {
        mainCameraTransform = Camera.main.transform;
    }
	
	void Update () {
        transform.LookAt(mainCameraTransform.position);	
	}
}
