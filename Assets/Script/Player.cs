using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.5f;
    [SerializeField]
    private GameObject laserPrefab;
    [SerializeField]
    private int _lives = 3;
    private SpawnManager _spawnManager;

    //variables for the cooldown system
    public float fireRate = 0.5f;
    private float _canFire = -1f; // start timing for the next firing.
    
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();

        if(_spawnManager == null)
        {
            Debug.LogError("The Spawn Manager is NULL");
        }
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            FireLaser();
        }
           

    }

    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
        transform.Translate(direction * _speed * Time.deltaTime);

        //We clamping the values of Y between 0 and 3.8F, using Mathf.clamp() to optimize the code.
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y,-3.8f,0), 0);


        if (transform.position.x > 11.2f)
        {
            transform.position = new Vector3(-11.2f, transform.position.y, 0);
        }
        else if (transform.position.x < -11.2f)
        {
            transform.position = new Vector3(11.2f, transform.position.y, 0);
        }

    }

    void FireLaser()
        {
            _canFire = Time.time + fireRate;
            Instantiate(laserPrefab, transform.position + new Vector3(0, 0.8f, 0), Quaternion.identity);
        }
    public void Damage()
    {
        _lives--;
        Debug.Log(_lives);
       if(_lives <1)
        {
            // communicate with the Spawn manager
            // let them know to stop spawning
            _spawnManager.OnPlayerDeath();
            Destroy(this.gameObject); 

        }
        
    }
    
}
