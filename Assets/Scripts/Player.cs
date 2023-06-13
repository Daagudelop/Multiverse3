using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    private Vector3 playerStartPosition;
    public Vector3 LobbyPlayerStartPosition;

    float ejeHorizontal;
    float ejeVertical;

    const string STATE_IS_ALIVE = "isAlive";
    const string STATE_ON_FLOOR = "onFloor";
    const string STATE_IS_FALLING = "isFalling";
    const string STATE_IS_MOVING = "isMoving";

    public float jumpForce = 6f;
    public LayerMask groundMask;

    bool actionRun = false;
    public bool actionGoMap = false;
    public bool actionGoMapNeg = false;
    bool gunLoaded = true;
    bool actionJump = true;

    Vector3 moveDirection;
    Vector2 facingDirection;

    [SerializeField] float fireRate = 7;
    [SerializeField] float moveSpeed;
    [SerializeField] int health = 3;
    [SerializeField] Transform aim;
    [SerializeField] Camera mainCamera;
    [SerializeField] CinemachineVirtualCamera virtualMainCamera;
    [SerializeField] Transform BulletPrefab;

    private Rigidbody2D playeRigidBody;
    private Animator playeAnimator;
    private SpriteRenderer playeSpriteRenderer;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void Awake()
    {
        playeRigidBody = GetComponent<Rigidbody2D>();
        playeAnimator = GetComponent<Animator>();
        playeSpriteRenderer = GetComponent<SpriteRenderer>();
        playerStartPosition = this.transform.position;
        //LobbyPlayerStartPosition = this.transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        if (GameManager.sharedInstanceGameManager.currentGameState == GameState.inGame)
        {
        ActionRecolector();
        Debug.DrawRay(this.transform.position, Vector2.down * 1.27f, Color.red);
        Jump();
        }
        else if (GameManager.sharedInstanceGameManager.currentGameState == GameState.gameOver)
        {

            playeRigidBody.velocity = Vector2.zero;
        }

        playeAnimator.SetBool(STATE_ON_FLOOR, IsTouchingTheGround());
        playeAnimator.SetBool(STATE_IS_FALLING, IsFalling());
        playeAnimator.SetBool(STATE_IS_MOVING, IsMoving());

    }

    private void FixedUpdate()
    {

        if (GameManager.sharedInstanceGameManager.currentGameState == GameState.inGame)
        {
            ToMove(moveSpeed);
            Aim();
            ToShoot();
            GoToMap();
            //Jump();
        }
        else if (GameManager.sharedInstanceGameManager.currentGameState == GameState.gameOver)
        {
            playeRigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            playeRigidBody.velocity = Vector2.zero;
        }

    }

    public void StartGame()
    {
        playeAnimator.SetBool(STATE_IS_ALIVE, true);
        playeAnimator.SetBool(STATE_ON_FLOOR, true);
        playeAnimator.SetBool(STATE_IS_FALLING, false);
        playeAnimator.SetBool(STATE_IS_MOVING, false);
        if (GameManager.sharedInstanceGameManager.currentGameState == GameState.gameOver)
        {
            Invoke("RestartPosition", 0.2f);

        }
    }

    private void GoToMap()
    {
        if (actionGoMap)
        {
            this.transform.position = LobbyPlayerStartPosition;
            virtualMainCamera.m_Lens.OrthographicSize = 10;
            //actionGoMap = false;
        } 
        else if (actionGoMapNeg)
        {
            this.transform.position = playerStartPosition;
            virtualMainCamera.m_Lens.OrthographicSize = 2.5f;
            //actionGoMapNeg = false;
        }
    }

    public void goDown()
    {
        this.transform.position = LobbyPlayerStartPosition;
        virtualMainCamera.m_Lens.OrthographicSize = 10;
    }
    public void goUp()
    {
        this.transform.position = playerStartPosition;
        virtualMainCamera.m_Lens.OrthographicSize = 2.5f;
    }
    private void RestartPosition()
    {
        this.transform.position = playerStartPosition;
        //this.playeRigidBody.velocity = Vector2.zero;
        playeAnimator.SetBool(STATE_IS_ALIVE, true);
    }

    void ActionRecolector()
    {
        //--------------------------------
        //walking.
        ejeHorizontal = Input.GetAxis("Horizontal");
        //ejeVertical = Input.GetAxis("Vertical");
        actionJump = (Input.GetButtonDown("Jump"));
        moveDirection.x = ejeHorizontal;
        moveDirection.y = 0;
        //--------------------------------
        //Running
        actionRun = Input.GetButton("Fire3");
        //--------------------------------
        //Go to Map
        actionGoMap = Input.GetButton("Fire1");
        actionGoMapNeg = Input.GetButton("Fire2");
    }

    void ToMove(float direc)
    {
        if (actionRun)
        {
            transform.position += moveDirection * Time.deltaTime * moveSpeed * 2;
            transform.rotation = Quaternion.identity;
            //playeRigidBody.velocity = new Vector2(direc * 2, playeRigidBody.velocity.y);
        }
        else
        {
            transform.position += moveDirection * Time.deltaTime * moveSpeed;
            //playeRigidBody.velocity = new Vector2(direc, playeRigidBody.velocity.y);
        }
        LookingDirection();
    }
    void LookingDirection()
    {
        if (moveDirection.x < 0)
        {
            playeSpriteRenderer.flipX = true;
        }
        else if (moveDirection.x > 0)
        {
            playeSpriteRenderer.flipX = false;
        }
        /*if (playeRigidBody.velocity.x < 0)
        {
            playeSpriteRenderer.flipX = true;
        }
        else if (playeRigidBody.velocity.x > 0)
        {
            playeSpriteRenderer.flipX = false;
        }*/
    }
    void Jump()
    {
        if (IsTouchingTheGround() && actionJump)
        {
            playeRigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    bool IsTouchingTheGround()
    {
        if (Physics2D.Raycast(this.transform.position, Vector2.down, 1.30f, groundMask))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    bool IsFalling()
    {
        if (playeRigidBody.velocity.y < 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    bool IsMoving()
    {
        if (moveDirection.x != 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void ToShoot()
    {
        //  Si click izq 
        if (Input.GetMouseButton(0) && gunLoaded)
        {
            gunLoaded = false;
            float angle = Mathf.Atan2(facingDirection.y, facingDirection.x) * Mathf.Rad2Deg;
            Quaternion BulletDirection = Quaternion.AngleAxis(angle, Vector3.forward);
            Instantiate(BulletPrefab, transform.position, BulletDirection);
            StartCoroutine(ReloadGun());
        }
    }

    void Aim()
    {
        
        facingDirection = mainCamera.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        //En este algoritmo se coje el vector2 y se le dice a unity que lo interprete como un vector3 y se normaliza (magnitud = 1)
        aim.position = transform.position + (Vector3)facingDirection.normalized;
    }

    public void IsDead()
    {
        //TODO: Hacer lógica de cuando esta vivo.
        //  1.2(Dead zone) Si se muere cambiara al estado gameOver.
        GameManager.sharedInstanceGameManager.GameOver();
        playeAnimator.SetBool(STATE_IS_ALIVE, false);
    }

    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Exit")
        {
            RestartPosition();
        }
    }*/

    //********************************
    //Corutinas
    IEnumerator ReloadGun()
    {
        yield return new WaitForSeconds(1 / fireRate);
        gunLoaded = true;
    }
}
