using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemysPozition : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 1);
        // Küre çizdik ve merkezini objemiz nerdeyse  bulundugun YERDEKÝ kendi boyutunun MERKEZÝ kabul ettik ve yarýcapý 1 olarak belirledik.

        /* NOT : Þimdi sahnede küreyi oluþturdun ve unýty kýsmýnda prefabý sahneye atarak 5 tane daha Enemy Pozitionu oluþturdun.
                 bu kürelerin içine Enemy'leri atabilmem için EnemyCameOutLocatio Scriptine gidip GEREKLÝ KODLARI YAZMAM GEREKÝYOR. */ 
    }
}
