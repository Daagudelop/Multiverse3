using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ExitZone : MonoBehaviour
{
    public int proximoLevelBlock;
    public bool entrada = false;
    private Player playerController;
    private Grafo mapa;

    private Vector3 LobbyPlayerStartPosition = new Vector3(9,-31,0);
    //*********************************
    // Unity methods

    private void Awake()
    {
        playerController = GameObject.Find("Player").GetComponent<Player>();
        mapa = GameObject.Find("Mapa").GetComponent<Grafo>();
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    // 1.6(Level Manager) Cuando se toque el collider se quitara el primer bloque y luego se añadira uno.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (entrada == false)
            {
                //Invoke("playerController.goDown", 0.2f);;
                playerController.goDown();
            }
            else
            {
                LevelManager.sharedInstanceLevelManager.RemoveLevelBlock();
                LevelManager.sharedInstanceLevelManager.AddLevelBlock();
                LevelManager.sharedInstanceLevelManager.numberOnGrafo = mapa.final;
                playerController.goUp();
            }
        }
    }
    //*********************************
}
