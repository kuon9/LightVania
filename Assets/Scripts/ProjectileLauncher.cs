using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLauncher : MonoBehaviour
{
    public GameObject projectile;
    
    
    public void FireProjectile()
    {
        Instantiate(projectile,transform.position,projectile.transform.rotation);
    } 
    
}
