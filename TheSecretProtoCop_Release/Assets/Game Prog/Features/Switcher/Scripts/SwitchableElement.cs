using UnityEngine;

public abstract class SwitchableElement : MonoBehaviour
{
    [Range(0, 1), SerializeField] protected int state;
    [Range(0, 1), SerializeField] protected int power;

    public abstract SwitchableType prefabType { get; }

    public GameObject MyGameObject { get { return this.gameObject; } set { MyGameObject = value; } }
    public virtual int Power { get { return state; } set { state = value; } }
    public virtual int State
    {
        get { return power; }
        set
        {
            power = value;
            if (power == 1) if (state == 1) TurnOn(); else TurnOff();
            else TurnOff();
        }
    }

    public abstract void TurnOn();

    public abstract void TurnOff();
}
