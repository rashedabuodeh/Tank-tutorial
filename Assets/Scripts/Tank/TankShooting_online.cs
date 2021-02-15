using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class TankShooting_online : MonoBehaviourPun, IPunObservable
{
    public int m_PlayerNumber = 1;       
    public GameObject m_Shell;            
    public Transform m_FireTransform;    
    public Slider m_AimSlider;           
    public AudioSource m_ShootingAudio;  
    public AudioClip m_ChargingClip;     
    public AudioClip m_FireClip;         
    public float m_MinLaunchForce = 15f; 
    public float m_MaxLaunchForce = 30f; 
    public float m_MaxChargeTime = 0.75f;

    private Vector3 firetransform2;

    GameObject shellprefab;


    private string m_FireButton;         
    private float m_CurrentLaunchForce;  
    private float m_ChargeSpeed;         
    private bool m_Fired;                




    private void OnEnable()
    {

        m_CurrentLaunchForce = m_MinLaunchForce;
        m_AimSlider.value = m_MinLaunchForce;
    }


    private void Start()
    {
        PhotonNetwork.SendRate = 20;
        PhotonNetwork.SerializationRate = 15;
        m_FireButton = "Fire" + m_PlayerNumber;

        m_ChargeSpeed = (m_MaxLaunchForce - m_MinLaunchForce) / m_MaxChargeTime;
    }
    

    private void Update()
    {
        // Track the current state of the fire button and make decisions based on the current launch force.
        m_AimSlider.value = m_MinLaunchForce;

        if (photonView.IsMine)
        {
            if (Input.GetButtonDown(m_FireButton)) 
            {
                m_Fired = false;
                m_CurrentLaunchForce = m_MinLaunchForce;
                m_ShootingAudio.clip = m_ChargingClip;
                m_ShootingAudio.Play();
            }

            if (Input.GetButton(m_FireButton) && !m_Fired)
            {
                m_CurrentLaunchForce += m_ChargeSpeed * Time.deltaTime;

                m_AimSlider.value = m_CurrentLaunchForce;
            }

            if (m_CurrentLaunchForce >= m_MaxLaunchForce && !m_Fired)
            {
                m_CurrentLaunchForce = m_MaxLaunchForce;
                m_CurrentLaunchForce = m_MinLaunchForce;
            }

            if (Input.GetButtonUp(m_FireButton) && !m_Fired)
            {
                Fire();
            }
        }
        else
        {
            if (m_Fired)
            {

                shellprefab.transform.position = Vector3.Lerp(shellprefab.transform.position, firetransform2, Time.deltaTime * 10);
            }

        }

    }


    private void Fire()
    {
        m_Fired = true;

        //        Rigidbody shellInstance = Instantiate(m_Shell, m_FireTransform.position, m_FireTransform.rotation) as Rigidbody;

        shellprefab = PhotonNetwork.Instantiate(m_Shell.name, m_FireTransform.position, m_FireTransform.rotation) as GameObject;

        Rigidbody shellInstance = shellprefab.GetComponent<Rigidbody>();

        shellInstance.AddForce(m_CurrentLaunchForce * m_FireTransform.forward, ForceMode.Impulse);
       // shelltrans.position = shellInstance.position;
        //if (shelltrans != null)
        //{ shellInstance.position = shelltrans.position; }
        m_ShootingAudio.clip = m_FireClip;

        m_ShootingAudio.Play();

        m_CurrentLaunchForce = m_MinLaunchForce;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            //if (shellprefab != null)
            
                stream.SendNext(shellprefab.transform.position);
            
        }
        else if (stream.IsReading)
        {
           // if (shellprefab != null)
            
                firetransform2 = (Vector3)stream.ReceiveNext();
            
        }
    }
}