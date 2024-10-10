using System;
using float3 = Unity.Mathematics.float3;

namespace Runtime.Data.ValueObjects
{
    [Serializable]
    public struct PlayerData
    {
        public PlayerMovementData MovementData;
        public PlayerMeshData MeshData;
        public PlayerForceData ForceData;
    }
    
    [Serializable]
    public struct PlayerForceData
    {
        public float3 ForceParameters;
    }

    [Serializable]
    public struct PlayerMeshData
    {
        public float ScaleCounter;
    }

    [Serializable]
    public struct PlayerMovementData
    {
        public float ForwardSpeed;
        public float SidewaySpeed;
    }
}