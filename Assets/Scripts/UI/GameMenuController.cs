using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class GameMenuController : MonoBehaviour
{
    // Start is called before the first frame update
    private bool i = false;
    [SerializeField]
    private UnityEvent paused;
    [SerializeField]
    private UnityEvent unPaused;
    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1f;
        i = false;

    }
    void Start()
    {
        
    }
    public void unpause()
    {
        Time.timeScale = 1f;
        i = false;
        unPaused.Invoke();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //Debug.Log("Escape key was pressed");
            if (i == true)
            {
                Time.timeScale = 1f;
                i = false;
                unPaused.Invoke();
            }
            else
            {
                i = true;
                Time.timeScale = 0f;
                paused.Invoke();
            }
        }
    }
}
