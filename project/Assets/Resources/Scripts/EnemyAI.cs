using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {
	public int MaxHp;
	public int hp;
	public GameObject Target;
	private NavMeshAgent NavAgent;
	public int PointValue = 0;
	public int CashValue = 0;
	public bool started = false;

	void OnEnable() {
		Target = GameObject.FindGameObjectWithTag ("Endpoint");
	}
	// Update is called once per frame
	void Update () {
		if(Target != null && !started) {
			NavAgent = gameObject.GetComponent<NavMeshAgent> ();
			NavAgent.SetDestination (Target.transform.position);
			hp = MaxHp;
			started = true;
		}
		if(hp < 1 && Target != null) {
			PointControl.AddPoints(PointValue);
			PointControl.AddCash((int)Random.Range(-2,2) + CashValue);
			started = false;
			gameObject.SetActive(false);
		}
		renderer.material.color = new Color (255, 0, 0, (float)hp/MaxHp);

	}
}
