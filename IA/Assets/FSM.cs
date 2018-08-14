using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM  {

    private int state;
    public int[,] fsm;

    public FSM(int statesCount, int eventsCount) {
        fsm = new int[statesCount, eventsCount];
    }

    public void SetRelation(int srcState, int evt, int destSrc) {
        fsm[srcState, evt] = destSrc;
    }

    public int GetState() {
        return state;
    }

    public void SetEvent(int evt) {
        if(fsm[state, evt] != -1)
        {
            state = fsm[state, evt];
        }
    }
}
