using UnityEngine;
using System.Collections;

public class DumpStuff : MonoBehaviour {

	public Transform thinghy;
	public bool hitthinghy;
	public string thinghytag;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		RaycastHit anotherthing;
		hitthinghy = Physics.Linecast (transform.position, thinghy.position, out anotherthing);
		if(hitthinghy) {
			thinghytag = anotherthing.collider.tag;
		}
	}
}
