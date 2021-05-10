using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    #region Properyties
    private float speed = 10f;
    public float thinsetting = 0.90f; // Oyunu çalýþtýrdýgýmda  Ship 'in bir kýsmý oyun dýþýnda gözüküyor onun için bu ince ayar özelligini tanýmladýk.
    public GameObject Bullet;
     public float BulletSpeed = 2f;
    public float FireRange = 2f; // 2 saniye sonra artýk bullet(mermi) atýlmýþ olacak.
    public float life = 400f;
    float xmin ;
    float xmax ;

    public AudioClip FireVoice;
    public AudioClip DeathVoice;

    #endregion

    void Start()
    {
       // Ekran 16:9 dan 5:4 oldugunda Ship objesi  ekrandan çýkabiliyor.BUNU DÝNAMÝK HALE GETÝRMEK ÝÇÝN ÞU KODLARI YAZICAZ : 
                                                                              
        float distance = transform.position.z - Camera.main.transform.position.z; 
        Vector3 leftend = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance)); //  0 kullanýcýnýn  ekranýnýn en sol kýsmýný temsil ediyor.
        Vector3 rightend = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance)); // 1 Kullanýcýnýn ekranýnýn en sað kýsmýný temsil ediyor.     
        xmin = leftend.x + thinsetting ;
        xmax = rightend.x - thinsetting ;
    }


    void Fire()
    {
        GameObject ShipBullet = Instantiate(Bullet, transform.position+ new Vector3(0,1.1f,0), Quaternion.identity) as GameObject;
        ShipBullet.GetComponent<Rigidbody2D>().velocity = new Vector3(0, BulletSpeed, 0);
        AudioSource.PlayClipAtPoint(FireVoice, transform.position); // PlayClipAtPoint bu noktada bu blibi çal demektir.
    }
        
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            InvokeRepeating("Fire", 0.0001f, FireRange);
        }

        if(Input.GetKeyUp(KeyCode.Space)) // GetKeyUp : O tuþtan elini kaldýrdýgýmýz anda devereye giren bir metottur.
        {
            CancelInvoke("Fire");// CancelInvoke : Invoke'u iptal et anlamýna gelir.
        }

        float newX = Mathf.Clamp(transform.position.x,xmin, xmax);
        transform.position = new Vector3(newX, transform.position.y, transform.position.z); 

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            // transform.position += new Vector3(-speed * Time.deltaTime, 0, 0);
             transform.position += Vector3.left * speed * Time.deltaTime; // Vector3.left=(-1,0,0) gibi iþlev görür.
        }           
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            //  transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
            transform.position += Vector3.right * speed * Time.deltaTime;
        }        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        BulletController bumpBullet = collision.gameObject.GetComponent<BulletController>(); // baþka bir class'ý çagýrdýk iyi ANLA BU KODU.

        /* ************* ÇOK ÖNEMLÝÝÝÝ *******  Benim Düþmanýma bullet TEMAS ETTÝGÝ ÝÇÝN SEN ONUN BulletController ismindeki SCRÝPTÝNÝ AL
        VE bumpBullet ismindeki deðiþkenime ATAMIÞ OLDUM. */

        if (bumpBullet)
        {
            bumpBullet.BumpDisappear();

            life -= bumpBullet.damageGive(); // Düþmanýn 100f lik canýndan merminin hasarýný(10f) i çýkardýk.
            if (life <= 0)
            {
                Destroy(gameObject);
                AudioSource.PlayClipAtPoint(DeathVoice, transform.position);
            }
        }
    }
}
