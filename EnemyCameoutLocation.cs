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
            // Art�k gizmo sayesinde olu�turdugum  5 tane K�renin i�ine 5 tane EnemyPosition objem gelmi� oldu.                                           
        }*/

       // GameObject enemy = Instantiate(EnemyPrefab, new Vector3(3.5f, 2.5f, 0), Quaternion.identity) as GameObject;
        //enemy.transform.parent = transform; // bu 2 kodla d�man� olu�turduk ve oyun esnas�nda ��kan d��man� Enemy Came out Location objesinin alt�na eklenmi� oldu
                                            // B�ylece daha Hierarcy'de daha d�zenli bir g�r�n�m elde edilmi� oldu.       
    }

    void EnemysCreate()
    {
        foreach(Transform child in transform)
        {
             GameObject enemy = Instantiate(EnemyPrefab, child.transform.position, Quaternion.identity) as GameObject;
             enemy.transform.parent = child; 
            // Art�k gizmo sayesinde olu�turdugum  5 tane K�renin i�ine 5 tane EnemyPosition objem gelmi� oldu.                                           
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
        // EnemyCameoutLocation objem sa� s�n�ra geldiginde sola dogru hareket etmesini sa�layan kod blogudur.
        float leftLimit = transform.position.x - 0.5f * width;
        // EnemyCameoutLocation objem sol s�n�ra geldiginde sa�a dogru hareket etmesini sa�layan kod blogudur.

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

  Transform ThenProperPosition () // �len d��man�n pozisyonun tutucaz  ve d��man tekrar yarat�ld�g�nda o konumda yarat�lmas�n� sa�l�caz.
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
  bool DidAllEnemysDeath()  // D��manlar�n�n tamam�n�n �l� �lmediginin kontrol edilmesi i�in b�r metot olu�turduk.
    {
        foreach(Transform ChildPosition in transform)
        {
            if(ChildPosition.childCount>0) // child oyun i�erisinde var ise :
            {
                return false;
            }
        }
        return true;
    }


}
