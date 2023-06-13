using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using Unity.Mathematics;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5;
    [SerializeField] float timeTillDespawn = 2;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, timeTillDespawn);
    }

    // Update is called once per frame
    void Update()
    {
        ToMove();
    }

    void ToMove()
    {
        transform.position += transform.right * Time.deltaTime * moveSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ToDamageEnemy(collision);
    }


    void ToDamageEnemy(Collider2D collision)
    {
        if (collision.CompareTag("point"))
        {
            collision.GetComponent<Pointo>().ToTakeDamageEnemy();
            //En cuanto ocurra se destruira
            //Destroy(gameObject);
        }
    }

}
