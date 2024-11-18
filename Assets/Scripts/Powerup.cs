using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    void Start()
    {
        Destroy(this.gameObject, 7);
    }

    void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D whatDidIHit)
    {
        if (whatDidIHit.tag == "Player")
        {
            GameObject.Find("GameManager").GetComponent<GameManager>().PlayPowerUp();
            Destroy(this.gameObject);
        }
    }
}
