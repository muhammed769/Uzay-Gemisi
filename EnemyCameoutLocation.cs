using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCameoutLocation : MonoBehaviour
{
    public GameObject EnemyPrefab;
    public float width;
    public float height;
    private float speed = 5f;

    private bool rightAction = true;
    private float xmin;
    private float xmax;
    public float createDelayTime = 0.8f;


    void Start()
    {
        float distance = transform.position.z - Camera.main.transform.position.z;
        Vector3 leftend = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
        Vector3 rightend = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));
        xmax = rightend.x;
        xmin = leftend.x;
       EnemysOneOneCreate();
       // EnemysCreate();

        /*foreach(Transform child in transform)
        {
             GameObject enemy = Instantiate(EnemyPrefab, child.transform.position, Quaternion.identity) as GameObject;
             enemy.transform.parent = child; 
            // Artýk gizmo sayesinde oluþturdugum  5 tane Kürenin içine 5 tane EnemyPosition objem gelmiþ oldu.                                           
        }*/

       // GameObject enemy = Instantiate(EnemyPrefab, new Vector3(3.5f, 2.5f, 0), Quaternion.identity) as GameObject;
        //enemy.transform.parent = transform; // bu 2 kodla dümaný oluþturduk ve oyun esnasýnda çýkan düþmaný Enemy Came out Location objesinin altýna eklenmiþ oldu
                                            // Böylece daha Hierarcy'de daha düzenli bir görünüm elde edilmiþ oldu.       
    }

    void EnemysCreate()
    {
        foreach(Transform child in transform)
        {
             GameObject enemy = Instantiate(EnemyPrefab, child.transform.position, Quaternion.identity) as GameObject;
             enemy.transform.parent = child; 
            // Artýk gizmo sayesinde oluþturdugum  5 tane Kürenin içine 5 tane EnemyPosition objem gelmiþ oldu.                                           
        }
    }

    void EnemysOneOneCreate()
    {
        Transform ProperPosition = ThenProperPosition();
        if (ProperPosition)
        {

            GameObject enemy = Instantiate(EnemyPrefab, ProperPosition.transform.position, Quaternion.identity) as GameObject;
            enemy.transform.parent = ProperPosition;
        }

        if (ThenProperPosition())
        {
            Invoke("EnemysOneOneCreate", createDelayTime);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position , new Vector3(width,height));
    }


    void Update()
    {
        if(rightAction)
        {
            transform.position += speed * Vector3.right  * Time.deltaTime; //  transform.position += new Vector3(speed * Time.deltaTime,0);
        }
        else
        {
            transform.position += speed * Vector3.left  * Time.deltaTime;
        }

         float rightLimit = transform.position.x + 0.5f * width;
        // EnemyCameoutLocation objem sað sýnýra geldiginde sola dogru hareket etmesini saðlayan kod blogudur.
        float leftLimit = transform.position.x - 0.5f * width;
        // EnemyCameoutLocation objem sol sýnýra geldiginde saða dogru hareket etmesini saðlayan kod blogudur.

        if(rightLimit>xmax )
        {
            rightAction = false;
        }
        else if(leftLimit<xmin)
        {
            rightAction = true;
        }

        if (DidAllEnemysDeath())
        {
            EnemysOneOneCreate();
            //EnemysCreate();
        }
    }

  Transform ThenProperPosition () // Ölen düþmanýn pozisyonun tutucaz  ve düþman tekrar yaratýldýgýnda o konumda yaratýlmasýný saðlýcaz.
    {
        foreach (Transform ChildPosition in transform)
        {
            if (ChildPosition.childCount == 0) 
            {
                return ChildPosition;
            }
        }
        return null;
    }
  bool DidAllEnemysDeath()  // Düþmanlarýnýn tamamýnýn ölü ölmediginin kontrol edilmesi için býr metot oluþturduk.
    {
        foreach(Transform ChildPosition in transform)
        {
            if(ChildPosition.childCount>0) // child oyun içerisinde var ise :
            {
                return false;
            }
        }
        return true;
    }


}
