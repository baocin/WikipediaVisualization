using UnityEngine;
using System.Collections;

public class GazeDetection : MonoBehaviour {
	private GameObject focusedGazeObject;
	private GameObject previousActivatedGazeObject;
	public float gazeTime;			//seconds
	public float gazeThreshold;		//seconds
	public float pullForwardForce;

	// Use this for initialization
	void Start () {
		gazeTime = 0f;	
		gazeThreshold = 0.01f;	
		pullForwardForce = 10f;
	}
	
	// Update is called once per frame
	void Update () {
		//Send out a ray from the main camera's forward direction
		RaycastHit gazeHit = new RaycastHit();
		Ray cameraRay = new Ray (Camera.main.transform.position, Camera.main.transform.forward);
		Debug.DrawRay (Camera.main.transform.position, Camera.main.transform.forward, Color.red, 10f);
		bool rayHit = Physics.Raycast(cameraRay, out gazeHit);

		//Check if the camera is looking at the same object as it was in the previous frame
		bool sameFocusAsPrevious = rayHit && gazeHit.transform.gameObject.Equals (focusedGazeObject);

		//If looking at the same object increment gazeTime by the elapsed time since last frame
		if (sameFocusAsPrevious) {
			gazeTime += Time.deltaTime;
		}

		//If focus has changed reset to new target
		if (rayHit && !sameFocusAsPrevious) {
			Debug.Log ("Not Same as previous");
			gazeTime = 0;

			focusedGazeObject = gazeHit.transform.gameObject;
		}

		//Only activate gaze once threshold reached and if object hasn't already been activated
		if (sameFocusAsPrevious && gazeTime > gazeThreshold && !focusedGazeObject.Equals(previousActivatedGazeObject)){
			Debug.Log ("Gaze Activated");

			//Pull Selected Page Forward
			focusedGazeObject.transform.position = (Camera.main.transform.forward * -1 * pullForwardForce + focusedGazeObject.transform.position);

			//Debug.DrawRay (focusedGazeObject.transform.position, focusedGazeObject.transform.right, Color.red, 10f);
			//Debug.DrawRay (focusedGazeObject.transform.position, focusedGazeObject.transform.forward, Color.blue, 10f);
			//Debug.DrawRay (focusedGazeObject.transform.position, focusedGazeObject.transform.up, Color.green, 10f);

			//reset gaze
			gazeTime = 0;

			//Push Back the previous Page
			if (previousActivatedGazeObject != null)
				previousActivatedGazeObject.transform.position = (Camera.main.transform.forward * pullForwardForce + previousActivatedGazeObject.transform.position);

			//Save the current gaze object as the previous (to avoid reactivating the current object)
			previousActivatedGazeObject = focusedGazeObject;

			
		}

	}

}
