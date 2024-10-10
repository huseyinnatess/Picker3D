using System;
using Runtime.Data.ValueObjects;
using Runtime.Managers;

namespace Runtime.Commands.Player
{
    public class ForceBallsToPoolCommand
    {
        private readonly PlayerManager _playerManager;
        private readonly PlayerForceData _playerDataForceData;

        public ForceBallsToPoolCommand(PlayerManager playerManager, PlayerForceData playerDataForceData)
        {
            _playerManager = playerManager;
            _playerDataForceData = playerDataForceData;
        }
        
        public void Execute()
        {
            
        }
    }
}