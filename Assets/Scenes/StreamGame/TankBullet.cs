using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class TankBullet : MonoBehaviour
{
    Rigidbody2D rb_bullet;
    [SerializeField]
    float speed;
    private void Start()
    {
        rb_bullet = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        rb_bullet.MovePosition(transform.position + transform.up * speed * Time.fixedDeltaTime);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Wall")
        {
            Collider2D[] collisions = Physics2D.OverlapCircleAll(transform.position, 0.5f);
            foreach(Collider2D col in collisions)
            {
                if (col.gameObject.tag != "Unbreaking")
                {
                    Destroy(col.gameObject);
                }
            }
            Destroy(gameObject);
        }
        if(collision.gameObject.tag == "Unbreaking")
        {
            Destroy(gameObject);
        }
    }
}
