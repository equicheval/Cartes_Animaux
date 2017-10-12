using UnityEngine;
using System.Collections;

public class DamageRandomCreature : CreatureEffect
{
	public DamageRandomCreature(Player owner, CreatureLogic creature, int specialAmount, string specialName): base(owner, creature, specialAmount, specialName){}

	// Excès de rage
	public override void WhenACreatureIsPlayed()
	{
		if (TurnManager.Instance.whoseTurn.otherPlayer.table.CreaturesOnTable.Count == 0)
			return;
		
		CreatureLogic[] CreatureToDamage = TurnManager.Instance.whoseTurn.otherPlayer.table.CreaturesOnTable.ToArray();
		int index = Random.Range (0, CreatureToDamage.Length);

		new DealDamageCommand(CreatureToDamage[index].ID, specialAmount, healthAfter: CreatureToDamage[index].Health - specialAmount).AddToQueue();
		CreatureToDamage[index].Health -= specialAmount;
	}
}

