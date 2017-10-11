using UnityEngine;
using System.Collections;

public class BiteOwner : CreatureEffect
{
	public BiteOwner(Player owner, CreatureLogic creature, int specialAmount, string specialName): base(owner, creature, specialAmount, specialName) {}

	public override void RegisterEventEffect()
	{
		owner.EndTurnEvent += CauseEventEffect;
		//owner.otherPlayer.EndTurnEvent += CauseEventEffect;
		Debug.Log ("Registered bite effect !!!!");

	}

	public override void UnregisterEventEffect()
	{
		owner.EndTurnEvent -= CauseEventEffect;
	}

	public override void CauseEventEffect()
	{
		Debug.Log ("InCauseEffect: owner: " + owner + " specialAmount: " + specialAmount);
		new DealDamageCommand (owner.PlayerID, specialAmount, owner.Health - specialAmount).AddToQueue();
		owner.Health -= specialAmount;
	}
}