using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemysPozition : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 1);
        // K�re �izdik ve merkezini objemiz nerdeyse  bulundugun YERDEK� kendi boyutunun MERKEZ� kabul ettik ve yar�cap� 1 olarak belirledik.

        /* NOT : �imdi sahnede k�reyi olu�turdun ve un�ty k�sm�nda prefab� sahneye atarak 5 tane daha Enemy Pozitionu olu�turdun.
                 bu k�relerin i�ine Enemy'leri atabilmem i�in EnemyCameOutLocatio Scriptine gidip GEREKL� KODLARI YAZMAM GEREK�YOR. */ 
    }
}
