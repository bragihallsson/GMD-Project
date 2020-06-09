using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Buttons : MonoBehaviour {

    private void Start() {
    }

    //main-menu
    public void easy() {
        SceneManager.LoadScene("Game");
    }
}
