using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
  public float leftLimit = 0.0f;
  public float rightLimit = 0.0f;
  public float topLimit = 0.0f;
  public float bottomLimit = 0.0f;

  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {
    GameObject player = GameObject.FindGameObjectWithTag("Player");
    if (player != null)
    {
      // カメラ座標更新
      float x = player.transform.position.x;
      float y = player.transform.position.y;
      float z = transform.position.z;
      // 横動機させる
      // 両端に移動制限をつける
      if (x < leftLimit)
      {
        x = leftLimit;
      }
      else if (x > rightLimit)
      {
        x = rightLimit;
      }
      // 縦同期させる
      // 上下に移動制限をつける
      if (y < bottomLimit)
      {
        y = bottomLimit;
      }
      else if (y > topLimit)
      {
        y = topLimit;
      }
      // カメラ位置のVector3を作成
      Vector3 v3 = new Vector3(x, y, z);
      this.transform.position = v3;
    }
  }
}
