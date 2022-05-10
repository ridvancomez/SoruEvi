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

    //Soru metinleri ve þýklar
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

    //Sonuç doðru veya yanlýþ
    [SerializeField]
    private Text sonuc;


    // Start is called before the first frame update
    void Start()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();

        currentStates = States.TumSorularKapali;

        //Sorular burada üretiliyor
        SoruUret("Ýstanbul' u kim feth etmiþtir", new string[] { "Mustafa Kemal Atatürk", "1. Beyazid", "Fatih Sultan Mehmet", "4. Murat" }, "C", false);
        SoruUret("Hangi ülkenin trafiði soldan akar", new string[] { "Ýngiltere", "Türkiye", "ABD", "Güney Kore" }, "A", false);
        SoruUret("Genellikle 4 ayak üzerine düþen hayvan hangisidir", new string[] { "Köpek", "Tavþan", "Sincap", "Kedi" }, "D", false);
        SoruUret("Hangisi telli çalgýdýr", new string[] { "Ney", "Davul", "Obua", "Çella" }, "D", false);
        SoruUret("Minik Serçe lakaplý þarkýcý kimdir", new string[] { "Demet Akalýn", "Sezen Aksu", "Hande Yener", "Ýrem Derici" }, "B", false);
        SoruUret("GDO nun açýlýmý nedir", new string[] { "Güney Doðu Anadolu Ormaný", "Geri Dönüþümlü Oyuncak", "Genetiði deðiþtirilmiþ organizma", "Gidilip Dönülen Otoyol" }, "C", false);
        SoruUret("Koronavirüsün hayatýmýza girdiðinden beri günlük haytýmýza giren kelime hangisi deðildir", new string[] { "Savaþ", "Sosyal Mesafe", "Karantina", "HES" }, "A", false);

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
            sonuc.text = "Doðru cevap";
            sorular[soruIndex].SoruSorulduMu = true;
        }
        else
        {
            sonuc.text = "Yanlýþ cevap";
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
