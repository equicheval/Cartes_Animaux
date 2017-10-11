using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class GetRandomSpell : SpellEffect
{
	public override void ActivateEffect(int specialAmount = 0, string specialName = "", ICharacter target = null)
	{
		List<CardAsset> allSpellsAssets = new List<CardAsset>();
		CardAsset[] allCards = Resources.LoadAll<CardAsset>("");
		foreach(CardAsset a in allCards)
			if((a.VieMaximum==0)  && (a.Description != "+1 Ressource pendant ce tour."))
				allSpellsAssets.Add(a);

		int index = Random.Range(0, allSpellsAssets.Count);
		CardAsset randomSpellAsset = allSpellsAssets[index];  

		CardLogic newCard = new CardLogic(randomSpellAsset);

		newCard.owner = TurnManager.Instance.whoseTurn;
		TurnManager.Instance.whoseTurn.hand.CardsInHand.Insert(0, newCard);

		new DrawACardCommand(newCard, TurnManager.Instance.whoseTurn, fast: true, fromDeck: false).AddToQueue();
	}
}