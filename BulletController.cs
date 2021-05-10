using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float giveDamage = 10f;

    public void BumpDisappear()  // Mermi düþmana çarptýgýnda  YOK OLMASINI ÝSTÝYORUZ.
    {
        Destroy(gameObject); // yani ShipBullet objem YOK OLACAK.
    }

    public float damageGive() //ARTIK BULLET OBJESÝNDEKÝ SCRÝPTEN BU damageGive() METOTUNU ÇAGIRIP BU giveDamage'ý OTOMATÝK DÜÞMANIN CANINDAN DÜÞÜCEZ.
    {
        return giveDamage;
    }


}
