using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingRoguelike : MonoBehaviour
{
    [SerializeField]
    Rigidbody2D rb_player;
    [SerializeField]
    float speed = 2;
    [SerializeField]
    GameObject BulletPref;
    [SerializeField]
    Transform ShootPos;
    float HP = 10;
    float reloadTime = 0.3f;
    [SerializeField]
    float baseReloadTime;
    private void Awake()
    {
         reloadTime = baseReloadTime;
    }
    private void FixedUpdate()
    {
        reloadTime = Mathf.Clamp(reloadTime-Time.fixedDeltaTime, 0, baseReloadTime);
        float AxisX = Input.GetAxisRaw("Horizontal");
        float AxisY = Input.GetAxisRaw("Vertical");

        rb_player.velocity = (new Vector3(AxisX,AxisY).normalized * speed);

        Vector3 RawMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        float MousePosX = RawMousePos.x - transform.position.x;
        float MousePosY = RawMousePos.y - transform.position.y;

        Vector3 MousePos = new Vector3(MousePosX, MousePosY);
        rb_player.gameObject.transform.localRotation = Quaternion.Euler(0, 0, Mathf.Atan2(MousePos.y,MousePos.x) * 180f/Mathf.PI);
        if (Input.GetMouseButton(0) && reloadTime <= 0)
        {
            reloadTime = baseReloadTime;
            Shoot();
            
        }
    }
    public void TakeDamage(float value)
    {
        if(HP > 0)
        {
            HP -= value;
        }
    }
    public void Shoot()
    {
        GameObject Bullet = Instantiate(BulletPref);
        Bullet.transform.position = ShootPos.position;
        Bullet.transform.localRotation = transform.localRotation * Quaternion.Euler(new Vector3(0, 0, GlobalVars.rnd.Next(-7, 7)));
    }
}
