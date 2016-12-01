using UnityEngine;
using System.Collections.Generic;
using System.Collections;


public class DrawTimeLine : MonoBehaviour {
	public GameObject emptyGraphPrefab;
	public WMG_Axis_Graph graph;

	public WMG_Series series1;
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

	public void UpdateGraph(List<Vector2> series1Data, List<string> series1Data2, string newTitle){

		Destroy (graphGO);
		graphGO = GameObject.Instantiate (emptyGraphPrefab);
		graphGO.transform.SetParent (this.transform, false);
		graph = graphGO.GetComponent<WMG_Axis_Graph> ();



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


		if (false) {
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

			series1.seriesName = newTitle;

			series1.UseXDistBetweenToSpace = true;

			series1.pointValues.SetList(data);
		}
		else {
			series1.pointValues.SetList(series1Data);
		}
	}



}
