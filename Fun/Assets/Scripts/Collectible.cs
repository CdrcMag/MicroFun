using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{

    public float value;
    public int score;
    private Animator anim;

    private bool a = false;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Manager manager = GameObject.Find("Manager").GetComponent<Manager>();
            manager.spawn();
            manager.audioSource.PlayOneShot(manager.toucher);
            Score.Instance.temps += value;
            Score.Instance.score += score;
            a = true;

            if(Score.Instance.score>PlayerPrefs.GetInt("Score"))
            {
                PlayerPrefs.SetInt("Score", Score.Instance.score);
                Score.Instance.SaveHighScore();
                Score.Instance.setHStxt();
            }

            StartCoroutine(die());
        }
    }
    

    IEnumerator die()
    {
        anim.SetTrigger("Death");
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
