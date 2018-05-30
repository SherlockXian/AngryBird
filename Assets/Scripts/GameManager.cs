
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour {

    public List<Bird> birds;//小鸟集合
    public List<Pig> pigs;//猪集合
    public static GameManager _instance;//让小鸟调用然后轮换小鸟
    private Vector3 originPos;//原始坐标

    //获取胜利和失败的UI
    public GameObject win;
    public GameObject lose;

    public GameObject[] stars;//星星数组

    private int starsNum = 0;

    private int totalNum = 5;

    private void Start()
    {
        Initialized();
    }

    private void Awake()
    {
        _instance = this;
        if(birds.Count>0)
            originPos = birds[0].transform.position;
    }

    //初始化小鸟
    public void Initialized()
    {
        for(int i=0;i<birds.Count;i++)
        {
            if(0==i)
            {
                //第一只小鸟
                birds[i].transform.position = originPos;
                birds[i].enabled = true;
                birds[i].sp.enabled = true;
                birds[i].canMove = true;
            }
            else
            {
                birds[i].enabled = false;
                birds[i].sp.enabled = false;
                birds[i].canMove = false;
            }
        }
    }

    public void NextBird()
    {
        //判定游戏逻辑
        if(pigs.Count>0)
        {
            if(birds.Count>0)
            {
                //下一只小鸟起飞
                Initialized();
            }
            else
            {
                //输了
                lose.SetActive(true);
            }
        }
        else
        {
            //获胜
            win.SetActive(true);
        }
    }

    public void ShowStars()
    {
        StartCoroutine("show");
    }

    IEnumerator show()
    {
        //协程,获胜后生成星星
        for (; starsNum < birds.Count + 1; starsNum++)
        {
            if(starsNum >= stars.Length)
            {
                break;
            }
            yield return new WaitForSeconds(0.2f);
            stars[starsNum].SetActive(true);
        }
        //print(starsNum);
    }

    //重玩
    public void Replay()
    {
        SaveData();
        SceneManager.LoadScene(2);
    }

    //返回主菜单
    public void Home()
    {
        SaveData();
        SceneManager.LoadScene(1);
    }

    //存储星星数量
    public void SaveData()
    {
        if(starsNum > PlayerPrefs.GetInt(PlayerPrefs.GetString("nowLevel")))
        {
            PlayerPrefs.SetInt(PlayerPrefs.GetString("nowLevel"), starsNum);
        }

        //存储所有的星星个数
        int sum = 0;
        for(int i=1;i<= totalNum;i++)
        {
            sum += PlayerPrefs.GetInt("level"+i.ToString());
        }

        PlayerPrefs.SetInt("totalNum",sum);
        print(PlayerPrefs.GetInt("totalNum"));
    }
}
