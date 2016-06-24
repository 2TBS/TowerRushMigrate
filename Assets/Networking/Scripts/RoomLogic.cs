﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RoomLogic : MonoBehaviour {

    public Transform playerPrefab;

    public GameObject[] gspawn;
    public GameObject[] bspawn;
	private string playerTeam;
    public ButtonActions buttons;
    public Vars.Team team;

    public void Awake()
    {
        // LoadMenu
        if (!PhotonNetwork.connected)
        {
            Debug.LogError("Lost Connection");
            SceneManager.LoadScene("MainMenu");
            return;
        }
        PhotonNetwork.isMessageQueueRunning = true;

    }

    public void Start()
    {
        PhotonNetwork.isMessageQueueRunning = true;
    }

    // Update is called once per frame
    void Update () {
        
    }

    public void joinBlue()
    {
        if(!Input.GetKey(KeyCode.Space)) {
            Debug.Log("JOINING BLUE");
        PhotonView photonView = PhotonView.Get(this);
        //playerTransform.position = Vars.testMapBlue;
        team = Vars.Team.blue;
        buttons.CloseTeamSelect();
        GameObject player = PhotonNetwork.Instantiate(this.playerPrefab.name, bspawn[Random.Range(0, 4)].transform.position, Quaternion.identity, 0);
        player.tag = "BluePlayer";
        //photonView.RPC("SpawnPlayerB", PhotonTargets.AllBuffered);
        }
        

    }

    public void joinGold()
    {
        if(!Input.GetKey(KeyCode.Space)) {
        Debug.Log("JOINING GOLD");
        PhotonView photonView = PhotonView.Get(this);
        //playerTransform.position = Vars.testMapGold;
        team = Vars.Team.gold;
        buttons.CloseTeamSelect();
        Debug.Log("test");
        GameObject player = PhotonNetwork.Instantiate(this.playerPrefab.name, gspawn[Random.Range(0,4)].transform.position, Quaternion.identity, 0);
        player.tag = "GoldPlayer";
        //photonView.RPC("SpawnPlayerG", PhotonTargets.AllBuffered);
        }
        
    }

    public void Respawn(Transform player) {
        if(player.tag == "BluePlayer") {
            player.position = bspawn[Random.Range(0,4)].transform.position;
        }
        else if(player.tag == "GoldPlayer") {
            player.position = gspawn[Random.Range(0,4)].transform.position;
        }
    }

}
