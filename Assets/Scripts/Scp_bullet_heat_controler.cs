using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scp_bullet_heat_controler : MonoBehaviour
{
    public int radius;
    public GameObject dummy;

    private float angle;
    private bool shoot;
    private List<GameObject> targets;
    
    // Start is called before the first frame update
    void Start()
    {
        shoot = false;
        targets = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        angle += 20 * Time.deltaTime;

        if (angle < 60f)
        {
            RaycastHit hit;
            Vector3 ray_pos = new Vector3(transform.position.x + 0.25f, transform.position.y, transform.position.z);
            transform.Rotate(new Vector3(0, 1, 0) * 20 * Time.deltaTime, Space.World);

            if (Physics.Raycast(ray_pos, transform.up, out hit, radius, 1, QueryTriggerInteraction.Ignore))
            {
                if (hit.transform.gameObject.tag == "Enemy")
                {
                    if (!targets.Contains(hit.transform.gameObject))
                    {
                        targets.Add(hit.transform.gameObject);
                        GameObject newBullet = Instantiate(dummy);
                        newBullet.transform.position = transform.position;
                        newBullet.transform.Translate(0, 0.15f, 0, Space.World);
                        newBullet.GetComponent<Rigidbody>().AddForce(transform.up * 750);
                    }
                }
            }
        }

        else
        {
            Destroy(gameObject);
        }
    }
}
