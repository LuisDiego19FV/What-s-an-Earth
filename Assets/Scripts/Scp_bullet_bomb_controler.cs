using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scp_bullet_bomb_controler : MonoBehaviour
{
    public Light light;
    public float aoe;

    private bool explode;
    private float run_time;

    // Start is called before the first frame update
    void Start()
    {
        explode = false;
    }

    // Update is called once per frame
    void Update()
    {

        run_time += Time.deltaTime;

        if (explode && run_time < 0.50f)
        {
            GetComponent<SphereCollider>().radius = aoe;
            light.intensity = light.intensity - 20f * Time.deltaTime;
        }

        else if (explode)
        {
            Destroy(gameObject);
        }

        if (!explode && run_time > 10)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((other.tag == "Ground" || other.tag == "Enemy") && !explode && !other.isTrigger)
        {
            explode = true;
            run_time = 0;
            light.intensity = 10;
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX |
                RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;
        }

        if (other.tag == "Enemy" && explode && !other.isTrigger)
        {
            Destroy(other.gameObject);
            Debug.Log(other);
        }
    }
}
