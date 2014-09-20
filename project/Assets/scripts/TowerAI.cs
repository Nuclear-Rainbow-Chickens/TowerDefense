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
			Gizmos.DrawRay (transform.position, transform.forward * SearchRange);
			Gizmos.DrawRay (transform.position, transform.forward * -1 * SearchRange);
			Gizmos.DrawRay (transform.position, transform.right * SearchRange);
			Gizmos.DrawRay (transform.position, transform.right * -1 * SearchRange);
			Gizmos.DrawRay (transform.position, (transform.forward * -1 + transform.right * -1) * SearchRange);
			Gizmos.DrawRay (transform.position, (transform.forward * -1 + transform.right) * SearchRange);
			Gizmos.DrawRay (transform.position, (transform.forward + transform.right* -1) * SearchRange);
			Gizmos.DrawRay (transform.position, (transform.forward + transform.right) * SearchRange);
		}
	}
	IEnumerator Seek() {
		while(true) {
			RaycastHit hit;
			Quaternion rot = new Quaternion();
			rot.eulerAngles = new Vector3(0,45 * Mathf.Sin(RotateSpeed * Time.time) - 90,0);
			transform.rotation = rot;
			transform.Rotate (Vector3.up * RotateSpeed, Space.World);
			if(Physics.Raycast(transform.position, transform.forward, out hit, SearchRange) ||
			   Physics.Raycast(transform.position, transform.right, out hit, SearchRange) ||
			   Physics.Raycast(transform.position, transform.right * -1, out hit, SearchRange) ||
			   Physics.Raycast(transform.position, transform.forward * -1, out hit, SearchRange) ||
			   Physics.Raycast(transform.position, (transform.forward + transform.right), out hit, SearchRange) ||
			   Physics.Raycast(transform.position, (transform.forward * -1 + transform.right * -1), out hit, SearchRange) ||
			   Physics.Raycast(transform.position, (transform.forward * -1 + transform.right), out hit, SearchRange) ||
			   Physics.Raycast(transform.position, (transform.forward + transform.right * -1), out hit, SearchRange)) {
				if(hit.collider.tag.Equals("Enemy")) {
					//call attack coroutine
					Debug.Log("Switch to Attack!");
					Target = hit.collider.gameObject;
					Debug.Log(hit.collider.tag);
					yield return StartCoroutine("Attack");
				}
			}
			yield return new WaitForFixedUpdate();
		}
	}
	IEnumerator Attack() {
		while(true) {
			RaycastHit hit;
			if(Vector3.Distance(transform.position, Target.transform.position) <= SearchRange &&
			   Physics.Linecast(transform.position, Target.transform.position,out hit) && 
			   !hit.collider.tag.Equals("Untagged")) { 
				transform.LookAt(Target.transform);
			}
			else{
				//call seek coroutine
				Debug.Log("Switch to Seek!");
				Target = null;
				yield return StartCoroutine("Seek");
			}
			yield return new WaitForFixedUpdate();
		}
		
	}
	
}