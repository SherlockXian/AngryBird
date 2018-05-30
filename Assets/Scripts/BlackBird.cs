using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackBird : Bird {

    private List<Pig> blocks = new List<Pig>();//物体集合

    //进入触发区域
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            blocks.Add(collision.gameObject.GetComponent<Pig>());
        }
    }

    //离开触发区域
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            blocks.Remove(collision.gameObject.GetComponent<Pig>());
        }
    }

    public override void ShowSkill()
    {
        base.ShowSkill();
        if(blocks.Count > 0 && blocks != null)
        {
            for(int i=0;i<blocks.Count;i++)
            {
                blocks[i].Dead();
            }
        }
        OnClear();
    }

    void OnClear()
    {
        rg.velocity = Vector3.zero;//速度为0
        Instantiate(boom, transform.position, Quaternion.identity);//播放爆炸特效
        render.enabled = false;//图片消失
        GetComponent<CircleCollider2D>().enabled = false;//关闭碰撞盒
        myTrail.ClearTrails();//关闭拖尾
    }

    protected override void Next()
    {
        //对小鸟进行轮换飞出
        GameManager._instance.birds.Remove(this);//移除小鸟
        Destroy(gameObject);
        GameManager._instance.NextBird();
    }
}
