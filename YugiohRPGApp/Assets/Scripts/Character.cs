using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[System.Serializable]
public class Character 
{   //character class will contain a character game objects information collected from databases and edited.
	public string charName;  //this needs to be set during character creation
	public int athletics;
	public string AthRank;
	public int intelect;
	public string IntRank;
	public int charisma;
	public string ChaRank;

	public int HP;
	public int EXP;  //This is edited by the player from the edit screen and dictates many other statistics.
	public int totalAbilityPoints;

	// class will modify these values
	public int remainingAbilityPoints;
	public int Level;  //automatically set by entering exp
	public int remainingLevelsToInvest;//check your list of class' and reduce this by the total levels in that list.
	public int CPD; //Class variables will edit this value
	public int SSLevel; //Class variables will edit this value


	//list of accessible myClass elements
	public List<CharactersClass> myClasses = new List<CharactersClass>();

	//Character Object
	public Character ()
	{
		totalAbilityPoints = 25; //This is the default points from the Players Handbook page 7.
		Level = 0; //Default level to trigger a level up since the Add Exp will return a level 1 character.
				   //sets up character stats
		IncreaseAbilityScore(0, 1);
		IncreaseAbilityScore(0, 2);
		IncreaseAbilityScore(0, 3);
	}

	//To be accessed only when creating a new character. This will set the characters name.
	public void WriteCharacterName(string newName) 
	{
		charName = newName;
	}

	public string ReadCharacterName() 
	{
		return charName;
	}

	//Class Functions
	public void EvaluateHitpoints()
	{
		string rank = AthRank;
		int result = 0;
		switch (rank)
		{
			case "D":
				result = (DiceManager.RollSomeDice(1, 2) * 100); 
                break;
            case "C":
				result = (DiceManager.RollSomeDice(2, 2) * 100); 
                break;
            case "B":
				result = (DiceManager.RollSomeDice(3, 2) * 100);
				break;
            case "A":
				result = (DiceManager.RollSomeDice(4, 2) * 100);
				break;
            case "SS":
				result = (DiceManager.RollSomeDice(5, 2) * 100);
				break;
            default:
				Debug.LogError("No rank assigned.");
				break;
        }
		Debug.Log(rank + " Rank was rolled. The result was: " + result.ToString());
		result += myClasses[0].hitpointMod;
		HP += result;
		// to do:Multiply the result of the athletics dice result by 100 and add it to the character health.
		// add the class Hitpoint Modifier.
	}

	public int ReturnHitpoints() 
	{
		return HP;
	}
	public void EvaluateCPD()
	{
		string rankToRoll = ReturnRankByClassDice(myClasses[0].classDice);
		int diceCount = 1;

		int CPDResult = RollDiceByRank(diceCount, rankToRoll) + myClasses[0].CPDMod;
		CPD += CPDResult;
		// to do: create switch to check which dice to use for the cpd roll from your class.
		// add the modifier from your class.
		// add the result to the total cards per day.
	}

	public string ReturnRankByClassDice(int val) //This is a helper function to quickly assess which dice to roll in progression and its rank.
	{
		//Decide which is your class dice. 
		int classDice = val;
		string rank = "";
		string result;
		switch (classDice)
		{
			case 1://Ath
				rank = AthRank;
				break;
			case 2://Cha
				rank = ChaRank;
				break;
			case 3://Int
				rank = IntRank;
				break;
		}
		result = rank;
		return result;
	}

	public int RollDiceByRank(int count, string rank) //accepts a rank and number of dice to roll. This helper function handles dicerolling for class progression.
	{
		int result = 0;
		switch (rank)
		{
			case "D":
				result = (DiceManager.RollSomeDice(1, count));
				break;
			case "C":
				result = (DiceManager.RollSomeDice(2, count));
				break;
			case "B":
				result = (DiceManager.RollSomeDice(3, count));
				break;
			case "A":
				result = (DiceManager.RollSomeDice(4, count));
				break;
			case "SS":
				result = (DiceManager.RollSomeDice(5, count));
				break;
			default:
				Debug.LogError("No rank assigned to class dice.");
				break;
		}
		return result;
	}
	public void EvaluateTotalAbP()//removed my class id peramater
	{
		totalAbilityPoints += myClasses[0].AbVBonus;
		remainingAbilityPoints = totalAbilityPoints;
		//increase the ABP by the class bonus.
	}

	public void IncreaseTotalAbPLEVELUP() 
	{
		totalAbilityPoints += myClasses[0].AbVBonus;
		remainingAbilityPoints += myClasses[0].AbVBonus;
	}

