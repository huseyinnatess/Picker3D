using UnityEngine;

namespace Runtime.Commands.Level
{
    public class OnLevelDestroyerCommand
    {
        private readonly Transform _levelHolder;
        public OnLevelDestroyerCommand(Transform levelHolder)
        {
            _levelHolder = levelHolder;
        }
        
        public void Execute()
        { 
            if (_levelHolder.childCount == 0) return;
            Object.Destroy(_levelHolder.transform.GetChild(0).gameObject);
        }
    }
}