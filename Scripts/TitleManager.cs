using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    public TextMeshProUGUI hiScoreText; // ハイスコアテキスト

    void Start()
    {
        // ハイスコアを読み込み、テキストを更新
        LoadHighScore();
    }

    // ハイスコアを読み込み、テキストを更新するメソッド
    public void LoadHighScore()
    {
        int hiScore = PlayerPrefs.GetInt("HighScore"); // ハイスコアを呼び出し
        hiScoreText.text = "High Score: " + hiScore.ToString(); // ハイスコアテキストを更新
    }

    // ハイスコアをリセットするメソッド
    public void ResetHighScore()
    {
        PlayerPrefs.SetInt("HighScore", 0); // ハイスコアを0にリセット
        PlayerPrefs.Save(); // 変更を保存
        LoadHighScore(); // ハイスコアを再読み込みしてテキストを更新
    }





    // シーンを切り替えるメソッド
     public void ChangeScene()
    {
        SceneManager.LoadScene("Main"); // 指定されたシーンをロード
    }
}
