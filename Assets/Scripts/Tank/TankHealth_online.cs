using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
//using Hashtable = ExitGames.Client.Photon.Hashtable;


public class TankHealth_online : MonoBehaviourPun, IPunObservable
{
    public int m_PlayerNumber = 1;
    public float m_StartingHealth = 100f;
    public Slider m_Slider;
    public Image m_FillImage;
    public Color m_FullHealthColor = Color.green;
    public Color m_ZeroHealthColor = Color.red;
    public GameObject m_ExplosionPrefab;

    int killscore0 = 0, killscore1 = 0;
    // Hashtable hash = new Hashtable();

    private ExitGames.Client.Photon.Hashtable customscore0 = new ExitGames.Client.Photon.Hashtable();
    private ExitGames.Client.Photon.Hashtable customscore1 = new ExitGames.Client.Photon.Hashtable();

    ShellExplosion_online d;
    float DAMAGE = 0;


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
        if (m_PlayerNumber == 2)
        {
            if (PhotonNetwork.PlayerList[1].CustomProperties.ContainsKey("player_score0"))
            {
                killscore0 = (int)PhotonNetwork.PlayerList[1].CustomProperties["player_score0"];
                Debug.Log("killscore0");
                Debug.Log(killscore0);
            }
        }

        if (m_PlayerNumber == 1)
        {
            if (PhotonNetwork.PlayerList[0].CustomProperties.ContainsKey("player_score1"))
            {
                killscore1 = (int)PhotonNetwork.PlayerList[0].CustomProperties["player_score1"];
                Debug.Log("killscore1");
                Debug.Log(killscore1);
            }
        }
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

        }
    }
    public void TakeDamage(float amount)
    {
        // Adjust the tank's current health, update the UI based on the new health and check whether or not the tank is dead.
        m_CurrentHealth -= amount;
        SetHealthUI();
        if (m_CurrentHealth <= 0 && !m_Dead)
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

        // ParticleSystem.MainModule mainModule = m_ExplosionParticles.main;

        Destroy(m_ExplosionParticles.gameObject, 1.5f);

        FindObjectOfType<Manager_tank>().flag = true;
        if (m_PlayerNumber == 2)
        {
            StartCoroutine(Setscore0());
        }
        if (m_PlayerNumber == 1)
        {
            StartCoroutine(Setscore1());

        }
        /*
        if (PhotonNetwork.IsMasterClient == false)
        {
            killscore0++;
            hash.Clear();
            hash.Add("player_score0", killscore0);
            PhotonNetwork.PlayerList[1].SetCustomProperties(hash);
        }
        if (PhotonNetwork.IsMasterClient == true)
        {
            killscore1++;
            hash.Clear();
            hash.Add("player_score1", killscore1);
            PhotonNetwork.PlayerList[0].SetCustomProperties(hash);
        }*/
        gameObject.SetActive(false);

        FindObjectOfType<Manager_tank>().Rest(this.gameObject.transform, m_PlayerNumber - 1);

        gameObject.SetActive(true);
    }

    private IEnumerator Setscore1()
    {
        killscore1++;
        customscore0["player_score1"] = killscore1;
        PhotonNetwork.PlayerList[0].SetCustomProperties(customscore0);
        yield return new WaitForEndOfFrame();

    }

    private IEnumerator Setscore0()
    {
        killscore0++;
        customscore1["player_score0"] = killscore0;
        PhotonNetwork.PlayerList[1].SetCustomProperties(customscore1);
        yield return new WaitForEndOfFrame();
        

    }


  /*  public void rest()
    {
        if (m_PlayerNumber == 2)
        {
            killscore1 = 0;
            killscore0 = 0;
            customscore1["player_score0"] = killscore0;
            PhotonNetwork.PlayerList[1].SetCustomProperties(customscore1);
        }
        if (m_PlayerNumber == 1)
        {
            killscore1 = 0;
            killscore0 = 0;
            customscore0["player_score1"] = killscore1;
            PhotonNetwork.PlayerList[0].SetCustomProperties(customscore0);
        }
    }*/




    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(m_Slider.value);
            stream.SendNext(m_CurrentHealth);
           
        }
        else if (stream.IsReading)
        {
            m_Slider.value = (float)stream.ReceiveNext();
            m_CurrentHealth = (float)stream.ReceiveNext();
         
        }
    }
}