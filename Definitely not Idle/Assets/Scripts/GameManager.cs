using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {


	public Text ducatDisp, stageDisp, dpsDisp, healthText, killedEnemies, clickDamageDisp;

	public Text[] bossText;

	public Slider healthbar;

	public GameObject UpgradePanel, UM, SettingsPanel, Sivir, Mercy, Ahri, BossWrapper;

	public int  stage = 1, 
				bossTimer = 30, 
				bossesKilled = 0, 
				stagesUntilBoss = 99,
				killedEn = 0;

	public double   ducats = 0, 
					enemyHP = 500,
					dps = 3.5, 
					heroClickDamage = 4.5, 
					enemyHPMax,
					bossHP = 1000,
					tickRate = 2;

	public bool done = false;
		
	// Use this for initialization
	void Start () {
		if (File.Exists(Application.persistentDataPath + "/DefNoSave.NoSav"))
		{
			Load();
		}
		else
		{
			//1.035^(x-5)
			enemyHP = Math.Pow(1.07, stage + 38);
			enemyHPMax = enemyHP;
		}
		bossText = BossWrapper.GetComponentsInChildren<Text>();
		BossWrapper.SetActive (false);
		UpgradePanel.SetActive (false);
		SettingsPanel.SetActive(false);
		Application.runInBackground = true;
		Sivir.SetActive (false);
		Mercy.SetActive (false);
		Ahri.SetActive (false);

	}
	
	// Update is called once per frame
	void Update () {
		ducatDisp.text = "Ducats: " + ducats.ToString("0.00E+0");
		stageDisp.text = "Stage: " + stage;
		dpsDisp.text = "Idle DPS: " + dps.ToString("0.00E+0");;
		clickDamageDisp.text = "Hero Damage: " + heroClickDamage.ToString("0.00E+0");;
		healthText.text = "HP: " + enemyHP.ToString("0.00E+0") + " / " + enemyHPMax.ToString("0.00E+0");
		killedEnemies.text = "Enemies killed: " + killedEn;
		healthbar.value = CalculateHealth ();
		EnemyInit ();

		UM.GetComponent<Upgrades>().dpsLvl.text = "DPS Level: " + UM.GetComponent<Upgrades>().dpsLvlInt;
		UM.GetComponent<Upgrades>().heroLvl.text = "Hero Level: " + UM.GetComponent<Upgrades>().heroLvlInt;
		UM.GetComponent<Upgrades>().tickLvl.text = "Tick Rate: " + tickRate;
		UM.GetComponent<Upgrades>().dpsCost.text = "Cost: " + Math.Round(UM.GetComponent<Upgrades>().dpsCostInt).ToString("0.00E+0");
		UM.GetComponent<Upgrades>().heroCost.text = "Cost: " + Math.Round(UM.GetComponent<Upgrades>().heroCostInt).ToString("0.00E+0");
		UM.GetComponent<Upgrades>().tickCost.text = "Cost: " + Math.Round(UM.GetComponent<Upgrades>().tickCostInt).ToString("0.00E+0");

		MoreHeros ();

		if (stage == 50 && !done) {
			ducats += 50000;
			done = true;
		}
	}

	public void GiveCash()
	{
		if (stage < 105) 
		{	
			ducats += Math.Round (enemyHP * 10);
		} 
		else 
		{
			ducats += Math.Round (enemyHP * 0.1);
		}
	}

	public void EnemyInit()
	{
		//1.035^(x-5)
		if (enemyHP <= 0 && stagesUntilBoss > 0) {
			stage += 1;
			enemyHP = Math.Pow(1.07, stage + 38);
			enemyHPMax = enemyHP;
			Debug.Log(enemyHP);
			GiveCash ();
			stagesUntilBoss -= 1;
			killedEn += 1;
			BossWrapper.SetActive (false);
			bossTimer = 30;
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
		bossesKilled++;
		stagesUntilBoss = 99;

		BossWrapper.SetActive (true);

		bossText[0].text = "Boss!";
		bossText[1].text = bossTimer + "s";

		StartCoroutine(BossTimer());
	}

	IEnumerator BossTimer()
	{
		do {
			bossTimer--;
			bossText[1].text = bossTimer + "s";
			if(bossTimer <= 0)
			{
				if(bossHP > 0)
				{
					stage -= 10;
					EnemyInit();
				}
				else
				{
					StopCoroutine(BossTimer());
				}
			}	
			yield return new WaitForSeconds (2);
		} while(true);
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

	public void NewGame()
	{
		ducats = 0;
		enemyHP = 500;
		dps = 3.5;
		heroClickDamage = 4.5;
		bossHP = 1000;
		tickRate = 2;
		stage = 1;
		bossTimer = 30;
		bossesKilled = 0;
		stagesUntilBoss = 99;
		killedEn = 0;
		done = false;

		UM.GetComponent<Upgrades>().dpsLvlInt = 1;
		UM.GetComponent<Upgrades>().heroLvlInt = 1;
		UM.GetComponent<Upgrades>().tickCostInt = 1000;
		UM.GetComponent<Upgrades>().tickLvlint = 0.25;
		UM.GetComponent<Upgrades>().dpsCostInt = 100;
		UM.GetComponent<Upgrades>().heroCostInt = 100;

		enemyHP = Math.Pow(1.07, stage + 38);
		enemyHPMax = enemyHP;

		BossWrapper.SetActive (false);
		SettingsPanel.SetActive(false);
	}

	public void ShowHideSettings()
	{
		if (SettingsPanel.activeSelf)
		{
			SettingsPanel.SetActive(false);
		}
		else
		{
			SettingsPanel.SetActive(true);
			UpgradePanel.SetActive(false);
		}
	}

	public void MoreHeros()
	{
		if (bossesKilled > 0) {
			Sivir.SetActive (true);
		} 
		else 
		{
			Sivir.SetActive (false);
		}

		if (bossesKilled > 4) {
			Mercy.SetActive (true);
		} 
		else 
		{
			Mercy.SetActive (false);
		}

		if (bossesKilled > 9) {
			Ahri.SetActive (true);
		} 
		else 
		{
			Ahri.SetActive (false);
		}
	}

	void OnApplicationQuit()
	{
		Save();
	}
			
}
