using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class GenerateVisualization : MonoBehaviour {
	public GameObject userTemplate;
	public GameObject pageTemplate;
	public GameObject editTemplate;

	private List<GameObject> userObjects;
	private List<GameObject> pageObjects;
	private List<GameObject> editObjects;

	private GameObject canvasObject;

	private DataManager cachedData;



	public int numberPageColumns = 100;
	private Transform centerPoint;

	// Use this for initialization
	void Start () {
		canvasObject = GameObject.Find("canvas");
		centerPoint = GameObject.Find ("PagesCenter").transform;

		//Vector2 newPoint = new Vector2(Random.Range(0,100), Random.Range(0,100));
		//canvasObject.GetComponent<WMG_X_Tutorial_1>().series1Data.Add(newPoint);

		userObjects = new List<GameObject> ();
		pageObjects = new List<GameObject> ();
		editObjects = new List<GameObject> ();

		cachedData = new DataManager ();

		//Load the data
		cachedData.LoadCache ();

		//Generate Level
		//GenerateCharacters();

		//Spawn
		SpawnPages();


	}
	
	// Update is called once per frame
	void Update () {
		
		//this.transform.Rotate(new Vector3(0.15f,0.0f, 0.0f));
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

//		i = 0;
//		foreach (Page p in cachedData.Pages){
//			GameObject newPage = GeneratePage (p);
//			pageObjects.Add(newPage);
//			i++;
//			if (i > 100)
//				break;
//		}

		i = 0;
		foreach (PageEdit e in cachedData.VandalEdits){
			GameObject newEdit = GenerateEdit (e);
			editObjects.Add(newEdit);
			i++;
			if (i > 100)
				break;
		}
	}

	private GameObject GenerateEdit(PageEdit data){
		GameObject newEdit = (GameObject) Instantiate (editTemplate, this.transform);

		//Randomish Position
		newEdit.transform.position = new Vector3 (this.transform.position.x + Random.Range (0, 10), this.transform.position.y +  Random.Range(0,7), this.transform.position.z +  Random.Range (0, 10));
		newEdit.GetComponent<StoreEdit> ().data = data;

		return newEdit;
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

	void SpawnPages ()
	{
		float numPagesPerColumn = cachedData.Pages.Count / numberPageColumns;
		Debug.Log ("numPagesPerColumn:" + numberPageColumns);
		numPagesPerColumn = 10;

		float yBuffer = 10f;

		for (int row = 0; row < numPagesPerColumn; row++){
			for (int col = 0; col < numPagesPerColumn; col++) {
				//for (int i = 0; i < numberPageColumns; i++){
				//foreach (Page p in cachedData.Pages){

				Page pageData = cachedData.Pages [row * col];

				//Get position
				Vector3 center = centerPoint.position;
				center.y += yBuffer * row;
				Vector3 pos = CirclePoint (col, numberPageColumns, center, 105.0f);
				Quaternion rot = Quaternion.FromToRotation (Vector3.forward, centerPoint.position - pos);

				//Generate
				GameObject newPage = (GameObject)Instantiate (pageTemplate, pos, rot, centerPoint);
				newPage.GetComponent<StorePage> ().data = pageData;
				newPage.transform.LookAt (centerPoint);
				newPage.transform.Rotate (new Vector3 (90, 0, 0));
			}
		}
	}

	Vector3 CirclePoint ( float i, int numberPoints, Vector3 center ,   float radius  ){
		//float ang = Random.value * 360;
		float ang = (i/numberPoints) * 360;
		Vector3 pos;
		pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
		pos.y = center.y;
		pos.z = center.z + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
		return pos;
	}
}
