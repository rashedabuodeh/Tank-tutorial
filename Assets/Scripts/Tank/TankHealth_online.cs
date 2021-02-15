using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Photon.Pun;


public class TankHealth_online : MonoBehaviourPun, IPunObservable
{
    public float m_StartingHealth = 100f;          
    public Slider m_Slider;                        
    public Image m_FillImage;                      
    public Color m_FullHealthColor = Color.green;  
    public Color m_ZeroHealthColor = Color.red;    
    public GameObject m_ExplosionPrefab;

    ShellExplosion_online d;
    float DAMAGE=0;

    //public int m_PlayerNumber = 1;
   // canvas_online c;



    private AudioSource m_ExplosionAudio;          
    private ParticleSystem m_ExplosionParticles;
    private float m_CurrentHealth, m_CurrentHealth2;
    private bool m_Dead;

    private void Start()
    {
        PhotonNetwork.SendRate = 20;
        PhotonNetwork.SerializationRate = 15;
    }

    private void OnEnable()
    {
       
        if (photonView.IsMine)
        {
            m_CurrentHealth = m_StartingHealth;
            m_Dead = false;

            SetHealthUI();
        }
        else
        {
            m_CurrentHealth = m_StartingHealth;
            m_Dead = false;

            SetHealthUI();
        }


    }
    
    void Update()
    {
        if (!photonView.IsMine)

        {

            SetHealthUI();
          
                if (DAMAGE >= 0)
                {
                    TakeDamage(DAMAGE);
                }
           
        }


    }
    public void TakeDamage(float amount)
    {
        // Adjust the tank's current health, update the UI based on the new health and check whether or not the tank is dead.
        m_CurrentHealth -= amount;
        SetHealthUI();
        if ( m_CurrentHealth <=0 && !m_Dead)
        { 

            OnDeath(); 
        }
    }


    private void SetHealthUI()
    {
        // Adjust the value and colour of the slider.
        m_Slider.value = m_CurrentHealth;
        m_FillImage.color = Color.Lerp(m_ZeroHealthColor, m_FullHealthColor, m_CurrentHealth / m_StartingHealth);

    }


    private void OnDeath()
    {
        // Play the effects for the death of the tank and deactivate it.
        m_Dead = true;

        m_ExplosionParticles = Instantiate(m_ExplosionPrefab).GetComponent<ParticleSystem>();
        m_ExplosionAudio = m_ExplosionParticles.GetComponent<AudioSource>();

        m_ExplosionParticles.transform.position = transform.position;
        m_ExplosionParticles.Play();
        m_ExplosionAudio.Play();
        
          /*  if (m_PlayerNumber==2  && photonView.IsMine) 
        {
            c.score1 += 1;
            c.lvl += 1;
        }
        if (m_PlayerNumber == 1 && photonView.IsMine)
        {
            c.score2 += 1;
            c.lvl += 1;

        }*/
        gameObject.SetActive(false);


        //gameObject.SetActive(true);

        //PhotonNetwork.Destroy(this.gameObject);


    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(m_Slider.value);
            stream.SendNext(m_CurrentHealth);
            //if (d != null)
            stream.SendNext(d.damage);
           // stream.SendNext(m_Dead);



        }
        else if (stream.IsReading)
        {
            m_Slider.value = (float)stream.ReceiveNext();
            m_CurrentHealth = (float)stream.ReceiveNext();
            //if (d != null)
            DAMAGE = (float)stream.ReceiveNext();
           // m_Dead  = (bool)stream.ReceiveNext();


        }
    }
}