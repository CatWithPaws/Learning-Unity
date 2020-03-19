using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForwardChecker : MonoBehaviour
{
    [SerializeField]
    LayerMask whatIsWall;
    [SerializeField]
    MoveController mvController;
    private void OnTriggerEnter2D(Collider2D collision)
    {

        //if (collision.gameObject.layer == whatIsWall)
        if (collision.gameObject.tag == "Wall")
        {
            mvController.CanSlide = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Wall")
        {
            mvController.CanSlide = false;
        }
       
    }
}
