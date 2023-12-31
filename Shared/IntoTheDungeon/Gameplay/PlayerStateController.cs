using System.Collections.Generic;

namespace IntoTheDungeon.Gameplay
{
    public enum PlayerAction
    {
        None = 0, Move, Attack, Block, Undo, Use, PrepareAttack, PrepareMove
    }
    public enum PlayerState
    {
        None = 0, Moved, Attacked, Block, UndoMove, PrepareAttack
    }
    public class PlayerStateController
    {
        public PlayerStateController() {
            _statesGraph = new Dictionary<PlayerState, Dictionary<PlayerAction, PlayerState>>();
            _statesGraph[PlayerState.None] = new Dictionary<PlayerAction, PlayerState>() { 
                [PlayerAction.Move] = PlayerState.Moved,
                [PlayerAction.Attack] = PlayerState.Attacked,
                [PlayerAction.Block] = PlayerState.Block,
            };
            _statesGraph[PlayerState.Moved] = new Dictionary<PlayerAction, PlayerState>()
            {
                [PlayerAction.Undo] = PlayerState.None,
                [PlayerAction.Attack] = PlayerState.Attacked,
                [PlayerAction.Block] = PlayerState.Block,
            };
            _statesGraph[PlayerState.Attacked] = new Dictionary<PlayerAction, PlayerState>();
            _statesGraph[PlayerState.Block] = new Dictionary<PlayerAction, PlayerState>()
            {
                [PlayerAction.Undo] = PlayerState.None,
                [PlayerAction.Attack] = PlayerState.Attacked,
            };
            _statesGraph[PlayerState.PrepareAttack] = new Dictionary<PlayerAction, PlayerState>() {
                [PlayerAction.Undo] = PlayerState.None,
                [PlayerAction.Attack] = PlayerState.Attacked,
                [PlayerAction.Block] = PlayerState.Block,
                [PlayerAction.Move] = PlayerState.Moved
            };
        }

        private PlayerState _state = PlayerState.None;
        private Dictionary<PlayerState, Dictionary<PlayerAction, PlayerState>> _statesGraph;

        public PlayerState GetState()
        {
            return _state;
        }

        public void ChangeState(PlayerAction action)
        {
            if (_statesGraph[_state].ContainsKey(action)) { 
                _state = _statesGraph[_state][action];
            }
        }

        public bool CanChangeState(PlayerAction action)
        {
            return _statesGraph[_state].ContainsKey(action);
        }

        public void Reset()
        {
            _state = PlayerState.None;
        }
    }
}