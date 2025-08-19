using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public static MenuController Instance { get; private set; }

    public GameObject gameOverUI;
    public GameObject menuCanvas;
    public static bool IsMenuOpen { get; private set; }

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        menuCanvas.SetActive(false);
        gameOverUI.SetActive(false);

        Time.timeScale = 1f;
        IsMenuOpen = false;

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            bool showMenu = !menuCanvas.activeSelf;
            menuCanvas.SetActive(showMenu);
            Time.timeScale = showMenu ? 0 : 1;

            IsMenuOpen = showMenu;
        }
    }
    public void GameOver()
    {
        gameOverUI.SetActive(true);
        Time.timeScale = 0f; // Pause the game
    }
}
