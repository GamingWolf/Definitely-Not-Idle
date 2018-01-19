using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageScript : MonoBehaviour {

	public GameManager GM;

	// Use this for initialization
	void Start () {
		InvokeRepeating("DPS", 1.0f, 0.1f);
	}

	public void DPS()
	{
		GM.enemyHP -= Math.Round(GM.dps);
	}

	// Update is called once per frame
	void Update () {
		
	}
}
