using System;
using UnityEngine;

public interface IMessage{}

public readonly struct mSetData : IMessage
{
    public readonly Database db;

    public mSetData(Database DB) {db = DB;}
}

public readonly struct mGetData : IMessage
{
    public readonly Action<mSetData> setData;
    public mGetData(Action<mSetData> SetData) {setData = SetData;}
}

public readonly struct mChangeData : IMessage
{
    public readonly mSetData setData;

    public mChangeData(mSetData SetData) {setData = SetData;}
}



// public readonly struct PlayerDamagedMessage : IGameMessage
// {
//     public readonly int PlayerId;
//     public readonly int Damage;
//     public readonly Vector3 HitPoint;

//     public PlayerDamagedMessage(int playerId, int damage, Vector3 hitPoint)
//     {
//         PlayerId = playerId;
//         Damage = damage;
//         HitPoint = hitPoint;
//     }
// }
