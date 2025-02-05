using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public const KeyCode FlightKey = KeyCode.W;
    public const KeyCode ShootKey = KeyCode.Space;

    [SerializeField] private PlayerMover _mover;
    [SerializeField] private PlayerAttacker _attacker;

    private void Awake()
    {
        _mover = GetComponent<PlayerMover>();
        _attacker = GetComponent<PlayerAttacker>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(FlightKey))
        {
            _mover.Fly();
        }
        else if (Input.GetKeyDown(ShootKey))
        {
            _attacker.Attack();
        }
    }
}
