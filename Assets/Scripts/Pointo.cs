using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointo : MonoBehaviour
{
    private SpriteRenderer pointSpriteRenderer;

    [SerializeField] int health = 10;
    public int value = 1; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        pointSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ToTakeDamageEnemy()
    {
        health--;

            pointSpriteRenderer.material.color = Random.ColorHSV(0f, 2f, 1f, 1f, 0.5f, 1f);
        transform.localScale += new Vector3(0.2f, 0.2f, 0.2f); 
        if (health == 0)
        {
            GameManager.sharedInstanceGameManager.PointosCollected(this);
            Destroy(gameObject);
        }
        //transform.localScale -= new Vector3(1, 1, 1); 
    }
}
