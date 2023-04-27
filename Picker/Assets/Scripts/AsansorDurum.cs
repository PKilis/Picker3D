using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsansorDurum : MonoBehaviour
{
    [SerializeField] private GameManager _GameManager;
    [SerializeField] private Animator bariyerAlani;

    public void BariyerKaldir()
    {
        bariyerAlani.Play("BariyerAna");
        
        Debug.Log("Çalýþýyorumm");
    }

    public void Bitti()
    {
        _GameManager.toplayiciHareketDurumu = true;
    }
}
