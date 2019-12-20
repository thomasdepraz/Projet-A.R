using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoseSceneManager : MonoBehaviour
{
    public GameObject Exit;
    public GameObject Restart;
    public Text elapsedTime;
    private GameObject controller;
    // Start is called before the first frame update
    void Start()
    {
        controller = GameObject.FindGameObjectWithTag("Controller");
        Restart.SetActive(true);
        Restart.GetComponent<Button>().onClick.AddListener(_Restart);
        Exit.SetActive(true);
        Exit.GetComponent<Button>().onClick.AddListener(_ExitGame);
        elapsedTime.text = "Time survived : " + controller.GetComponent<TestController>().elapsedTime.ToString() + " seconds";
    }

    // Update is called once per frame
    void _Restart()
    {
        SceneManager.LoadScene("Menu");
    }
    void _ExitGame()
    {
        Application.Quit();
    }
}
