using UnityEngine;
using System.Collections;

public class TowerAI : MonoBehaviour {
	public bool DebugMode = false;
	public float RotateSpeed = 0.0f;
	public float SearchRange = 0.0f;
	public float AttackSpeed;
	public int AttackDamage = 0;
	public GameObject Target;
	public int hp = 0;
	public AudioClip FireSound;
	private EnemyAI TargetAI;
	float NextAttack = 0;
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
		if(DebugMode == true) {
			Gizmos.color = Color.green;
			Gizmos.DrawWireSphere(transform.position, SearchRange);

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
			for(int i = 0; i < hits.Length; i++) {
				Collider c = hits[i];
				if(c.tag.Equals("Enemy")) {
					if(Physics.Linecast(transform.position, c.gameObject.transform.position, out hit)) {
						if(hit.collider.tag.Equals("Enemy") && Vector3.Distance(transform.position, hit.collider.gameObject.transform.position) <= SearchRange) {
							Found = true;
							Target = c.gameObject;
							TargetAI = c.gameObject.GetComponent<EnemyAI>();
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
				Found = false;
				StartCoroutine("Attack");
				StopCoroutine("Seek");
			}

			yield return new WaitForFixedUpdate();
		}
	}
	IEnumerator Attack() {
		while(true) {

			RaycastHit hit;
			if(Vector3.Distance(transform.position, Target.transform.position) <= (SearchRange) &&
			   Physics.Linecast(transform.position, Target.transform.position, out hit) &&
			   hit.collider.tag.Equals("Enemy")) {
				transform.LookAt(Target.transform);
			}
			else {

				//call seek coroutine
				Target = null;
				Target = null;
				StartCoroutine("Seek");
				StopCoroutine("Attack");
			}
			if(NextAttack < Time.time) {
				NextAttack = Time.time + AttackSpeed;
				TargetAI.hp -= AttackDamage;
				//AudioSource.PlayClipAtPoint(FireSound, transform.position);
			}

			yield return new WaitForFixedUpdate();
		}
		
	}
	
}