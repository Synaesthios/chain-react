using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

	public float moveSpeed;

	private Camera mainCamera;
	private Rigidbody rigidbody;
	private Vector3 moveVelocity;

	void Start () {
		mainCamera = FindObjectOfType<Camera>();
		rigidbody = GetComponent<Rigidbody>();	
	}
	
	void Update () {
		UpdateVelocity();
		UpdateDirection();
	}

	void FixedUpdate() {
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
}
