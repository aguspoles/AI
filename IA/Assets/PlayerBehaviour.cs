using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour {

    enum STATES { IDLE, WALK, RUN}
    enum EVENTS { START_WALKING , BACK_TO_IDLE }

    FSM playerStateMachine = new FSM(3, 2);

	// Use this for initialization
	void Start () {
        for(uint i = 0; i < playerStateMachine.fsm.GetLength(0); i++)
        {
            for(uint j = 0; j < playerStateMachine.fsm.GetLength(1); j++)
            {
                playerStateMachine.fsm[i, j] = -1;
            }
        }
        playerStateMachine.SetRelation((int)STATES.IDLE, (int)EVENTS.START_WALKING, (int)STATES.WALK);
        playerStateMachine.SetRelation((int)STATES.WALK, (int)EVENTS.BACK_TO_IDLE, (int)STATES.IDLE);
        playerStateMachine.SetEvent((int)EVENTS.BACK_TO_IDLE);
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerStateMachine.SetEvent((int)EVENTS.START_WALKING);
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            playerStateMachine.SetEvent((int)EVENTS.BACK_TO_IDLE);
        }

        if (playerStateMachine.GetState() == (int)STATES.IDLE)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, (transform.position.z + Mathf.Cos(Time.time))*0.5f);
        }

        if (playerStateMachine.GetState() == (int)STATES.WALK)
        {
            transform.position = new Vector3((transform.position.x + Mathf.Cos(Time.time)) * 0.5f, transform.position.y, transform.position.z);
        }

    }
}
