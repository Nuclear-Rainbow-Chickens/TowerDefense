using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {

	public GameObject Target;
	private NavMeshAgent NavAgent;
	// Use this for initialization
	void Start () {
		NavAgent = gameObject.GetComponent<NavMeshAgent> ();
		NavAgent.SetDestination (Target.transform.position);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
