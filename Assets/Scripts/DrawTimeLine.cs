using UnityEngine;
using System.Collections.Generic;
using System.Collections;


public class DrawTimeLine : MonoBehaviour {
	public GameObject emptyGraphPrefab;
	public WMG_Axis_Graph graph;

	public WMG_Series series1;
    public WMG_Series series2;
    public List<Vector2> series1Data;
	public bool useData2;
	public List<string> series1Data2;

	private GameObject graphGO;

	// Use this for initialization
	void Start () {
		graphGO = GameObject.Instantiate (emptyGraphPrefab);
		graphGO.transform.SetParent (this.transform, false);
		graph = graphGO.GetComponent<WMG_Axis_Graph> ();


		series1 = graph.addSeries();
		graph.xAxis.AxisMaxValue = 5;

		if (useData2) {
			List<string> groups = new List<string>();
			List<Vector2> data = new List<Vector2>();
			for (int i = 0; i < series1Data2.Count; i++) {
				string[] row = series1Data2[i].Split(',');
				groups.Add(row[0]);
				if (!string.IsNullOrEmpty(row[1])) {
					float y = float.Parse(row[1]);
					data.Add(new Vector2(i+1, y));
				}
			}

			graph.groups.SetList(groups);
			graph.useGroups = true;

			graph.xAxis.LabelType = WMG_Axis.labelTypes.groups;
			graph.xAxis.AxisNumTicks = groups.Count;

			series1.seriesName = "Look at a page to get started";

			series1.UseXDistBetweenToSpace = true;

			series1.pointValues.SetList(data);
		}
		else {
			series1.pointValues.SetList(series1Data);
		}
	}

    public void UpdateGraph(List<string> benignData, List<string> vandalData, string newTitle)
    {
        //foreach (Vector2 tmp in series1Data)
        //{
        //    Debug.Log(tmp.x + "," + tmp.y);
        //}

        //foreach (string tmp in vandalData)
        //{
        //    Debug.Log(tmp);
        //}

        //Delete All previous series (instead of deleting and remaking the graph)
        for (int i = 0; i < 10; i++)
        {
            graph.deleteSeries();
        }

        //Graph Settings
        graph.graphTitleString = newTitle;
        graph.xAxis.LabelType = WMG_Axis.labelTypes.groups;
        graph.useGroups = true;

        //graph.xAxis.changeLabelText();
        //graph.yAxis.changeLabelText();
       
        //Number Of Edits
        //Dates (MM-DD)


        //Vandal Edit Series Settings
        series1 = graph.addSeries();
        series1.lineColor = Color.red;
        series1.seriesName = "Vandal Edits";
        series1.UseXDistBetweenToSpace = true;

        //Populate Vandal data
        List<string> groups = new List<string>();
        List<Vector2> data = new List<Vector2>();
        for (int i = 0; i < vandalData.Count; i++)
        {
            string[] row = vandalData[i].Split(',');
            groups.Add(row[0]);
            if (!string.IsNullOrEmpty(row[1]))
            {
                float y = float.Parse(row[1]);
                data.Add(new Vector2(i + 1, y));
            }
        }
        //Populate Data
        series1.pointValues.SetList(data);




        //Benign Edit Series Settings
        series2 = graph.addSeries();
        series2.lineColor = Color.blue;
        series2.seriesName = "Benign Edits";
        series2.UseXDistBetweenToSpace = true;

        //Populate Vandal data
        groups = new List<string>();
        data = new List<Vector2>();
        for (int i = 0; i < benignData.Count; i++)
        {
            string[] row = benignData[i].Split(',');
            groups.Add(row[0]);
            if (!string.IsNullOrEmpty(row[1]))
            {
                float y = float.Parse(row[1]);
                data.Add(new Vector2(i + 1, y));
            }
        }

        //Populate Data
        series2.pointValues.SetList(data);


        //Set groups
        graph.groups.SetList(groups);
        graph.xAxis.AxisLabelSkipInterval = 1;


        if (groups.Count > 1){
            graph.xAxis.AxisNumTicks = groups.Count;
        }
        else{
            graph.xAxis.AxisNumTicks = 2;
        }
        


        /*Destroy(graphGO);
        graphGO = GameObject.Instantiate(emptyGraphPrefab);
        graphGO.transform.SetParent(this.transform, false);
        graph = graphGO.GetComponent<WMG_Axis_Graph>();
        
        
        series1 = graph.addSeries();
		//graph.xAxis.AxisMaxValue = 100;
		//series1.seriesName = newTitle;
		//series1.pointValues.SetList(newData);
		if (series1Data.Count > 1) {
			graph.xAxis.AxisNumTicks = series1Data.Count;
		} else {
			graph.xAxis.AxisNumTicks = 2;
		}
		series1.pointValues.SetList(series1Data);
        series1.seriesName = newTitle;

        if (true) {
			List<string> groups = new List<string>();
			List<Vector2> data = new List<Vector2>();
			for (int i = 0; i < series1Data2.Count; i++) {
				string[] row = series1Data2[i].Split(',');
				groups.Add(row[0]);
				if (!string.IsNullOrEmpty(row[1])) {
					float y = float.Parse(row[1]);
					data.Add(new Vector2(i+1, y));
				}
			}

			graph.groups.SetList(groups);
			graph.useGroups = true;

			graph.xAxis.LabelType = WMG_Axis.labelTypes.groups;
            if (groups.Count > 1)
            {
                graph.xAxis.AxisNumTicks = groups.Count;
            }
            else
            {
                graph.xAxis.AxisNumTicks = 2;
            }


			

			series1.UseXDistBetweenToSpace = true;

			series1.pointValues.SetList(data);
		}
		else {
			series1.pointValues.SetList(series1Data);
		}
	}
    */

    }


}
