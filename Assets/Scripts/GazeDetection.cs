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
		gazeTime = 0;
		gazeThreshold = 0f;
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

			//Push Back
			if (focusedGazeObject != null)
				focusedGazeObject.transform.position = (Camera.main.transform.forward * pullForwardForce + focusedGazeObject.transform.position);

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
			previousActivatedGazeObject = focusedGazeObject;
		}

	}

	IEnumerator PullIn(Vector3 pointA, Vector3 pointB)
	{
		while (true) {
			yield return StartCoroutine(MoveObject(transform, pointA, pointB, 3.0f));
			//yield return StartCoroutine(MoveObject(transform, pointB, pointA, 3.0f));
		}
	}

	IEnumerator PushOut(Vector3 pointA, Vector3 pointB)
	{
		while (true) {
			//yield return StartCoroutine(MoveObject(transform, pointA, pointB, 3.0f));
			yield return StartCoroutine(MoveObject(transform, pointB, pointA, 3.0f));
		}
	}

	IEnumerator MoveObject(Transform thisTransform, Vector3 startPos, Vector3 endPos, float time)
	{
		var i= 0.0f;
		var rate= 1.0f/time;
		while (i < 1.0f) {
			i += Time.deltaTime * rate;
			thisTransform.position = Vector3.Lerp(startPos, endPos, i);
			yield return null; 
		}
	}



}
