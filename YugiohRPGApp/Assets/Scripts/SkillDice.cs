using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SkillDice
{
	//cache random seed
	protected System.Random rnd = new System.Random();

	protected string actualRank { get; set; }
	protected int[] results { get; set; }
	
	//create maximum and minimum values for randomization.
	private int maxVal = 7;
	private int minVal = 1;


	//constructor for the skill dice instantiation.
	public SkillDice(string selRank, int[] pRes)
	{
		actualRank = selRank; //key for lookup
		results = pRes; //outcomes in array
	}

	//constructor to encapsulate data to send out.
	public class DieResult
	{
		public bool critResult { get; set; }
		public int rollValue { get; set; }

		public DieResult(bool crit, int val)
		{
			critResult = crit;
			rollValue = val;
		}
	}

	public DieResult Roll()
	{
		bool _crit = false;
		int randResult = rnd.Next(minVal, maxVal);
		//Debug.Log(randResult);
		if (randResult == maxVal -1) 
		{
			_crit = true;
			Debug.Log("dieCrits");
		}

		DieResult _outResult = new DieResult(_crit, results[randResult - 1]);
		return _outResult;
	}

}
