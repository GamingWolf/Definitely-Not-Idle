using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Money : MonoBehaviour {

	private string ducatString;
	private int bSize = 1000 ;
	private int aSize = 1 ;


	string MoneyScale(double number)
	{
		double aScore = Math.Floor (number / aSize);
		double bScore = Math.Floor ((number - aScore * aSize) / bSize);

		return ducatString;
	}

	private void SuffixScale(string s)
	{
		
	}
}
