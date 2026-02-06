using Photon.Pun;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviourPunCallbacks
{
    int playerHp = 100;

    private InputAction fireAction;

    private GameObject _mainCamera;

    private int layerMask;

    public bool canFire = true;

    public static GameObject LocalPlayerInstance;

    public static GameObject RSPInstance;

    private void Awake()
    {
        if (_mainCamera == null)
        {
            _mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        }
        canFire = true;
        fireAction = InputSystem.actions["Attack"];
        layerMask = (1 << LayerMask.NameToLayer("Ground") | 1 << LayerMask.NameToLayer("Player"));
    }

    public void Damage(int damage)
    {
        playerHp -= damage;
        GameManager.instance.UpdateHp(playerHp);
        if (playerHp <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        if (!photonView.IsMine)
        {
            return;
        }

        GameManager.instance.LeaveRoom();
    }

    public void AiminTarget()
    {
        if (!photonView.IsMine) { return; }

        Vector3 head = _mainCamera.transform.position;
        Vector3 direction = _mainCamera.transform.forward;

        if (Physics.Raycast(head, direction, out var hit, 5000f, layerMask, QueryTriggerInteraction.Ignore))
        {
            if (hit.collider.CompareTag("Player"))
            {
                var targetView = hit.collider.GetComponentInParent<PhotonView>();

                if (targetView != null)
                {
                    targetView.RPC(nameof(RPCApplyDamage), targetView.Owner, 10);
                }
            }
        }
    }

    [PunRPC]
    private void RPCApplyDamage(int damage, PhotonMessageInfo info)
    {
        if (!photonView.IsMine) { return; }

        Damage(damage);
    }

    public void OnAttack(InputValue value)
    {
        if (!photonView.IsMine)
            return;

        if (!value.isPressed) return;

        if (!canFire) return;

        canFire = false;
        AiminTarget();
        StartCoroutine(FireDelay());
    }

    public IEnumerator FireDelay()
    {
        yield return new WaitForSeconds(0.5f);

        canFire = true;
    }
}
