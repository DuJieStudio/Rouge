using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public GameObject mainmenu;
    public bool isusingmenu;
    private void Awake()
    {
        mainmenu = GameObject.Find("UI_Menu");
        isusingmenu = false;
        mainmenu.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        switchbackground();
    }
    public void switchbackground()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isusingmenu = !isusingmenu;
            mainmenu.SetActive(isusingmenu);

        }
        if (isusingmenu)
        {
            Time.timeScale = 0;

        }
        else
        {
            Time.timeScale = 1;
        }
    }
    public void backgame()
    {
        isusingmenu = !isusingmenu;
        mainmenu.SetActive(isusingmenu);

    }
    public  void resetgame()
    {
        SceneManager.LoadScene(1);
        
    }
    public  void quitgame()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        UnityEngine.Application.Quit();
    }
    public void backmenu()
    {
        SceneManager.LoadScene(0);
        
    }
}
