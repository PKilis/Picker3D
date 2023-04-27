using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;


[Serializable]
public class TopAlaniTeknikIslemler
{

    public Animator TopAlaniAsansor;
    public TextMeshProUGUI sayiText;
    public int atilmasiGerekenTop;
    public GameObject[] toplar;

}

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject toplayiciObje;
    [SerializeField] private GameObject[] toplayiciPaletler;
    [SerializeField] private GameObject[] bonusToplar;
    bool paletlerVarmi;

    [SerializeField] private GameObject topKontrolObjesi;
    public bool toplayiciHareketDurumu;

    int atilanTopSayisi;
    int toplamCheckPointSayisi;
    int mevcutCheckPointIndex;
    float parmakPozx;

    [SerializeField] private List<TopAlaniTeknikIslemler> _TopAlaniTeknikIslemler = new List<TopAlaniTeknikIslemler>();

    void Start()
    {
        toplayiciHareketDurumu = true;

        for (int i = 0; i < _TopAlaniTeknikIslemler.Count ; i++)
        {
            _TopAlaniTeknikIslemler[i].sayiText.text = atilanTopSayisi + "/" + _TopAlaniTeknikIslemler[i].atilmasiGerekenTop;

        }


        toplamCheckPointSayisi = _TopAlaniTeknikIslemler.Count - 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (toplayiciHareketDurumu)
        {
            toplayiciObje.transform.position += 5f * Time.deltaTime * toplayiciObje.transform.forward;

            if (Time.timeScale != 0)
            {
                if (Input.touchCount > 0)
                {
                    Touch touch = Input.GetTouch(0);

                    Vector3 touchPosition = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 10f));

                    switch (touch.phase)
                    {
                        case TouchPhase.Began:
                            parmakPozx = touchPosition.x - toplayiciObje.transform.position.x;
                            break;
                        case TouchPhase.Moved:
                            if (touchPosition.x - parmakPozx > -1.15 && touchPosition.x - parmakPozx < 1.15f)
                            {
                                toplayiciObje.transform.position = Vector3.Lerp(toplayiciObje.transform.position, new Vector3(touchPosition.x - parmakPozx, 
                                    toplayiciObje.transform.position.y, toplayiciObje.transform.position.z), 3f);
                            }
                            break;
                        default:
                            break;
                    }



                }


                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    toplayiciObje.transform.position = Vector3.Lerp(toplayiciObje.transform.position, new Vector3
                        (toplayiciObje.transform.position.x - 1f, toplayiciObje.transform.position.y, toplayiciObje.transform.position.z), 0.010f);

                }
                if (Input.GetKey(KeyCode.RightArrow))
                {
                    toplayiciObje.transform.position = Vector3.Lerp(toplayiciObje.transform.position, new Vector3
                        (toplayiciObje.transform.position.x + 1f, toplayiciObje.transform.position.y, toplayiciObje.transform.position.z), 0.010f);

                }
            }
        }
    }

    public void SiniraGelindi()
    {

        if (paletlerVarmi)
        {
            toplayiciPaletler[0].SetActive(false);
            toplayiciPaletler[1].SetActive(false);
        }
        toplayiciHareketDurumu = false;
        Invoke("AsamaKontrol", 2f);
        Collider[] hitColl = Physics.OverlapBox(topKontrolObjesi.transform.position, topKontrolObjesi.transform.localScale / 2, Quaternion.identity);
        // Olcay hoca sor neden localscale 2'ye böldük
        int i = 0;
        while (i < hitColl.Length)
        {
            hitColl[i].gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, 0.8f), ForceMode.Impulse);

            i++;
        }
    }

    public void ToplariSay()
    {
        atilanTopSayisi++;
        _TopAlaniTeknikIslemler[mevcutCheckPointIndex].sayiText.text = atilanTopSayisi + "/" + _TopAlaniTeknikIslemler[mevcutCheckPointIndex].atilmasiGerekenTop;
    }

    public void AsamaKontrol()
    {
        if (atilanTopSayisi >= _TopAlaniTeknikIslemler[mevcutCheckPointIndex].atilmasiGerekenTop)
        {
            Debug.Log("Kazandýn");
            // Atilan TopSayisi = 0;
             
            _TopAlaniTeknikIslemler[mevcutCheckPointIndex].TopAlaniAsansor.Play("Asansor");

            foreach (var item in _TopAlaniTeknikIslemler[mevcutCheckPointIndex].toplar)
            {
                item.SetActive(false);
            }

            if (mevcutCheckPointIndex == toplamCheckPointSayisi)
            {
                Debug.Log("OyunBitti  -  Kazandýn paneli ortaya çýkabilir");
            }
            else
            {
                mevcutCheckPointIndex++;
                atilanTopSayisi = 0;
                if (paletlerVarmi)
                {
                    toplayiciPaletler[0].SetActive(true);
                    toplayiciPaletler[1].SetActive(true);
                }
            }
        }
        else
        {
            Debug.Log("OyunBitti  -  Kaybettin paneli ortaya çýkabilir");
        }
    }

    public void PaletleriOrtayaCikar()
    {
        paletlerVarmi = true;
        toplayiciPaletler[0].SetActive(true);
        toplayiciPaletler[1].SetActive(true);
    }

    public void BonusToplariOrtayaCikar(int bonusTopIndex)
    {
        bonusToplar[bonusTopIndex].SetActive(true);
    }

     public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(topKontrolObjesi.transform.position, topKontrolObjesi.transform.localScale);
    } 
}
