using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
  Rigidbody2D rbody;
  float axisH = 0.0f;
  public float speed = 3.0f;

  // Start is called before the first frame update
  void Start()
  {
    rbody = GetComponent<Rigidbody2D>();
  }

  // Update is called once per frame
  void Update()
  {
    axisH = Input.GetAxisRaw("Horizontal");
    if (axisH > 0.0f)
    {
      Debug.Log("右移動");
      transform.localScale = new Vector2(1, 1);
    }
    else if (axisH < 0.0f)
    {
      Debug.Log("左移動");
      transform.localScale = new Vector2(-1, 1); // 左右反転
    }
  }

  void FixedUpdate()
  {
    // 速度更新
    rbody.velocity = new Vector2(axisH * speed, rbody.velocity.y);
  }
}
