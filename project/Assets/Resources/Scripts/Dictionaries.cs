using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Dictionary : MonoBehaviour {
	//enemy class and dictionary
	public static Dictionary<string, EnemyEntry> EnemyDict = new Dictionary<string, EnemyEntry>();

	public class EnemyEntry {
		string name;
		GameObject prefab;
		public List<GameObject> objlist = new List<GameObject> ();
		public EnemyEntry(string tempname, int tempcash, int temppoint, int temphp, int tempspeed) {
			this.name = tempname;
			prefab = Instantiate(Resources.Load("Prefab/Enemy")) as GameObject;
			prefab.name = name + "_PREFAB";
			prefab.GetComponent<EnemyAI> ().CashValue = tempcash;
			prefab.GetComponent<EnemyAI> ().PointValue = temppoint;
			prefab.GetComponent<EnemyAI> ().MaxHp = temphp;	
			prefab.GetComponent<NavMeshAgent>().speed = tempspeed;
			for(int i = 0; i < 40; i++) {
				GameObject spawn = (GameObject)Instantiate(prefab,Vector3.zero,Quaternion.identity);
				spawn.name = name;
				spawn.SetActive(false);
				objlist.Add(spawn);
			}
			EnemyDict.Add(this.name,this);
		}
		public GameObject Spawn(Vector3 pos) {
			for(int i = 0; i < objlist.Count; i++) {
					if(!objlist[i].activeInHierarchy) {			
					objlist[i].SetActive(true);
					objlist[i].transform.position = pos;
					return objlist[i];
				}
			}
			GameObject spawn = (GameObject)Instantiate(prefab,pos,Quaternion.identity);
			spawn.name = name;
			objlist.Add(spawn);
			return spawn;
		}
	}
	//tower class and dictionary
	public static Dictionary<string, TowerEntry> TowerDict = new Dictionary<string, TowerEntry>();

	public class TowerEntry {
		public string name;
		public GameObject prefab;
		List<GameObject> objlist = new List<GameObject>();
		public TowerEntry(string tempname, int temprange, float tempspeed, int tempdamage, int temphp) {
			this.name = tempname;
			GameObject prefab = Instantiate(Resources.Load("Prefab/Tower")) as GameObject;
			prefab.name = name + "_PREFAB";
			prefab.GetComponent<TowerAI> ().SearchRange = temprange;
			prefab.GetComponent<TowerAI> ().AttackSpeed = tempspeed;
			prefab.GetComponent<TowerAI> ().AttackDamage = tempdamage;
			prefab.GetComponent<TowerAI> ().hp = temphp;
			TowerDict.Add(this.name,this);
			for(int i = 0; i < 40; i++) {
				GameObject spawn = (GameObject)Instantiate(prefab,Vector3.zero,Quaternion.identity);
				spawn.name = name;
				spawn.SetActive(false);
				objlist.Add(spawn);
			}
		}

		public GameObject Spawn(Vector3 pos) {
			for(int i = 0; i < objlist.Count; i++) {
				if(!objlist[i].activeInHierarchy) {			
					objlist[i].SetActive(true);
					objlist[i].transform.position = pos;
					return objlist[i];
				}
			}
			GameObject spawn = (GameObject)Instantiate(this.prefab,pos,Quaternion.identity);
			spawn.name = name;
			objlist.Add(spawn);
			return spawn;
		}
	}

}

