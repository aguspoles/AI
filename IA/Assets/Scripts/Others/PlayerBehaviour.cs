using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour {

    public float goldPocketSize = 50;
    public float goldInPocket = 0;
    public float speed = 5;
    public float miningStep = 1;

    private GameObject m_home;
    private GameObject[] m_mines;
    private CEnemyStateMachine m_playerStateMachine;

    // Use this for initialization
    void Start () {
        m_home = GameObject.Find ("Base");
        m_mines = GameObject.FindGameObjectsWithTag("Mine");
        m_playerStateMachine = new CEnemyStateMachine();
    }

    // Update is called once per frame
    void Update () {
       
        switch (m_playerStateMachine.GetState())
        {
            case (int)CEnemyStateMachine.EState.IDLE:
                Idle();
                break;
            case (int)CEnemyStateMachine.EState.GO_TO_MINE:
                GoToMine();
                break;
            case (int)CEnemyStateMachine.EState.MINE:
                Mine();
                break;
            case (int)CEnemyStateMachine.EState.RETURN_TO_BASE:
                ReturnToBase();
                break;
            case (int)CEnemyStateMachine.EState.LEAVE_GOLD:
                LeaveGold();
                break;
            default:
                break;
        }
    }

    private GameObject MineWithGold()
    {
        foreach (GameObject mine in m_mines)
        {
            GoldMine goldMine = mine.GetComponent<GoldMine>();
            if (goldMine.goldLeft > 0)
            {
                return mine;
            }
        }
        return null;
    }

    private void Idle()
    {
        transform.RotateAround(transform.position, Vector3.up, 90 * Time.deltaTime);
        if (MineWithGold())
        {
            m_playerStateMachine.SetEvent((int)CEnemyStateMachine.EEvent.ON_MINE_EXIST);
        }
        else
        {
            m_playerStateMachine.SetEvent((int)CEnemyStateMachine.EEvent.ON_MINE_NOT_EXIST);
        }
    }

    private void GoToMine()
    {
        GameObject mine = MineWithGold();
        if (mine)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, mine.transform.position, step);
            if (transform.position == mine.transform.position)
                m_playerStateMachine.SetEvent((int)CEnemyStateMachine.EEvent.ON_ARRIVE_TO_MINE);
        }
        Debug.Log("Going to mine...");
    }

    private void Mine()
    {
        GameObject mine = MineWithGold();
        if (mine)
        {
            float amountMined = miningStep * Time.deltaTime;
            goldInPocket += amountMined;
            if (goldInPocket >= goldPocketSize)
            {
                goldInPocket = goldPocketSize;
                mine.GetComponent<GoldMine>().goldLeft -= goldPocketSize;
                m_playerStateMachine.SetEvent((int)CEnemyStateMachine.EEvent.ON_POCKETS_FULL);
            }
        }
        Debug.Log("Mining...");
    }

    private void ReturnToBase()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, m_home.transform.position, step);
        if (transform.position == m_home.transform.position)
        {
            m_playerStateMachine.SetEvent((int)CEnemyStateMachine.EEvent.ON_ARRIVE_TO_HOME);
        }
        Debug.Log("Returning to base...");
    }

    private void LeaveGold()
    {
        goldInPocket = 0;
        m_playerStateMachine.SetEvent((int)CEnemyStateMachine.EEvent.ON_LEAVE_GOLD);
        Debug.Log("Leaving gold...");
    }

}