using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WheelOfBiomes : MonoBehaviour
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
            DisableColliders();
		}
	}

    // TODO résoudre le bug de non-disparition des colliders après le double click qui relance la roue

    // Update is called once per frame
    void Update () {
        if (Input.GetMouseButtonDown(0)) {
            CheckWhichColliderIsClicked();
        }       
    }

    void CheckWhichColliderIsClicked() {
        RaycastHit hit; 
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast (ray, out hit, 100.0f))
        {
            if (hit.collider == buttonRessourceOcean){
                GlobalSettings.Instance.Players[owner].AddRessourceOcean();
                wasUsedThisTurn = !wasUsedThisTurn;
                CheckWhoseTurn();
                DisableColliders();
            } else if(hit.collider == buttonRessourceSun) {
                GlobalSettings.Instance.Players[owner].AddRessourceSun();
                wasUsedThisTurn = !wasUsedThisTurn;
                CheckWhoseTurn();
                DisableColliders();
            } else if(hit.collider == buttonDraw) {
                TurnManager.Instance.whoseTurn.DrawACard(fast: false);
                wasUsedThisTurn = !wasUsedThisTurn;
                CheckWhoseTurn();
                DisableColliders();
            }
        }
    }

    void DisableColliders()
    {
        buttonRessourceSun.enabled = false;
        buttonRessourceOcean.enabled = false;
        buttonDraw.enabled = false;
    }
}
