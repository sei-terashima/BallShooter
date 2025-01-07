using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static string gameState; // ゲームの状態
    public TextMeshProUGUI hiScoreText; // ハイスコア
    public TextMeshProUGUI scoreText; // スコア
    public TextMeshProUGUI ballAmountText; // ボール残数
    public TextMeshProUGUI timeText; // 時間
    public GameObject statusText; // 開始・終了の表示
    public Shooter shooter; // shooterスクリプト
    public ScoreController scoreController; // ScoreControllerスクリプト
    public float gameTime = 60.0f; // カウントダウン時間
    public string sceneToLoad = "Main"; // インスペクタから変更可能なシーン名
    public AudioClip gameOver; // 効果音
    private AudioSource audioSource; // AudioSourceコンポーネント

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>(); // AudioSourceコンポーネントを取得
        if (audioSource == null)
        {
            Debug.LogError("AudioSourceコンポーネントがアタッチされていません！");
        }

        Invoke("HideStatusText", 1.0f); // 1秒後にテキストを非表示にする
        gameState = "playing"; // ゲーム中にする
        int highScore = PlayerPrefs.GetInt("HighScore", 0); // ハイスコア読み込み
        hiScoreText.text = "Hi-Score: " + highScore; // ハイスコアを更新
    }

    void HideStatusText()
    {
        statusText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameState != "playing") // プレイ状態でなければ何もしない
        {
            return;
        }
        gameTime -= Time.deltaTime; // カウントダウン
        timeText.text = "Time: " + gameTime.ToString("F2"); // タイム更新

        scoreText.text = "Score: " + scoreController.score; // スコアを更新
        ballAmountText.text = "Last: " + shooter.ballAmount; // 残ボール数更新

        if (gameTime <= 0.0f)
        {
            TimeOver();
        }
    }

    void TimeOver()
    {
        // ハイスコア呼び出し
        int highScore = PlayerPrefs.GetInt("HighScore", 0);
        // 現スコアがハイスコアより上であればハイスコアを更新
        if (scoreController.score > highScore)
        {
            PlayerPrefs.SetInt("HighScore", scoreController.score);
        }

        gameState = "timeover"; // ゲームの状態をタイムオーバーに変更

        statusText.GetComponent<TextMeshProUGUI>().text = "GAME OVER!";
        statusText.SetActive(true);

        if (gameOver != null)
        {
            audioSource.PlayOneShot(gameOver); // 効果音を再生

            Invoke("ChangeScene", 3.0f); // 3秒後にシーンを変更
        }

        void ChangeScene()
        {
            SceneManager.LoadScene(sceneToLoad); // インスペクタで指定されたシーンに移行
        }
    }
}
