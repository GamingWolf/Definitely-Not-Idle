using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageScript : MonoBehaviour {

	public GameManager GM;

	// Use this for initialization
	void Start () {
		InvokeRepeating("DPS", 1.0f, 1.0f);
	}

	public void DPS()
	{
		GM.enemyHP -= Mathf.Round((float)GM.dps);
	}

	// Update is called once per frame
	void Update () {
		
	}
}
