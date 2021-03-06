using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AzarashiController : MonoBehaviour
{
    Rigidbody2D rb2d;
    Animator animator;
    float angle;
    bool isDead;

    public float maxHeight;
    public float flapVelocity;
    public float relativeVelocityX;
    public GameObject sprite;

    public bool IsDead()
    {
        return isDead;
    }
    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = sprite.GetComponent<Animator>();
    }
    void Update()
    {
        //最高速度に達していない場合に限りタップの入力を受け付ける
        if (Input.GetButtonDown("Fire1") && transform.position.y < maxHeight)
        {
            Flap();
        }
        //角度を反映
        ApplyAngle();

        //angle が水平以上だったら、アニメーターのflapフラグをtrueにする
        animator.SetBool("flap", angle >= 0.0f && !isDead);

    }
    public void Flap()
    {

        //死んだらはばたけない
        if (isDead) return;
        //重力が効いていない時は操作しない
        if (rb2d.isKinematic) return;
        //velocity を調節書き換え上方向に加速
        rb2d.velocity = new Vector2(0.0f, flapVelocity);
    }
    void ApplyAngle()
    {
        float targetAngle;

        //死亡したら常にひっくり変える
        if (isDead)
        {
            targetAngle = 180.0f;
        }
        else
        {


            //現在の速度相対速度から進んでいる角度を求める
            targetAngle = Mathf.Atan2(rb2d.velocity.y, relativeVelocityX) * Mathf.Rad2Deg;
        }

        //回転アニメをスムージング
        angle = Mathf.Lerp(angle, targetAngle, Time.deltaTime * 10.0f);

        //Rotationの反映
        sprite.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, angle);

    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDead) return;

        Camera.main.SendMessage("Clash");

        //何かにぶつかったら死亡フラグを立てる。
        isDead = true;

    }
    public void SetSteerActive(bool active)
    {
        rb2d.isKinematic = !active;
    }
}
