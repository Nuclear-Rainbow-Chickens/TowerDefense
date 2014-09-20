using UnityEngine;
using System.Collections;

public class TowerAI : MonoBehaviour {
	public bool DebugMode = false;
	public float RotateSpeed = 0.0f;
	public float SearchRange = 0.0f;
	public GameObject Target;
	private int LineCastIgnore = 8;
	// Use this for initialization
	void Start () {
		StartCoroutine ("Seek");
		Debug.Log ("hi");
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
		
	}
	void Update() {
		
	}
	void OnDrawGizmos() {
		if(DebugMode == true) {
			Gizmos.color = Color.green;

		}
	}
	IEnumerator Seek() {
		while(true) {
			bool Found = false;
			RaycastHit hit;
			Quaternion rot = new Quaternion();
			rot.eulerAngles = new Vector3(0,45 * Mathf.Sin(RotateSpeed * Time.time) - 90,0);
			transform.rotation = rot;
			transform.Rotate (Vector3.up * RotateSpeed, Space.World);
			//enemy detection
			Collider[] hits = Physics.OverlapSphere(transform.position, SearchRange);
			foreach(Collider c in hits) {
				if(c.tag.Equals("Enemy")) {
					if(Physics.Linecast(transform.position, c.gameObject.transform.position, out hit)) {
						if(hit.collider.tag.Equals("Enemy")) {
						Found = true;
						Target = c.gameObject;
						break;
						}
					}
				}
				else {
					continue;
				}
			}
			if(Found == true) {
				//call attack coroutine
				Debug.Log("Switch to Attack!");
				Found = false;
				yield return StartCoroutine("Attack");
			}

			yield return new WaitForFixedUpdate();
		}
	}
	IEnumerator Attack() {
		while(true) {
			RaycastHit hit;
			if(Vector3.Distance(transform.position, Target.transform.position) <= (SearchRange + 0.5) &&
			   Physics.Linecast(transform.position, Target.transform.position, out hit) &&
			   hit.collider.tag.Equals("Enemy")) {
				transform.LookAt(Target.transform);
			}
			else {

				//call seek coroutine
				Debug.Log("Switch to Seek!");
				Target = null;
				yield return StartCoroutine("Seek");
			}
			yield return new WaitForFixedUpdate();
		}
		
	}
	
}