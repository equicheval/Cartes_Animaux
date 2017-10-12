using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GetRandomCreature : CreatureEffect
{ 
	public GetRandomCreature(Player owner, CreatureLogic creature, int specialAmount, string specialName): base(owner, creature, specialAmount, specialName){}
	// Excès de rage
	public override void WhenACreatureIsPlayed()
	{
		List<CardAsset> allCreatureAssets = new List<CardAsset>();
		CardAsset[] allCards = Resources.LoadAll<CardAsset>("");
		// only select creatures
		foreach(CardAsset a in allCards)
			if((a.VieMaximum>0))
				allCreatureAssets.Add(a);

		if (TurnManager.Instance.whoseTurn.table.CreaturesOnTable.Count == 7)
			return;

		int compteur;
		for (compteur = 0; compteur < specialAmount; compteur++) {

			if (TurnManager.Instance.whoseTurn.hand.CardsInHand.Count >= 11)
				return;
			
			// Select a random CardAsset:
			int index = Random.Range (0, allCreatureAssets.Count);
			CardAsset randomCreatureAsset = allCreatureAssets [index];        

			// make a CardLogic to call PlayACreatureFromHand():
			CardLogic creatureCard = new CardLogic (randomCreatureAsset); 


			creatureCard.owner = TurnManager.Instance.whoseTurn;
			TurnManager.Instance.whoseTurn.hand.CardsInHand.Insert (0, creatureCard);

			new DrawACardCommand (creatureCard, TurnManager.Instance.whoseTurn, fast: true, fromDeck: false).AddToQueue ();
		}

	}
}