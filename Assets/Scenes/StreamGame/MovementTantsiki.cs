using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Rigidbody2D))]
public class MovementTantsiki : MonoBehaviour
{

    [SerializeField]
    Rigidbody2D rb_player;
    Vector3 velocity;
    float speed = 7;
    [SerializeField]
    GameObject bulletPref;
    [SerializeField]
    Transform ShootPos;
    float MaxBullets = 10;
    float CurrBullets;
    float reloadTime = 1;
    float reloadTimer;
    [SerializeField]
    Image Ammo;
    bool CanReload;
    private void Start()
    {
        rb_player = GetComponent<Rigidbody2D>();
        rb_player.gravityScale = 0;
        CurrBullets = MaxBullets;
        reloadTimer = reloadTime;
        CanReload = false;
    }

    private void FixedUpdate()
    {
        var moveX = Input.GetAxisRaw("Horizontal");
        var moveY = Input.GetAxisRaw("Vertical");
        float rotationZ;
        if(moveX != 0)
        {
            velocity = Vector3.zero;
            velocity.x = moveX;
            if (moveX > 0) rotationZ = -90;
            else rotationZ = 90;
            transform.localRotation = Quaternion.Euler(new Vector3(0,0,rotationZ));
        }
        else if(moveY != 0)
        {
            velocity = Vector3.zero;
            velocity.y = moveY;
            if (moveY > 0) rotationZ = 0;
            else rotationZ = -180;
            transform.localRotation = Quaternion.Euler(new Vector3(0, 0,rotationZ));
        }
        else
        {
            velocity = Vector3.zero;
        }
        rb_player.MovePosition(transform.position + velocity * speed*Time.fixedDeltaTime);
        if (Input.GetKeyDown(KeyCode.Space) && CurrBullets > 0)
        {
            Shoot();
            CurrBullets--;
            reloadTimer = reloadTime;
        }

        CanReload = CurrBullets < 10;
        if (reloadTimer <= 0 && CurrBullets < 10)
        {
            CurrBullets++;
            reloadTimer = reloadTime;

        }
        else if(reloadTime > 0 && CanReload)
            reloadTimer -= Time.fixedDeltaTime;Ammo.fillAmount = CurrBullets / MaxBullets;

        
    }
    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPref);
        bullet.transform.position = ShootPos.position;
        bullet.transform.localRotation = transform.localRotation;
    }
}
