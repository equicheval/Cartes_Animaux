using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class SummonChosenAnimal : CreatureEffect
{ 
	public SummonChosenAnimal(Player owner, CreatureLogic creature, int specialAmount, string specialName): base(owner, creature, specialAmount, specialName){}
	// Excès de rage
	public override void WhenACreatureIsPlayed()
	{
		CardAsset[] allCards = Resources.LoadAll<CardAsset>("");

		CardAsset creature = null;
		foreach(CardAsset a in allCards)
			if(a.Nom == specialName)
			{	
				creature = a; 
				break; 
			}

		if (owner.table.CreaturesOnTable.Count == 7)
			return;

		CardLogic creatureCard = new CardLogic(creature);  

		// add a creature to player`s table:
		owner.PlayACreatureFromHand(creatureCard, tablePos: 0, causeBattlecry: false);

	}
}