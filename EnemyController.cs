using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject bullet;
    public float bulletSpeed = 145f;
    public float life = 100f;
    public float secondBulletFire = 0.6f;
    // Olas�l�k her zaman 0 �LE 1 ARASINDADIR.Yani  0 ile 1 aras�ndaki her �ey olas�l�kt�r.

    public int ScoreValue = 200;
    public ScoreController scoreController;

    public AudioClip FireVoice;
    public AudioClip DeathVoice;

    private void Start()
    {
       scoreController = GameObject.Find("Score").GetComponent<ScoreController>(); // Gameobject Hierarcy'deki objeler manas�na gelir.
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        BulletController bumpBullet = collision.gameObject.GetComponent<BulletController>(); // ba�ka bir class'� �ag�rd�k iyi ANLA BU KODU.

        /* ************* �OK �NEML���� *******  Benim D��man�ma bullet TEMAS ETT�G� ���N SEN ONUN BulletController ismindeki SCR�PT�N� AL
        VE bumpBullet ismindeki de�i�kenime ATAMI� OLDUM. */

        if(bumpBullet)
        {
            bumpBullet.BumpDisappear();

            life -= bumpBullet.damageGive(); // D��man�n 100f lik can�ndan merminin hasar�n�(10f) i ��kard�k.
            if(life<=0)
            {
                Destroy(gameObject);
                AudioSource.PlayClipAtPoint(DeathVoice, transform.position);
                scoreController.increaseScore(ScoreValue);
            }
        }
    }

    private void Update()
    {
        float bumpPossibility = Time.deltaTime * secondBulletFire;
        if(Random.value<bumpPossibility)
        {
            Fire();
        }
       // Random.value = Random.value 0 ile 1 aras�nda rastgele bir de�er �retir.
    }

    void Fire()
    {
        Vector3 BulletStartPosition = transform.position + new Vector3(0, -0.8f, 0);
        // D��man�n mermisinin ba�lang�� pozisyonu D��man�n pozisyondan 0.8 uzakta olsunk� d��man  mermiyi atarken kendi kendini �ld�rmesin.

        GameObject Enemybullet = Instantiate(bullet, BulletStartPosition, Quaternion.identity) as GameObject;
        Enemybullet.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -bulletSpeed);
        AudioSource.PlayClipAtPoint(FireVoice, transform.position);
    }


}
