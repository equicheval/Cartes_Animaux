using UnityEngine;
using System.Collections;

public class DealDamageToTarget : SpellEffect 
{
    

	public override void ActivateEffect(int specialAmount = 0, string specialName = "", ICharacter target = null)
    {
        new DealDamageCommand(target.ID, specialAmount, healthAfter: target.Health - specialAmount).AddToQueue();
        target.Health -= specialAmount;
    }
}
