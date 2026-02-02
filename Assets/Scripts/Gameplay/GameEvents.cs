using System;

public static class GameEvents
{
    public static Action<float> OnPlayerHealed;
    public static Action<float> OnPlayerDamaged;
    public static Action<float> OnHealthChanged;
    public static Action OnPlayerDied;

    public static Action<string> OnMapChanged;
}
