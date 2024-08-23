using System;
using UnityEngine;

namespace Services.Input
{
    public interface IPlayerInputService
    {
        Vector3 Input { get; }
        Vector2 PointerInput { get; }
        event Action UseButtonClicked;
        event Action<float> ItemRotation;
    } 
}

