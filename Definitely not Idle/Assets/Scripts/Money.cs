using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Money : MonoBehaviour {

	private string scoreString ;
	private int cSize = 100000 ;
	private int bSize = 1000 ;
	private int aSize = 1 ;

	// Use this for initialization
	void Start () {
		
	}

	string ReturnNumber(float number)
	{
		float aScore = Math.Floor (number / aSize);
		float bScore = Math.Floor ((number - aScore * aSize) / bSize);
		float cScore = Math.Floor ((number - bScore * bSize - bScore * bSize) / kSize);


		return scoreString;
	}
}
