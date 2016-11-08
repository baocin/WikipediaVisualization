using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class LoadData : MonoBehaviour {
	List<PageEdit> Edits;

	// Use this for initialization
	void Start () {
		Edits = new List<PageEdit>();

		TextAsset[] benignFiles = Resources.LoadAll<TextAsset> ("SetupData/benign/");
		TextAsset[] vandalFiles = Resources.LoadAll<TextAsset> ("SetupData/vandal/");

		Debug.Log ("Loading all Benign Page Edits (File Count:" + benignFiles.Length + ")");
		foreach (var file in benignFiles) {
			PageEdit [] edits = JsonHelper.getJsonArray<PageEdit> (file.text);
			Edits.AddRange (edits);
		}
		Debug.Log ("PageEdits currently loaded:" + Edits.Count);


		Debug.Log ("Loading all Vandal Page Edits (File Count:" + vandalFiles.Length + ")");
		foreach (var file in vandalFiles) {
			PageEdit [] edits = JsonHelper.getJsonArray<PageEdit> (file.text);
			Edits.AddRange (edits);
		}

		Debug.Log ("PageEdits currently loaded:" + Edits.Count);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}


public class JsonHelper
{
	public static T[] getJsonArray<T>(string json)
	{
		string newJson = "{ \"array\": " + json + "}";
		Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>> (newJson);
		return wrapper.array;
	}

	[Serializable]
	private class Wrapper<T>
	{
		public T[] array;
	}
}