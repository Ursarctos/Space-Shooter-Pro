using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float _powerupSpeed = 3;
    [SerializeField]
    private int powerupID;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    { 
        // move down speed of 3 (adjustable in inspector)
        transform.Translate(Vector3.down * _powerupSpeed * Time.deltaTime);

       
        // after leaving screen, destroy this object
        if (transform.position.y <= -5.0f)
        {
            Destroy(this.gameObject);
        }

        //OnTriggerCollision
        //destroy when collected
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Player player = collision.transform.GetComponent<Player>();
            if (player != null)
            {
                switch (powerupID)
                {
                    case 0:
                        player.TripleShot();
                        break;
                    case 1:
                        Debug.Log("Speed Boost Collected");
                        player.SpeedBoost();
                        break;
                    case 2:
                        Debug.Log("Shield Collected");
                        player.ShieldActive();
                        break;
                    default:
                        Debug.Log("Default Case");
                        break;
                }
            }
            Destroy(this.gameObject);
        }
    }
}
