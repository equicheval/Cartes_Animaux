/* using UnityEngine;
using System.Collections;

public class BuffRandomAnimal : CreatureEffect
{
	public BuffRandomAnimal(Player owner, CreatureLogic creature, int specialAmount, string specialName): base(owner, creature, specialAmount, specialName){}
	TODO
	// Excès de rage
	public override void WhenACreatureIsPlayed()
	{
		if (TurnManager.Instance.whoseTurn.table.CreaturesOnTable.Count == 0)
			return;

		CreatureLogic[] CreatureToBuff = TurnManager.Instance.whoseTurn.table.CreaturesOnTable.ToArray();
		int index = Random.Range (0, CreatureToBuff.Length);

		new DealDamageCommand(CreatureToBuff[index].ID, specialAmount, healthAfter: CreatureToBuff[index].Health + specialAmount).AddToQueue();

		CreatureToBuff[index].Health += specialAmount; 
	}
}*/

