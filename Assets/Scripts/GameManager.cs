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

  // Start is called before the first frame update
  void Start()
  {
    // 画像を非表示にする
    Invoke("InactiveImage", 1.0f);
    // ボタン（パネル）を非表示にする
    panel.SetActive(false);
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
    }
    else if (PlayerController.gameState == "playing")
    {
      // ゲーム中
    }
  }
  void InactiveImage()
  {
    mainImage.SetActive(false);
  }
}
