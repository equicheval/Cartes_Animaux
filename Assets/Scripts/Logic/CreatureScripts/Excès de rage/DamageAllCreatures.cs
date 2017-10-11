using UnityEngine;
using System.Collections;

public class DamageAllCreatures : CreatureEffect
{
	public DamageAllCreatures(Player owner, CreatureLogic creature, int specialAmount, string specialName): base(owner, creature, specialAmount, specialName){}

	// Excès de rage
	public override void WhenACreatureIsPlayed()
	{
		if (TurnManager.Instance.whoseTurn.otherPlayer.table.CreaturesOnTable.Count == 0)
			return;

		CreatureLogic[] CreaturesToDamage = TurnManager.Instance.whoseTurn.otherPlayer.table.CreaturesOnTable.ToArray();
		foreach (CreatureLogic cl in CreaturesToDamage)
		{
			new DealDamageCommand(cl.ID, specialAmount, healthAfter: cl.Health - specialAmount).AddToQueue();
			cl.Health -= specialAmount;
		}
	}
}

