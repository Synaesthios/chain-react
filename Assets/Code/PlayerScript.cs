using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

	public float moveSpeed;
	public int Health;
	private Camera mainCamera;
	private Rigidbody rigidbody;
	private Vector3 moveVelocity;
	public Renderer shipRenderer;

	public float secondsOfInvulnerability;
    private bool m_invulnerable;

	void Start () {
		mainCamera = FindObjectOfType<Camera>();
		rigidbody = GetComponent<Rigidbody>();	
	}
	
	void Update () {
		UpdateVelocity();
		UpdateDirection();
		rigidbody.velocity = moveVelocity;
	}

	private void UpdateVelocity() {
		Vector3 moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
		moveVelocity = moveSpeed * moveInput;
	}

	private void UpdateDirection() {
		Ray cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);

		Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
		float rayLength;

		if(groundPlane.Raycast(cameraRay, out rayLength)) {
			Vector3 pointToLook = cameraRay.GetPoint(rayLength);

			transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
		}
	}

	public bool isDead() {
		return Health <= 0;
	}

	private void loseHealth() {
        if (m_invulnerable)
            return;

		StartCoroutine("Invulnerable");
		Health -= 1;
		if (isDead()){
			gameObject.SetActive(false);
		}
        GameObject.FindObjectOfType<LowPassFilterManager>().SetFilterAmountImmediate(0);
	}

	private void OnTriggerEnter(Collider col)
    {
        if(col.transform.gameObject.tag == "EnemyBullet" || col.transform.gameObject.tag == "BossBullet")
        {
            loseHealth();
            Destroy(col.gameObject);
        }
    }

	private void OnCollisionEnter(Collision col) {
		if (col.collider.GetComponent<Enemy>() != null) {
			loseHealth();
            col.collider.GetComponent<Enemy>().HitPlayer();

        }
	}

	IEnumerator Invulnerable() {
        m_invulnerable = true;
		float flickerRate = 0.1f;
		float timesToFlicker = secondsOfInvulnerability/flickerRate;
		for (int i = 0; i < timesToFlicker; i++) {
			shipRenderer.enabled = !shipRenderer.enabled;
			yield return new WaitForSeconds(flickerRate);
		}
		shipRenderer.enabled = true;
        m_invulnerable = false;
	}
}
