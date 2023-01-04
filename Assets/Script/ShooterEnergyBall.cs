using UnityEngine;
using System.Collections;

public class ShooterEnergyBall : MonoBehaviour
{
    [SerializeField] GameObject energyBallInstance;
    [SerializeField] float launchForce = 300f;
    [SerializeField] float launchCooldown = 2f;
    [SerializeField] Light lightObj;
    bool isLaunch = true;

    private void FixedUpdate()
    {
        if (isLaunch)
        {
            isLaunch = false;
            Invoke(nameof(Launch), launchCooldown);
        }
    }

    void Launch()
    {
        StartCoroutine(Light());
        isLaunch = true;
        GameObject projectileObject = Instantiate(energyBallInstance, transform.position, Quaternion.identity);
        projectileObject.GetComponent<Projectile>().isParent = false;
        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.Launch(gameObject.transform.up*2f, launchForce);
    }

    private IEnumerator Light()
    {
        lightObj.color = Color.cyan;
        yield return new WaitForSeconds(0.5f);
        lightObj.color = Color.yellow + Color.red;
    }
}
