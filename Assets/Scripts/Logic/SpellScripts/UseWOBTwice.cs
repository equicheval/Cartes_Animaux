using UnityEngine;
using System.Collections;

public class UseWOBTwice : SpellEffect
{
	public override void ActivateEffect(int specialAmount = 0, string specialName = "", ICharacter target = null)
	{
		TurnManager.Instance.whoseTurn.UseWheelOfBiomesAgain();
	}
}
