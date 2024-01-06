using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Diagnostics.Tracing;

public class PengolahSoal : MonoBehaviour
{

    public TextAsset QuestionAsset;

    private string[] Question;

    private string[,] soalBag;

    int indexSoal;
    int maxSoal;
    bool ambilSoal;
    char kunciJ;

    //Komponen UI
    public TMP_Text txtSoal;
    public TMP_Text txtOpsiA;
    public TMP_Text txtOpsiB;
    public TMP_Text txtOpsiC;
    public TMP_Text txtOpsiD;

    bool isHasil;
    private float durasi;
    public float durasiPenilaian;

    int jwbBenar;
    int jwbSalah;
    float nilai;

    public GameObject panel;
    public GameObject imgPenilaian;
    public GameObject imgHasil;
    public TMP_Text txtHasil;

    public GameObject imgCounter;
    public TMP_Text Counter;

    void Start()
    {
        Time.timeScale = 1;
        durasi = durasiPenilaian;

        Question = QuestionAsset.ToString().Split('#');

        soalBag = new string[Question.Length, 6];
        maxSoal = Question.Length;
        QuestionManager();

        int TotalQuestion = maxSoal;
        int Answered = 0;

        Counter.text = Answered + "/" + TotalQuestion;

        ambilSoal = true;
        TampilkanSoal();

    }

    private void QuestionManager()
    {
        for (int i = 0; i < Question.Length; i++)
        {
            string[] tempQuestion = Question[i].Split('+');
            for (int j = 0; j < tempQuestion.Length; j++)
            {
                soalBag[i, j] = tempQuestion[j];
                continue;
            }
            continue;
        }
    }

    private void TampilkanSoal()
    {
        if (indexSoal < maxSoal)
        {
            if (ambilSoal)
            {
                txtSoal.text = soalBag[indexSoal, 0];
                txtOpsiA.text = soalBag[indexSoal, 1];
                txtOpsiB.text = soalBag[indexSoal, 2];
                txtOpsiC.text = soalBag[indexSoal, 3];
                txtOpsiD.text = soalBag[indexSoal, 4];
                kunciJ = soalBag[indexSoal, 5][0];

                ambilSoal = false;
            }
        }
    }

    public void Opsi(string opsiHuruf)
    {
        CheckJawaban(opsiHuruf[0]);

        int TotalQuestion = maxSoal;
        int Answered = indexSoal + 1;

        Counter.text = Answered + "/" + maxSoal;

        if (indexSoal == maxSoal - 1)
        {
            isHasil = true;
        }
        else
        {
            indexSoal++;
            ambilSoal = true;
        }

        panel.SetActive(true);

    }

    private float HitungNilai()
    {
        return nilai = (float)jwbBenar / maxSoal * 100;
    }

    public TMP_Text txtPenilaian;

    private void CheckJawaban(char huruf)
    {
        string penilaian;

        if (huruf.Equals(kunciJ))
        {
            penilaian = "Benar!";
            jwbBenar++;
        }
        else
        {
            penilaian = "Salah!";
            jwbSalah++;
        }

        txtPenilaian.text = penilaian;

 
    }

    private bool nextLevelUnlocked = false;

    void Update()
    {
        if (panel.activeSelf)
        {
            durasiPenilaian -= Time.deltaTime;

            if (isHasil)
            {
                imgPenilaian.SetActive(true);
                imgHasil.SetActive(false);

                if (durasiPenilaian <= 0)
                { 
                    txtHasil.text = "<b><color=#DAED00>SELESAI</color> <color=white>\nJumlah Benar : " + jwbBenar + "\nJumlah Salah : " + jwbSalah + "\n\nNilai : </b>" + HitungNilai();
                    imgPenilaian.SetActive(false);
                    imgHasil.SetActive(true);
                    Time.timeScale = 0;

                    durasiPenilaian = 0;

                    if (!nextLevelUnlocked)
                    {
                        UnlockNewLevel();
                        nextLevelUnlocked = true; 
                    }
                }
            }
            else
            {
                imgPenilaian.SetActive(true);
                imgHasil.SetActive(false);
                Time.timeScale = 1;

                if (durasiPenilaian <= 0)
                {
                    panel.SetActive(false);
                    durasiPenilaian = durasi;

                    TampilkanSoal();
                }
            }
        }
    }

    void UnlockNewLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex >= PlayerPrefs.GetInt("Finished"))
        {
            PlayerPrefs.SetInt("Finished", SceneManager.GetActiveScene().buildIndex + 1);
            PlayerPrefs.SetInt("UnlockedLevel", PlayerPrefs.GetInt("UnlockedLevel", 1) + 1);
            PlayerPrefs.Save();
        }
    }
}