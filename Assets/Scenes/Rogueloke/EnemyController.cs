using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public bool isTriggered = false;

    float bulletSpeed = 5;
    bool isShooting = false;
    [SerializeField]
    GameObject EnemyBulletPref;
    [SerializeField]
    Transform Target;
    [SerializeField]
    Transform ShootPos;
    
    private float HP = 6;
    Vector3 direction;

    Rigidbody2D enemy_rb;
    private void Start()
    {
        enemy_rb = GetComponent<Rigidbody2D>();
    }

    float currRotation = 0;
    float willRotation;
    Quaternion rott;
    float moveTimer = 2;
    float maxMoveTimer = 2;
    private void FixedUpdate()
    {
        moveTimer -= Time.fixedDeltaTime;
        if(moveTimer <= 0)
        {
            ChangeDir();
            moveTimer = maxMoveTimer;
        }
        if (isTriggered)
        {
            Vector3 bulletDir = transform.position - Target.transform.position;
            willRotation = Mathf.Atan2(bulletDir.y, bulletDir.x) * 180 / Mathf.PI;
            rott = Quaternion.Slerp(Quaternion.Euler(0, 0, currRotation), Quaternion.Euler(0, 0, willRotation), 1);
            transform.localRotation = rott;
            enemy_rb.MovePosition(transform.position + direction * 3 * Time.fixedDeltaTime);

        }
        else
        {
            isShooting = false;
        }
        if (isTriggered && !isShooting)
        {
            isShooting = true;
            StartCoroutine(Shoot());
        }
        print(Random.Range(0f,1f));
    }
    void ChangeDir()
    {
        float rr = Random.Range(0f, 1f);
        print(rr);
        if (rr >= 0.5f)
        {
            float xAxis = Random.Range(-1f, 1f);

            direction = new Vector3(xAxis,0);
        }
        else
        {
            float yAxis = Random.Range(-1f, 1f);
            direction = new Vector3(0, yAxis);
        }
    }
    public void TakeDamage(float value)
    {
        HP -= value;
    }

    IEnumerator Shoot()
    {
        do
        {
            GameObject bullet = Instantiate(EnemyBulletPref);
            bullet.transform.position = ShootPos.position;
            bullet.transform.localRotation = transform.localRotation * Quaternion.Euler(new Vector3(0, 0, GlobalVars.rnd.Next(-7, 7)));
            yield return new WaitForSeconds(1);
        }
        while (isShooting);
    }
}

