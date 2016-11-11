using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GenerateVisualization : MonoBehaviour {
	private bool renderVisualization;
	public GameObject userTemplate;
	public GameObject pageTemplate;

	private List<GameObject> userObjects;
	private List<GameObject> pageObjects;

	private DataManager cachedData;

	// Use this for initialization
	void Start () {
		userObjects = new List<GameObject> ();
		pageObjects = new List<GameObject> ();

		cachedData = new DataManager ();

		//Load the data
		cachedData.LoadCache ();

		//Generate Level
		GenerateCharacters();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Space)) {
			renderVisualization = !renderVisualization;
		}


	}

	void GenerateCharacters(){
		foreach (User u in cachedData.Users){
			GameObject newUser = GenerateUser (u);
			userObjects.Add(newUser);
		}

//		foreach (Page p in cachedData.Pages){
//			GameObject newPage = GeneratePage (p);
//			pageObjects.Add(newPage);
//		}

	}

	private GameObject GenerateUser(User data){
		GameObject newUser = (GameObject) Instantiate (userTemplate, this.transform);

		//Randomish Position
		newUser.transform.position = new Vector3 (Random.Range (0, 10), Random.Range(0,1), Random.Range (0, 10));
		newUser.GetComponent<StoreUser> ().data = data;

		return newUser;
	}

	private GameObject GeneratePage(Page data){
		GameObject newPage = Instantiate (pageTemplate);

		//Randomish Position
		newPage.transform.position = new Vector3 (Random.Range (10, 20), Random.Range (0, 1), Random.Range (0, 10));
		newPage.GetComponent<StorePage> ().data = data;

		return newPage;
	}
}
