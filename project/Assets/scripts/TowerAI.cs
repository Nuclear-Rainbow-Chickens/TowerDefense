using UnityEngine;
using System.Collections;

public class TowerAI : MonoBehaviour {

	public float RotateSpeed = 0.0f;
	public float SearchRange = 0.0f;
	public GameObject Target;
	// Use this for initialization
	void Start () {
		StartCoroutine ("Seek");
	}
	
	// Update is called once per frame
	void FixedUpdate () {

	
	}
	void Update() {

	}
	void OnDrawGizmos() {
		Gizmos.color = Color.green;
		Gizmos.DrawRay (transform.position, transform.forward * SearchRange);
	}
	IEnumerator Seek() {
		while(true) {
			RaycastHit hit;
			transform.Rotate (Vector3.up * RotateSpeed, Space.World);
			if(Physics.Raycast(transform.position, transform.forward,out hit, SearchRange) ) {
				//call attack routine
				Target = hit.collider.gameObject;
			}
			yield return new WaitForEndOfFrame();
		}
	}
	IEnumerator Attack() {
		while(true) {
			if(Vector3.Distance(transform.position, Target.transform.position) <= SearchRange) {
				transform.LookAt(Target.transform);
			}
			else{
				//call seek coroutine
			}
			yield return new WaitForEndOfFrame();
		}
	}

}
