using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcLikeabilityManager : MonoBehaviour
{
    public int likebility = 0;

    public void AddLikebility(int _point)
    {
        likebility += _point;
    }

    public void RemoveLikebility(int _point)
    {
        likebility -= _point;
    }
}
