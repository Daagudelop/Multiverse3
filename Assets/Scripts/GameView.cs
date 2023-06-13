using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameView : MonoBehaviour
{

    public TextMeshProUGUI Pointos;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        TocountPoints();
    }

    private void TocountPoints()
    {
        if (GameManager.sharedInstanceGameManager.currentGameState == GameState.inGame)
        {
            int pointos = GameManager.sharedInstanceGameManager.collectedObject;

            Pointos.text = "Pointos: " + pointos.ToString();

        }
    }
}
