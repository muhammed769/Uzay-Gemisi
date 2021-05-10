using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject bullet;
    public float bulletSpeed = 145f;
    public float life = 100f;
    public float secondBulletFire = 0.6f;
    // Olasýlýk her zaman 0 ÝLE 1 ARASINDADIR.Yani  0 ile 1 arasýndaki her þey olasýlýktýr.

    public int ScoreValue = 200;
    public ScoreController scoreController;

    public AudioClip FireVoice;
    public AudioClip DeathVoice;

    private void Start()
    {
       scoreController = GameObject.Find("Score").GetComponent<ScoreController>(); // Gameobject Hierarcy'deki objeler manasýna gelir.
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        BulletController bumpBullet = collision.gameObject.GetComponent<BulletController>(); // baþka bir class'ý çagýrdýk iyi ANLA BU KODU.

        /* ************* ÇOK ÖNEMLÝÝÝÝ *******  Benim Düþmanýma bullet TEMAS ETTÝGÝ ÝÇÝN SEN ONUN BulletController ismindeki SCRÝPTÝNÝ AL
        VE bumpBullet ismindeki deðiþkenime ATAMIÞ OLDUM. */

        if(bumpBullet)
        {
            bumpBullet.BumpDisappear();

            life -= bumpBullet.damageGive(); // Düþmanýn 100f lik canýndan merminin hasarýný(10f) i çýkardýk.
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
       // Random.value = Random.value 0 ile 1 arasýnda rastgele bir deðer üretir.
    }

    void Fire()
    {
        Vector3 BulletStartPosition = transform.position + new Vector3(0, -0.8f, 0);
        // Düþmanýn mermisinin baþlangýç pozisyonu Düþmanýn pozisyondan 0.8 uzakta olsunký düþman  mermiyi atarken kendi kendini öldürmesin.

        GameObject Enemybullet = Instantiate(bullet, BulletStartPosition, Quaternion.identity) as GameObject;
        Enemybullet.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -bulletSpeed);
        AudioSource.PlayClipAtPoint(FireVoice, transform.position);
    }


}
