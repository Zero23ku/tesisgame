﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    public GameObject target;
    public float xOffset = 0;
    public float yOffset = 0;
    public float zOffset = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void LateUpdate() {
        this.transform.position = new Vector3(target.transform.position.x + xOffset,
                                              target.transform.position.y + yOffset,
                                              target.transform.position.z + zOffset);
    }
}
