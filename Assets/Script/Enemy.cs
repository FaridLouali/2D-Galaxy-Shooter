using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject enemyPrefab;
    [SerializeField]
    private float _speed = 4f;
   

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    { 
        transform.Translate(Vector3.down *_speed * Time.deltaTime);

        if (transform.position.y < -5f)
        {
            float randomX = Random.Range(-8.5f, 8.5f);
            transform.position = new Vector3(randomX, 5f, 0);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
            {
            Player player = other.transform.GetComponent<Player>();

            if(player != null)// (NULL CHECKING) USE IT OFTEN!!!!
            {
                player.Damage();
            }

            Destroy(this.gameObject);
            }

        if (other.tag == "Laser")
            {
                Destroy(other.gameObject);
                Destroy(this.gameObject);
            }         
    }
}


