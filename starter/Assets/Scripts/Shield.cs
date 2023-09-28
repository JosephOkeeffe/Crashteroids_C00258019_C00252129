using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public int health = 0;
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(true);
        health = 3;

        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            gameObject.transform.position = player.transform.position;
        }

        if (health <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    public void LoseHealth(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            //gameObject.SetActive(false);
            Destroy(gameObject);
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Asteroid")
        {
            Destroy(collision.gameObject);
            LoseHealth(1);
            //gameObject.SetActive(false);

        }

    }


}
