using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public Text ducatDisp, stageDisp, dpsDisp;

	public float ducats = 0;
	public int stage = 1, bossTimer = 30, heroDamage = 0, dps = 0;
				

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		ducatDisp.text = "Ducats: " + ducats;
		stageDisp.text = "Stage: " + stage;
		dpsDisp.text = "Idle DPS: " + dps;
	}
}
