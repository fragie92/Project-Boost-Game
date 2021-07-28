using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    Vector3 startingPos;
    [SerializeField] Vector3 movementVector;
    [SerializeField] [Range(0, 1)] float movementFactor;  /* serializedfield veya range vermemize gerek yok
                                                           inspector'dan çalýþýp çalýþmadýðýný kontrol etmek için
                                                           verdim*/
    [SerializeField] float period = 2f; //objenin gidiþ ve geliþ periyodu. deðeri küçülürse obje hýzlanýr


    void Start()
    {
        startingPos = transform.position;
    }
    void Update()
    {
        if(period <= Mathf.Epsilon) { return; }  /* float sayýlarda noktadan sonraki kýsým çok ayrýntýlý olabileceði için
                                                  burda ==0 demek çok doðru deðil. onun yerine sýfýra kabul edilecebilecek
                                                  en küçük aralýðý kapsayan mathf.epsillon kullanmak daha mantýklý*/
        float cycles = Time.time / period;   // devamlý büyüyecek olan cycle deðiþkeni
        const float tau = Mathf.PI * 2;     // tau = 2 pi
        float rawSinWave = Mathf.Sin(tau * cycles);  // -1 ile 1 arasýnda sürekli olarak deðiþecek deðer
        movementFactor = (rawSinWave + 1) / 2;    // bu deðeri daha rahat okuyabilmemiz için 0 ile 1 aralýðýna çektim
                                                  // bunu yapmasak da olurdu

        Vector3 offset = movementFactor * movementVector; 
        transform.position = startingPos + offset;
    }
}
