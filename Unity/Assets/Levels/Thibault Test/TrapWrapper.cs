using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class TrapWrapper : MonoBehaviour
{
    TrapInterface _myTrap;
    public void Trigger()
    {
        _myTrap.Trigger();
    }
}