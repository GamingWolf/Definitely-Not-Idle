using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public Text ducatDisp, stageDisp, dpsDisp, healthText, killedEnemies, clickDamageDisp;

	public Slider healthbar;

	public GameObject UpgradePanel;

	public int  stage = 1, 
				bossTimer = 30, 
				bossesKilled = 0, 
				stagesUntilBoss = 100,
				killedEn = 0;

	public double   ducats = 0, 
					enemyHP = 500,
					dps = 10, 
					heroClickDamage = 10, 
				    enemyHPMax,
					bossHP = 1000;

	public bool firstStart = true;
		
	// Use this for initialization
	void Start () {
		if (firstStart) 
		{
			enemyHP = 100 * ( 1 + (( stage - 1 ) * ( stage - 1 ))  * 0.015 );
			enemyHPMax = enemyHP;
			UpgradePanel.SetActive (false);
			firstStart = false;
		}
		Application.runInBackground = true;
	}
	
	// Update is called once per frame
	void Update () {
		ducatDisp.text = "Ducats: " + ducats;
		stageDisp.text = "Stage: " + stage;
		dpsDisp.text = "Idle DPS: " + Math.Round(dps);
		clickDamageDisp.text = "Hero Damage: " + Math.Round(heroClickDamage);
		healthText.text = "HP: " + Math.Round(enemyHP) + " / " + Math.Round(enemyHPMax);
		healthbar.value = CalculateHealth ();
		EnemyInit ();
	}

	public void GiveCash()
	{
		ducats += Mathf.Round((float)(enemyHP * 0.1));
	}

	public void EnemyInit()
	{
		if (enemyHP <= 0 && stagesUntilBoss > 0) {
			stage += 1;
			enemyHP = Math.Round(100 * (1 + ((stage - 1) * (stage - 1)) * 0.015));
			enemyHPMax = enemyHP;
			GiveCash ();
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
		bossHP = Math.Round(1000 * ( 1 + (( stage + bossesKilled ) * ( stage + bossesKilled ))  * 0.015 ));
		enemyHPMax = bossHP;
		enemyHP = bossHP;
		stagesUntilBoss = 100;
	}

	public float CalculateHealth()
	{
		return (float)enemyHP / (float)enemyHPMax;
	}

	public void Exit()
	{
		Application.Quit ();
	}
}
