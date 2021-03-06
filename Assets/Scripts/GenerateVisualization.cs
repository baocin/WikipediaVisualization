﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class GenerateVisualization : MonoBehaviour {
	public GameObject userTemplate;
	public GameObject pageTemplate;
	public GameObject editTemplate;

	private List<GameObject> userObjects;
	private List<GameObject> pageObjects;
	private List<GameObject> editObjects;

	private GameObject canvasObject;

	private static DataManager cachedData;

	public int numberColumns = 100;
	private Transform centerPoint;
	private float defaultRadius = 100f;

	private List<GameObject> renderedPages;

    public Material vandalMaterial;
    public Material benignMaterial;

    public Texture t;


	// Use this for initialization
	void Start () {
		canvasObject = GameObject.Find("canvas");
		centerPoint = GameObject.Find ("PagesCenter").transform;

		//Vector2 newPoint = new Vector2(Random.Range(0,100), Random.Range(0,100));
		//canvasObject.GetComponent<WMG_X_Tutorial_1>().series1Data.Add(newPoint);

		userObjects = new List<GameObject> ();
		pageObjects = new List<GameObject> ();
		editObjects = new List<GameObject> ();
		renderedPages = new List<GameObject> ();

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
		GameObject newEdit = (GameObject)Instantiate(editTemplate);
		newEdit.transform.parent = this.transform;

		//Randomish Position
		newEdit.transform.position = new Vector3 (this.transform.position.x + Random.Range (0, 10), this.transform.position.y +  Random.Range(0,7), this.transform.position.z +  Random.Range (0, 10));
		newEdit.GetComponent<StoreEdit> ().data = data;

		return newEdit;
	}

	private GameObject GenerateUser(User data){
		GameObject newUser = (GameObject) Instantiate (userTemplate);
		newUser.transform.parent = this.transform;

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
		//float numPagesPerColumn = cachedData.Pages.Count / numberPageColumns;
		//Debug.Log ("numPagesPerColumn:" + numberPageColumns);
		float numberRows = 25f;

		float yBuffer = 8f;
		//float xBuffer = 5f;

		for (int row = 0; row < numberRows; row++){
			for (int  col = 0; col < numberColumns; col++) {
				//for (int i = 0; i < numberPageColumns; i++){
				//foreach (Page p in cachedData.Pages){

				Page pageData = cachedData.Pages [row * col];

                //if (pageData.pagetitle)
                List<PageEdit> vandalEdits = cachedData.VandalEdits.Where(x => x.pagetitle.Equals(pageData.pagetitle)).ToList();
                List<PageEdit> benignEdits = cachedData.BenignEdits.Where(x => x.pagetitle.Equals(pageData.pagetitle)).ToList();
                //Debug.Log(pageEdits);
                //Highlight the pages with vandal attributes

                //Select(x => x.pagetitle = pageData.pagetitle);

                //Get position
                Vector3 center = centerPoint.position;
				center.y += yBuffer * row;

				float radius = defaultRadius;
				radius -= row * 4f;

				//Debug.Log("col:" + col);
				Vector3 pos = CirclePoint (col, numberColumns, center, radius);
				Quaternion rot = Quaternion.FromToRotation (Vector3.forward, centerPoint.position - pos);

				//Generate
				GameObject newPage = (GameObject)Instantiate (pageTemplate, pos, rot);
                newPage.transform.SetParent(centerPoint);
                newPage.GetComponent<StorePage>().data = pageData;

                newPage.transform.LookAt(centerPoint);
                newPage.transform.Rotate(new Vector3(90, 0, 0));

                if (vandalEdits.Count > 0)
                {
                    //newPage.GetComponent<MeshRenderer>().material.shader = Shader.Find("wood_texture_background4");
                    newPage.GetComponent<MeshRenderer>().material = vandalMaterial;
                    //newPage.GetComponent<MeshRenderer>().material.color = Color.black;
                    //newPage.GetComponent<MeshRenderer>().enabled = false;
                    //newPage.transform.position = (Camera.main.transform.forward * -1 * 20 + newPage.transform.position);
                }
                else
                {
                    //newPage.GetComponent<MeshRenderer>().material.shader  = Shader.Find("wood_texture_background1");
                    newPage.GetComponent<MeshRenderer>().material = benignMaterial;
                }
                renderedPages.Add(newPage);


            }
		}
	}

	Vector3 CirclePoint ( int i, int numberPoints, Vector3 center ,   float radius  ){
		//float ang = Random.value * 360;
		float ang = (i/ (float) numberPoints) * 360.0f;
		//Debug.Log("ang:" + ang);
		Vector3 pos;
		pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
		pos.y = center.y;
		pos.z = center.z + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
		return pos;
	}

	public static void UpdateGraph(Page page){
        Debug.Log(page);
        string title = page.pagetitle;
        

        List<string> benignGroups = cachedData.getUserBenignValues(page);
        List<string> vandalGroups = cachedData.getUserVandalValues(page);
        
		GameObject timeline = GameObject.Find ("Canvas");
		timeline.GetComponent<DrawTimeLine> ().UpdateGraph (benignGroups, vandalGroups, title);
        


        Dictionary<string, int> benignEdits = cachedData.getUserBenignEdits(page);
        Dictionary<string, int> vandalEdits = cachedData.getUserVandalEdits(page);

        List<string> names = new List<string>();
        //names[0] = "# Benign";
        names.Add("# Benign");
        names.Add("# Vandal");
        //names[1] = "# Vandal";
        IList<float> data = new List<float>();

        float [] dataArray = new float[2];

        foreach (string key in benignEdits.Keys)
        {
            //data.Insert(0, data.ElementAt(0) + benignEdits[key]);
            dataArray[0] += benignEdits[key];
        }
        foreach (string key in vandalEdits.Keys)
        {
            //data.Insert(1, data.ElementAt(1) + vandalEdits[key]);
            dataArray[1] += vandalEdits[key];
        }
        data = dataArray.ToList<float>();
        Debug.Log("names: " + names[0] + " " + names[1]);
        Debug.Log("data: " + data[0] + " " + data[1]);
        GameObject pie = GameObject.Find("PieGraph");
        pie.GetComponent<PieGraph>().UpdateGraph(names, data, title);


        //timeline.
    }
}
