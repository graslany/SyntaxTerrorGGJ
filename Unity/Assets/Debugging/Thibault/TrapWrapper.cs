using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class TrapWrapper : MonoBehaviour
{
    ITrap _myTrap;
    public void Trigger()
    {
        _myTrap.Spring();
    }
}