using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UISceneManagement : MonoBehaviour
{
    public void LoadScene() { SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); Time.timeScale = 1f; }
    public void QuitGame() { Application.Quit(); }
    public void Menu() { Time.timeScale = 1; SceneManager.LoadScene("MainMenu"); }
}
