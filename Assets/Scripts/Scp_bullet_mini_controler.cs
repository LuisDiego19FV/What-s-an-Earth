using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scp_bullet_mini_controler : MonoBehaviour
{

    private float min;
    private float run_time;
    private bool redirect;
    private bool minimize;
    private GameObject target;
    
    // Start is called before the first frame update
    void Start()
    {
        min = 1;
        minimize = false;
        redirect = false;
    }

    // Update is called once per frame
    void Update()
    {
        run_time += Time.deltaTime;

        if (minimize)
        {
            float obj_s = target.transform.localScale.x;
            float self_s = transform.localScale.x;

            if (obj_s < 0.1f)
            {
                Destroy(target);
                Destroy(gameObject);
            }

            target.transform.localScale = new Vector3(obj_s - min * Time.deltaTime, 
                obj_s - min * Time.deltaTime, obj_s - min * Time.deltaTime);
            transform.localScale = new Vector3(self_s + min * 3 * Time.deltaTime,
                self_s + min * 2 * Time.deltaTime, self_s + min * 3 * Time.deltaTime);
            GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            transform.position = target.transform.position;
        }
        else if (run_time > 3f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!minimize && other.tag == "Enemy" && !other.isTrigger)
        {
            target = other.gameObject;
            minimize = true;
        }

        if (!minimize && !redirect  && other.tag == "Enemy" && other.isTrigger)
        {
            GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            transform.LookAt(other.gameObject.transform);
            GetComponent<Rigidbody>().AddForce(transform.forward * 600f);
            redirect = true;
        }
    }
}
