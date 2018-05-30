using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PausePanel : MonoBehaviour {

    private Animator anim;
    public GameObject button;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void Retry()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(2);
    }

    //点击Pause按钮
    public void Pause()
    {
        //1.播放pause动画
        anim.SetBool("isPause", true);
        button.SetActive(false);

        //判断是否还有小鸟
        if(GameManager._instance.birds.Count > 0)
        {
            //第一只小鸟没有飞出不可以操作
            if(GameManager._instance.birds[0].isReleased == false)
            {
                GameManager._instance.birds[0].canMove = false;
            }
        }
    }

    //点击了继续按钮
    public void Resume()
    {
        //1.播放resume动画
        Time.timeScale = 1;
        anim.SetBool("isPause", false);

        //判断是否还有小鸟
        if (GameManager._instance.birds.Count > 0)
        {
            //第一只小鸟没有飞出不可以操作
            if (GameManager._instance.birds[0].isReleased == false)
            {
                GameManager._instance.birds[0].canMove = true;
            }
        }
    }


    public void Home()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }

    //pause动画播放完调用
    public void PauseAnimEnd()
    {
        Time.timeScale = 0;
    }

    //resume动画播放完调用
    public void ResumeAnimEnd()
    {
        button.SetActive(true);
    }

}
