using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class States : MonoBehaviour
{
    public virtual void Enter(States currstates) { }
    public virtual void Exit(States currstates) { }

}

public class Attack : States
{
    private void Update()
    {
        
    }
}
public class FindWeapon : States
{
    private void Update()
    {

    }
}
public class Escape : States
{
    private void Update()
    {

    }
}