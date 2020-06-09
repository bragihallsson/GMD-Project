using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishButtons : MonoBehaviour {
    //game
    public void restartLevel() {
        SceneManager.LoadScene("Game");
    }

    public void back() {
        SceneManager.LoadScene("MainMenu");
    }
}
