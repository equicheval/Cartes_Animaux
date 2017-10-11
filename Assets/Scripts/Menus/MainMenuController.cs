using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour {

	public void PlayGame() {
		System.Threading.Thread.Sleep(300);
		Application.LoadLevel("BattleScene");
	}

	public void QuitGame() {
		Application.Quit();
	}

}
