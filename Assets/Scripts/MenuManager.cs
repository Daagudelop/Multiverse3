using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public static MenuManager sharedInstance;
    public Canvas canvasInGame;
    public Canvas canvasPlayMenu;
    public Canvas canvasPausedMenu;
    public Canvas canvasGameOver;

    // Start is called before the first frame update
    private void Awake()
    {
        if (sharedInstance == null)
        {
            sharedInstance = this;
        }
        canvasPausedMenu.enabled = false;
        canvasGameOver.enabled = false;
        //canvasPlayMenu.enabled = false;
        canvasInGame.enabled = false;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void showPausedGameMenu()
    {
        canvasPausedMenu.enabled = true;
    }

    public void HidePausedGameMenu()
    {
        canvasPausedMenu.enabled = false;
    }
    public void showGameOverMenu()
    {
        canvasGameOver.enabled = true;
    }

    public void HideGameOverMenu()
    {
        canvasGameOver.enabled = false;
    }

    public void showInGameMenu()
    {
        canvasInGame.enabled = true;
    }

    public void HideInGameMenu()
    {
        canvasPlayMenu.enabled = false;
    }

    public void ShowMainMenu()
    {
        canvasPlayMenu.enabled = true;
    }

    public void HideMainMenu() 
    {
        canvasPlayMenu.enabled=false;
    }

    //public void CollecObject(Collec)


    public void ExitGame() 
    {
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #else
        Application.Quit();
    #endif

    }
}
