using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class DiceManager
{
	//generate instance of skillDice.
	public static SkillDice mySkillDice;
	public static SkillDice.DieResult myDiceResult; //cashe result for use 
	public static Dictionary<int, SkillDice> DiceBag = new Dictionary<int, SkillDice>();

	//cashe reference to skill die constructor
	private static SkillDice D = new SkillDice("D", new[]{0,1,1,2,2,3});    //{0,1,2,2,3,3}
	private static SkillDice C = new SkillDice("C", new[]{0,1,2,3,3,4});
	private static SkillDice B = new SkillDice("B", new[]{1,2,3,3,4,5});
	private static SkillDice A = new SkillDice("A", new[]{2,3,4,4,5,6});
	private static SkillDice SS = new SkillDice("SS", new[]{3,3,4,5,6,7});

	
//stores all rank Dice in KeyPair format
public static void GenerateDiceList()
{
		if (DiceBag.ContainsKey(1)) 
		{ return; }
	DiceBag.Add(1, D);
	DiceBag.Add(2, C);
	DiceBag.Add(3, B);
	DiceBag.Add(4, A);
	DiceBag.Add(5, SS);
}

//method to get roll a number of particular rank dice from the dice bag and return the result as a integer value.

public static int RollSomeDice(int rankToRoll, int numDice)
{
	int returnVal = 0;
	int tempVal = 0;
	int critSuccesses = 0;
	bool critCheck = false;

	myDiceResult = new SkillDice.DieResult(critCheck, tempVal);

		//make sure lookup contains the paramater string.
		if (DiceBag.ContainsKey(rankToRoll))
		{
			for (int i = 0; i < numDice; i++)
			{
				myDiceResult = DiceBag[rankToRoll].Roll();
				returnVal += myDiceResult.rollValue;
				if (myDiceResult.critResult)
				{
					//Debug.Log("result had " + critSuccesses);
					critSuccesses += 1;
				}
				Debug.Log("Dice number " + i + " roll value was: " + myDiceResult.rollValue);

			}
		}
		else 
		{
			Debug.LogError("Failure to find key" + rankToRoll);
		}
	if (critSuccesses >= 2)
	{
		returnVal += 10;
			Debug.Log("CRIT");
		//trigger crit event.
	}
	return returnVal;
}

//evaluate the appropriate skill die based on the characters stats
public static string GetDiceRank(int abv)
{
	string rankStr = "Default";
		if (abv <= 5)
		{
			rankStr = "D";
		}
		else if (abv > 5 && abv <= 9)
		{
			rankStr = "C";
		}
		else if (abv > 9 && abv <= 19)
		{
			rankStr = "B";
		}
		else if (abv > 19 && abv <= 29)
		{
			rankStr = "A";
		}
		else if (abv >= 30)
		{
			rankStr = "SS";
		}
		return rankStr; 
}

//return a string and take in a integer value
//compare value to thresholds

}
