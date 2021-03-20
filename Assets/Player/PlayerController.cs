using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
  Rigidbody2D rbody;
  float axisH = 0.0f;
  public float speed = 3.0f;

  public float jump = 9.0f;
  public LayerMask groundLayer; // 着地できるレイヤー
  bool goJump = false;
  bool onGround = false;

  // アニメーション対応
  Animator animator; // アニメーター
  public string stopAnime = "PlayerStop";
  public string moveAnime = "PlayerMove";
  public string jumpAnime = "PlayerJump";
  public string goalAnime = "PlayerGoal";
  public string deadAnime = "PlayerOver";
  string nowAnime = "";
  string oldAnime = "";
  public static string gameState = "playing"; // ゲームの状態

  // Start is called before the first frame update
  void Start()
  {
    rbody = GetComponent<Rigidbody2D>();
    // アニメーター取得
    animator = GetComponent<Animator>();
    nowAnime = stopAnime;
    oldAnime = stopAnime;
    gameState = "playing"; // ゲーム中にする
  }

  // Update is called once per frame
  void Update()
  {
    // プレイ中でない場合はキャラ移動をできなくする
    if (gameState != "playing")
    {
      return;
    }

    axisH = Input.GetAxisRaw("Horizontal");
    if (axisH > 0.0f)
    {
      transform.localScale = new Vector2(1, 1);
    }
    else if (axisH < 0.0f)
    {
      transform.localScale = new Vector2(-1, 1); // 左右反転
    }

    // キャラクターをジャンプさせる
    if (Input.GetButtonDown("Jump"))
    {
      Jump();
    }
  }

  void FixedUpdate()
  {
    // プレイ中でない場合はキャラ移動をできなくする
    if (gameState != "playing")
    {
      return;
    }

    // 地上判定
    // Linecast: レイヤーとの接触判定に使用
    onGround = Physics2D.Linecast(transform.position, transform.position - (transform.up * 0.1f), groundLayer);

    // キー入力がないとキーを離したときに横向きの速度が0になり、真下に落ちるので、
    // 飛んでいる間に左右キーが離れても物理演算を勝手にしてくれるよう条件を追加
    if (onGround || axisH != 0)
    {
      // 地面の上 or 速度が0でない
      // 速度更新
      rbody.velocity = new Vector2(axisH * speed, rbody.velocity.y);
    }
    if (onGround && goJump)
    {
      // 地面の上でジャンプキーが押された
      // ジャンプさせる
      Debug.Log("ジャンプ");
      // ジャンプさせるベクトルを作る
      Vector2 jumpPw = new Vector2(0, jump);
      // 瞬間的な力を加える
      rbody.AddForce(jumpPw, ForceMode2D.Impulse);
      // ジャンプフラグをおろす
      goJump = false;
    }
    if (onGround)
    {
      // 地面の上
      if (axisH == 0)
      {
        nowAnime = stopAnime; // 停止
      }
      else
      {
        nowAnime = moveAnime; // 移動
      }
    }
    else
    {
      // 空中
      nowAnime = jumpAnime;
    }
    // 状態が変化したらアニメーション再生
    if (nowAnime != oldAnime)
    {
      oldAnime = nowAnime;
      animator.Play(nowAnime); // アニメーション再生
    }
  }
  public void Jump()
  {
    goJump = true;
    Debug.Log("ジャンプボタン押した！");
  }

  // 接触開始
  void OnTriggerEnter2D(Collider2D collision)
  {
    if (collision.gameObject.tag == "Goal")
    {
      Goal();
    }
    else if (collision.gameObject.tag == "Dead")
    {
      GameOver();
    }
  }

  public void Goal()
  {
    animator.Play(goalAnime);
    gameState = "gameclear";
    GameStop();
  }
  public void GameOver()
  {
    animator.Play(deadAnime);

    gameState = "gameover";
    GameStop();
    // ================
    // ゲームオーバー演出
    // ================
    // プレイヤーの当たりを消す
    GetComponent<CapsuleCollider2D>().enabled = false;
    // プレイヤーを少し上に跳ね上げる演出
    rbody.AddForce(new Vector2(0, 5), ForceMode2D.Impulse);
  }

  void GameStop()
  {
    Rigidbody2D rbody = GetComponent<Rigidbody2D>();
    rbody.velocity = new Vector2(0, 0);
  }
}
