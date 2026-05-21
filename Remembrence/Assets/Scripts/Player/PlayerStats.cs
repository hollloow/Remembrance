using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class PlayerStats
{
    //aqui ta todos os status do player
    public static int PlayerHp = 30;
    public static int PlayerMaxHp = 30;
    
    public static bool invincibility  = false;
    public static float InInvincibility =0f;
    public static float invincibilityTime =1.0f;
    
    public static bool Dead = false;

    public static int PlayerMana = 20;
    public static int PlayerManaMax = 20;
    
    public static int PlayerBasicAttackDamage = 5;
    
    public static int Money = 0;
    
}
