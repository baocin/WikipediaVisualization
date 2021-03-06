﻿using System;
using System.Collections;
using System.Collections.Generic;
//using NLight;
using UnityEngine;
//using NLight.IO.Text;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using ProtoBuf;

public class DataManager {
    public List<PageEdit> BenignEdits { get; set; }
    public List<PageEdit> VandalEdits { get; set; }
    public List<User> Users { get; set; }
    public List<Page> Pages { get; set; }
    public Dictionary<string, Revision> Revisions { get; set; }

    private const string benignEditsPath = "Cache/BenignEdits.bin";
    private const string vandalEditsPath = "Cache/VandalEdits.bin";
    private const string usersPath = "Cache/Users.bin";
    private const string pagesPath = "Cache/Pages.bin";
    private const string revisionsPath = "Cache/Revisions.bin";

    private const string BenignSetupPath = "SetupData/benign/";
    private const string VandalSetupPath = "SetupData/vandal/";
    private const string PagesSetupPath = "SetupData/other/pages";
    private const string UsersSetupPath = "SetupData/other/users";
    private const string RevisionSetupPath = "Assets/Resources/SetupData/WikipediaRevisionLog/reverteds.all.p_0-43609236.r_0-622033840.tsv";

    //Returns True if Loads from Cache, False if it has to recalculate
    public bool LoadCache() {

        if (CacheExists()) {
            //Load Pre-Parsed Data
            Debug.Log("Loading Pre-Parsed Data");
            LoadCachedBenignEdits();
            LoadCachedVandalEdits();
            LoadCachedPages();
            LoadCachedUsers();
            LoadCachedRevisions();
            Debug.Log("Done loading data");

            Debug.Log(BenignEdits[0]);
            Debug.Log(VandalEdits[0]);
            Debug.Log(Users[0]);
            Debug.Log(Pages[0]);
            Debug.Log(Revisions[(string)Revisions.Keys.First()]);

            return true;
        } else {
            //Recalculate everything from scratch
            Debug.Log("Could not find pre-parsed data, now parsing from scratch");
            Debug.Log("Better go get some coffee");
            Parse();

            return false;
        }


    }

    private bool LoadCachedBenignEdits() {
        if (File.Exists(benignEditsPath)) {
            using (var file = File.OpenRead(benignEditsPath)) {
                BenignEdits = Serializer.Deserialize<List<PageEdit>>(file);
            }
            return true;
        }
        return false;
    }

    private bool LoadCachedVandalEdits() {
        if (File.Exists(vandalEditsPath)) {
            using (var file = File.OpenRead(vandalEditsPath)) {
                VandalEdits = Serializer.Deserialize<List<PageEdit>>(file);
            }
            return true;
        }
        return false;
    }

    private bool LoadCachedUsers() {
        if (File.Exists(usersPath)) {
            using (var file = File.OpenRead(usersPath)) {
                Users = Serializer.Deserialize<List<User>>(file);
            }
            return true;
        }
        return false;
    }

    private bool LoadCachedPages() {
        if (File.Exists(pagesPath)) {
            using (var file = File.OpenRead(pagesPath)) {
                Pages = Serializer.Deserialize<List<Page>>(file);
            }
            return true;
        }
        return false;
    }

    private bool LoadCachedRevisions() {
        if (File.Exists(revisionsPath)) {
            using (var file = File.OpenRead(revisionsPath)) {
                Revisions = Serializer.Deserialize<Dictionary<string, Revision>>(file);
            }
            return true;
        }
        return false;
    }

    private bool CacheExists() {
        return (File.Exists(benignEditsPath) & File.Exists(vandalEditsPath) & File.Exists(pagesPath) & File.Exists(usersPath) & File.Exists(revisionsPath));
    }

    private void LoadBenignEdits() {
        TextAsset[] benignFiles = Resources.LoadAll<TextAsset>(BenignSetupPath);

        Debug.Log("Loading all Benign Page Edits (File Count:" + benignFiles.Length + ")");
        foreach (var file in benignFiles) {
            PageEdit[] edits = JsonHelper.getJsonArray<PageEdit>(file.text);
            BenignEdits.AddRange(edits);
        }
        Debug.Log("PageEdits loaded:" + BenignEdits.Count);
    }

    private void LoadVandalEdits() {
        TextAsset[] vandalFiles = Resources.LoadAll<TextAsset>(VandalSetupPath);

        Debug.Log("Loading all Vandal Page Edits (File Count:" + vandalFiles.Length + ")");
        foreach (var file in vandalFiles) {
            PageEdit[] edits = JsonHelper.getJsonArray<PageEdit>(file.text);
            VandalEdits.AddRange(edits);
        }
        Debug.Log("PageEdits loaded:" + VandalEdits.Count);
    }

    private void LoadUsers() {
        Debug.Log("Loading Users");
        TextAsset userFile = Resources.Load<TextAsset>("SetupData/other/users");        //TODO: Replace with variable UsersSetupPath
        User[] users = JsonHelper.getJsonArray<User>(userFile.text);
        Users = new List<User>(users);
        Debug.Log(Users[0]);
    }

    private void LoadPages() {
        Debug.Log("Loading pages");
        TextAsset pageFile = Resources.Load<TextAsset>("SetupData/other/pages");        //TODO: Replace with variable PagesSetupPath
        Page[] pages = JsonHelper.getJsonArray<Page>(pageFile.text);
        Pages = new List<Page>(pages);
        Debug.Log(Pages[0]);
    }

