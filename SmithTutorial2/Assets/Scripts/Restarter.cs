﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restarter : MonoBehaviour
{
    void Update()
    {
		if (Input.GetKey("r")) {
			SceneManager.LoadScene("Level1");
		}
		if (Input.GetKey("escape")) {
			Application.Quit();
		}
	}
}
