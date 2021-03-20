using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
  public GameObject mainImage;
  public Sprite gameOverSpr;
  public Sprite gameClearSpr;
  public GameObject panel;
  public GameObject restartButton;
  public GameObject nextButton;

  Image titleImage;

  // +++ 時間制限追加 +++ 
  public GameObject timeBar;  // 時間表示イメージ
  public GameObject timeText;  // 時間テキスト
  TimeController timeCnt;  // TimeController

  // Start is called before the first frame update
  void Start()
  {
    // 画像を非表示にする
    Invoke("InactiveImage", 1.0f);
    // ボタン（パネル）を非表示にする
    panel.SetActive(false);

    // +++ 時間制限追加 +++ 
    // TimeControllerを取得
    timeCnt = GetComponent<TimeController>();
    if (timeCnt != null)
    {
      if (timeCnt.gameTime == 0.0f)
      {
        timeBar.SetActive(false);  // 制限時間なしなら非表示
      }
    }
  }

  // Update is called once per frame
  void Update()
  {
    if (PlayerController.gameState == "gameclear")
    {
      // ゲームクリア
      mainImage.SetActive(true); // 画像を表示
      panel.SetActive(true);  // ボタン(パネル)を表示
      // RESTSRTボタンを無効化
      Button bt = restartButton.GetComponent<Button>();
      bt.interactable = false;
      mainImage.GetComponent<Image>().sprite = gameClearSpr;  // 画像設定
      PlayerController.gameState = "gameend";

      // +++ 時間制限追加 +++
      if (timeCnt != null)
      {
        timeCnt.isTimeOver = true;  // 時間カウント停止
      }
    }
    else if (PlayerController.gameState == "gameover")
    {
      // ゲームオーバー
      mainImage.SetActive(true); // 画像を表示
      panel.SetActive(true);  // ボタン(パネル)を表示
      // NEXTボタンを無効化
      Button bt = nextButton.GetComponent<Button>();
      bt.interactable = false;
      mainImage.GetComponent<Image>().sprite = gameOverSpr;  // 画像設定
      PlayerController.gameState = "gameend";

      // +++ 時間制限追加 +++
      if (timeCnt != null)
      {
        timeCnt.isTimeOver = true;  // 時間カウント停止
      }
    }
    else if (PlayerController.gameState == "playing")
    {
      // ゲーム中
      GameObject player = GameObject.FindGameObjectWithTag("Player");
      // PlayerControllerを取得
      PlayerController playerCnt = player.GetComponent<PlayerController>();
      // +++ 時間制限追加 +++
      // タイムを更新する
      if (timeCnt != null)
      {
        if (timeCnt.gameTime > 0.0f)
        {
          // 整数に代入することで少数を切り捨て
          int time = (int)timeCnt.displayTime;
          // タイム更新
          timeText.GetComponent<Text>().text = time.ToString();
          // タイムオーバー
          if (time == 0)
          {
            playerCnt.GameOver();  // ゲームオーバーにする
          }
        }
      }
    }
  }
  void InactiveImage()
  {
    mainImage.SetActive(false);
  }
}
