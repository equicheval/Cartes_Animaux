using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DealDamageToHeroDS : CreatureEffect
{
	public DealDamageToHeroDS(Player owner, CreatureLogic creature, int specialAmount, string specialName): base(owner, creature, specialAmount, specialName){}

	public override void WhenACreatureDies()
	{
		new DealDamageCommand(owner.otherPlayer.PlayerID, specialAmount, owner.otherPlayer.Health - specialAmount).AddToQueue();
		owner.otherPlayer.Health -= specialAmount;
	}
}