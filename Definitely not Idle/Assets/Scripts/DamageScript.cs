﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageScript : MonoBehaviour {

	public GameManager GM;

	// Use this for initialization
	void Start () {
		StartCoroutine(DPS()) ;
	}

	IEnumerator DPS()
	{
		yield return null;

		do 
		{
			GM.enemyHP -= Math.Round (GM.dps);
			yield return new WaitForSeconds((float)GM.tickRate);
		} while(true);
	}

	// Update is called once per frame
	void Update () {
		
	}
}
