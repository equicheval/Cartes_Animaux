using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class SummonChosenAnimalSpell : SpellEffect
{
	public override void ActivateEffect(int specialAmount = 0, string specialName = "", ICharacter target = null)
	{
		int compteur;
		for (compteur = 0; compteur < specialAmount; compteur++) {
			
			CardAsset[] allCards = Resources.LoadAll<CardAsset> ("");

			CardAsset creature = null;
			foreach (CardAsset a in allCards)
				if (a.Nom == specialName) {	
					creature = a; 
					break; 
				}

			if (TurnManager.Instance.whoseTurn.table.CreaturesOnTable.Count == 7)
				return;

			CardLogic creatureCard = new CardLogic (creature);  

			// add a creature to player`s table:
			TurnManager.Instance.whoseTurn.PlayACreatureFromHand (creatureCard, tablePos: 0, causeBattlecry: false);
		}
	}
}