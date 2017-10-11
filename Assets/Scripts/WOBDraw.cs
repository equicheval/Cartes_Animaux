using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WOBDraw : MonoBehaviour
{
	public AreaPosition owner;

	public GameObject Front;
	public GameObject Back;
	public GameObject TourAdverse;

	public Player player;

	public Collider buttonRessourceSun;
	public Collider buttonDraw;
	public Collider buttonRessourceOcean;

	private bool wasUsed = false;
	public bool wasUsedThisTurn
	{
		get
		{
			return wasUsed;
		}

		set
		{
			wasUsed = value;
			if (!wasUsed)
			{
				Front.SetActive(true);
				Back.SetActive(false);
				buttonRessourceSun.enabled = true;
				buttonRessourceOcean.enabled = true;
				buttonDraw.enabled = true;
			}
			else
			{
				Front.SetActive(false);
				Back.SetActive(true);
				buttonRessourceSun.enabled = false;
				buttonRessourceOcean.enabled = false;
				buttonDraw.enabled = false;
			}
		}
	}

	void LateUpdate()
	{
		CheckWhoseTurn();
	}

	public void CheckWhoseTurn()
	{
		if (TurnManager.Instance.whoseTurn == player)
		{
			Front.SetActive(true);
			TourAdverse.SetActive(false);
			buttonRessourceSun.enabled = true;
            buttonRessourceOcean.enabled = true;
			buttonDraw.enabled = true;
		}
		else if (TurnManager.Instance.whoseTurn != player)
		{
			TourAdverse.SetActive(true);
			Front.SetActive(false);
			Back.SetActive(false);
			buttonRessourceSun.enabled = false;
            buttonRessourceOcean.enabled = false;
			buttonDraw.enabled = true;
		}
	}

	void OnMouseDown()
	{
		if (!wasUsedThisTurn)
		{
			TurnManager.Instance.whoseTurn.DrawACard(fast: false);
			wasUsedThisTurn = !wasUsedThisTurn;
			CheckWhoseTurn();
		}
	}
}
