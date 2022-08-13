using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameOver : MonoBehaviour
{
    private Animator anim;
    private Button button;
    private void Start()
    {
        anim = GetComponent<Animator>();
        button = GetComponent<Button>();
    }
    private void Update()
    {    
       
    
    }
    public void RestartButton()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(PlayerPrefs.GetInt("SavedScene"));
        }
    }
    public void ExittButton()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
