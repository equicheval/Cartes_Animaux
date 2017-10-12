using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GetRandomSpellER : CreatureEffect
{
	public GetRandomSpellER(Player owner, CreatureLogic creature, int specialAmount, string specialName): base(owner, creature, specialAmount, specialName){}

	public override void WhenACreatureIsPlayed()
	{
		List<CardAsset> allSpellsAssets = new List<CardAsset>();
		CardAsset[] allCards = Resources.LoadAll<CardAsset>("");
		foreach(CardAsset a in allCards)
			if((a.VieMaximum==0)  && (a.Description != "+1 Ressource pendant ce tour."))
				allSpellsAssets.Add(a);

		int index = Random.Range(0, allSpellsAssets.Count);
		CardAsset randomSpellAsset = allSpellsAssets[index];  

		CardLogic newCard = new CardLogic(randomSpellAsset);

		newCard.owner = owner;
		owner.hand.CardsInHand.Insert(0, newCard);

		new DrawACardCommand(newCard, owner, fast: true, fromDeck: false).AddToQueue();
	}
}