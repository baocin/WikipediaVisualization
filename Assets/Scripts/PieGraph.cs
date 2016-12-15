using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PieGraph : MonoBehaviour
{

    public GameObject emptyPiePrefab;
    public WMG_Pie_Graph pieGraph;
    public List<float> testData;
    public List<string> testStrings;

    // Use this for initialization
    void Start()
    {
        GameObject graphGO = GameObject.Instantiate(emptyPiePrefab);
        graphGO.transform.SetParent(this.transform, false);
        pieGraph = graphGO.GetComponent<WMG_Pie_Graph>();

        pieGraph.Init(); // Important this gets called before setting data

        
    }

    public void UpdateGraph(List<string> names, IList<float> data, string newTitle)
    {
        pieGraph.Init();

        //List<int> values = new List<int>();
        //List<string>
        //foreach (KeyValuePair<string, int> pair in vandalEdits)
        //{
        //    vandalValues.Add((string)pair.Key + "," + (int)pair.Value);
        //    //vandalValues.Add((int)pair.Value + "," + (string)pair.Key);
        //}
        pieGraph.sliceValues.SetList(data);
        pieGraph.sliceLabels.SetList(names);
        //pieGraph.sliceColors.SetList(new List<Color>() { Color.blue, Color.red });
    }
}
