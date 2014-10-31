using UnityEngine;
using System.Collections;
using System.Xml;
using System.IO;

public class XMLStats : MonoBehaviour {
	public TextAsset Towers;
	public TextAsset Enemies;

	public GameObject Pistol;

	public GameObject Grunt;
	// Use this for initialization
	void Start () {
		//tower stuff
		string TowerData = Towers.text;
		XmlDocument TowerDoc = new XmlDocument ();
		TowerDoc.Load (new StringReader (TowerData));
		XmlNodeList towers = TowerDoc.GetElementsByTagName ("turrets") [0].ChildNodes;
		foreach (XmlElement t in towers) {
			Dictionary.TowerEntry temp = new Dictionary.TowerEntry(t.Name,
			int.Parse(t.GetAttribute("range")),
			float.Parse(t.GetAttribute("speed")),
			int.Parse(t.GetAttribute("damage")),
			int.Parse(t.GetAttribute("hp")));
		}

		//enemy stuff
		string EnemyData = Enemies.text;
		XmlDocument EnemyDoc = new XmlDocument ();
		EnemyDoc.Load (new StringReader (EnemyData));
		XmlNodeList enemies = EnemyDoc.GetElementsByTagName ("enemies") [0].ChildNodes;
		foreach (XmlElement e in enemies) {
			Dictionary.EnemyEntry temp = new Dictionary.EnemyEntry(e.Name,
			int.Parse(e.GetAttribute("cash")),
			int.Parse(e.GetAttribute("points")),
			int.Parse(e.GetAttribute("hp")),
			int.Parse(e.GetAttribute("speed")));
		}
		GetComponent<MouseControl> ().enabled = true;
		GetComponent<SpawnControl> ().enabled = true;
		this.enabled = false;
	}

	// Update is called once per frame
	void Update () {
	
	}
}
