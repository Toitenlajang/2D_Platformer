using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public GameObject menuCanvas;
    public static bool IsMenuOpen { get; private set; }
    private void Start()
    {
        menuCanvas.SetActive(false);
        Time.timeScale = 1f;
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
}
