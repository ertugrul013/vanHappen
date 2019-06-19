using System;

[Serializable]
public class Obstacle
{
    public enum Type
    {
        Bin,
        MoleHill,
        TrashMonster
    }

    public Type myType;

    public Single offsetY;

} 