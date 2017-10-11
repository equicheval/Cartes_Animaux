using UnityEngine;
using System.Collections;

public class DamageOpponentBattlecry : CreatureEffect
{
	public DamageOpponentBattlecry(Player owner, CreatureLogic creature, int specialAmount, string specialName): base(owner, creature, specialAmount, specialName){}

	// Excès de rage
	public override void WhenACreatureIsPlayed()
	{
		new DealDamageCommand (owner.otherPlayer.PlayerID, specialAmount, owner.otherPlayer.Health - specialAmount).AddToQueue ();
		owner.otherPlayer.Health -= specialAmount;
	}
}

