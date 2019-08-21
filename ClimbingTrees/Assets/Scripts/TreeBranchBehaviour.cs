﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeBranchBehaviour : MonoBehaviour
{

    MeshRenderer materialRenderer;
    
    private Transform teleportPosition;

    private Color color;

    private bool isCurrentBranch;

	[SerializeField]
	private bool isTreeBase;

    [SerializeField]
    private Collider endCollider;

    [SerializeField]
    private Collider beginningCollider;

    public bool IsCurrentBranch
    {
        get { return isCurrentBranch; }
        private set { isCurrentBranch = value; }
    }

	public bool IsTreeBase
    {
		get { return isTreeBase; }
		private set { isTreeBase = value; }
    }
    // Use this for initialization
    void Start()
    {
        GraspManager.PlayerTeleported += ResetBranch;
		GraspManager.PlayerReachedGound += ResetBranch;
        PlayerBalance.PlayerFellFromBranch += ResetBranch;
        materialRenderer = GetComponent<MeshRenderer>();
        color = materialRenderer.material.color;
        teleportPosition = transform;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (InputKeysManager.Instance.IsFalling || isCurrentBranch)
            return;

        if (materialRenderer.material.color == Color.yellow)
            materialRenderer.material.color = Color.green;
        else
            materialRenderer.material.color = Color.yellow;
    }

    private void OnTriggerExit(Collider collision)
    {
        if (InputKeysManager.Instance.IsFalling || isCurrentBranch)
            return;

        if (materialRenderer.material.color == Color.yellow)
            materialRenderer.material.color = color;
        else
            materialRenderer.material.color = Color.yellow;
    }

    private void ResetBranch()
    {
        isCurrentBranch = false;
        materialRenderer.material.color = color;
        if (!isTreeBase)
        {
            beginningCollider.enabled = false;
            endCollider.enabled = false;
        }        
    }

    public void SetAsCurrentBranch()
    {
        isCurrentBranch = true;
		Color transluscent = color;
		transluscent.a = 0.4f;
		materialRenderer.material.color = transluscent;
        if (!isTreeBase)
        {
            beginningCollider.enabled = true;
            endCollider.enabled = true;
        }
    }

    public Vector3 GetTeleportPosition()
    {
        return teleportPosition.position;
    }
}
