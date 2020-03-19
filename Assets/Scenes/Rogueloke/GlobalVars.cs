using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalVars : MonoBehaviour
{
    public static GlobalVars instance;
    public GameObject Player;
    public static System.Random rnd = new System.Random();
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance == this)
        {
            Destroy(gameObject);

            DontDestroyOnLoad(gameObject);
        }
    }
}
