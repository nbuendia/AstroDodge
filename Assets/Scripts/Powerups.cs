using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerups : MonoBehaviour
{
    [SerializeField]
    GameObject powerupParticle;
    [SerializeField]
    int powerupID;

    float powerupSpeed = 5f;

    // Update is called once per frame
    void Update()
    {
        //moves power-up down
        transform.Translate(Vector2.down * powerupSpeed * Time.deltaTime);

        //destroys power-up once it leaves game scene
        if (transform.position.y < -7f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.transform.GetComponent<Player>();
            if (player != null)
            {
                switch(powerupID)
                {
                    case 1:
                        player.KibbleHealthPowerup();
                        Instantiate(powerupParticle, transform.position, Quaternion.identity);
                        break;
                    case 2:
                        player.ColorSpeedPowerup();
                        Instantiate(powerupParticle, transform.position, Quaternion.identity);
                        break;
                    case 3:
                        player.ScalePowerup();
                        Instantiate(powerupParticle, transform.position, Quaternion.identity);
                        break;
                }
            }
        }

        Destroy(gameObject);
    }
}
