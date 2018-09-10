using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour {

    enum STATES {
        IDLE = 0,
        GO_TO_MINE = 1,
        MINE = 2,
        RETURN_BASE = 3,
        LEAVE_GOLD = 4
    }
    enum EVENTS {
        MINE_EXIST = 0,
        MINE_NOT_EXIST = 1,
        ARRIVE_TO_MINE = 2,
        POCKETS_FULL = 3,
        ARRIVE_TO_HOME = 4,
        LEAVE_GOLD = 5
    }

    FSM playerStateMachine = new FSM (5, 6);

    public float goldPocketSize = 50;
    public float goldInPocket = 0;

    public float speed = 5;
    public float miningStep = 1;
    private GameObject home;

    // Use this for initialization
    void Start () {
        home = GameObject.Find ("Base");
        for (uint i = 0; i < playerStateMachine.fsm.GetLength (0); i++) {
            for (uint j = 0; j < playerStateMachine.fsm.GetLength (1); j++) {
                playerStateMachine.fsm[i, j] = -1;
            }
        }
        playerStateMachine.SetRelation ((int) STATES.IDLE, (int) EVENTS.MINE_EXIST, (int) STATES.GO_TO_MINE);
        playerStateMachine.SetRelation ((int) STATES.IDLE, (int) EVENTS.MINE_NOT_EXIST, (int) STATES.IDLE);
        playerStateMachine.SetRelation ((int) STATES.GO_TO_MINE, (int) EVENTS.ARRIVE_TO_MINE, (int) STATES.MINE);
        playerStateMachine.SetRelation ((int) STATES.MINE, (int) EVENTS.POCKETS_FULL, (int) STATES.RETURN_BASE);
        playerStateMachine.SetRelation ((int) STATES.RETURN_BASE, (int) EVENTS.ARRIVE_TO_HOME, (int) STATES.LEAVE_GOLD);
        playerStateMachine.SetRelation ((int) STATES.LEAVE_GOLD, (int) EVENTS.LEAVE_GOLD, (int) STATES.IDLE);
        playerStateMachine.SetEvent ((int) EVENTS.MINE_NOT_EXIST);
    }

    // Update is called once per frame
    void Update () {
        List<GameObject> mines = MinesWithGold ();

        if (mines.Count > 0) {
            playerStateMachine.SetEvent ((int) EVENTS.MINE_EXIST);
        } else {
            playerStateMachine.SetEvent ((int) EVENTS.MINE_NOT_EXIST);
        }

        if (playerStateMachine.GetState () == (int) STATES.IDLE) {
            transform.RotateAround (transform.position, Vector3.up, 90 * Time.deltaTime);
        }

        else if (playerStateMachine.GetState () == (int) STATES.GO_TO_MINE) {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards (transform.position, mines[0].transform.position, step);
            if (transform.position == mines[0].transform.position)
                playerStateMachine.SetEvent ((int) EVENTS.ARRIVE_TO_MINE);
        } 

        else if (playerStateMachine.GetState () == (int) STATES.MINE) {
            float amountMined = miningStep * Time.deltaTime;
            goldInPocket += amountMined;
            if (goldInPocket >= goldPocketSize) {
                goldInPocket = goldPocketSize;
                mines[0].GetComponent<GoldMine> ().goldLeft -= goldPocketSize;
                playerStateMachine.SetEvent ((int) EVENTS.POCKETS_FULL);
            }
        } 

        else if (playerStateMachine.GetState () == (int) STATES.RETURN_BASE) {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards (transform.position, home.transform.position, step);
            if (transform.position == home.transform.position) {
                playerStateMachine.SetEvent ((int) EVENTS.ARRIVE_TO_HOME);
            }
        }
         
        else if (playerStateMachine.GetState () == (int) STATES.LEAVE_GOLD) {
            goldInPocket = 0;
            playerStateMachine.SetEvent ((int) EVENTS.LEAVE_GOLD);
        }

}

private List<GameObject> MinesWithGold () {
    List<GameObject> res = new List<GameObject> ();
    GameObject[] mines = GameObject.FindGameObjectsWithTag ("Mine");
    foreach (GameObject mine in mines) {
        GoldMine goldMine = mine.GetComponent<GoldMine> ();
        if (goldMine.goldLeft > 0) {
            res.Add (mine);
        }
    }
    return res;
}
}