using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    #region Properyties
    private float speed = 10f;
    public float thinsetting = 0.90f; // Oyunu �al��t�rd�g�mda  Ship 'in bir k�sm� oyun d���nda g�z�k�yor onun i�in bu ince ayar �zelligini tan�mlad�k.
    public GameObject Bullet;
     public float BulletSpeed = 2f;
    public float FireRange = 2f; // 2 saniye sonra art�k bullet(mermi) at�lm�� olacak.
    public float life = 400f;
    float xmin ;
    float xmax ;

    public AudioClip FireVoice;
    public AudioClip DeathVoice;

    #endregion

    void Start()
    {
       // Ekran 16:9 dan 5:4 oldugunda Ship objesi  ekrandan ��kabiliyor.BUNU D�NAM�K HALE GET�RMEK ���N �U KODLARI YAZICAZ : 
                                                                              
        float distance = transform.position.z - Camera.main.transform.position.z; 
        Vector3 leftend = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance)); //  0 kullan�c�n�n  ekran�n�n en sol k�sm�n� temsil ediyor.
        Vector3 rightend = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance)); // 1 Kullan�c�n�n ekran�n�n en sa� k�sm�n� temsil ediyor.     
        xmin = leftend.x + thinsetting ;
        xmax = rightend.x - thinsetting ;
    }


    void Fire()
    {
        GameObject ShipBullet = Instantiate(Bullet, transform.position+ new Vector3(0,1.1f,0), Quaternion.identity) as GameObject;
        ShipBullet.GetComponent<Rigidbody2D>().velocity = new Vector3(0, BulletSpeed, 0);
        AudioSource.PlayClipAtPoint(FireVoice, transform.position); // PlayClipAtPoint bu noktada bu blibi �al demektir.
    }
        
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            InvokeRepeating("Fire", 0.0001f, FireRange);
        }

        if(Input.GetKeyUp(KeyCode.Space)) // GetKeyUp : O tu�tan elini kald�rd�g�m�z anda devereye giren bir metottur.
        {
            CancelInvoke("Fire");// CancelInvoke : Invoke'u iptal et anlam�na gelir.
        }

        float newX = Mathf.Clamp(transform.position.x,xmin, xmax);
        transform.position = new Vector3(newX, transform.position.y, transform.position.z); 

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            // transform.position += new Vector3(-speed * Time.deltaTime, 0, 0);
             transform.position += Vector3.left * speed * Time.deltaTime; // Vector3.left=(-1,0,0) gibi i�lev g�r�r.
        }           
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            //  transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
            transform.position += Vector3.right * speed * Time.deltaTime;
        }        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        BulletController bumpBullet = collision.gameObject.GetComponent<BulletController>(); // ba�ka bir class'� �ag�rd�k iyi ANLA BU KODU.

        /* ************* �OK �NEML���� *******  Benim D��man�ma bullet TEMAS ETT�G� ���N SEN ONUN BulletController ismindeki SCR�PT�N� AL
        VE bumpBullet ismindeki de�i�kenime ATAMI� OLDUM. */

        if (bumpBullet)
        {
            bumpBullet.BumpDisappear();

            life -= bumpBullet.damageGive(); // D��man�n 100f lik can�ndan merminin hasar�n�(10f) i ��kard�k.
            if (life <= 0)
            {
                Destroy(gameObject);
                AudioSource.PlayClipAtPoint(DeathVoice, transform.position);
            }
        }
    }
}
