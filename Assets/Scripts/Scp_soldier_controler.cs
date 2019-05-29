using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scp_soldier_controler : MonoBehaviour
{

    public float speed;

    private bool target_found;
    private Animator ani;
    private GameObject target_player;
    
    // Start is called before the first frame update
    void Start()
    {
        target_found = false;
        ani = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (target_found)
        {
            Vector3 offset = transform.position - target_player.transform.position;
            transform.LookAt(target_player.transform);

            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !target_found)
        {
            target_found = true;
            ani.SetBool("run", true);
            target_player = other.gameObject;
        }
    }
}
