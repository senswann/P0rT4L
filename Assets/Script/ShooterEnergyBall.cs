using UnityEngine;

public class ShooterEnergyBall : MonoBehaviour
{
    [SerializeField] GameObject energyBallInstance;
    [SerializeField] float launchForce = 300f;
    [SerializeField] float launchCooldown = 2f;
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
        isLaunch = true;
        GameObject projectileObject = Instantiate(energyBallInstance, transform.position + gameObject.transform.forward, Quaternion.identity);

        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.Launch(gameObject.transform.forward*2f, launchForce);
    }
}
