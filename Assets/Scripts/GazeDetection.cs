using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GazeDetection : MonoBehaviour {
	private GameObject focusedGazeObject;
	private GameObject previousActivatedGazeObject;
	public float gazeTime;			//seconds
	public float gazeThreshold;		//seconds
	public float pullForwardForce;
	public Vector3 previousPosition;
    public GameObject light;
    public GameObject pageLabel;


	// Use this for initialization
	void Start () {
		gazeTime = 0f;	
		gazeThreshold = 0.2f;	
		pullForwardForce = 10f;

        light = GameObject.Find("highlight");
        pageLabel = GameObject.Find("PageLabel");
        
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
			//Debug.Log ("Not Same as previous");
			gazeTime = 0;

			focusedGazeObject = gazeHit.transform.gameObject;
		}

		//Only activate gaze once threshold reached and if object hasn't already been activated
		if (sameFocusAsPrevious && gazeTime > gazeThreshold && !focusedGazeObject.Equals(previousActivatedGazeObject)){
			Debug.Log ("Gaze Activated");

            //focusedGazeObject.GetComponent<Renderer>().material.color = Color.blue;

            //Light to highlight the original 
            //light.transform.position = focusedGazeObject.transform.position;
            //light.SetActive(true);

            //Set Text Label Position
            pageLabel.transform.position = focusedGazeObject.transform.position;
            pageLabel.transform.position = (Camera.main.transform.forward * -1 * 20 + pageLabel.transform.position);
            pageLabel.transform.LookAt(Camera.main.transform);
            pageLabel.transform.Rotate(new Vector3(0f, 180f, 0f));

            //Update the pageLabel with the page title
            var textLabel = pageLabel.GetComponent<Text>();
            textLabel.text = focusedGazeObject.GetComponent<StorePage>().data.pagetitle;

			//reset gaze
			gazeTime = 0;

			//Push Back the previous Page
			if (previousActivatedGazeObject != null) {
				previousActivatedGazeObject.transform.position = previousPosition;//(Camera.main.transform.forward * pullForwardForce + previousActivatedGazeObject.transform.position);
			}

			//Pull Selected Page Forward
			previousPosition = focusedGazeObject.transform.position;
			focusedGazeObject.transform.position = (Camera.main.transform.forward * -1 * pullForwardForce + focusedGazeObject.transform.position);

			//Debug.DrawRay (focusedGazeObject.transform.position, focusedGazeObject.transform.right, Color.red, 10f);
			//Debug.DrawRay (focusedGazeObject.transform.position, focusedGazeObject.transform.forward, Color.blue, 10f);
			//Debug.DrawRay (focusedGazeObject.transform.position, focusedGazeObject.transform.up, Color.green, 10f);

			//Save the current gaze object as the previous (to avoid reactivating the current object)
			previousActivatedGazeObject = focusedGazeObject;

			//-------------- Update Graph --------------------------
			try {
				Page wikiPage = (Page) focusedGazeObject.GetComponent<StorePage>().data;
				//Debug.Log(wikiPage);
				GenerateVisualization.UpdateGraph(wikiPage);

			}catch(MissingComponentException e){
				Debug.Log ("Could not find Page component of the focused object");
			}

			
		}

	}

}
