using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{

    public GameObject menu;
    public GameObject parent;

    public GameObject button;

    public Transform pointA;
    public Transform pointB;
    public Transform pointC;

    public Transform player;
    public Transform targeter;
    public Transform nextTransform;

    public float targeterRotationSpeed;

    public bool moving = false;
    public bool shaking = false;

    public Vector3 _direction;

    public float speed = 12;

    public AudioSource audioSource;
    public AudioClip dash;

    public ParticleSystem particlesPrefab;

    public Transform behind;


    private void Awake()
    {
        parent.SetActive(false);
        menu.SetActive(true);

        originalRot = menu.transform.rotation;

        player.position = pointA.position;
    }

    private void Update()
    {
        targeter.transform.Rotate(new Vector3(0, 0, 1), targeterRotationSpeed * Time.deltaTime);
    }

    public void ButtonAction()
    {
        if (!moving && nextTransform)
        {
            _direction = (nextTransform.position - player.transform.position).normalized;

            //CameraShake
            shaking = true;
            StartCoroutine(shake());

            float rot_z = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
            player.rotation = Quaternion.Euler(0f, 0f, rot_z);
            audioSource.PlayOneShot(dash);
            Instantiate(particlesPrefab, behind.position, behind.rotation);
            moving = true;
            StartCoroutine(move(nextTransform));
        }
    }

    IEnumerator move(Transform nextPoint)
    {
        while (moving)
        {
            float distance = Vector3.Distance(player.position, nextPoint.position);

            if (distance > 0.01f)
            {
                player.transform.position = Vector3.MoveTowards(player.position, nextPoint.position, speed * Time.deltaTime);
            }
            else
            {
                moving = false;

            }

            yield return null;

        }

    }

    private float x;
    private float y;
    private float z;

    public float time2wait;
    private float timerA;

    public Quaternion originalRot;

    IEnumerator shake()
    {
        while (shaking)
        {
            x = Random.Range(-1, 1);
            y = Random.Range(-1, 1);
            z = Random.Range(-1, 1);
            timerA += Time.deltaTime;
            if (timerA < time2wait)
                menu.transform.rotation = Quaternion.Euler(originalRot.x + x, originalRot.y + y, originalRot.z + z);
            else
            {
                shaking = false;
                timerA = 0;
                menu.transform.rotation = originalRot;
            }



            yield return null;
        }


    }

    public void launchGame()
    {
        parent.SetActive(true);
        menu.SetActive(false);
        GetComponent<Score>().enabled = true;
        GetComponent<Manager>().enabled = true;
        enabled = false;
        
    }

}
