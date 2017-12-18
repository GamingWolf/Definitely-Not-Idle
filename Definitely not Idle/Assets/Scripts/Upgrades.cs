using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Upgrades : MonoBehaviour {

	public GameObject UpgradePanel;

	public GameManager GM;

	public Text dpsLvl, heroLvl, dpsCost, heroCost;

	public double  	dpsLvlInt = 1,
					heroLvlInt = 1,
					dpsCostInt = 10,
					heroCostInt = 10;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		dpsLvl.text = "DPS Level: " + dpsLvlInt;
		heroLvl.text = "Hero Level: " + heroLvlInt;
		dpsCost.text = "Cost: " + Mathf.Round((float)dpsCostInt);
		heroCost.text = "Cost: " + Mathf.Round((float)heroCostInt);
	}

	public void UpgradeDPS()
	{
		if (GM.ducats >= Mathf.Round((float)dpsCostInt)) 
		{
			dpsLvlInt += 1;
			GM.dps = GM.dps + (dpsLvlInt * (1.07 + (GM.dps / 100)));
			dpsCostInt = dpsLvlInt * ((5 + dpsLvlInt) * 1.07 * (dpsLvlInt * 0.7));
		}
	}

	public void UpgradeHero()
	{
		if (GM.ducats >= Mathf.Round((float)heroCostInt)) 
		{
			heroLvlInt += 1;
			GM.heroClickDamage = GM.heroClickDamage + (heroLvlInt * (1.10 + (GM.heroClickDamage / 100)));
			heroCostInt = heroLvlInt * ((5 + heroLvlInt) * 1.07 * (heroLvlInt * 0.7));
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
