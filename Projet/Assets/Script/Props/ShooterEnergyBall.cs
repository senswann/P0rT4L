using UnityEngine;
using System.Collections;

public class ShooterEnergyBall : MonoBehaviour
{
    //boule d energie tirer par le shooter
    [SerializeField] GameObject energyBallInstance;

    //variable pour gerer le lancer des boules d energies
    [SerializeField] float launchForce = 300f;

    //lumiere du shooter
    [SerializeField] Light lightObj;

    //boolean pour verifier si il tire
    bool isLaunch = true;
    bool isStop = false;

    private void FixedUpdate()
    {
        //si on peux tirer une boule on le fait en attendant la fin du cooldown
        if (isLaunch && !isStop)
        {
            isLaunch = false;
            //Invoke(nameof(Launch), launchCooldown);
            Launch();
        }
    }

    //fonction permettant de tirer une boule insteancie de celle de depart
    void Launch()
    {
        StartCoroutine(Light());
        GameObject projectileObject = Instantiate(energyBallInstance, transform.position, Quaternion.identity);
        projectileObject.GetComponent<Projectile>().isParent = false;
        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.Launch(gameObject.transform.up*2f, launchForce,this);
    }

    //fonction stoppant le tire de projectile
    public void Stop()
    {
        lightObj.color = Color.red;
        isStop = true;
        this.enabled = false;
    }

    //setter du vérificateur pour tirer
    public void SetIsLaunch(bool _isLaunch)
    {
        isLaunch = _isLaunch;
    }

    //IEnumerator permettant de gerer l effet de la lumiere
    private IEnumerator Light()
    {
        lightObj.color = Color.cyan;
        yield return new WaitForSeconds(0.5f);
        lightObj.color = Color.yellow + Color.red;
    }
}
