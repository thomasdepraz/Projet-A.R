using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class TestUi : MonoBehaviour
{
    public GameObject Exit;
    public GameObject Restart;
    public GameObject Logo;
    public GameObject Return;
    public GameObject Menu;
    
    // Start is called before the first frame update
    void Start()
    {
        Menu.SetActive(true);
        Menu.GetComponent<Button>().onClick.AddListener(_MenuOpen);
    }

    // Update is called once per frame
    
    void _ExitGame()
    {
        Application.Quit();
    }
    void _MenuOpen()
    {
        Menu.SetActive(false);
        Menu.GetComponent<Button>().onClick.RemoveListener(_MenuOpen);
        Exit.SetActive(true);
        Exit.GetComponent<Button>().onClick.AddListener(_ExitGame);
        Restart.SetActive(true);
        Restart.GetComponent<Button>().onClick.AddListener(_Restart);
        Logo.SetActive(true);
        Return.SetActive(true);
        Return.GetComponent<Button>().onClick.AddListener(_Return);
    }
    void _Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    void _Return()
    {
        Menu.SetActive(true);
        Menu.GetComponent<Button>().onClick.AddListener(_MenuOpen);
        Exit.SetActive(false);
        Exit.GetComponent<Button>().onClick.RemoveListener(_ExitGame);
        Restart.SetActive(false);
        Restart.GetComponent<Button>().onClick.RemoveListener(_Restart);
        Logo.SetActive(false);
        Return.SetActive(false);
        Return.GetComponent<Button>().onClick.RemoveListener(_Return);
    }
}
