using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public enum GameState
{
    Intro,
    Playing,
    Dead
}
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameState State = GameState.Intro;       // 다른 스크립트에서 사용할 수 있게 함
    public float PlayStartTime;                     // 플레이어가 게임을 시작한 시간
    public int Lives = 3;

    [Header("References")]
    public GameObject IntroUI;
    public GameObject DeadUI;
    public GameObject EnemySpawner;
    public GameObject FoodSpawner;
    public GameObject GoldSpawner;
    public Player PlayerScript;
    public TMP_Text scoreText;

    void Awake()                            // start 메서드보다 빠르게 호출됨
    {
        if (Instance == null)               // 게임매니저 인스턴스를 다른 곳에서도 공유해서 사용 가능
        {
            Instance = this;
        }
    }
    void Start()
    {
        IntroUI.SetActive(true);            // 게임이 실행되면 인트로UI 상태를 true값으로(화면에 보이게 활성화)
    }
    
    float CalculateScore()                  // 스코어 계산 함수
    {
        return Time.time - PlayStartTime;
    }

    void SaveHighScore()
    {
        int score = Mathf.FloorToInt(CalculateScore()); // Mathf 클래스에 있는 FloorToInt 메서드 호출, CalculateScore의 반환값을 넘겨줌
        int currentHighScore = PlayerPrefs.GetInt("HighScore"); // PlayerPrefs를 이용해 기기의 디스크에 데이터를 저장, 불러올 때 사용할 키 이름 == HighScore
        if(score > currentHighScore)                    // 현재 점수가 최고 점수를 넘었다면
        {
            PlayerPrefs.SetInt("HighScore", score);     // 최고 점수에 현재 점수 대입
            PlayerPrefs.Save();                         // 변경사항 저장
        }
    }
    int GetHighScore()                                  // highscore 불러오는 메소드
    {
        return PlayerPrefs.GetInt("HighScore");
    }
    public float CalculateGameSpeed()                   // 시간이 지날수록 게임 속도를 올려 난이도를 높여주는 메소드
    {
        if(State != GameState.Playing)                  // 게임 플레이 중이 아니라면
        {
            return 5f;                                  // 게임 속도를 원래대로
        }
        float speed = 8f + (0.5f * Mathf.Floor(CalculateScore() / 10f)); // 10초마다 0.5씩 속도가 올라가게. 점수가 올라갈수록 모든 요소의 속도가 빨라짐
        return Mathf.Min(speed, 30f);                   // speed나 최대 속도값 중에서 하나를 반환
    }
    void Update()
    {
        if(State == GameState.Playing)                  // 게임 상태가 플레이 중일 때
        {
            scoreText.text = "Score : " + Mathf.FloorToInt(CalculateScore());
            // 스코어 텍스트에 다음과 같이 보이게 입력
        }
        else if(State == GameState.Dead)
        {
            scoreText.text = "HighScore : " + GetHighScore();
        }
        if(State == GameState.Intro && Input.GetKeyDown(KeyCode.Space)) // 게임 상태가 인트로 화면이고, 스페이스 키가 눌렸을 때
        {
            State = GameState.Playing;      // 게임 상태를 플레이로 바꾸고
            IntroUI.SetActive(false);       // UI 숨기기
            EnemySpawner.SetActive(true);   // 스포너들 활성화상태로 바꿔주기
            FoodSpawner.SetActive(true);
            GoldSpawner.SetActive(true);
            PlayStartTime = Time.time;      // 플레이어가 게임을 시작한 시간을 기록
        }
        if(State == GameState.Playing && Lives == 0)    // 게임 상태가 플레이 중이고, 생명이 0일 때
        {
            SaveHighScore();
            PlayerScript.KillPlayer();                  // 플레이어 스크립트에 있는 킬플레이어 메소드 호출
            EnemySpawner.SetActive(false);              // 스포너들 비활성화상태로 바꿔주기
            FoodSpawner.SetActive(false);
            GoldSpawner.SetActive(false);
            DeadUI.SetActive(true);
            State = GameState.Dead;                     // 게임 상태를 데드로 바꿔주기
        }
        if(State == GameState.Dead && Input.GetKeyDown(KeyCode.Space)) // 게임 상태가 데드이고, 스페이스 키가 눌렸을 때
        {
            SceneManager.LoadScene("main");             // 씬매니저로 메인 씬 로드하기
        }
    }
}
