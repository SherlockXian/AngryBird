using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Load : MonoBehaviour {

    private void Awake()
    {
       Instantiate(Resources.Load(PlayerPrefs.GetString("nowLevel")));
    }
}
