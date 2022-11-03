using System;
using System.Collections.Generic;
using Balance.Config;
using Balance.Data;
using Core;
using Core.Level;
using Core.PlayerSystem;
using Entities;
using Models;
using State;
using UI;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Initialize
{
    public class EntryPoint : MonoBehaviour, IDisposable, IStarter
    {
        [SerializeField] private UiManager _uiManager;
        private DisposableList _disposableList = new DisposableList();
        private List<IUpdatable> _updatables = new List<IUpdatable>();
        private StateSaver _stateSaver;
        private TimeProvider _timeProvider;
        private Player _player;
        private EndGameProvider _endGameProvider;
        private PlayerState _state;

        private void OnDestroy()
        {
            _stateSaver.WriteStateSync();
            _stateSaver.Dispose();
            Dispose();
        }

        void Start()
        {
            enabled = false;
            _stateSaver = new StateSaver();
            _timeProvider = new TimeProvider();
            var balance = Initializer.GetBalance();
            try
            {
                _state = _stateSaver.Read();
            }
            catch (Exception e)
            {
                _state = new PlayerState();
                _stateSaver.SetState(_state);
            }

            var difficultProvider = new DifficultProvider(balance.Difficultes);
            _player = Initializer.GetPlayer(balance.PlayerData, difficultProvider, _timeProvider, _updatables, _disposableList);
            WallUpdatable wallUpdatable = Initializer.GetWallUpdatable(difficultProvider, _timeProvider, balance.LevelData, _updatables);
            ScoreProvider scoreProvider = new ScoreProvider(_state);
            List<IDropable> dropables = new List<IDropable>() { _player, wallUpdatable, _timeProvider };
            _endGameProvider =
                Initializer.GetEndGameProvider(_timeProvider, _state, _player, _disposableList, scoreProvider, dropables);
            Dependencys dependencys = new Dependencys(_endGameProvider, scoreProvider, this, difficultProvider);
            _uiManager.Bind(dependencys);
        }

        private void Update()
        {
            if (_endGameProvider.IsEnd)
            {
                return;
            }
            _timeProvider.Update();
            foreach (var updatable in _updatables)
            {
                updatable.Updating();
            }
        }

        public void Dispose()
        {
            _disposableList?.Dispose();
        }

        public void Enable()
        {
            _state.TryCount += new Amount(1);
            _player.Enable();
            enabled = true;
        }
    }

    public static class Initializer
    {
        public static BalanceData GetBalance()
        {
            var config = Resources.Load<BalanceConfig>("BalanceConfig");
            var data = config.Get();
            Resources.UnloadAsset(config);
            return data;
        }

        public static Player GetPlayer(PlayerData playerData, DifficultProvider difficultProvider,
            ITimeProvider timeProvider, List<IUpdatable> updatables, DisposableList disposableList)
        {
            var playerPrefab = Resources.Load<PlayerModel>("PlayerModel");
            var playerModel = Object.Instantiate(playerPrefab, playerData.StartPosition,Quaternion.identity);
            var player = new Player(playerModel, difficultProvider, playerData, timeProvider);
            updatables.Add(player);
            disposableList.Add(player);
            return player;
        }

        public static WallUpdatable GetWallUpdatable(IDifficultProvider difficultProvider, ITimeProvider timeProvider,
            LevelData levelData, List<IUpdatable> updatables)
        {
            var wall = Resources.Load<WallCell>("WallCell");
            WallFactory wallFactory = new WallFactory(wall);
            var pool = new WallPool(wallFactory);
            var wallUpdatable = new WallUpdatable(timeProvider, pool, levelData, difficultProvider);
            updatables.Add(wallUpdatable);
            return wallUpdatable;
        }

        public static EndGameProvider GetEndGameProvider(ITimeProvider timeProvider, PlayerState playerState,
            Player player, DisposableList disposableList, ScoreProvider scoreProvider, List<IDropable> dropables)
        {
            var endGameProvider = new EndGameProvider(player, playerState, timeProvider, scoreProvider, dropables);
            endGameProvider.AddTo(disposableList);
            return endGameProvider;
        }
    }
}
