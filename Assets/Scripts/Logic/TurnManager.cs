﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;

// this class will take care of switching turns and counting down time until the turn expires
public class TurnManager : MonoBehaviour {

	public CardAsset CoinCard;

	private RopeTimer timer;

    // for Singleton Pattern
    public static TurnManager Instance;

    private Player _whoseTurn;
    public Player whoseTurn
    {
        get
        {
            return _whoseTurn;
        }

        set
        {
            _whoseTurn = value;
            timer.StartTimer();

            GlobalSettings.Instance.EnableEndTurnButtonOnStart(_whoseTurn);

            TurnMaker tm = whoseTurn.GetComponent<TurnMaker>();
            // player`s method OnTurnStart() will be called in tm.OnTurnStart();
            tm.OnTurnStart();
            if (tm is PlayerTurnMaker)
            {
                whoseTurn.HighlightPlayableCards();
            }
            // remove highlights for opponent.
            whoseTurn.otherPlayer.HighlightPlayableCards(true);
                
        }
    }

    void Awake()
    {
        Instance = this;
        timer = GetComponent<RopeTimer>();
    }

    void Start()
    {
        OnGameStart();
    }

    public void OnGameStart()
    {
        //Debug.Log("In TurnManager.OnGameStart()");

        CardLogic.CardsCreatedThisGame.Clear();
        CreatureLogic.CreaturesCreatedThisGame.Clear();

        foreach (Player p in Player.Players)
        {
            p.RessourceSunThisTurn = 0;
            p.RessourceOceanThisTurn = 0;
            p.LoadCharacterInfoFromAsset();
            p.TransmitInfoAboutPlayerToVisual();
            p.PArea.PDeck.CardsInDeck = p.deck.cards.Count;
            // move both portraits to the center
			p.PArea.Portrait.transform.position = p.PArea.InitialPortraitPosition.position;
            //move both WheelOfBiomes to Z coords
            p.PArea.WheelAnimation.transform.position = p.PArea.InitialWOBPosition.position;
        }

        Sequence s = DOTween.Sequence();
        s.Append(Player.Players[0].PArea.Portrait.transform.DOMove(Player.Players[0].PArea.PortraitPosition.position, 1f).SetEase(Ease.InQuad));
        s.Insert(0f, Player.Players[1].PArea.Portrait.transform.DOMove(Player.Players[1].PArea.PortraitPosition.position, 1f).SetEase(Ease.InQuad));
        s.PrependInterval(3f);
        s.OnComplete(() =>
            {
                // determine who starts the game.
                int rnd = Random.Range(0,2);  // 2 is exclusive boundary
                // Debug.Log(Player.Players.Length);
                Player whoGoesFirst = Player.Players[rnd];
                // Debug.Log(whoGoesFirst);
                Player whoGoesSecond = whoGoesFirst.otherPlayer;
                // Debug.Log(whoGoesSecond);
         
                // draw 4 cards for first player and 5 for second player
                int initDraw = 4;
                for (int i = 0; i < initDraw; i++)
                {            
                    // second player draws a card
                    whoGoesSecond.DrawACard(true);
                    // first player draws a card
                    whoGoesFirst.DrawACard(true);
                }
                // add one more card to second player`s hand
                whoGoesSecond.DrawACard(true);
                //new GivePlayerACoinCommand(null, whoGoesSecond).AddToQueue();
                whoGoesSecond.DrawACoin();
                new StartATurnCommand(whoGoesFirst).AddToQueue();

				Sequence t = DOTween.Sequence();
            s.Append(Player.Players[0].PArea.WheelAnimation.transform.DOMove(Player.Players[0].PArea.WOBPosition.position, 5f).SetEase(Ease.InQuad));
				s.Insert(0f, Player.Players[1].PArea.WheelAnimation.transform.DOMove(Player.Players[1].PArea.WOBPosition.position, 5f).SetEase(Ease.InQuad));
				s.PrependInterval(3f);
                
            });

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            EndTurn();
    }

    public void EndTurn()
    {
        // stop timer
        timer.StopTimer();
        // send all commands in the end of current player`s turn
        whoseTurn.OnTurnEnd();

        new StartATurnCommand(whoseTurn.otherPlayer).AddToQueue();
    }

	//FOR TEST ONLY
	public void EndTurnTest(){
		timer.StopTimer();
		timer.StartTimer();
	}

    public void StopTheTimer()
    {
        timer.StopTimer();
    }
		
}

