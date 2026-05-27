using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class PlayerStats
{
    //aqui ta todos os status do player
    
    //para o HP
    public static int PlayerHp = 30;
    public static int PlayerMaxHp = 30;
    
    //para a invencibilidade
    public static bool invincibility  = false;
    public static float InInvincibility =0f;
    public static float invincibilityTime =1.0f;
    
    //para morte
    public static bool Dead = false;

    //para magia
    public static float PlayerMana = 20;
    public static int PlayerManaMax = 20;
    public static GameObject Magic;
    public static bool MagicCoolDown = false;

    //para a cura
    public static float HealingTime = 2.0f;
    public static int HealingCost = 10;
    public static int HealingEffectiveness = 10;
    
    //para o dano do attack basico
    public static int PlayerBasicAttackDamage = 5;

    //para o reposicionamento na próxima scene
    public static Vector3 posicao = Vector3.zero;


}
