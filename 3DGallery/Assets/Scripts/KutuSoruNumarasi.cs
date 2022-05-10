using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KutuSoruNumarasi : MonoBehaviour
{
    public int soruNumarasi;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            GameObject.FindObjectOfType<GameManager>().HangiSoruIndexi(soruNumarasi);
        }
    }
}
