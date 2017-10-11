using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DrawCards : SpellEffect
{
	public override void ActivateEffect(int specialAmount = 0, string specialName = "", ICharacter target = null)
	{
		int compteur;
		for (compteur = 0; compteur < specialAmount; compteur++) {
			TurnManager.Instance.whoseTurn.DrawACard();
		}
	}
}