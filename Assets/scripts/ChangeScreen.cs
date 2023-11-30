using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScreen : MonoBehaviour
{
    public void MoveToScene(string ecra)
    {
        SceneManager.LoadScene(ecra);
        Time.timeScale = 1;
    }
}
