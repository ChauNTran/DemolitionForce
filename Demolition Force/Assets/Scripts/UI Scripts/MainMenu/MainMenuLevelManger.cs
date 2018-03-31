using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenuLevelManger : MonoBehaviour {

    public void StartBTN()
    {
        SceneManager.LoadScene(1);
    }
}
