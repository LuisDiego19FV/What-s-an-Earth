using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scp_on_hit_destroy : MonoBehaviour
{
    private float run_time;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        run_time += Time.deltaTime;

        if (run_time > 2.50f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy" && !other.isTrigger)
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
