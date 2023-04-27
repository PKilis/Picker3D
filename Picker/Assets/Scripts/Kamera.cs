using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kamera : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 targetOffset;

    void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, target.position + targetOffset, 0.125f);        
    }
}
