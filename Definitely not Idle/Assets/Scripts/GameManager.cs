using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {


	public Text ducatDisp, stageDisp, dpsDisp, healthText, killedEnemies, clickDamageDisp;

	public Slider healthbar;

	public GameObject UpgradePanel, UM;

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
					bossHP = 1000,
					tickRate = 1;

		
	// Use this for initialization
	void Start () {
		if (File.Exists(Application.persistentDataPath + "/DefNoSave.NoSav"))
		{
			Load();
		}
		else
		{
			enemyHP = 100 * (1 + ((stage - 1) * (stage - 1)) * 0.015);
			enemyHPMax = enemyHP;
		}
		UpgradePanel.SetActive (false);
		Application.runInBackground = true;
		Screen.autorotateToPortrait = true;
	}
	
	// Update is called once per frame
	void Update () {
		ducatDisp.text = "Ducats: " + ducats.ToString("0.00E+0");
		stageDisp.text = "Stage: " + stage;
		dpsDisp.text = "DPS: " + Math.Round(dps).ToString("0.00E+0");;
		clickDamageDisp.text = "Hero Damage: " + Math.Round(heroClickDamage).ToString("0.00E+0");;
		healthText.text = "HP: " + Math.Round(enemyHP).ToString("0.00E+0") + " / " + Math.Round(enemyHPMax).ToString("0.00E+0");
		killedEnemies.text = "Enemies killed: " + killedEn;
		healthbar.value = CalculateHealth ();
		EnemyInit ();

		UM.GetComponent<Upgrades>().dpsLvl.text = "DPS Level: " + UM.GetComponent<Upgrades>().dpsLvlInt;
		UM.GetComponent<Upgrades>().heroLvl.text = "Hero Level: " + UM.GetComponent<Upgrades>().heroLvlInt;
		UM.GetComponent<Upgrades>().tickLvl.text = "Tick Rate: " + tickRate;
		UM.GetComponent<Upgrades>().dpsCost.text = "Cost: " + Math.Round(UM.GetComponent<Upgrades>().dpsCostInt).ToString("0.00E+0");
		UM.GetComponent<Upgrades>().heroCost.text = "Cost: " + Math.Round(UM.GetComponent<Upgrades>().heroCostInt).ToString("0.00E+0");
		UM.GetComponent<Upgrades>().tickCost.text = "Cost: " + Math.Round(UM.GetComponent<Upgrades>().tickCostInt).ToString("0.00E+0");
	}

	public void GiveCash()
	{
		ducats += Math.Round(enemyHP * 0.1);
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

	public void Save()
	{
		SnL.SavePlayer(this, UM);
	}

	public void Load()
	{
		double[] loadedStats = SnL.LoadPlayer();

		//gm double data
		ducats = loadedStats[0];
		enemyHP = loadedStats[1];
		dps = loadedStats[2];
		heroClickDamage = loadedStats[3];
		enemyHPMax = loadedStats[4];
		bossHP = loadedStats[5];
		tickRate = loadedStats[6];

		//gm int data
		stage = (int)loadedStats[7];
		bossTimer = (int)loadedStats[8];
		bossesKilled = (int)loadedStats[9];
		stagesUntilBoss = (int)loadedStats[10];
		killedEn = (int)loadedStats[11];

		//upgrade data
		UM.GetComponent<Upgrades>().dpsLvlInt = loadedStats[12];
		UM.GetComponent<Upgrades>().heroLvlInt = loadedStats[13];
		UM.GetComponent<Upgrades>().tickCostInt = loadedStats[14];
		UM.GetComponent<Upgrades>().tickLvlint = loadedStats[15];
		UM.GetComponent<Upgrades>().dpsCostInt = loadedStats[16];
		UM.GetComponent<Upgrades>().heroCostInt = loadedStats[17];
	}

	public static void NewGame()
	{
		if (File.Exists(Application.persistentDataPath + "/DefNoSave.NoSav"))
		{
			File.Delete(Application.persistentDataPath + "/DefNoSave.NoSav");
			//FileUtil.DeleteFileOrDirectory(Application.persistentDataPath + "/DefNoSave.NoSav");
		}
	}
	void OnApplicationQuit()
	{
		Save();
	}
			
}
