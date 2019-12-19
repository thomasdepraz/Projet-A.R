using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MenuUI : MonoBehaviour
{
    public GameObject Exit;
    
    public GameObject start;
    // Start is called before the first frame update
    void Start()
    {
        Exit.SetActive(true);
        Exit.GetComponent<Button>().onClick.AddListener(_ExitGame);
        start.SetActive(true);
        start.GetComponent<Button>().onClick.AddListener(_start);
        
    }

    // Update is called once per frame
    
    void _ExitGame()
    {
        Application.Quit();
    }
    void _start()
    {
        SceneManager.LoadScene("LUD_Scene");
    }
}
