﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
	private Rigidbody2D rd2d;

	public float speed;
	public Text score;
	private int scoreValue = 0;

	// Start is called before the first frame update
	void Start() {
		rd2d = GetComponent<Rigidbody2D>();
		score.text = scoreValue.ToString();
	}

	// Update is called once per frame
	void FixedUpdate() {
		float hozMovement = Input.GetAxis("Horizontal");
		float vertMovement = Input.GetAxis("Vertical");
		rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));
		if (Input.GetKeyDown("escape")) {
			Application.Quit();
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.CompareTag("Coin")) {
			Destroy(other.gameObject);
			scoreValue += 1;
			score.text = scoreValue.ToString();
		}
	}

	private void OnCollisionStay2D(Collision2D collision) {
		if (collision.collider.tag == "Ground") {
			if (Input.GetKey(KeyCode.W)) {
				rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
			}
		}
	}
}