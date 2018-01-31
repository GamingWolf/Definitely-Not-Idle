using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Upgrades : MonoBehaviour
{

	public GameObject UpgradePanel, SettingsPanel;

	public GameManager GM;

	public Text dpsLvl, heroLvl, dpsCost, heroCost, tickLvl, tickCost;

	public double dpsLvlInt = 1,
					heroLvlInt = 1,
					tickCostInt = 1000,
					tickLvlint = 0.25,
					dpsCostInt = 100,
					heroCostInt = 100;

	public void UpgradeDPS()
	{
		if (GM.ducats >= Math.Round(dpsCostInt))
		{
			GM.ducats -= Math.Round(dpsCostInt);
			dpsLvlInt += 1;
			GM.dps = GM.dps + (dpsLvlInt * (1.07 + (GM.dps / 100)));
			dpsCostInt = Math.Pow(1.1, dpsLvlInt + 100);
		}
	}

	public void UpgradeHero()
	{
		if (GM.ducats >= Math.Round(heroCostInt))
		{
			GM.ducats -= Math.Round(heroCostInt);
			heroLvlInt += 1;
			GM.heroClickDamage = GM.heroClickDamage + (heroLvlInt * (1.10 + (GM.heroClickDamage / 180)));
			heroCostInt = Math.Pow(1.1, heroLvlInt + 100);
		}
	}

	public void UpgradeTick()
	{
		if (GM.ducats >= Math.Round(tickCostInt) && GM.tickRate > 0.0001)
		{
			GM.ducats -= Math.Round(tickCostInt);
			//function 1/x +1 
			GM.tickRate = 1 / (tickLvlint + 1);
			tickCostInt = tickCostInt * 2;
			tickLvlint += 0.25;
		}
	}

	public void ShowHideUpgrades()
	{
		if (UpgradePanel.activeSelf)
		{
			UpgradePanel.SetActive(false);
		}
		else
		{
			UpgradePanel.SetActive(true);
			SettingsPanel.SetActive(false);
		}
	}
}