    private HashSet<string> GetUniqueRevisionIds() {
        //Collect all revision ids mentioned in the given data
        HashSet<string> mentionedRevisionIds = new HashSet<string>();
        foreach (PageEdit e in BenignEdits) {
            mentionedRevisionIds.Add(e.revid);
        }
        foreach (PageEdit e in VandalEdits) {
            mentionedRevisionIds.Add(e.revid);
        }
        Debug.Log("Collected " + mentionedRevisionIds.Count + " rev ids");

        return mentionedRevisionIds;
    }

    private void LoadRevisions() {
        //Get the Revision ids that were mentioned in the dataset
        HashSet<string> revisionIds = GetUniqueRevisionIds();

        //Try to load 13GB file
        Debug.Log("Collecting revisions of just mentioned rev ids");

        var lines = new StreamReader(RevisionSetupPath);
        string columns = lines.ReadLine();
        char[] delimiters = { '	' };

        int totalLineCount = 65434198;
        int linesProcessed = 1;

        //Get only the rows referenced by revid from the Edits list
        while (!lines.EndOfStream) {
            string line = lines.ReadLine();
            string[] split = line.Split(delimiters);
            string revid = split[0];

            if (revisionIds.Contains(revid) && !Revisions.ContainsKey(revid)) {
                Debug.Log("Percent Complete: " + (linesProcessed / totalLineCount));
                Revision r = new Revision(split[0], split[1], split[2], split[3], split[4], split[5], split[6], split[7], split[8], split[9], split[10], split[11], split[12], split[13], split[14], split[15], split[16], split[17], split[18], split[19], split[20], split[21], split[22]);
                Revisions.Add(split[0], r);
            }

            linesProcessed += 1;
        }
    }

    public void Parse() {
        VandalEdits = new List<PageEdit>();
        BenignEdits = new List<PageEdit>();
        Users = new List<User>();
        Pages = new List<Page>();
        Revisions = new Dictionary<string, Revision>();


        LoadBenignEdits();
        using (var file = File.Create(benignEditsPath)) {
            Serializer.Serialize(file, BenignEdits);
        }

        LoadVandalEdits();
        using (var file = File.Create(vandalEditsPath)) {
            Serializer.Serialize(file, VandalEdits);
        }

        LoadUsers();
        using (var file = File.Create(usersPath)) {
            Serializer.Serialize(file, Users);
        }

        LoadPages();
        using (var file = File.Create(pagesPath)) {
            Serializer.Serialize(file, Pages);
        }

        LoadRevisions();
        using (var file = File.Create(revisionsPath)) {
            Serializer.Serialize(file, Revisions);
        }
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="page"></param>
    /// <param name="editValues"></param>
    /// <param name="vandalValues"></param>
    public Dictionary<string, int> getUserVandalEdits(Page page)
    {
        Dictionary<string, int> vandalEdits = new Dictionary<string, int>();
        
        //VANDAL
        foreach (PageEdit be in VandalEdits) {
            if (page.pagetitle.Equals(be.pagetitle)) {
                DateTime revTime;

                bool revTimeParseSuccess = DateTime.TryParse(be.revtime, out revTime);
                if (revTimeParseSuccess)
                {
                    string revKey = revTime.ToString("MM-dd");
                    if (vandalEdits.ContainsKey(revKey)) {
                        vandalEdits[revKey] += 1;
                    } else
                    {
                        vandalEdits[revKey] = 1;
                    }
                }
            }
        }
        return vandalEdits;
    }

    public List<string> getUserVandalValues(Page page)
    {
        Dictionary<string, int> vandalEdits = getUserVandalEdits(page);
        var vandalValues = new List<string>();
        foreach (KeyValuePair<string, int> pair in vandalEdits)
        {
            vandalValues.Add((string)pair.Key + "," + (int)pair.Value);
            //vandalValues.Add((int)pair.Value + "," + (string)pair.Key);
        }

        return vandalValues;
    }

    public Dictionary<string, int> getUserBenignEdits(Page page) {
        Dictionary<string, int> benignEdits = new Dictionary<string, int>();


        //BENIGN
        foreach (PageEdit be in BenignEdits)
        {
            if (page.pagetitle.Equals(be.pagetitle))
            {
                DateTime revTime;

                bool revTimeParseSuccess = DateTime.TryParse(be.revtime, out revTime);
                if (revTimeParseSuccess)
                {
                    string revKey = revTime.ToString("MM-dd");
                    if (benignEdits.ContainsKey(revKey))
                    {
                        benignEdits[revKey] += 1;
                    }
                    else
                    {
                        benignEdits[revKey] = 1;
                    }
                }
            }
        }
        return benignEdits;
    }

    public List<string> getUserBenignValues(Page page)
    {
        Dictionary<string, int> benignEdits = getUserBenignEdits(page);
        var benignValues = new List<string>();
        foreach (KeyValuePair<string, int> pair in benignEdits)
        {
            benignValues.Add((string)pair.Key + "," + (int)pair.Value);
            //benignValues.Add((int)pair.Value + "," + (string)pair.Key);
        }

        return benignValues;
    }

   
    private static bool IsRemovableData(Vector2 element)
    {
        return (element.Equals(Vector2.zero));
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