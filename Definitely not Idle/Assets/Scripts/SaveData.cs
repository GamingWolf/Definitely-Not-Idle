using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveData : MonoBehaviour {

	public GameManager GM;

	public void Save()
	{
		PlayerPrefs.SetInt ("stage", GM.stage);
	}

	public void Load()
	{
		GM.stage = PlayerPrefs.GetInt("stage");

	}
}
