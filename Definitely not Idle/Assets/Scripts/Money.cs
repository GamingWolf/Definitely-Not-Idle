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

	string ReturnNumber(double number)
	{
		double aScore = Math.Floor (number / aSize);
		double bScore = Math.Floor ((number - aScore * aSize) / bSize);


		return scoreString;
	}
}
