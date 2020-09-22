using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    
    public float speed;
    public float targeterRotationSpeed;

    private bool moving = false;

    //Shaking camera
    private bool shaking = false;
    private Quaternion originalRot;
    private float x;
    private float y;
    private float z;
    public float time2wait;
    private float timerA;
    
    public Transform player;
    public Transform nextTransform;
    public Transform behind;
    public Transform parent;
    public Transform targeter;

    public GameObject particlesPrefab;
    public GameObject colPrefab;

    //values for internal use
    private Quaternion _lookRotation;
    private Vector3 _direction;

    public AudioSource audioSource;
    public AudioClip dash;
    public AudioClip toucher;

    public List<Transform> points;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        player.position = points[0].position;

        Instantiate(colPrefab, points[Random.Range(0,7)].position, Quaternion.identity);

        originalRot = parent.rotation;

        
    }

    private void Update()
    {


        targeter.transform.Rotate(new Vector3(0, 0, 1), targeterRotationSpeed * Time.deltaTime);

    }

    public void ButtonAction()
    {
        if(!moving && nextTransform)
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
            StartCoroutine(move(nextTransform.position));
        }
    }

    IEnumerator move(Vector3 nextPoint)
    {
        while(moving)
        {
            float distance = Vector3.Distance(player.position, nextPoint);

            if(distance > 0.0001f)
            {
                player.transform.position = Vector3.MoveTowards(player.position, nextPoint, speed * Time.deltaTime);
            }
            else
            {
                moving = false;
            }

            yield return null;
        }
        
    }

    

    IEnumerator shake()
    {
        while(shaking)
        {
            x = Random.Range(-1, 1);
            y = Random.Range(-1, 1);
            z = Random.Range(-1, 1);
            timerA += Time.deltaTime;
            if (timerA < time2wait)
                parent.rotation = Quaternion.Euler(originalRot.x + x, originalRot.y + y, originalRot.z + z);
            else
            {
                shaking = false;
                timerA = 0;
                parent.rotation = originalRot;
            }
                


            yield return null;
        }

        
    }

    public void spawn()
    {
        Instantiate(colPrefab, points[Random.Range(0, points.Count)].position, Quaternion.identity);
    }

    public void quitGame()
    {
        Application.Quit(0);
    }
}
