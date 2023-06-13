using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;

public class DeathWall : MonoBehaviour
{
    private Rigidbody2D playeRigidBody;
    private Vector3 deadWallPosition;
    private float originalacc = 0.1f;
    [SerializeField] float acceleration = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        playeRigidBody = GetComponent<Rigidbody2D>();
        deadWallPosition = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void FixedUpdate()
    {
        if (GameManager.sharedInstanceGameManager.currentGameState == GameState.inGame)
        {
            ToMove();
            StartCoroutine(Accelerate());
        }
        else if (GameManager.sharedInstanceGameManager.currentGameState == GameState.gameOver)
        {
            Begin1();
        }

    }

    public void Begin1() 
    {
        if (GameManager.sharedInstanceGameManager.currentGameState == GameState.gameOver)
        {
            acceleration = originalacc;
            Invoke("RestartPosition", 0.2f);
        }
    }

    private void RestartPosition()
    {
        this.transform.position = deadWallPosition;
        //this.playeRigidBody.velocity = Vector2.zero;
    }

    void ToMove()
    {
            transform.position += new Vector3(acceleration,0,0);
    }

    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            GameManager.sharedInstanceGameManager.GameOver();
        }
    }
    IEnumerator Accelerate()
    {
        yield return new WaitForSeconds(4);
        acceleration = acceleration + 0.0001f;
    }
}
