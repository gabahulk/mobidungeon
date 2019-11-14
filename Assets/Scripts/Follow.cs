using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public Transform target;
    public Vector2 offset;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPos = this.transform.position;
        newPos.x = target.position.x + offset.x;
        newPos.y = target.position.y + offset.y;
        this.transform.position = newPos;
    }
}
