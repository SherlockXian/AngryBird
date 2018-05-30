using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pig : MonoBehaviour {

    public float maxSpeed = 10;
    public float minSpeed = 5;
    private SpriteRenderer render;
    public Sprite hurt;//受伤图片
    public GameObject boom;//爆炸特效
    public GameObject score;//分数
    public bool isPig = false;//判断是否为猪

    public AudioClip hurtClip;
    public AudioClip dead;
    public AudioClip birdCollision;

    private void Awake()
    {
        //初始化
        render = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //当鸟和猪的相对速度为0时候判断猪受伤
        //collision.relativeVelocity可以求相对速度

        //print(collision.relativeVelocity.magnitude);

        if(collision.gameObject.tag == "Player")
        {
            Audioplay(birdCollision);
            collision.transform.GetComponent<Bird>().Hurt();
        }
        if (collision.relativeVelocity.magnitude>maxSpeed)
        {
            //当场去世
            Dead();
        }
        else if(collision.relativeVelocity.magnitude > minSpeed && collision.relativeVelocity.magnitude < maxSpeed)
        {
            //更换受伤图片
            render.sprite = hurt;
            Audioplay(hurtClip);
        }
    }

    public void Dead()
    {
        if(isPig)
        {
            GameManager._instance.pigs.Remove(this);//移除猪
        }
        Destroy(gameObject);//消灭猪
        Instantiate(boom,transform.position,Quaternion.identity);//生成爆炸特效
        Audioplay(dead);
        GameObject go = Instantiate(score, transform.position + new Vector3(0,0.5f,0), Quaternion.identity);//生成分数特效
        Destroy(go, 1.5f);//1.5秒后销毁
    }

    //播放音效
    public void Audioplay(AudioClip clip)
    {
        AudioSource.PlayClipAtPoint(clip, transform.position);
    }
}
