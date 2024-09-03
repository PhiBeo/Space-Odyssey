using System;
using UnityEngine;

[Serializable]
public enum LandmarkType
{
    None,
    RockBelt,
    Pirate,
    Alien,
    ShipYard,
    Resource
}

[CreateAssetMenu(fileName = "New Landmark", menuName = "Landmark")]
public class Lanmark : ScriptableObject
{
    public string locationName;
    public string peopleDialog;
    public LandmarkType type;

    public Sprite backgroundSprite;
    public Sprite mapIcon;
}