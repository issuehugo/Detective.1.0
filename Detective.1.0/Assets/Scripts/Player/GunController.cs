using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    #region Variables
    [Header("Gun Stats")]
    [SerializeField] float bulletSpeed;
    [SerializeField] float fireRate; //delay between shots
    [SerializeField] float bulletSpread;
    [SerializeField] int maxAmmo = 10;
    [SerializeField] float reloadTime; //how long it takes to reload
    private int currentAmmo;
    private float nextAvailableReloadTime = 0;
    private float nextAvailableFireTime = 0;
    [SerializeField] AttackData attackData;
    [Space(2)]
    [Header("Object References")]
    [SerializeField] GameObject normalBullet;
    [SerializeField] Transform gunTipIndicator;
    [SerializeField] GameObject player;

    #endregion
    private void Start()
    {
        currentAmmo = maxAmmo;
    }
    private void Update()
    {
        ShootingController();
        ReloadController();
    }
    void ShootingController()
    {
        /* When the Mouse is clicked (TO BE CHANGED)
         * Get the Mouse Position and rotate it with a random spread
         * Then Draw a raycast to simulate shooting
         * Then Call coroutine to draw the tracer
         */
        if (Input.GetKey(KeyCode.N)) 
        {
            //IF not enough time has passed, return
            if (Time.time < nextAvailableFireTime || currentAmmo <= 0)
            {
                return;
            }
            //Gunshot Sound ------------------------------------------------------
            FMODUnity.RuntimeManager.PlayOneShot("event:/Characters/Player/Pistol", GetComponent<Transform>().position);

            //LEGACY SYSTEM; Shoots based on mouse position ----------------------
            //Vector3 worldPosMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //worldPosMouse.z = 0;
            //Creating New endpoint with spread
            //float spread = Random.Range(-bulletSpread / 2, bulletSpread / 2);
            //Vector3 worldPosMouseWithSpread = worldPosMouse - gunTipIndicator.position; //the relative vector from P2 to P1.
            //worldPosMouseWithSpread = Quaternion.Euler(0, 0, spread) * worldPosMouseWithSpread; //rotatate
            //worldPosMouseWithSpread = gunTipIndicator.position + worldPosMouseWithSpread; //bring back to world space


            //Update ammo
            currentAmmo -= 1;

            //Generating the Bullet
            GameObject bullet = Instantiate(normalBullet, gunTipIndicator.position, Quaternion.Euler(0,0,0));
            bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(player.transform.localScale.x,0) * bulletSpeed;
            bullet.GetComponent<BulletScript>().attackData = attackData;

            //Screenshake
            //gm.screenShake.SmallShake();

            //New Last Shot Time
            nextAvailableFireTime = Time.time+fireRate;
        }
    }
    void ReloadController()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            currentAmmo = maxAmmo;
        }
    }
}
