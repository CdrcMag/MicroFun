using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Score : MonoBehaviour
{

    public static Score Instance;

    public TextMeshProUGUI text;
    public TextMeshProUGUI scoretxt;
    public TextMeshProUGUI high;

    public TextMeshProUGUI gameOver;

    public float temps = 0;
    public int score = 0;
    public bool status = true;


    private void Awake()
    {
        Instance = this;
        gameOver.gameObject.SetActive(false);
        high.text = "Highest score : " + PlayerPrefs.GetInt("Score");
    }

    bool once = true;

    private void Update()
    {
        if (status == false && once)
        {
            StartCoroutine(GameOver());
            //game over
        }

        if (!status)
            return;

        if (temps > 0)
        {
            temps -= Time.deltaTime;
            text.text = temps.ToString("00.00");

            scoretxt.text = score.ToString();
            
            status = true;
        }
        else
        {
            temps = 0;
            status = false;
        }

        
    }

    public void SaveHighScore()
    {
        PlayerPrefs.SetInt("Score", score);
    }

    public void setHStxt()
    {
        high.text = "Highest score : " + PlayerPrefs.GetInt("Score");
    }

    IEnumerator GameOver()
    {
        Destroy(GameObject.Find("Collectible(Clone)"));
        gameOver.gameObject.SetActive(true);
        parent.SetActive(false);
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(0);
        
    }

    public GameObject parent;

}
