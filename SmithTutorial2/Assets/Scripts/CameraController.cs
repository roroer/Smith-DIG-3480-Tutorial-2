﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	public GameObject target;
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
		this.transform.position = new Vector3(target.transform.position.x, target.transform.position.y, this.transform.position.z);
	}
}
