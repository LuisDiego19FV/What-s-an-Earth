using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Scp_player_controles : MonoBehaviour
{
    public float speed;
    public float rotate_speed;
    public RawImage img_void;
    public RawImage img_bomb;
    public RawImage img_misile;
    public RawImage img_smaller;
    public GameObject bullet_void;
    public GameObject bullet_bomb;
    public GameObject bullet_directional;
    public GameObject bullet_minimizer;
    public GameObject camera_view;

    private float run_time;
    private float cool_down_time;
    private bool lockchange;
    private bool cool_down;
    private bool weapon_void;
    private bool weapon_bombs;
    private bool weapon_directedbullet;
    private bool weapon_minimizer;
    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        offset = camera_view.transform.position - transform.position;
        cool_down_time = 4.01f;

        lockchange = false;
        cool_down = false;
        weapon_void = true;
        weapon_bombs = false;
        weapon_directedbullet = false;

        img_void.color = new Color(img_void.color.r, img_void.color.g, img_void.color.b, 155);
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        float z = -Input.GetAxis("Horizontal") * speed * Time.deltaTime;

        run_time += Time.deltaTime;

        if (x > 0 && transform.position.z < 12f)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
        else if (x < 0)
        {
            transform.Translate(Vector3.forward * -speed * Time.deltaTime);
        }

        if (z > 0)
        {
            transform.Rotate(new Vector3(0, 1, 0) * -rotate_speed * Time.deltaTime, Space.World);
        }
        else if (z < 0)
        {
            transform.Rotate(new Vector3(0, 1, 0) * rotate_speed * Time.deltaTime, Space.World);
        }

        Debug.Log(Input.GetAxis("Fire1"));

        if (Input.GetAxis("Fire1") != 0 && !cool_down && weapon_void)
        {
            GameObject void_bullet = Instantiate(bullet_void);
            void_bullet.transform.position = transform.position;
            void_bullet.transform.rotation = transform.rotation;
            void_bullet.transform.Translate(new Vector3(0.5f, 0.0f, 1.2f), Space.Self);
            void_bullet.GetComponent<Rigidbody>().AddForce(transform.forward * 450);
            cool_down = true;
            cool_down_time = 4.01f;
            run_time = 0;
        }

        if (Input.GetAxis("Fire1") != 0 && !cool_down && weapon_bombs)
        {
            GameObject bomb_bullet = Instantiate(bullet_bomb);
            bomb_bullet.transform.position = transform.position + transform.forward;
            bomb_bullet.transform.rotation = transform.rotation;
            bomb_bullet.transform.Translate(new Vector3(0.5f, 0.0f, 1.2f), Space.Self);
            bomb_bullet.GetComponent<Rigidbody>().AddForce(transform.forward * 400);
            bomb_bullet.GetComponent<Rigidbody>().AddForce(transform.up * 400);
            cool_down = true;
            cool_down_time = 3.01f;
            run_time = 0;
        }

        if (Input.GetAxis("Fire1") != 0 && !cool_down && weapon_directedbullet)
        {
            GameObject directional_bullet = Instantiate(bullet_directional);
            directional_bullet.transform.position = transform.position + transform.forward;
            directional_bullet.transform.rotation = transform.rotation;
            directional_bullet.transform.Translate(new Vector3(0.5f, 0.0f, 1.2f), Space.Self);
            directional_bullet.transform.Rotate(new Vector3(0f, -120f, -90f), Space.Self);
            cool_down = true;
            cool_down_time = 5.01f;
            run_time = 0;
        }

        if (Input.GetAxis("Fire1") != 0 && !cool_down && weapon_minimizer)
        {
            GameObject minimizer_bullet = Instantiate(bullet_minimizer);
            minimizer_bullet.transform.position = transform.position;
            minimizer_bullet.transform.rotation = transform.rotation;
            minimizer_bullet.transform.Translate(new Vector3(0.5f, 0.0f, 1.2f), Space.Self);
            minimizer_bullet.GetComponent<Rigidbody>().AddForce(transform.forward * 600);
            cool_down = true;
            cool_down_time = 2.01f;
            run_time = 0;
        }

        if (Input.GetAxis("Fire2") != 0 && !lockchange)
        {
            Change_wapon();
            lockchange = true;
        }
        else if (Input.GetAxis("Fire2") == 0)
        {
            lockchange = false;
        }

        if (cool_down == true && run_time > cool_down_time)
        {
            cool_down = false;
        }

        camera_view.transform.position = transform.position + offset;

        if (camera_view.transform.position.z < -14.5f)
        {
            camera_view.transform.position = new Vector3(camera_view.transform.position.x,
            camera_view.transform.position.y, -14.5f);
        }

    }

    void Change_wapon()
    {
        if (weapon_void)
        {
            weapon_void = false;
            weapon_bombs = true;
            img_void.color = new Color(img_void.color.r, img_void.color.g, img_void.color.b, 0);
            img_bomb.color = new Color(img_void.color.r, img_void.color.g, img_void.color.b, 155);
        }

        else if (weapon_bombs)
        {
            weapon_bombs = false;
            weapon_directedbullet = true;
            img_bomb.color = new Color(img_void.color.r, img_void.color.g, img_void.color.b, 0);
            img_misile.color = new Color(img_void.color.r, img_void.color.g, img_void.color.b, 155);
        }

        else if (weapon_directedbullet)
        {
            weapon_directedbullet = false;
            weapon_minimizer = true;
            img_misile.color = new Color(img_void.color.r, img_void.color.g, img_void.color.b, 0);
            img_smaller.color = new Color(img_void.color.r, img_void.color.g, img_void.color.b, 155);
        }

        else if (weapon_minimizer)
        {
            weapon_minimizer = false;
            weapon_void = true;
            img_smaller.color = new Color(img_void.color.r, img_void.color.g, img_void.color.b, 0);
            img_void.color = new Color(img_void.color.r, img_void.color.g, img_void.color.b, 155);
        }
    }
}
