using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Gun : MonoBehaviour
{
    public static Gun instance;

    [Header("Bullets")]
    public GameObject bulletToFire;
    public Transform firePoint;

    [Header("Fire System")]
    public float timeBetweenShots;
    private float shotCounter;

    [Header("Weapon System")]
    public string weaponName;
    public Sprite gunUI;

    [Header("Shop System")]
    public int itemCost;
    public Sprite gunShopSprite;

    private void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerController.instance.canMove && !LevelManager.instance.isPaused)
        {
            if (shotCounter > 0)
            {
                shotCounter -= Time.deltaTime;
            }
            else
            {
                if (Input.GetMouseButtonDown(0) || Input.GetMouseButton(0))
                {
                    Instantiate(bulletToFire, firePoint.position, firePoint.rotation);
                    shotCounter = timeBetweenShots;
                    AudioManager.instance.PlaySFX(12);
                }
            }
        }
    }
}
