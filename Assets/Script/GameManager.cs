using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Slider = UnityEngine.UI.Slider;

public class GameManager : MonoBehaviour
{
    [Header("---Ball Options ----")]
    [SerializeField] private GameObject[] Balls;
    [SerializeField] private GameObject FirePoint;
    [SerializeField] private float CannonerForce;
    int ActiveBallIndex;

    [SerializeField] private AudioSource[] BallSounds;
    int ActiveSoundIndex;

    [Header("---Other Options----")]
    [SerializeField] private Animator Cannon;
    [SerializeField] private ParticleSystem CannonEffect;
    [SerializeField] private ParticleSystem BallEffect;
    [SerializeField] private Renderer BucketTrans;
    float BucketStartValue;
    float BucketValue;
    [SerializeField] private AudioSource[] OtherSounds;

    string SceneName;



    [Header("--Level Options--")]
    [SerializeField] private int BallGoal;
    [SerializeField] private int TotalBall;
    int SuccessBall;

    [SerializeField] private Slider LevelSlider;
    [SerializeField] private TextMeshProUGUI RestOfBallsText;

    private bool PauseCheck = false;


    [Header("---UI Options----")]
    [SerializeField] private GameObject[] Panels;
    [SerializeField] private TextMeshProUGUI StarCounter;
    [SerializeField] private TextMeshProUGUI WinLevelText;
    [SerializeField] private TextMeshProUGUI LoseLevelText;

    // Start is called before the first frame update
    void Start()
    {
        ActiveSoundIndex = 0;
        LevelSlider.maxValue = BallGoal;
        LevelSlider.minValue = 0;
        RestOfBallsText.text = TotalBall.ToString();
        BucketStartValue = .5f;

        SceneName = SceneManager.GetActiveScene().name;

        BucketValue = .25f / BallGoal;


    }

    public void BallIn()
    {
        SuccessBall++;
        LevelSlider.value = SuccessBall;

        BucketStartValue -= BucketValue;
        BucketTrans.material.SetTextureScale("_MainTex", new Vector2(1f, BucketStartValue));

        BallSounds[ActiveSoundIndex].Play();
        ActiveSoundIndex++;

        if (ActiveSoundIndex == BallSounds.Length - 1)
            ActiveSoundIndex = 0;
        if (SuccessBall == BallGoal)
        {
            Win();
        }
        int num = 0;
        foreach (var item in Balls)
        {
            if (item.activeInHierarchy)
                num++;
        }
        if (num == 0)
        {
            if (TotalBall == 0 && SuccessBall != BallGoal)
            {
                Lose();
            }
            if ((TotalBall + SuccessBall) < BallGoal)
            {
                Lose();
            }

        }
    }
    public void Win()
    {
        OtherSounds[0].Play();
        PlayerPrefs.SetInt("Level", SceneManager.GetActiveScene().buildIndex + 1);
        PlayerPrefs.SetInt("Star", PlayerPrefs.GetInt("Star") + 10);
        StarCounter.text = PlayerPrefs.GetInt("Star").ToString();
        WinLevelText.text = SceneName;
        Panels[1].SetActive(true);
        PauseCheck = true;
    }
    public void Lose()
    {
        OtherSounds[1].Play();
        LoseLevelText.text = SceneName;
        Panels[2].SetActive(true);
        PauseCheck = true;
    }
    public void BallOut()
    {

        if (TotalBall == 0)
        {
            Lose();
        }
        if ((TotalBall + SuccessBall) < BallGoal)
        {
            Lose();
        }
    }

    // Update is called once per frame
    void Update()
    {
        

    }
    public void FireBall()
    {
        if (!PauseCheck)
        {

            TotalBall--;
            RestOfBallsText.text = TotalBall.ToString();

            Cannon.Play("Cannon");
            CannonEffect.Play();
            OtherSounds[2].Play();
            Balls[ActiveBallIndex].transform.SetPositionAndRotation(FirePoint.transform.position, FirePoint.transform.rotation);
            Balls[ActiveBallIndex].SetActive(true);
            //Clone way       
            //GameObject tp = Instantiate(Ball, FirePoint.transform.position, FirePoint.transform.rotation);
            Balls[ActiveBallIndex].GetComponent<Rigidbody>().AddForce(Balls[ActiveBallIndex].transform.TransformDirection(90, 90, 0) * CannonerForce, ForceMode.Force);



            if (ActiveBallIndex == Balls.Length - 1)
            {
                ActiveBallIndex = 0;
            }
            else
            {
                ActiveBallIndex++;
            }



        }
    }
    public void PlayBallEfect(Vector3 poss, Color efectColor)
    {
        BallEffect.transform.position = poss;
        var main = BallEffect.main;
        main.startColor = efectColor;
        BallEffect.gameObject.SetActive(true);

    }

    public void PauseGame()
    {
        Panels[0].SetActive(true);
        Time.timeScale = 0;
        PauseCheck = true;
    }

    public void PanelButtons(string option)
    {
        switch (option)
        {

            case "Resume":
                Panels[0].SetActive(false);
                Time.timeScale = 1;
                PauseCheck = false;
                break;
            case "Restart":
                Time.timeScale = 1;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                break;
            case "Settings":

                break;
            case "Next":
                Time.timeScale = 1;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                break;
            case "MainMenu":
                SceneManager.LoadScene(0);
                break;
            case "Quit":
                Application.Quit();
                break;
        }

    }
}
