using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ScoreController : MonoBehaviour
{
    public int score = 0;
    public string targetTag; // 指定されたタグ
    


    // オブジェクトがコライダーに入った時に呼び出される
    void OnTriggerEnter(Collider other)
    {
        BallController ballController = other.GetComponent<BallController>();
        if (other.gameObject.tag == targetTag)
        {
            score += ballController.ballScore;
            Debug.Log(score);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        BallController ballController = other.GetComponent<BallController>();
        if (other.gameObject.tag == targetTag)
        {
            score -= ballController.ballScore;
            Debug.Log(score);
        }
    }




}
