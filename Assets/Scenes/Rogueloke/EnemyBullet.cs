using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{

    [SerializeField]
    float Speed;
    Rigidbody2D rb_bullet;
    void Start()
    {
        rb_bullet = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb_bullet.MovePosition(transform.position- transform.right * Speed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "Player")
        {
            GlobalVars.instance.Player.GetComponent<MovingRoguelike>().TakeDamage(1);
            Destroy(gameObject);
        }
    }
}
