using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoving : MonoBehaviour
{
    [SerializeField]
    Transform CameraPos;
    [SerializeField]
    Transform TargetPos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CameraPos.position = new Vector3(TargetPos.position.x, TargetPos.position.y,CameraPos.position.z);
    }
}
