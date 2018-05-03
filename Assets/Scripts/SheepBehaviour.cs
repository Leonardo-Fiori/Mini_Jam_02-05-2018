using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SheepBehaviour : MonoBehaviour {

    #region"inizializzazione"
    bool lupoVicino;
    float tempoLupoVicino;
    bool playerVicino;
    float tempoPlayerVicino;
    float tempoDaSola;
    bool scappando;

    public float tempoMinLupo = 2f;
    public float tempoMinPlayer = 1f;
    public float tempoMinDaSola = 10f;

    public float distanzaMinLupo = 2f;
    public float distanzaPauraLupo = 10f;
    public float distanzaMinPlayer = 4f;

    private GameObject player;
    private GameObject lupo;
    private Material material;

    public NavMeshAgent agent;
    public SEvent eventoPauraLupo;

    private void SetColor(Color color)
    {
        material.SetColor("_Color", color);
    }

    private void AggiornaBooleane()
    {
        lupoVicino = Mathf.Abs(Vector3.Distance(transform.position, lupo.transform.position)) <= distanzaMinLupo;
        playerVicino = Mathf.Abs(Vector3.Distance(transform.position, player.transform.position)) <= distanzaMinPlayer;
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        lupo = GameObject.FindGameObjectWithTag("Lupo");
        material = transform.GetComponentInChildren<MeshRenderer>().material;
        tempoDaSola = tempoMinDaSola;
    }
    #endregion

    private void Update()
    {
        AggiornaBooleane();

        if (playerVicino)
        {
            PlayerVicino();
        }

        if (lupoVicino && !playerVicino)
        {
            LupoVicino();
        }

        if(!lupoVicino && !playerVicino)
        {
            DaSola();
        }
    }

    private void DaSola()
    {
        tempoDaSola += Time.deltaTime;
        scappando = false;

        if (tempoDaSola >= tempoMinDaSola)
        {
            tempoDaSola = 0f;
            SetColor(Color.white);
            agent.SetDestination(new Vector3(Random.Range(-20, 20), 0f, Random.Range(-20, 20)));
        }
    }

    private void PlayerVicino()
    {
        tempoPlayerVicino += Time.deltaTime;
        scappando = false;

        if(tempoPlayerVicino >= tempoMinPlayer)
        {
            tempoPlayerVicino = 0f;
            SetColor(Color.green);
            agent.SetDestination(Vector3.zero);
        }
    }

    private void LupoVicino()
    {
        tempoLupoVicino += Time.deltaTime;
        if (!scappando)
        {
            agent.SetDestination(new Vector3(Random.Range(-20, 20), 0f, Random.Range(-20, 20)));
        }
        scappando = true;
        SetColor(Color.red);

        if (tempoLupoVicino >= tempoMinLupo && !WolfController.HaAppenaMangiato())
        {
            tempoLupoVicino = 0f;
            eventoPauraLupo.Raise();
            //Destroy(gameObject);
            gameObject.SetActive(false);
        }
    }

    public void PecoraMangiata()
    {
        if (Mathf.Abs(Vector3.Distance(transform.position,lupo.transform.position)) <= distanzaPauraLupo)
        {
            if (!scappando)
            {
                agent.SetDestination(new Vector3(Random.Range(-20, 20), 0f, Random.Range(-20, 20)));
                SetColor(Color.red);
            }
            scappando = true;
        }
    }
}
