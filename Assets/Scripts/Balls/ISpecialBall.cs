using UnityEngine;
using System.Collections;

public interface ISpecialBall
{
    void Initialize(float _timeToLive);
    void CheckLifeTimer(float _timeCreated);
    void MakeSpecialty(Floor _floor, Color _realColor);
}
