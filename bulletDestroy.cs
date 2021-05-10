using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletDestroy : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(collision.gameObject); // Yani ShipBullet objem BulletDestroyField objesiyle Temas ettiginde YOK ET.
    }
}
