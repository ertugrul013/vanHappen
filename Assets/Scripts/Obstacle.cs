using System;

[Serializable]
public class Obstacle
{
    public enum Type
    {
        Bin,
        MoleHill
    }

    public Type myType;

    public Single offsetY;

} 