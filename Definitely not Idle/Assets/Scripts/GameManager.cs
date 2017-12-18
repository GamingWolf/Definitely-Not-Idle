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

	public double    enemyHP = 500,
				     enemyHPMax,
					 bossHP = 1000;

	public bool firstStart = true;
		
	// Use this for initialization
	void Start () {
		if (firstStart) 
		{
			enemyHP = 100 * ( 1 + (( stage - 1 ) * ( stage - 1 ))  * 0.015 );
			enemyHPMax = enemyHP;
			firstStart = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
		ducatDisp.text = "Ducats: " + ducats;
		stageDisp.text = "Stage: " + stage;
		dpsDisp.text = "Idle DPS: " + dps;
		placeHP.text = "HP: " + enemyHP + " / " + enemyHPMax;
		EnemyInit ();
		GiveCash ();
	}

	public void GiveCash()
	{
		if (enemyHP <= 0) 
		{
			ducats +=  (int)(enemyHP * 0.1);
		}
	}

	public void EnemyInit()
	{
		if (enemyHP <= 0 && stagesUntilBoss > 0) {
			stage += 1;
			enemyHP = 100 * (1 + ((stage - 1) * (stage - 1)) * 0.015);
			enemyHPMax = enemyHP;
			stagesUntilBoss -= 1;
			killedEn += 1;
			killedEnemies.text = "Enemies killed: " + killedEn;
		} 
		else if (stagesUntilBoss <= 0 && enemyHP <= 0) 
		{
			BossInit ();
		}
	}

	public void BossInit()
	{
		bossHP = 1000 * ( 1 + (( stage + bossesKilled ) * ( stage + bossesKilled ))  * 0.015 );
		enemyHPMax = bossHP;
		enemyHP = bossHP;
		stagesUntilBoss = 100;
	}
}
