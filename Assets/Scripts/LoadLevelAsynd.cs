using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevelAsynd : MonoBehaviour {

	// Use this for initialization
	void Start () {
        //设置分辨率
        Screen.SetResolution(800, 500, false);
        //加载
        Invoke("Load", 2);
	}

    void Load()
    {
        SceneManager.LoadSceneAsync(1);//异步场景加载
    }
}
