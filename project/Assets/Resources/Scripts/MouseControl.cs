using UnityEngine;
using System.Collections;
using System.Linq;
using System.Text;
public class MouseControl : MonoBehaviour {
	private StringBuilder str = new StringBuilder();
	public GUIStyle select;
	public GUIStyle point;
	public float Sensitivity;
	// Use this for initialization
	Vector3 left = Vector3.forward + Vector3.left;
	Vector3 right = Vector3.back + Vector3.right;
	Vector3 forward = Vector3.forward + Vector3.right;
	Vector3 backward = Vector3.back + Vector3.left;
	Rect TopRect = new Rect (0, 0, Screen.width, 25);
	Rect BottomRect = new Rect (0, Screen.height - 25, Screen.width, 25);
	Rect LeftRect = new Rect (0,0, 25, Screen.height);
	Rect RightRect = new Rect (Screen.width - 25,0, 25, Screen.height);
	Ray MousePosition;
	GameObject target = null;
	public GameObject pistol;
	string SelectedTower = "";
	GameObject BuyObject;
	MeshFilter CurrentMesh = null;
	Renderer ObjRenderer;
	bool Clear = false;
	bool NotInside;
	void OnEnable () {
		BuyObject = GameObject.Find ("BuyObject");
		ObjRenderer = BuyObject.renderer;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		MousePosition = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if(LeftRect.Contains(Input.mousePosition)) {
			transform.Translate(left * Sensitivity, Space.World);
		}
		if(RightRect.Contains(Input.mousePosition)) {
			transform.Translate(right * Sensitivity, Space.World);
		}
		//top and bottom are inversed for some reason
		if(TopRect.Contains(Input.mousePosition)) {
			transform.Translate(backward * Sensitivity, Space.World);
		}
		if(BottomRect.Contains(Input.mousePosition)) {
			transform.Translate(forward * Sensitivity, Space.World);
		}
		if(Physics.Raycast(MousePosition,out hit)) {
			if(hit.collider.tag.Equals("Enemy")) {
				target = hit.collider.gameObject;
			}
			else if(hit.collider.tag.Equals("Friendly")) {
				target = hit.collider.gameObject;
			}

		}
		try {
			if(target.activeInHierarchy == true) {
				if(target.tag.Equals("Enemy")) {
					str.Length = 0;
					str.Append(target.name);
					str.Append(" Health: ");
					str.Append(target.GetComponent<EnemyAI>().hp);
				}
				else if(target.tag.Equals("Friendly")) {
						str.Length = 0;
						str.Append(target.name);
						str.Append(" Health: ");
						str.Append(target.GetComponent<TowerAI>().hp);
				}
			}
			else {
				str.Length = 0;
			}
		}
		catch(System.NullReferenceException){
			str.Length = 0;
		}

		NotInside = false;
		RaycastHit buyhit;
		if(Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out buyhit,1 << 8)) {
			BuyObject.transform.position = buyhit.point + new Vector3(0,1,0);
			if(Physics.CheckSphere(BuyObject.transform.position + new Vector3(0,1,0),1.0f) == true) {
				ObjRenderer.material.color = Color.red;
				Clear = false;
			}
			else {
				ObjRenderer.material.color = Color.green;
				Clear = true;
			}
			int i = 0;
			for(int x = 0; x < Dictionary.TowerDict.Values.Count; x++) {
				if(new Rect(i,Screen.width - 50,50, 50).Contains(Input.mousePosition)) {
					NotInside = false;
					break;
				}
				NotInside = true;
				i += 50;
			}

			}
		if(Clear == true && NotInside == true && !SelectedTower.Equals("")) {
			if(Input.GetMouseButtonDown(0)) {
				GameObject clone = (GameObject) Dictionary.TowerDict[SelectedTower].Spawn(BuyObject.transform.position + new Vector3(0,1,0));
				Clear = false;
			}
		}
	}
	void OnGUI() {
		GUI.Label (new Rect (0, 0, 500, 20), "Wave: "+(SpawnControl.CurrentWave + 1), select);
		GUI.Label (new Rect (0, 25, 500, 20), str.ToString(), select);

		GUI.Label (new Rect (Screen.width - 175, 0, 175, 20), "Gold: "+PointControl.GetCash(), point);
		GUI.Label (new Rect (Screen.width - 175, 25, 175, 20), "Score: "+PointControl.GetPoints(), point);

		int i = 0;
		for(int x = 0; x < Dictionary.TowerDict.Values.Count; x++) {
			if(GUI.Button(new Rect(i,50,50, 50), Dictionary.TowerDict.Values.ToArray()[x].name)) {
				if(new Rect(i,Screen.width - 50,50, 50).Contains(Input.mousePosition)) {
					Clear = false;
				}
				SelectedTower = Dictionary.TowerDict.Values.ToArray()[x].name;
			}
			i += 50;
		}
	}

}
