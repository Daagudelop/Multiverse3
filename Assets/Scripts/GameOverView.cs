using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverView : MonoBehaviour
{
    public TextMeshProUGUI Pointos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        TocountPointsFinal();
    }

    private void TocountPointsFinal()
    {
        if (GameManager.sharedInstanceGameManager.currentGameState == GameState.inGame)
        {
            int pointos = GameManager.sharedInstanceGameManager.collectedObject;

            Pointos.text = "-Score: " + pointos.ToString()+" Pointos.";

        }
    }
}
