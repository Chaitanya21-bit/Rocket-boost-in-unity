using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Collision : MonoBehaviour
{
    [SerializeField] float loadLevelDelay = 1f;
    [SerializeField] AudioClip success;
    [SerializeField] AudioClip crash;
    [SerializeField] ParticleSystem successParticle;
    [SerializeField] ParticleSystem crashParticle;

    AudioSource audioSource;
    bool isTransitioning = false;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(UnityEngine.Collision other)
    {
        if (isTransitioning)
            return;
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("friend");
                break;
            case "Finish":
                StartSuccessSequence();
                break ;
           
            default:
                StartCrashSequence();
                break;
        }
    }
    void StartSuccessSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        successParticle.Play();
        audioSource.PlayOneShot(success);
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", loadLevelDelay);
    }

    void StartCrashSequence()
    {
        isTransitioning = true ;
        audioSource.Stop();
        crashParticle.Play();
        audioSource.PlayOneShot(crash);
        GetComponent<Movement>().enabled = false;   

      Invoke("ReloadLevel", loadLevelDelay);
    }

    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextCurrentIndex = currentSceneIndex + 1;
        if (nextCurrentIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextCurrentIndex = 0;
        }
        SceneManager.LoadScene(nextCurrentIndex);
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

   

}
