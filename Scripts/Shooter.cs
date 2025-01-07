using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public GameObject[] ballPrefs; // 生成するボール
    public Transform ballParentTransform; // ボールを入れるオブジェクト
    public float shotForce; // ショットパワー
    public float shotTorque; // 回転パワー
    public int ballAmount; // ボール残数
    public int ballsNumber; // ボールの識別番号
    public AudioClip shotSound; // ショット音
    private AudioSource audioSource; // AudioSourceコンポーネント

    void Start()
    {
        audioSource = GetComponent<AudioSource>(); // AudioSourceコンポーネントを取得
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.gameState == "timeover") // プレイ状態でなければ何もしない
        {
            return;
        }

        if (Input.GetButtonDown("Fire1")) Shot(); // 左クリックで発射
    }

    GameObject SampleBall() // ボールをランダムで選ぶ
    {
        int index = Random.Range(0, ballPrefs.Length);
        return ballPrefs[index];
    }

    public void Shot()
    {
        if (ballAmount <= 0) // ボール不足なら何もしない
        {
            return;
        }
        // ランダムで選ばれたボールを生成
        GameObject ball = (GameObject)Instantiate(
            SampleBall(),
            transform.position,
            Quaternion.identity
        );
        // ボールの識別番号を+1
        ball.GetComponent<BallController>().ballNumber = ballsNumber;
        ballsNumber++;

        // ボールの残数-1
        ballAmount--;
        // 生成したボールの親オブジェクトにBallsオブジェクトを指名
        ball.transform.parent = ballParentTransform;

        // Rigidbodyを取得
        Rigidbody ballRigidbody = ball.GetComponent<Rigidbody>();
        // ボールのRigidbodyのAddForceで発射
        ballRigidbody.AddForce(transform.forward * shotForce);
        // ボールのRigidbodyのAddTorqueで回転
        ballRigidbody.AddTorque(new Vector3(0, shotTorque, 0));

        // ショット音を再生
        audioSource.PlayOneShot(shotSound);
    }
}
