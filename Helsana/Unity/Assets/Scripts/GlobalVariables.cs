using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class GlobalVariables {

    //Current respawn Position
    public static GameObject respawnPoint;
    public static int totalCoins = 0;

    public static void ReloadScene()
    {
        respawnPoint = null;
        totalCoins = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
