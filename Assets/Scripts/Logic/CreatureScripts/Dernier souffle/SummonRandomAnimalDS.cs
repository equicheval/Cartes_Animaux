using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SummonRandomAnimalDS : CreatureEffect
{
	public SummonRandomAnimalDS(Player owner, CreatureLogic creature, int specialAmount, string specialName): base(owner, creature, specialAmount, specialName){}
// Excès de rage
public override void WhenACreatureDies()
{
	List<CardAsset> allCreatureAssets = new List<CardAsset>();
	CardAsset[] allCards = Resources.LoadAll<CardAsset>("");
	// only select creatures
	foreach(CardAsset a in allCards)
		if(a.VieMaximum>0)
			allCreatureAssets.Add(a);

	if (TurnManager.Instance.whoseTurn.table.CreaturesOnTable.Count == 7)
		return;
	// Select a random CardAsset:
	int index = Random.Range(0, allCreatureAssets.Count);
	CardAsset randomCreatureAsset = allCreatureAssets[index];        

	// make a CardLogic to call PlayACreatureFromHand():
	CardLogic creatureCard = new CardLogic(randomCreatureAsset);  

	owner.PlayACreatureFromHand(creatureCard, tablePos: 0, causeBattlecry: false);
	// or you can change it to: tablePos: 6 to place him on the other side.
}
}