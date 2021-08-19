using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneTransform : MonoBehaviour//, IPooledObject
{
    public float speed = 10.0f;

    public void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    
}
