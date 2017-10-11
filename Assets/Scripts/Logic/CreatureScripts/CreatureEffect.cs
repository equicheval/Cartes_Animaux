using UnityEngine;
using System.Collections;

public abstract class CreatureEffect 
{
    protected Player owner;
    protected CreatureLogic creature;
    protected int specialAmount;
	public string specialName;

	public CreatureEffect(Player owner, CreatureLogic creature, int specialAmount, string specialName)
    {
        this.creature = creature;
        this.owner = owner;
        this.specialAmount = specialAmount;
		this.specialName = specialName;
    }

	public virtual void RegisterEventEffect(){}

	public virtual void UnregisterEventEffect(){}

	public virtual void CauseEventEffect(){}

	// EXCES DE RAGE

	public virtual void WhenACreatureIsPlayed(){}

	// DERNIER SOUFFLE

	public virtual void WhenACreatureDies(){}

}
