using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public string level;
    public GameObject menupanel;
    //[SerializeField] private AudioListener playerlistener;

    private bool menu = false;
    // Start is called before the first frame update
    void Start()
    {
        ResumeGame();
        menu = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (menu == false)
            {
                PauseGame();
                menu = true;
            }
            else
            {
                ResumeGame();
                menu = false;
            }
        }
        else if (Input.GetKeyDown(KeyCode.M) && menu == true)
        {
            MainMenu();
        }
    }

    public void PauseGame()
    {
        menupanel.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0;
        AudioListener.volume = 0;
    }

    public void ResumeGame()
    {
        menupanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1;
        AudioListener.volume = 1;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(level);
    }
}
