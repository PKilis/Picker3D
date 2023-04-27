using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DonerKol : MonoBehaviour
{
    bool don;
    [SerializeField] private int donecekYon;
    public void DonmeyeBasla()
    {
        don = true;
    }


    void Update()
    {
        if (don)
         transform.Rotate(0, 0, donecekYon, Space.Self);
               
    }
}
