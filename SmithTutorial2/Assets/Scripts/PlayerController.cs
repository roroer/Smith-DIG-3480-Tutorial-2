using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PlayerController : MonoBehaviour {
	private Rigidbody2D rd2d;

	public float speed;
	public Text score;
	public Text winText;
	public Text livesText;

	private int scoreValue = 0;
	private int scoreMax = 4;
	public int levelCounter = 0;
	public int lives = 3;
	public Transform nextLevel;
	public AudioClip newLevel;

	Animator anim;
	AudioSource audioSource;

	// Start is called before the first frame update
	void Start() {
		rd2d = GetComponent<Rigidbody2D>();
		SetScoreText();
		winText.text = "";
		SetLivesText();
		anim = GetComponent<Animator>();
		audioSource = GetComponent<AudioSource>();
	}

	// Update is called once per frame
	private void Update() {
		if (Input.GetKey(KeyCode.D)) {
			anim.SetInteger("State", 1);
		} else if (Input.GetKeyUp(KeyCode.D)) {
			anim.SetInteger("State", 0);
		}
		if (Input.GetKey(KeyCode.A)) {
			anim.SetInteger("State", 1);
		}
		if (Input.GetKeyUp(KeyCode.A)) {
			anim.SetInteger("State", 0);
		}
		if (Input.GetKeyUp(KeyCode.W)) {
			anim.SetBool("Jumping", false);
		}
	}
	void FixedUpdate() {
		float hozMovement = Input.GetAxis("Horizontal");
		float vertMovement = Input.GetAxis("Vertical");
		rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));
		if (hozMovement < 0) {
			Vector3 objScale = transform.localScale;
			objScale.x = -0.5f;
			transform.localScale = objScale;
		}
		if (hozMovement > 0) {
			Vector3 objScale = transform.localScale;
			objScale.x = 0.5f;
			transform.localScale = objScale;
		}
		if (vertMovement > 0.3) {
			anim.SetBool("Jumping", false);
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.CompareTag("Coin")) {
			Destroy(other.gameObject);
			scoreValue += 1;
			SetScoreText();
		}
		if (scoreValue == scoreMax) {
			LoadNextLevel();
		}
		if (other.gameObject.CompareTag("Death")) {
			lives -= 1;
			other.gameObject.SetActive(false);
			SetLivesText();
			if (lives < 1) {
				SetLivesText();
				winText.text = "you lose! press R to restart.";
				gameObject.SetActive(false);
			}
		}
	}
	void SetLivesText() {
		livesText.text = "Lives: " + lives.ToString();
	}
	private void LoadNextLevel() {
		audioSource.PlayOneShot(newLevel);
		levelCounter++;
		scoreValue = 0;
		lives = 3;
		SetLivesText();
		SetScoreText();
		if (levelCounter == 1) {
			transform.position = nextLevel.transform.position;
		}
		if (levelCounter == 2) {
			winText.text = "You Win! Game by Alex Smith";
		}
	}

	private void SetScoreText() {
		score.text = "Score:" + scoreValue.ToString();
	}

	private void OnCollisionStay2D(Collision2D collision) {
		if (collision.collider.tag == "Ground") {
			if (Input.GetKey(KeyCode.W)) {
				anim.SetBool("Jumping", true);
				rd2d.AddForce(new Vector2(0, 5), ForceMode2D.Impulse);
			}
		}
	}
}
