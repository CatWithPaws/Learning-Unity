using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformerMovementControllerBase : MonoBehaviour
{
    Rigidbody2D playerRB;

    protected void GetRigidBody(Animator anim)
    {
        playerRB = anim.gameObject.GetComponent<Rigidbody2D>();
    }
}
