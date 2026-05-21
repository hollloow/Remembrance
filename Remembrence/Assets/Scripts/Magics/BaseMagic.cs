using UnityEngine;

public abstract class BaseMagic : MonoBehaviour
{
    //ainda n sei oq fazer aqui direito
    public int manaCost;
    
    public abstract void ApplyMagicEffect(float direction);
}
