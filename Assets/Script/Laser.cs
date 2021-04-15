using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private float laserSpeed = 10.0f;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * laserSpeed * Time.deltaTime);

        if(transform.position.y > 7f )
            {
            Destroy(this.gameObject);
        }
    }
}
