using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum TargetingOptions
{
    NoTarget,
    AllCreatures, 
    EnemyCreatures,
    YourCreatures, 
    AllCharacters, 
    EnemyCharacters,
    YourCharacters
}

public enum Rarete
{
	Classique,
	Boss, 
	SuperBoss,
	MegaBoss, 
	Legendaire
}

public enum Set
{
	Desert,
	Ocean
}

public class CardAsset : ScriptableObject 
{
    // this object will hold the info about the most general card
    [Header("Infos générales")]
    public CharacterAsset characterAsset;  // if this is null, it`s a neutral card
    [TextArea(2,3)]
	public string Nom;
	[TextArea(2,3)]
    public string Description;  // Description for spell or character
	public Set Set;
	public bool DernierSouffle;
	public bool Tension;
	public Sprite ImageCarte;
    public Rarete Rarete;

    [Header("Coûts en ressources")]
    public bool couteRessourcesDesert;
    public int CoutEnRessourceDesert;

	public bool couteRessourcesOcean;
	public int CoutEnRessourceOcean;
	

    [Header("Informations de la créature")]
	public int Attaque;
    public int VieMaximum; // =0 => carte aléa
	[TextArea(2,3)]
	public string TypeText;
	public Sprite TypeImage;
    public int NombreAttaqueParTour = 1; //Furie des vents
    public bool Taunt;
	public bool Meute;
    public bool Charge;
    public string CreatureScriptName;
    public int specialCreatureAmount;
	public string specialCreatureName;
	[TextArea(2,3)]
	public string PireEnnemi;
	[TextArea(2,3)]
	public string MeilleurAllie;

    [Header("Informations des sorts")]
	public string SpellScriptName;
    public int specialSpellAmount;
	public string specialSpellCreatureName;
    public TargetingOptions Cible;

}
