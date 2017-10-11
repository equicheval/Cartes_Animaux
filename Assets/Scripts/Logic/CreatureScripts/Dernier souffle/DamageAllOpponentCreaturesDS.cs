using UnityEngine;
using System.Collections;

public class DamageAllOpponentCreaturesDS : CreatureEffect
{
	public DamageAllOpponentCreaturesDS(Player owner, CreatureLogic creature, int specialAmount, string specialName): base(owner, creature, specialAmount, specialName){}

	// Dernier souffle
	public override void WhenACreatureDies()
	{
		CreatureLogic[] CreaturesToDamage = owner.otherPlayer.table.CreaturesOnTable.ToArray();
		foreach (CreatureLogic cl in CreaturesToDamage)
		{
			new DealDamageCommand(cl.ID, specialAmount, healthAfter: cl.Health - specialAmount).AddToQueue();
			cl.Health -= specialAmount;
		}
	}
}

