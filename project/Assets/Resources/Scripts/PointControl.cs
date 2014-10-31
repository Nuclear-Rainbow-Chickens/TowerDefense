using UnityEngine;
using System.Collections;

public class PointControl : MonoBehaviour {
	private static int Score = 0;
	private static int Cash = 150;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public static void AddPoints(int additional) {
		Score += additional;
	}
	public static int GetPoints() {
		return Score;
	}
	public static void AddCash(int additional) {
		Cash += additional;
	}
	public static int GetCash() {
		return Cash;
	}
	public static bool WithdrawCash(int deposit) {
		if(Cash >= deposit) {
			Cash -= deposit;
			return true;
		}
		else {
			return false;
		}
	}
}
