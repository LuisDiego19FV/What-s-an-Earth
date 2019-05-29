using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scp_bullet_void_controler : MonoBehaviour
{
    public float effect_radio;

    private float run_time;
    private bool implode;
    private bool bring_enemies;
    private bool bullet_triggered;
    public List<GameObject> enemies_on_aoe; 

    // Start is called before the first frame update
    void Start()
    {
        implode = false;
        bring_enemies = true;
        bullet_triggered = false;
    }

    // Update is called once per frame
    void Update()
    {
        run_time += Time.deltaTime;

        if (!implode)
        {
            if (run_time > 5.00f)
            {
                Destroy(gameObject);
            }

            if (run_time > 1.00f && bullet_triggered)
            {
                implode = true;
            }


            if (Input.GetAxis("Fire3") != 0 && !bullet_triggered)
            {
                bullet_triggered = true;
                run_time = 0;
            }

            if (bullet_triggered)
            {
                float obj_s = transform.localScale.x;

                transform.localScale = new Vector3(obj_s + effect_radio  * Time.deltaTime, 
                    obj_s + effect_radio * Time.deltaTime, obj_s + effect_radio * Time.deltaTime);
                GetComponent<Rigidbody>().angularVelocity = new Vector3 (0, 50, 0);
                GetComponent<Rigidbody>().velocity = Vector3.zero;

            }
        }

        else
        {
            if (bullet_triggered)
            {
                float i = effect_radio * 4;
                float obj_s = transform.localScale.x;

                if (obj_s < 0.2f)
                {

                    for (int j = enemies_on_aoe.Count - 1; j >= 0; j--)
                    {
                        Destroy(enemies_on_aoe[j]);
                    }

                    Destroy(gameObject);
                }

                if (obj_s < 7f && bring_enemies)
                {

                    for (int j = enemies_on_aoe.Count - 1; j >= 0; j--)
                    {
                        Vector3 force = (enemies_on_aoe[j].transform.position - transform.position).normalized * -1500 ;
                        enemies_on_aoe[j].GetComponent<Rigidbody>().AddForce(force);
                    }

                    bring_enemies = false;
                }

                transform.localScale = new Vector3(obj_s - i * Time.deltaTime, obj_s - i * Time.deltaTime, obj_s - i * Time.deltaTime);
                GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                GetComponent<Rigidbody>().velocity = Vector3.zero;

            }
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {

        if (!enemies_on_aoe.Contains(other.gameObject) && other.tag == "Enemy" && !other.isTrigger)
        {
            enemies_on_aoe.Add(other.gameObject);
        }
    }
}
