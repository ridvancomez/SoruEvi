using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

enum States
{
    TumSorularKapali,
    SoruKismiAcik,
    SonucKismiAcik,
    SorulmusSoruKismiAcik
}
public class GameManager : MonoBehaviour
{
    PlayerControl playerScript;

    private int soruIndex;
    private States currentStates;
    private List<Soru> sorular = new List<Soru>();

    //paneller
    [SerializeField]
    private GameObject soruPanel;
    [SerializeField]
    private GameObject sonucPanel;
    [SerializeField]
    private GameObject sorulmusSoruPanel;

    //Soru metinleri ve ��klar
    [SerializeField]
    private Text soruMetin;
    [SerializeField]
    private Text aSikki;
    [SerializeField]
    private Text bSikki;
    [SerializeField]
    private Text cSikki;
    [SerializeField]
    private Text dSikki;

    //Sonu� do�ru veya yanl��
    [SerializeField]
    private Text sonuc;


    // Start is called before the first frame update
    void Start()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();

        currentStates = States.TumSorularKapali;

        //Sorular burada �retiliyor
        SoruUret("�stanbul' u kim feth etmi�tir", new string[] { "Mustafa Kemal Atat�rk", "1. Beyazid", "Fatih Sultan Mehmet", "4. Murat" }, "C", false);
        SoruUret("Hangi �lkenin trafi�i soldan akar", new string[] { "�ngiltere", "T�rkiye", "ABD", "G�ney Kore" }, "A", false);
        SoruUret("Genellikle 4 ayak �zerine d��en hayvan hangisidir", new string[] { "K�pek", "Tav�an", "Sincap", "Kedi" }, "D", false);
        SoruUret("Hangisi telli �alg�d�r", new string[] { "Ney", "Davul", "Obua", "�ella" }, "D", false);
        SoruUret("Minik Ser�e lakapl� �ark�c� kimdir", new string[] { "Demet Akal�n", "Sezen Aksu", "Hande Yener", "�rem Derici" }, "B", false);
        SoruUret("GDO nun a��l�m� nedir", new string[] { "G�ney Do�u Anadolu Orman�", "Geri D�n���ml� Oyuncak", "Geneti�i de�i�tirilmi� organizma", "Gidilip D�n�len Otoyol" }, "C", false);
        SoruUret("Koronavir�s�n hayat�m�za girdi�inden beri g�nl�k hayt�m�za giren kelime hangisi de�ildir", new string[] { "Sava�", "Sosyal Mesafe", "Karantina", "HES" }, "A", false);

    }

    // Update is called once per frame
    void Update()
    {
        switch (currentStates)
        {
            case States.TumSorularKapali:
                StateTumSoruKapali();
                break;
            case States.SoruKismiAcik:
                StateSoruKismi();
                break;
            case States.SonucKismiAcik:
                StateSonucKismi();
                break;
            case States.SorulmusSoruKismiAcik:
                StateSorulmusSoru();
                break;

        }

        if (playerScript.soruAcilsin)
        {
            playerScript.soruAcilsin = false;
            currentStates = States.SoruKismiAcik;
        }
    }

    private void SoruUret(string soruMetin, string[] soruSik, string soruDogruCevap, bool soruSorulduMu)
    {
        Soru soru = new Soru();

        soru.SoruMetin = soruMetin;
        soru.SoruSik = soruSik;
        soru.SoruDogruCevap = soruDogruCevap;
        soru.SoruSorulduMu = soruSorulduMu;

        sorular.Add(soru);
    }

    public void SoruKisminaDus()
    {
        if (!sorular[soruIndex].SoruSorulduMu)
        {
            soruMetin.text = sorular[soruIndex].SoruMetin;
            aSikki.text = sorular[soruIndex].SoruSik[0];
            bSikki.text = sorular[soruIndex].SoruSik[1];
            cSikki.text = sorular[soruIndex].SoruSik[2];
            dSikki.text = sorular[soruIndex].SoruSik[3];
        }
        else
        {
            currentStates = States.SorulmusSoruKismiAcik;
        }
    }

    public void SoruKontrol(string soruSik)
    {
        if (soruSik == sorular[soruIndex].SoruDogruCevap)
        {
            sonuc.text = "Do�ru cevap";
            sorular[soruIndex].SoruSorulduMu = true;
        }
        else
        {
            sonuc.text = "Yanl�� cevap";
        }

        SonucKisminaDus();
    }
    public void KapaliKisminaDus()
    {
        
        currentStates = States.TumSorularKapali;
    }
    public void SorulmusKisminaDus()
    {
        currentStates = States.SorulmusSoruKismiAcik;
    }
    public void SonucKisminaDus()
    {
        currentStates = States.SonucKismiAcik;
    }

    public void OyunKisminaDus()
    {
        currentStates = States.SoruKismiAcik;
    }

    private void StateTumSoruKapali()
    {
        soruPanel.SetActive(false);
        sonucPanel.SetActive(false);
        sorulmusSoruPanel.SetActive(false);
    }

    private void StateSoruKismi()
    {
        soruPanel.SetActive(true);
        sonucPanel.SetActive(false);
        sorulmusSoruPanel.SetActive(false);

        SoruKisminaDus();
    }

    private void StateSonucKismi()
    {
        soruPanel.SetActive(false);
        sonucPanel.SetActive(true);
        sorulmusSoruPanel.SetActive(false);
    }
    private void StateSorulmusSoru()
    {
        
        soruPanel.SetActive(false);
        sonucPanel.SetActive(false);
        sorulmusSoruPanel.SetActive(true);
    }

    public void HangiSoruIndexi(int kutudanGelenSoruIndex)
    {
        soruIndex = kutudanGelenSoruIndex;
    }
}
