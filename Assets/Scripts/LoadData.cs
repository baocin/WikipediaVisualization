using System;
using System.Collections;
using System.Collections.Generic;
//using NLight;
using UnityEngine;
//using NLight.IO.Text;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using ProtoBuf;

public class LoadData : MonoBehaviour {
	private List<PageEdit> Edits;
	private List<User> Users;
	private List<Page> Pages;
	private Dictionary<string, Revision> Revisions;

	// Use this for initialization
	void Start () {
		
		if (File.Exists ("Cache/Revisions.bin")) {
			//Load Pre-Parsed Data
			Debug.Log("Loading Pre-Computed Data");
			using (var file = File.OpenRead("Cache/Edits.bin")) {
				Edits = Serializer.Deserialize<List<PageEdit>>(file);
			}
			using (var file = File.OpenRead("Cache/Users.bin")) {
				Users = Serializer.Deserialize<List<User>>(file);
			}
			using (var file = File.OpenRead("Cache/Pages.bin")) {
				Pages = Serializer.Deserialize<List<Page>>(file);
			}
			using (var file = File.OpenRead("Cache/Revisions.bin")) {
				Revisions = Serializer.Deserialize<Dictionary<string, Revision>>(file);
			}

		} else {
			//Recalculate everything from scratch
			Debug.Log("Could not find pre-parsed data, now parsing from scratch");
			Parse();
		}
		Debug.Log ("Done Loading data");
		Debug.Log (Edits [0]);
		Debug.Log (Users [0]);
		Debug.Log (Pages [0]);
		Debug.Log (Revisions[(string)Revisions.Keys.First()]);

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Parse(){
		Edits = new List<PageEdit>();
		Users = new List<User> ();
		Pages = new List<Page> ();
		Revisions = new Dictionary<string, Revision> ();

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

		Debug.Log ("Loading Users");
		TextAsset userFile = Resources.Load<TextAsset> ("SetupData/other/users");
		Debug.Log (userFile.text);
		User [] users = JsonHelper.getJsonArray<User> (userFile.text);
		Users = new List<User> (users);
		Debug.Log (Users [0]);

		Debug.Log ("Loading pages");
		TextAsset pageFile = Resources.Load<TextAsset> ("SetupData/other/pages");
		Page [] pages = JsonHelper.getJsonArray<Page> (pageFile.text);
		Pages = new List<Page> (pages);
		Debug.Log (Pages [0]);

		//Collect all revision ids mentioned in the given data
		HashSet<string> mentionedRevisionIds = new HashSet<string>();
		foreach (PageEdit e in Edits) {
			mentionedRevisionIds.Add(e.revid);
		}
		Debug.Log ("Collected " + mentionedRevisionIds.Count + " rev ids");


		//Try to load 13GB file
		//Get only the rows referenced by revid from the Edits list
		string filePath = "Assets/Resources/SetupData/WikipediaRevisionLog/reverteds.all.p_0-43609236.r_0-622033840.tsv";

		Debug.Log ("Collecting revisions of just mentioned rev ids");

		var lines = new StreamReader(filePath);
		string columns = lines.ReadLine();
		char [] delimiters = { '	' };

		int lineCount = 65434198;
		int lineCounter = 1;
		while (!lines.EndOfStream) {
			string line = lines.ReadLine ();
			string [] split = line.Split (delimiters);
			string revid = split[0];

			if (mentionedRevisionIds.Contains(revid) && !Revisions.ContainsKey(revid)){
				//				Debug.Log ("Percent Complete: " + (lineCounter / lineCount));
				Revision r = new Revision (split[0], split[1], split[2], split[3], split[4], split[5], split[6], split[7], split[8], split[9], split[10], split[11], split[12], split[13], split[14], split[15], split[16], split[17], split[18], split[19], split[20], split[21], split[22]);
				Revisions.Add(split[0], r);
			}

			lineCounter += 1;
		}


//		CacheHelper.SaveData ("Cache", "Edits.json", Edits);
//		SimpleSerializer a = new SimpleSerializer();
//		a.SaveToFile (Edits, "Cache/Edits.bin", gzipCompress: true);

//		Debug.Log (Edits);
		using (var file = File.Create("Cache/Edits.bin")) {
			Serializer.Serialize(file, Edits);
		}
		using (var file = File.Create("Cache/Users.bin")) {
			Serializer.Serialize(file, Users);
		}
		using (var file = File.Create("Cache/Pages.bin")) {
			Serializer.Serialize(file, Pages);
		}
		using (var file = File.Create("Cache/Revisions.bin")) {
			Serializer.Serialize(file, Revisions);
		}

//		a.SaveToFile (Users, "Cache/Users.bin", gzipCompress: true);
//		a.SaveToFile (Pages, "Cache/Pages.bin", gzipCompress: true);
//		a.SaveToFile (Revisions, "Cache/Revisions.bin", gzipCompress: true);
//		CacheHelper.SaveData ("Cache", "Users.json", Users);
//		CacheHelper.SaveData ("Cache", "Pages.json", Pages);
//		CacheHelper.SaveData ("Cache", "Revisions.json", Revisions);
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

	public static T getJson<T>(string json)
	{
		T wrapper = JsonUtility.FromJson<T> (json);
		return wrapper;
	}

	[Serializable]
	private class Wrapper<T>
	{
		public T[] array;
	}
}
//
//public class CacheHelper
//{
//	public static void SaveData(string directory, string filename, object data)
//	{
//		if (!Directory.Exists(directory))
//			Directory.CreateDirectory(directory);
//		
////		BinaryFormatter formatter = new BinaryFormatter();
////		FileStream saveFile = File.Create(Path.Combine(directory, filename));
////
////		formatter.Serialize(saveFile, data);
////
//		File outFile = Path.Combine (directory, filename);
//
//		string json = JsonConvert.SerializeObject (data);
//		using (StreamWriter outputFile = new StreamWriter ()) {
//			outputFile.Write (json);
//		}
//	}
//
//	public static T LoadData<T>(string directory, string filename)
//	{
////		BinaryFormatter formatter = new BinaryFormatter();
////		FileStream saveFile = File.Open(Path.Combine(directory, filename), FileMode.Open);
////
////		T data = (T)formatter.Deserialize(saveFile);
////
////		saveFile.Close();
////
////
//		using (StreamReader inputFile = new StreamReader (Path.Combine (directory, filename))) {
//			T deserializedProduct = JsonConvert.DeserializeObject<T>(inputFile.ReadToEnd());
//			return deserializedProduct;
//		}
//		return null;
//	}
//}