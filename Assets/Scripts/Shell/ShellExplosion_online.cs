using Photon.Pun;
using UnityEngine;

public class ShellExplosion_online : MonoBehaviourPun, IPunObservable
{
    public LayerMask m_TankMask;
    public ParticleSystem m_ExplosionParticles;
    public AudioSource m_ExplosionAudio;



    bool expo, foundrigid;
    public float damage;

    public float m_MaxDamage = 100f;
    public float m_ExplosionForce = 1000f;
    public float m_MaxLifeTime = 2f;
    public float m_ExplosionRadius = 5f;



    private void Start()
    {
        PhotonNetwork.SendRate = 20;
        PhotonNetwork.SerializationRate = 15;
        Destroy(gameObject, m_MaxLifeTime);
    }

    void Update()
    {
        if (photonView.IsMine)
        {
            if(m_ExplosionParticles.transform.parent == null && foundrigid)
            {
                expo = true;
             }

            else
            {
                expo = false;
                

             }
        }

        else
        {
            if (expo)
            {
                m_ExplosionParticles.transform.parent = null;

                m_ExplosionParticles.Play();

                m_ExplosionAudio.Play();

                ParticleSystem.MainModule mainModule = m_ExplosionParticles.main;

                Destroy(m_ExplosionParticles.gameObject, mainModule.duration);

                Destroy(gameObject);
            }


        }


    }

    private void OnTriggerEnter(Collider other)
    {
        foundrigid = false;
        Collider[] colliders = Physics.OverlapSphere(transform.position, m_ExplosionRadius, m_TankMask);

            for (int i = 0; i < colliders.Length; i++)

            {
                Rigidbody targetRigidbody = colliders[i].GetComponent<Rigidbody>();


                targetRigidbody.AddExplosionForce(m_ExplosionForce, transform.position, m_ExplosionRadius);

                TankHealth_online targetHealth = targetRigidbody.GetComponent<TankHealth_online>();

                 damage = CalculateDamage(targetRigidbody.position);

                targetHealth.TakeDamage(damage);

            
            }
            
            foundrigid = true; 

            m_ExplosionParticles.transform.parent = null;

            m_ExplosionParticles.Play();

            m_ExplosionAudio.Play();

            ParticleSystem.MainModule mainModule = m_ExplosionParticles.main;

            Destroy(m_ExplosionParticles.gameObject, mainModule.duration);

            Destroy(gameObject);
        

        
    }




    private float CalculateDamage(Vector3 targetPosition)
    {
        // Calculate the amount of damage a target should take based on it's position.
        Vector3 explosionToTarget = targetPosition - transform.position;

        float explosionDistance = explosionToTarget.magnitude;

        float ratio = (m_ExplosionRadius - explosionDistance) / m_ExplosionRadius;

        float damage = ratio * m_MaxDamage;

        damage = Mathf.Max(0f, damage);

        return damage;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(expo);

        }
        else if (stream.IsReading)
        {
            expo = (bool)stream.ReceiveNext();

        }
    }
}