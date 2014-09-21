using UnityEngine;
using System.Collections;

public interface ISpecialBall
{
    void Initialize(float _timeToLive, float _timeCreated, GameObject _parent_go);
    void CheckLifeTimer();
    bool MakeSpeciality(Color _realColor);
}
