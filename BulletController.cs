using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float giveDamage = 10f;

    public void BumpDisappear()  // Mermi d��mana �arpt�g�nda  YOK OLMASINI �ST�YORUZ.
    {
        Destroy(gameObject); // yani ShipBullet objem YOK OLACAK.
    }

    public float damageGive() //ARTIK BULLET OBJES�NDEK� SCR�PTEN BU damageGive() METOTUNU �AGIRIP BU giveDamage'� OTOMAT�K D��MANIN CANINDAN D���CEZ.
    {
        return giveDamage;
    }


}
