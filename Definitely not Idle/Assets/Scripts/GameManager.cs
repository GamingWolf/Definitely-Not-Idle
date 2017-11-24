using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public Text ducatDisp, stageDisp, dpsDisp;

	public int ducats = 0,
				stage = 1, 
				bossTimer = 30, 
				heroClickDamage = 0, 
				dps = 0, 
				enemyHP = 500,
				bossHP = 1000,
				bossesKilled = 0, 
				stagesUntilBoss = 10;
				

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		ducatDisp.text = "Ducats: " + ducats;
		stageDisp.text = "Stage: " + stage;
		dpsDisp.text = "Idle DPS: " + dps;
	}

	public void EnemyInit()
	{
		enemyHP = 500 * (stage / 100);
	}

	public void BossInit()
	{
		bossHP = 1000 * (stage / 100 + bossesKilled)
	}
}
