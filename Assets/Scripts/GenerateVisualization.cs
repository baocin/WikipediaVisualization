using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GenerateVisualization : MonoBehaviour {
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


	}

	void GenerateCharacters(){
		int i = 0;
//		foreach (User u in cachedData.Users){
		while(i < 100){
			User u = cachedData.Users[Random.Range(0, cachedData.Users.Count-1)];
			GameObject newUser = GenerateUser (u);
			if (newUser.GetComponent<StoreUser> ().data.status.Equals("benign")) {
				//Benign
				newUser.GetComponent<Renderer> ().material.color = Color.blue;
			} else {
				//Vandal
				newUser.GetComponent<Renderer> ().material.color = Color.red;
			}
			userObjects.Add(newUser);
			i++;
			if (i > 100)
				break;
		}

		i = 0;
		foreach (Page p in cachedData.Pages){
			GameObject newPage = GeneratePage (p);
			pageObjects.Add(newPage);
			i++;
			if (i > 100)
				break;
		}

	}

	private GameObject GenerateUser(User data){
		GameObject newUser = (GameObject) Instantiate (userTemplate, this.transform);

		//Randomish Position
		newUser.transform.position = new Vector3 (this.transform.position.x + Random.Range (0, 10), this.transform.position.y +  Random.Range(0,7), this.transform.position.z +  Random.Range (0, 10));
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
