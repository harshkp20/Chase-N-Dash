using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;


public class EnemyPatrol : MonoBehaviour
{
    public GameObject Player;
    public float Speed;
    public GameObject Lose;
    public TextMeshProUGUI Text;
    public GameObject Score;
    public AudioSource MainAudio;
    public GameObject MainMenu;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        if(PlayerPrefs.GetInt("HaveRestarted",0) !=1)
        {
            Time.timeScale=0f;
        }
        else
        {
            MainAudio.Play();
            MainMenu.SetActive(false);
            PlayerPrefs.SetInt("HaveRestarted",0);
        } 
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = Vector3.MoveTowards(this.transform.position , Player.transform.position,Speed*Time.deltaTime);
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Player.SetActive(false);
            Text.text = Player.GetComponent<PlayerMovement>().clock.ToString();
            Score.SetActive(false);
            Lose.SetActive(true);
            MainAudio.Stop();
        }        
    }

    public void Restart()
    {
        PlayerPrefs.SetInt("HaveRestarted",1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GameStart()
    {
        Time.timeScale=1f;
        MainAudio.Play();
        MainMenu.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
