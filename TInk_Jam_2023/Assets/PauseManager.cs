using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
	[SerializeField]
	private GameObject pauseMenuCanvas;

	private bool inMenu = false;

	public void OnPauseMenu() {
		if (inMenu) {
			Time.timeScale = 1f;
			inMenu = false;
			pauseMenuCanvas.SetActive(false);
		} else {
			inMenu = true;
			Time.timeScale = 0f;
			pauseMenuCanvas.SetActive(true);
		}
	}

	public void QuitGame() {
		Application.Quit();
	}
}
