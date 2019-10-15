using Humans;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public interface ITargetable 
{
    HumanType GetHumanType();
    void Lock();
    void UnLock();
    void Remove();
}
