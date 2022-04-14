using OWML.Common;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace EmptyHollow
{
    public interface INewHorizons
    {
        GameObject GetPlanet(string name);
        string GetCurrentStarSystem();
        UnityEvent<string> GetStarSystemLoadedEvent();
    }
}
