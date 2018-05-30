using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour {

    public bool isSelect = false;
    public Sprite levelBG;
    private Image image;

    public GameObject[] stars;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    private void Start()
    {
        if(transform.parent.GetChild(0).name == gameObject.name)
        {
            //第一关不锁
            isSelect = true;
        }
        else
        {
            //不是第一关则判断当前关卡是否可以选择，前一关通关后才可以选择
            int beforeNum = int.Parse(gameObject.name) - 1;//前一关
            if(PlayerPrefs.GetInt("level"+beforeNum.ToString()) > 0)
            {
                isSelect = true;
            }
        }

        if(isSelect)
        {
            image.overrideSprite = levelBG;
            transform.Find("num").gameObject.SetActive(true);
            
            //获取当前关卡的星星数量
            int count = PlayerPrefs.GetInt("level" + gameObject.name);
            //print(count + "count"+ gameObject.name);
            if(count > 0)
            {
                for(int i=0 ; i < count;i++)
                {
                    //print(i + "i"+ gameObject.name);
                    stars[i].SetActive(true);
                }
            }
        }
    }

    public void Selected()
    {
        if(isSelect)
        {
            //关卡可选择
            PlayerPrefs.SetString("nowLevel","level"+gameObject.name);//存储当前关卡
            SceneManager.LoadScene(2);//加载关卡
        }
    }
}
