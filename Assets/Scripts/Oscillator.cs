using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    Vector3 startingPos;
    [SerializeField] Vector3 movementVector;
    [SerializeField] [Range(0, 1)] float movementFactor;  /* serializedfield veya range vermemize gerek yok
                                                           inspector'dan �al���p �al��mad���n� kontrol etmek i�in
                                                           verdim*/
    [SerializeField] float period = 2f; //objenin gidi� ve geli� periyodu. de�eri k���l�rse obje h�zlan�r


    void Start()
    {
        startingPos = transform.position;
    }
    void Update()
    {
        if(period <= Mathf.Epsilon) { return; }  /* float say�larda noktadan sonraki k�s�m �ok ayr�nt�l� olabilece�i i�in
                                                  burda ==0 demek �ok do�ru de�il. onun yerine s�f�ra kabul edilecebilecek
                                                  en k���k aral��� kapsayan mathf.epsillon kullanmak daha mant�kl�*/
        float cycles = Time.time / period;   // devaml� b�y�yecek olan cycle de�i�keni
        const float tau = Mathf.PI * 2;     // tau = 2 pi
        float rawSinWave = Mathf.Sin(tau * cycles);  // -1 ile 1 aras�nda s�rekli olarak de�i�ecek de�er
        movementFactor = (rawSinWave + 1) / 2;    // bu de�eri daha rahat okuyabilmemiz i�in 0 ile 1 aral���na �ektim
                                                  // bunu yapmasak da olurdu

        Vector3 offset = movementFactor * movementVector; 
        transform.position = startingPos + offset;
    }
}
