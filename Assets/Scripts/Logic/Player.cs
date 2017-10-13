using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour, ICharacter
{
    public int PlayerID;
    public CharacterAsset charAsset;
    public PlayerArea PArea;
    public SpellEffect HeroPowerEffect;

    public Deck deck;
    public Hand hand;
    public Table table;

    public bool usedHeroPowerThisTurn = false;
    public bool usedWheelOfBiomes = false;

    public int ID
    {
        get{ return PlayerID; }
    }

    private int sunTerrains;
    private int oceanTerrains;

   private int ressourceSunThisTurn;
    public int RessourceSunThisTurn
    {
        get{ return ressourceSunThisTurn;}
        set
        {
            ressourceSunThisTurn = value;

            new UpdateManaCrystalsCommand(this, RessourceSunThisTurn, RessourceOceanThisTurn).AddToQueue();

			if (TurnManager.Instance.whoseTurn == this)
				HighlightPlayableCards();
        }
    }

    private int ressourceOceanThisTurn;
    public int RessourceOceanThisTurn
    {
        get
        { return ressourceOceanThisTurn;}
        set
        {
            ressourceOceanThisTurn = value;
            //PArea.ManaBar.AvailableCrystals = manaLeft;
            new UpdateManaCrystalsCommand(this, RessourceSunThisTurn, RessourceOceanThisTurn).AddToQueue();

            if (TurnManager.Instance.whoseTurn == this)
                HighlightPlayableCards();
        }
    }

    public Player otherPlayer
    {
        get
        {
            if (Players[0] == this)
                return Players[1];
            else
                return Players[0];
        }
    }

    private int health;
    public int Health
    {
        get { return health;}
        set
        {
            health = value;
            if (value <= 0)
                Die(); 
        }
    }

    public delegate void VoidWithNoArguments();
    //public event VoidWithNoArguments CreaturePlayedEvent;
    //public event VoidWithNoArguments SpellPlayedEvent;
    //public event VoidWithNoArguments StartTurnEvent;
    public event VoidWithNoArguments EndTurnEvent;

    public static Player[] Players;

    void Awake()
    {
        Players = GameObject.FindObjectsOfType<Player>();
        PlayerID = IDFactory.GetUniqueID();
    }

    public virtual void OnTurnStart()
    {
        Debug.Log("In ONTURNSTART for "+ gameObject.name);
        usedHeroPowerThisTurn = false;
        usedWheelOfBiomes = false;

        int compteur;
        for (compteur = 0; compteur < sunTerrains; compteur++)
        {
            RessourceSunThisTurn++;
        }

        int compteur2;
        for (compteur2 = 0; compteur2 < oceanTerrains; compteur2++)
        {
            RessourceOceanThisTurn++;
        }

        foreach (CreatureLogic cl in table.CreaturesOnTable)
            cl.OnTurnStart();
        PArea.HeroPower.WasUsedThisTurn = false;
        PArea.WheelOfBiomes.wasUsedThisTurn = false;
    }

    public void UseWheelOfBiomesAgain()
    {
        usedWheelOfBiomes = false;
        PArea.WheelOfBiomes.wasUsedThisTurn = false;
    }


    public void OnTurnEnd()
    {
        if(EndTurnEvent != null)

        GetComponent<TurnMaker>().StopAllCoroutines();
    }

    public void DrawACard(bool fast = true)
    {
        if (deck.cards.Count > 0)
        {
            if (hand.CardsInHand.Count < PArea.handVisual.slots.Children.Length)
            {
                // 1) logic: add card to hand
                CardLogic newCard = new CardLogic(deck.cards[0]);
                newCard.owner = this;
                hand.CardsInHand.Insert(0,newCard);
                // Debug.Log(hand.CardsInHand.Count);
                // 3) logic: remove the card from the deck
                deck.cards.RemoveAt(0);
                // 4) create a command
				new DrawACardCommand(hand.CardsInHand[0], this, fast: true, fromDeck: true).AddToQueue();  
            }
        }
        else
        {
            // there are no cards in the deck, take fatigue damage.
        }
       
    }

    public void DrawACoin()
    {
        if (hand.CardsInHand.Count < PArea.handVisual.slots.Children.Length)
        {
            // 1) logic: add card to hand
            CardLogic newCard = new CardLogic(TurnManager.Instance.CoinCard);
            newCard.owner = this;
            hand.CardsInHand.Add(newCard);
            // 2) send message to the visual Deck
			new DrawACardCommand(hand.CardsInHand[hand.CardsInHand.Count - 1], this, fast: false, fromDeck: false).AddToQueue();
        }
        // no removal from deck because the coin was not in the deck
    }

    public void PlayASpellFromHand(int SpellCardUniqueID, int TargetUniqueID)
    {
        // TODO: !!!
        // if TargetUnique ID < 0 , for example = -1, there is no target.
        if (TargetUniqueID < 0)
            PlayASpellFromHand(CardLogic.CardsCreatedThisGame[SpellCardUniqueID], null);
        else if (TargetUniqueID == ID)
        {
            PlayASpellFromHand(CardLogic.CardsCreatedThisGame[SpellCardUniqueID], this);
        }
        else if (TargetUniqueID == otherPlayer.ID)
        {
            PlayASpellFromHand(CardLogic.CardsCreatedThisGame[SpellCardUniqueID], this.otherPlayer);
        }
        else
        {
            // target is a creature
            PlayASpellFromHand(CardLogic.CardsCreatedThisGame[SpellCardUniqueID], CreatureLogic.CreaturesCreatedThisGame[TargetUniqueID]);
        }
          
    }

    public void PlayASpellFromHand(CardLogic playedCard, ICharacter target)
    {
        RessourceSunThisTurn -= playedCard.CurrentManaCostDesert;
        RessourceOceanThisTurn -= playedCard.CurrentManaCostOcean;
        // cause effect instantly:
        if (playedCard.effect != null)
			playedCard.effect.ActivateEffect(playedCard.ca.specialSpellAmount, playedCard.ca.specialSpellCreatureName, target);
        else
        {
            Debug.LogWarning("No effect found on card " + playedCard.ca.name);
        }
        // no matter what happens, move this card to PlayACardSpot
        new PlayASpellCardCommand(this, playedCard).AddToQueue();
        // remove this card from hand
        hand.CardsInHand.Remove(playedCard);
        // check if this is a creature or a spell
    }

    public void PlayACreatureFromHand(int UniqueID, int tablePos)
    {
        PlayACreatureFromHand(CardLogic.CardsCreatedThisGame[UniqueID], tablePos, true);
    }

	public void PlayACreatureFromHand(CardLogic playedCard, int tablePos, bool causeBattlecry)
    {
		if (causeBattlecry == true) {
			RessourceSunThisTurn -= playedCard.CurrentManaCostDesert;
			RessourceOceanThisTurn -= playedCard.CurrentManaCostOcean;
		}
        // create a new creature object and add it to Table
        CreatureLogic newCreature = new CreatureLogic(this, playedCard.ca);
        table.CreaturesOnTable.Insert(tablePos, newCreature);
        // no matter what happens, move this card to PlayACardSpot
		new PlayACreatureCommand(playedCard, this, tablePos, newCreature.UniqueCreatureID).AddToQueue();
        // Effet Excès de rage
		if (newCreature.effect != null && causeBattlecry == true)
			newCreature.effect.WhenACreatureIsPlayed ();


		// remove this card from hand
        hand.CardsInHand.Remove(playedCard);
        HighlightPlayableCards();
    }

    public void Die()
    {
        // game over
        // block both players from taking new moves 
        PArea.ControlsON = false;
        otherPlayer.PArea.ControlsON = false;
        TurnManager.Instance.StopTheTimer();
        new GameOverCommand(this).AddToQueue();
    }

    // METHODS TO SHOW GLOW HIGHLIGHTS
    public void HighlightPlayableCards(bool removeAllHighlights = false)
    {
        //Debug.Log("HighlightPlayable remove: "+ removeAllHighlights);
        foreach (CardLogic cl in hand.CardsInHand)
        {
            GameObject g = IDHolder.GetGameObjectWithID(cl.UniqueCardID);
            if (g!=null)
                g.GetComponent<OneCardManager>().CanBePlayedNow = (cl.CurrentManaCostDesert <= RessourceSunThisTurn) && (cl.CurrentManaCostOcean <= RessourceOceanThisTurn) && !removeAllHighlights;
        }

        foreach (CreatureLogic crl in table.CreaturesOnTable)
        {
            GameObject g = IDHolder.GetGameObjectWithID(crl.UniqueCreatureID);
            if(g!= null)
                g.GetComponent<OneCreatureManager>().CanAttackNow = (crl.AttacksLeftThisTurn > 0) && !removeAllHighlights;
        }
            
        // highlight hero power
        PArea.HeroPower.Highlighted = (!usedHeroPowerThisTurn) && (RessourceSunThisTurn > 1) && !removeAllHighlights;
    }

    // START GAME METHODS
    public void LoadCharacterInfoFromAsset()
    {
        Health = charAsset.MaxHealth;
        // change the visuals for portrait, hero power, etc...
        PArea.Portrait.charAsset = charAsset;
        PArea.Portrait.ApplyLookFromAsset();
        if (charAsset.HeroPowerName != null && charAsset.HeroPowerName != "")
        {
            HeroPowerEffect = System.Activator.CreateInstance(System.Type.GetType(charAsset.HeroPowerName)) as SpellEffect;
        }
        else
        {
            Debug.LogWarning("Check hero powr name for character " + charAsset.ClassName);
        }
    }

    public void TransmitInfoAboutPlayerToVisual()
    {
        PArea.Portrait.gameObject.AddComponent<IDHolder>().UniqueID = PlayerID;
        if (GetComponent<TurnMaker>() is AITurnMaker)
        {
            // turn off turn making for this character
            PArea.AllowedToControlThisPlayer = false;
        }
        else
        {
            // allow turn making for this character
            PArea.AllowedToControlThisPlayer = true;
        }
    }

    public void UseHeroPower()
    {
        RessourceOceanThisTurn -= 2;
        usedHeroPowerThisTurn = true;
        HeroPowerEffect.ActivateEffect();
    }

    public void AddRessourceSun()
    {
        RessourceSunThisTurn++;
        sunTerrains++;
        Debug.Log(RessourceSunThisTurn);
        usedWheelOfBiomes = true;
    }

    public void AddRessourceOcean()
    {
		RessourceOceanThisTurn++;
        oceanTerrains++;
		usedWheelOfBiomes = true;
    }
		
}
