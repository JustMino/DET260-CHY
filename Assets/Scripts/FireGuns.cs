using UnityEngine;

public class FireGuns : MonoBehaviour
{
    public GameObject muzzleParticles;

    public Transform muzzlePoint;

    public GameObject boomBoomParticles;

    public GameObject bulletHoleParticles;

    public float hitForce = 5000.0f;
    // GREAT number, but you can adjust in the Inspector


    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            makeGoBoom();
        }
    }


    void makeGoBoom()
    {
        // this particle system is the muzzle flash stuffs
        GameObject Temporary_Blast_Effect;
        Temporary_Blast_Effect = Instantiate(muzzleParticles, muzzlePoint.transform.position, muzzlePoint.transform.rotation) as GameObject;
        Temporary_Blast_Effect.transform.SetParent(muzzlePoint);

        Destroy(Temporary_Blast_Effect, 0.25f);  // don't keep it around for long


        // Bit shift the index of the layer (0) to get a bit mask (used in the IF below)
        int layerMask = 1 << 0;

        RaycastHit hit;

        // Does the ray intersect any objects in the Default layer?
        if (Physics.Raycast(muzzlePoint.transform.position, muzzlePoint.transform.forward, out hit, Mathf.Infinity, layerMask))
        {
            // Debug.DrawRay(muzzlePoint.transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.red, 3.0f);
            Debug.Log("Did Hit - " + hit.transform.name);

            if (hit.rigidbody != null)
            {
                // blast it away!
                hit.rigidbody.AddForce (-hit.normal * hitForce);

                // this particle system is the BIG BOOM for when we hit cars or other objects that we send flying
                GameObject tempBoomEffect;
                tempBoomEffect = Instantiate(boomBoomParticles, hit.point, Quaternion.FromToRotation(Vector3.forward, hit.normal)) as GameObject;
                Destroy(tempBoomEffect, 3.0f);
            }
            else
            {
                // this particle system is just the bullet holes on walls and other
                // objects we can't BLAST away (they have no rigidbody)
                GameObject tempBulletEffect;
                tempBulletEffect = Instantiate(bulletHoleParticles, hit.point, hit.transform.rotation) as GameObject;
                Destroy(tempBulletEffect, 10.0f);
            }
        }
        else
        {
            // Debug.DrawRay(muzzlePoint.position, transform.TransformDirection(Vector3.forward) * 1000, Color.blue, 3.0f);
            Debug.Log("Did not Hit");
        }
    }

}
