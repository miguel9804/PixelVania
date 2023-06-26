using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pausa : MonoBehaviour
{
    // Start is called before the first frame update
    public static bool isPaused = false;
    public GameObject pauseMenu, store, store2;
    [SerializeField] private StoreController tienda;

    // Update is called once per frame
    void Update()
    {
        if (!store.activeInHierarchy || !store.activeInHierarchy)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (isPaused)
                {
                    Resume();
                }
                else
                {
                    Pause();
                }
            }
        }
        if (store.activeInHierarchy || !store.activeInHierarchy) { if (Input.GetKeyDown(KeyCode.Escape)){ tienda.CerrarTienda(); } }
        
    }
    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }
    void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void LoadMenu() { SceneManager.LoadScene("MainMenu"); }
    public void Restart() { SceneManager.LoadScene("Hielo"); Time.timeScale = 1f; }
    public void Quit() { Application.Quit(); }
}
