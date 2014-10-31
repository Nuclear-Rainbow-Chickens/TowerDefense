using UnityEngine;
using System.Collections;
using System.Xml;
using System.IO;
using System.Collections.Generic;
using System.Linq;

public class SpawnControl : MonoBehaviour {
	private XmlNodeList waves;
	public int CurrentStage;
	public int MaxStageNumber;
	public int StageNumber = 0;
	public static int CurrentWave;
	public int NonStaticWave;
	private string StageType;
	public XmlNodeList Stages;
	private GameObject[] SpawnPoints;
	bool paused;
	WWW WebDoc;
	// Use this for initialization
	void Start () {
		WebDoc = new WWW("http://nuclear-rainbow-chickens.github.io/XML/Waves.xml"); 
		while(!WebDoc.isDone) {

		}
		XmlDocument xmldoc = new XmlDocument ();
		xmldoc.Load (new StringReader (WebDoc.text));
		Debug.Log (WebDoc.text);
		waves = xmldoc.GetElementsByTagName ("wave");
		CurrentWave = 0;
		CurrentStage = 0;
		SpawnPoints = GameObject.FindGameObjectsWithTag ("Spawn");
		StartCoroutine ("Wave");
	}

	void Update() {
		NonStaticWave = CurrentWave;
	}
	
	// Update is called once per frame
	IEnumerator Wave () {
		while(CurrentWave < waves.Count) {
			paused = false;
			Stages = waves [CurrentWave].ChildNodes;
			while(CurrentStage < Stages.Count) {
				XmlElement StageElement = (XmlElement) Stages [CurrentStage];
				StageType = StageElement.GetAttribute("type");
				MaxStageNumber = int.Parse(StageElement.GetAttribute("value"));
				if(StageNumber <= MaxStageNumber) {
					if(StageType.Equals("wait")) {
						StageNumber += 1;
					}
					else {
						Dictionary.EnemyDict[StageType].Spawn(SpawnPoints[Random.Range(0,SpawnPoints.Length)].transform.position);
						StageNumber += 1;
					}
				}
				else{
					Debug.Log("WAVE: " + CurrentWave.ToString() + " STAGE-COMPLETED " + CurrentStage.ToString());
					CurrentStage += 1;
					StageNumber = 0;
				}
				yield return new WaitForSeconds(1);
			}
			StageNumber = 0;
			Debug.Log("WAVE FINISHED " + CurrentWave.ToString() + " NOT LAST WAVE: " + (CurrentWave < waves.Count).ToString());
			++CurrentWave;
			CurrentStage = 0;
			Debug.Break();
		}
		Debug.Log ("NO MORE WAVES");
		StopCoroutine ("Wave");

	}
	IEnumerator PauseCoroutine() {
		paused = true;
		System.GC.Collect ();
		Time.timeScale = 0.0f;
		while (true)
		{
			if (Input.GetKeyDown(KeyCode.P))
			{	paused = false;
				Time.timeScale = 1.0f;
				Debug.Log("PRESSED RUNNING GC");
				StopCoroutine("PauseCoroutine");
			}    
			yield return null; 
		}
	}

	void OnGUI() {
		if(paused == true) {
			GUI.Label (new Rect (510, 0, 200, 20), "Wave Finished, press P to begin!");
		}
	}
}
