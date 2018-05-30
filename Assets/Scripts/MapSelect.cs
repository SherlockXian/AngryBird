using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapSelect : MonoBehaviour {

    public int starsNum = 0;
    private bool isSelect = false;

    public GameObject locks;
    public GameObject stars;

    public GameObject panel;
    public GameObject map;

    public Text starsText;

    public int startNum = 0;
    public int endNum = 0;
    private void Start()
    {
        //PlayerPrefs.DeleteAll();//数据清零
        //获得存储的星星总数
        if (PlayerPrefs.GetInt("totalNum",0)>=starsNum)
        {
            isSelect = true;
        }
        if(isSelect)
        {
            locks.SetActive(false);
            stars.SetActive(true);

            //TODO:text显示
            //先计算从开始关卡到结尾关卡总共获得了多少星星
            //print(startNum.ToString()+gameObject.name);
           // print(endNum.ToString()+gameObject.name);
            int counts = 0;
            for(int i=startNum;i<=endNum;i++)
            {
                counts += PlayerPrefs.GetInt("level" + i.ToString(),0);
            }
            starsText.text = counts.ToString() + "/" + (3*(endNum-startNum+1)).ToString();
        }
    }

    //鼠标点击
    public void Selected()
    {
        if(isSelect)
        {
            //是可选择的,隐藏掉map，同时显现出Panel
            panel.SetActive(true);
            map.SetActive(false);
        }
    }

    //返回
    public void panelSelect()
    {
        panel.SetActive(false);
        map.SetActive(true);
    }
}
