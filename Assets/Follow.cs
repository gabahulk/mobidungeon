using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public Transform target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPos = this.transform.position;
        newPos.x = target.position.x;
        newPos.y = target.position.y;
        this.transform.position = newPos;
    }
}
