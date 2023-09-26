using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    int health = 0;
    [SerializeField] GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(true);
        health = 3;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = player.transform.position;
        if(health <= 0)
        {
            gameObject.SetActive(false);
        }
    }

   public void LoseHealth(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            gameObject.SetActive(false);
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Asteroids")
        {
            Destroy(collision.gameObject);
            LoseHealth(1);
            
        }
      
    }


}
