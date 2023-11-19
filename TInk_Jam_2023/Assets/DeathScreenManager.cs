using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreenManager : MonoBehaviour
{
	public void RestartGame() {
		this.gameObject.SetActive(false);
		Time.timeScale = 1f;
		SceneManager.LoadScene("TestLevel");
	}

	public void QuitGame() {
		Application.Quit();
	}
}
