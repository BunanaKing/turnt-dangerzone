using UnityEngine;
using System.Collections;

public interface ISpecialBall
{
    void Initialize(float _timeToLive, float _timeCreated);
    void CheckLifeTimer();
    void MakeSpecialty(Color _realColor);
}
