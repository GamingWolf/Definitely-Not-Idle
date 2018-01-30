using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public static class SnL
{
	


	public static void SavePlayer(GameManager GM, GameObject UM)
	{
		BinaryFormatter bf = new BinaryFormatter();

		FileStream stream = new FileStream(Application.persistentDataPath + "/DefNoSave.NoSav", FileMode.Create);

		PlayerData data = new PlayerData(GM, UM);

		bf.Serialize(stream, data);
		stream.Close();
	}

	public static double[] LoadPlayer()
	{
		if (File.Exists(Application.persistentDataPath + "/DefNoSave.NoSav"))
		{
			BinaryFormatter bf = new BinaryFormatter();

			FileStream stream = new FileStream(Application.persistentDataPath + "/DefNoSave.NoSav", FileMode.Open);

			PlayerData data = bf.Deserialize(stream) as PlayerData;

			stream.Close();

			return data.stats;
		}
		else {
			return new double[18];
		}
	}
}

[Serializable]
public class PlayerData
{
	public double[] stats;

	public PlayerData(GameManager GM, GameObject UM) 
	{
		//gm double data
		stats = new double[18];
		stats[0] = GM.ducats;
		stats[1] = GM.enemyHP;
		stats[2] = GM.dps;
		stats[3] = GM.heroClickDamage;
		stats[4] = GM.enemyHPMax;
		stats[5] = GM.bossHP;
		stats[6] = GM.tickRate;

		//gm int data
		stats[7] = GM.stage;
		stats[8] = GM.bossTimer;
		stats[9] = GM.bossesKilled;
		stats[10] = GM.stagesUntilBoss;
		stats[11] = GM.killedEn;

		//upgrade data
		stats[12] = UM.GetComponent<Upgrades>().dpsLvlInt;
		stats[13] = UM.GetComponent<Upgrades>().heroLvlInt;
		stats[14] = UM.GetComponent<Upgrades>().tickCostInt;
		stats[15] = UM.GetComponent<Upgrades>().tickLvlint;
		stats[16] = UM.GetComponent<Upgrades>().dpsCostInt;
		stats[17] = UM.GetComponent<Upgrades>().heroCostInt;
	}
}