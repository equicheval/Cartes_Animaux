using UnityEngine;
using System.Collections;

public class DealDamageRandomEnemy : CreatureEffect
{
	public DealDamageRandomEnemy(Player owner, CreatureLogic creature, int specialAmount, string specialName): base(owner, creature, specialAmount, specialName) {}

	public override void RegisterEventEffect()
	{
		owner.EndTurnEvent += CauseEventEffect;
		//owner.otherPlayer.EndTurnEvent += CauseEventEffect;
		Debug.Log ("Registered damage effect !!!!");

	}

	public override void UnregisterEventEffect()
	{
		owner.EndTurnEvent -= CauseEventEffect;
	}

	public override void CauseEventEffect()
	{
		if (TurnManager.Instance.whoseTurn.otherPlayer.table.CreaturesOnTable.Count == 0)
			return;

		CreatureLogic[] CreatureToDamage = owner.otherPlayer.table.CreaturesOnTable.ToArray();
		int index = Random.Range (0, CreatureToDamage.Length);

		new DealDamageCommand(CreatureToDamage[index].ID, specialAmount, healthAfter: CreatureToDamage[index].Health - specialAmount).AddToQueue();
		CreatureToDamage[index].Health -= specialAmount;
	}
}