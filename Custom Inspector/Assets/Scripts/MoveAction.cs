using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAction : MonoBehaviour
{
    public enum MoveActionType
    {
        MOVE_TO_TRANSFORM,
        MOVE_RANDOM
    }

    [Serializable]
    public class MoveToTransformConfig
    {
        public Vector3 TargetPosition;
        public float MoveSpeed;
    }

    [Serializable]
    public class MoveRandomConfig
    {
        public Vector3 RandomDistance;
        public Vector3 RandomSwitchDelay;
        public float MoveSpeed;
    }

    [SerializeField] 
    private MoveActionType m_moveActionType;
    [SerializeField] 
    private MoveToTransformConfig m_moveToTransformConfig;
    [SerializeField] 
    private MoveRandomConfig m_moveRandomConfig;

    public MoveActionType CurrentMoveActionType => m_moveActionType;
    public MoveToTransformConfig CurrentMoveToTransformConfig => m_moveToTransformConfig;
    public MoveRandomConfig CurrentMoveRandomConfig => m_moveRandomConfig;
}
