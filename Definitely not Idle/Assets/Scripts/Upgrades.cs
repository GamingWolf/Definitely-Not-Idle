using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Upgrades : MonoBehaviour {

	public GameObject UpgradePanel;

	public GameManager GM;

	public Text dpsLvl, heroLvl, dpsCost, heroCost,tickLvl, tickCost;

	public double  	dpsLvlInt = 1,
					heroLvlInt = 1,
					tickCostInt = 1000,
					tickLvlint = 0.25,
					dpsCostInt = 10,
					heroCostInt = 10;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		dpsLvl.text = "DPS Level: " + dpsLvlInt;
		heroLvl.text = "Hero Level: " + heroLvlInt;
		tickLvl.text = "Tick Rate: " + GM.tickRate;
		dpsCost.text = "Cost: " + Math.Round(dpsCostInt).ToString("0.00E+0");
		heroCost.text = "Cost: " + Math.Round(heroCostInt).ToString("0.00E+0");
		tickCost.text = "Cost: " + Math.Round(tickCostInt).ToString("0.00E+0");
	}

	public void UpgradeDPS()
	{
		if (GM.ducats >= Math.Round(dpsCostInt)) 
		{
			GM.ducats -= Math.Round (dpsCostInt);
			dpsLvlInt += 1;
			GM.dps = GM.dps + (dpsLvlInt * (1.07 + (GM.dps / 100)));
			dpsCostInt = dpsLvlInt * ((5 + dpsLvlInt) * 1.07 * (dpsLvlInt * 0.7));
		}
	}

	public void UpgradeHero()
	{
		if (GM.ducats >= Math.Round(heroCostInt)) 
		{
			GM.ducats -= Math.Round (heroCostInt);
			heroLvlInt += 1;
			GM.heroClickDamage = GM.heroClickDamage + (heroLvlInt * (1.10 + (GM.heroClickDamage / 180)));
			heroCostInt = heroLvlInt * ((5 + heroLvlInt) * 1.07 * (heroLvlInt * 0.7));
		}
	}

	public void UpgradeTick()
	{
		if (GM.ducats >= Math.Round (tickCostInt) && GM.tickRate > 0.0001) 
		{
			GM.ducats -= Math.Round (tickCostInt);
			//function 1/x +1 
			GM.tickRate = 1/(tickLvlint + 1);
			tickCostInt = tickCostInt * 2;
			tickLvlint += 0.25;
		}
	}

	public void ShowHideUpgrades()
	{
		if (UpgradePanel.activeSelf == true) 
		{
			UpgradePanel.SetActive (false);
		}
		else
		{
			UpgradePanel.SetActive (true);
		}
	}
}
