using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    inGame,
    menu,
    gameOver,

}
public class GameManager : MonoBehaviour
{

    public GameState currentGameState = GameState.menu;

    public static GameManager sharedInstanceGameManager;
    private Player playerController;
    private DeathWall dW;
    public int collectedObject = 0;
    //public GameObject PausedMenu;
    // Start is called before the first frame update

    private void Awake()
    {
        if (sharedInstanceGameManager == null)
        {
            sharedInstanceGameManager = this;
        }
        //-----------------------
        playerController = GameObject.Find("Player").GetComponent<Player>();

        
        //dW = GameObject.Find("DeathWall").GetComponent<DeathWall>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Submit"))
        {
            StartGame();
        }
        else if (Input.GetButtonDown("Cancel"))
        {
            BackToMenu();
        }
    }

    public void StartGame()
    {
        SetGameState(GameState.inGame);

    }

    public void GameOver()
    {
        SetGameState(GameState.gameOver);
    }

    public void BackToMenu()
    {
        SetGameState(GameState.menu);
    }

    private void SetGameState(GameState newGameState)
    {
        if (newGameState == GameState.inGame)
        {
            //TODO
            LevelManager.sharedInstanceLevelManager.RemoveAllLevelBlocks();
            ReloadGame();
            Time.timeScale = 1f;
            playerController.StartGame();
            MenuManager.sharedInstance.HideMainMenu();
            MenuManager.sharedInstance.HidePausedGameMenu();
            MenuManager.sharedInstance.showInGameMenu();
            MenuManager.sharedInstance.HideGameOverMenu();
            //dW.Begin1();
        }
        else if (newGameState == GameState.menu)
        {
            //TODO
            Time.timeScale = 0f;
            MenuManager.sharedInstance.showPausedGameMenu();
            MenuManager.sharedInstance.HideInGameMenu();
        }
        else if (newGameState == GameState.gameOver)
        {
            //TODO
            MenuManager.sharedInstance.showGameOverMenu();
            MenuManager.sharedInstance.HideInGameMenu();

        }
        this.currentGameState = newGameState;
    }
    private void ReloadGame()
    {
        LevelManager.sharedInstanceLevelManager.GenerateInitialBlocks();
    }

    public void PointosCollected(Pointo pointos) 
    {
        collectedObject += pointos.value;
    }
}
