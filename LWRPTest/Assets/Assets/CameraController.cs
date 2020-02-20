using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	private GameObject player;

	private Vector3 offset;

	public float smoothSpeed = 5f;
	public Vector2 boundaries;

	// Use this for initialization
	void Start () {

		//Player GameObject.
		player = GameObject.Find ("Player");

		//Offset of camera relative to player.
		offset = transform.position - player.transform.position;
	}

	// Fixed Update is called every couple of frames. Used if an update every frame not needed.
	void FixedUpdate () {

		//If the player is inside the boundaries of the level.
        if (transform.position.x > boundaries.x && transform.position.x < boundaries.y) {

			//Set desiredPosition to the position where camera should be.
            Vector3 desiredPosition = new Vector3(player.transform.position.x, 0, 0) + offset;

			//Set smoothedPosition to a vector in between the current and the desired position.
			Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
			//Set the camera position to the smoothed position.
			transform.position = smoothedPosition;

		}
	}
}
