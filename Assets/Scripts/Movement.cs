using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rb;
    AudioSource audioSource;

    [SerializeField] float mainThrust = 1000f;
    [SerializeField] float rotateThrust = 1000f;

    [SerializeField] AudioClip mainEngineSound;

    [SerializeField] ParticleSystem mainBoosterParticles;
    [SerializeField] ParticleSystem LeftBoosterParticles;
    [SerializeField] ParticleSystem RightBoosterParticles;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            RotationToLeft();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            RotationToRight();
        }
        else
        {
            StopRotation();
        }
    }

    private void StartThrusting()
    {
        rb.AddRelativeForce(mainThrust * Time.deltaTime * Vector3.up);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngineSound);
        }
        if (!mainBoosterParticles.isPlaying)
        {
            mainBoosterParticles.Play();
        }
    }

    private void StopThrusting()
    {
        audioSource.Stop();
        mainBoosterParticles.Stop();
    }



    private void RotationToRight()
    {
        ApplyRotation(-rotateThrust);
        if (!LeftBoosterParticles.isPlaying)
        {
            LeftBoosterParticles.Play();
        }
    }

    private void RotationToLeft()
    {
        ApplyRotation(rotateThrust);
        if (!RightBoosterParticles.isPlaying)
        {
            RightBoosterParticles.Play();
        }
    }
    private void StopRotation()
    {
        RightBoosterParticles.Stop();
        LeftBoosterParticles.Stop();
    }


    private void ApplyRotation(float rotationThrust)
    {
        transform.Rotate(Vector3.forward * Time.deltaTime * rotationThrust);
    }
}