	public string EvaluateSkillRank(int val) 
	{
		string result = "";

        if (val <= 5)
		{
			result = "D";
		}
		else if (val > 5 && val <= 9)
		{
			result = "C";
		}
		else if (val > 9 && val <= 19)
		{
			result = "B";
		}
		else if (val > 19 && val <= 29)
		{
			result = "A";
		}
		else if (val >= 30) 
		{
			result = "SS";
		}
		return result;
	}

	public int ReadAbP() 
	{
		return totalAbilityPoints;
	}
	public int ReadRemainingAbP() 
	{
		return remainingAbilityPoints;
	}
	public void CasheACharacterClass(int i) 
	{
		if (!ClassDatabase._gameClass.ContainsKey(i)) //check for the valid key.
		{ 
			Debug.LogError("Passed Key " + i.ToString() + " does not exist in registry.");
			return;
		}
		myClasses.Add(new CharactersClass(i));
		EvaluateTotalAbP();
		//To do: Set up a characters class and add it to the list of character classes.
	}

	public int ReadExp() 
	{
		return EXP;
	}

	//user influenced functions on creation and level up
	public void IncreaseAbilityScore(int upVal, int coreStat)
	{
		int result = athletics + intelect + charisma;

		if (upVal > remainingAbilityPoints) 
		{
			return;
		}
		if (result >= totalAbilityPoints && upVal > 0)
		{
			//if the requested ammount to add is greater than the remaining ability points.
			return;
		}

		//increases an ability score by the parameter upVal
		switch (coreStat) 
		{
			case 1: //athletics
				athletics += upVal;
				AthRank = EvaluateSkillRank(athletics);
				remainingAbilityPoints -= upVal;
				break;
			case 3: //intelect
				intelect += upVal;
				IntRank = EvaluateSkillRank(intelect); 
				remainingAbilityPoints -= upVal;
				break;
			case 2: //charisma
				charisma += upVal;
				ChaRank = EvaluateSkillRank(charisma);
				remainingAbilityPoints -= upVal;
				break;
		}
		//create a sum of the abilities
	}

	public int ReturnAthVal() 
	{
		return athletics;
	}
	public int ReturnChaVal() 
	{
		return charisma;
	}
	public int ReturnIntVal() 
	{
		return intelect;
	}

	//increase the character EXP by a passed value. then call a level
	public int IncreaseEXP(int expVal)
	{
		EXP += expVal;
		int newLevel = 0;

		if (EXP < 69)
		{
			newLevel = 1;
		}
		else if (EXP >= 69 && EXP < 148)
		{
			newLevel = 2;
		}
		else if (EXP >= 148 && EXP < 237)
		{
			newLevel = 3;
		}
		else if (EXP >= 237 && EXP < 337)
		{
			newLevel = 4;
		}
		else if (EXP >= 337)
		{
			newLevel = 5;
		}

		if (newLevel == Level)
		{ return Level; }

		//call a levelup game event.
		//grab the level differential. new level is 3, level is 1 level dif = 2
		//the player has not invested any levels. so he still has level 1 to spend.
		int levelDif = newLevel - Level; //this should resolve as as one on a single level up.
		if (remainingLevelsToInvest <= Level) //if you have the same or less levels to invest than your previous level.
		{
			remainingLevelsToInvest += levelDif; //add the difference between the new level and the last level to your remaining levels to invest.
			//Generate a popup alerting the player that they may spend stored levels?
			//Generate a button which allows the players to invest levels.
		}
		Level = newLevel;
		return Level;
	}
	// this will handle leveling up a character perhaos on a button press inside a popup.
	public void InvestALevel() //This will always invest one level at a time.
	{
		int nextLevel;
		//Obligitory if to make sure things stay within bounds.
		if (remainingLevelsToInvest <= 0) { return; }
		//reduce our pool of investable levels by 1 ie(remainingLevelsToInvest --;)
		remainingLevelsToInvest--;
		//we need the class table at level so first we will level the class.
		if (Level != 1) 
		{
			myClasses[0].classLevel++; 
		}
		nextLevel = myClasses[0].classLevel;
		myClasses[0].StoreTableAtClassLevel(nextLevel);
		//then we will need to draw information from the class table at its new level.
		SSLevel = myClasses[0].SSLevel;
		EvaluateCPD();
		if(Level != 1) 
		{
			IncreaseTotalAbPLEVELUP();
		}
		EvaluateHitpoints();
		//increase the characters HP, SS level, cards per day, and AbilityScore total.
		//if there are no remaining levels to invest hide the button associated with this.
	}

}
