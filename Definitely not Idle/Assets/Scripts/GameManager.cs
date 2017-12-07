using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public Text ducatDisp, stageDisp, dpsDisp, placeHP, killedEnemies;

	public int  ducats = 0,
				stage = 1, 
				bossTimer = 30, 
				heroClickDamage = 10, 
				dps = 10, 
				bossesKilled = 0, 
				stagesUntilBoss = 100,
				killedEn = 0;

	public double   enemyHP = 500,
					bossHP = 1000;

	public bool firstStart = true;
		
	// Use this for initialization
	void Start () {
		if (firstStart) 
		{
			enemyHP = 500 / 100 * stage;
			firstStart = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
		ducatDisp.text = "Ducats: " + ducats;
		stageDisp.text = "Stage: " + stage;
		dpsDisp.text = "Idle DPS: " + dps;
		placeHP.text = "HP: " + enemyHP;
		EnemyInit ();
	}


	public void EnemyInit()
	{
		if (enemyHP <= 0) 
		{
			stage += 1;
			enemyHP = 500 / 100 * stage + Mathf.Pow(2, stage);
			stagesUntilBoss -= 1;
			killedEn += 1;
			killedEnemies.text = "Killed: " + killedEn;
		}
	}

	public void BossInit()
	{
		bossHP = 1000 * (stage / 100 + bossesKilled);
	}
}
