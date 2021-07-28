using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float nextSceneDelay = 2f;
    [SerializeField] float reloadSceneDelay = 1f;
    [SerializeField] AudioClip crashSound;
    [SerializeField] AudioClip successSound;
    [SerializeField] ParticleSystem crashParticles;
    [SerializeField] ParticleSystem successParticles;

    AudioSource audioSource;

    bool isTransitioning = false;
    bool collisionDisabled = false;


    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // CheatKeys();
    }

    void CheatKeys()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log("Sonraki levele geçiliyor!");
            Invoke("LoadNextLevel", 1);
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            collisionDisabled = !collisionDisabled;
            if(collisionDisabled)
            {
                Debug.Log("Ölümsüzlük açýldý!");
            }
            else if (!collisionDisabled)
            {
                Debug.Log("Ölümsüzlük kapandý!");
            }
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("Level yeniden yükleniyor!");
            Invoke("ReloadLevel", 1);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (isTransitioning || collisionDisabled) {return;}
        else
        {
            switch (other.gameObject.tag)
            {

                case "Friendly":
                    break;
                case "Fuel":
                    Debug.Log("You got the fuel <(*-*)>");
                    break;
                case "Obstacle":
                    StartCrashSequence();
                    break;
                case "Finish":
                    StartSuccessSequence();
                    break;
            }
        }
        
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        // þu olayý aklým almýyor. tamam clean kod, readability eyv. hepsini anlýyorum. haklýsýnýz.
        // ama þurda da ne gerek var deðiþkene filan yaz gitsin iþte *mk.
        // umarým gelecekte bu notu görünce "ne kadar salakmýþým" derim yoksa pýçaklarým bu kýliin kodcularý!
    }
    void LoadNextLevel()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if(nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }
    void StartCrashSequence()
    {
        isTransitioning = true;
        GetComponent<Movement>().enabled = false;
        audioSource.Stop();
        crashParticles.Play();
        audioSource.PlayOneShot(crashSound);
        Invoke("ReloadLevel", reloadSceneDelay);
    }
    void StartSuccessSequence()
    {
        isTransitioning = true;
        GetComponent<Movement>().enabled = false;
        audioSource.Stop();
        successParticles.Play();
        audioSource.PlayOneShot(successSound);
        Invoke("LoadNextLevel", nextSceneDelay);
    }
}
