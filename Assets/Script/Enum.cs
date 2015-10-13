using UnityEngine;
using System.Collections;

public enum RESOURCE
{
    NONE = 0,
    BRICK,
    WHEAT,
    ROCK,
    WOOD,
    SHEEP,
    DESERT
}

public enum BUILDING
{
    NONE = 0,
    VILLAGE,
    CITY,
    STREET
}

public enum PLAYER
{
    NONE = 0,
    A,
    B,
    C,
    D
}

public enum GAMESTATE
{
    NONE = 0,
    IDLE,
    FIRSTSETTING,
    FIRSTBUILDING,
    FIRSTSTREET,
    THROWDICE,
    GETCARD,
    TURN,
    BUILDINGSETTING,
    STREETSETTING
}