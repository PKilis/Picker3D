using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopItem : MonoBehaviour
{
    [SerializeField] private GameManager _GameManager;
    [SerializeField] private string ItemName;
    [SerializeField] private int bonusTopIndex;
    void Start()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ToplayiciSinirObjesi"))
        {
            if (ItemName == "BonusPaletTop")
            {
                _GameManager.PaletleriOrtayaCikar();
                gameObject.SetActive(false);
            }
            else if (ItemName == "BonusToplar")
            {
                _GameManager.BonusToplariOrtayaCikar(bonusTopIndex);
                gameObject.SetActive(false);
            }

        }
    }

}