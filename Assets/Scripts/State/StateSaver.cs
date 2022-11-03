using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;

namespace State
{
    public class StateSaver
    {
        public static string StatePath => Path.Combine(Application.persistentDataPath, "state.json");

        private PlayerState _state;
        private string _path;
        private int _writeInterval;
        private Thread _thread;
        private JsonSerializer _serializer;

        public StateSaver(int writeInterval = 300)
        {
            _writeInterval = writeInterval;
            _serializer = new JsonSerializer();
            _path = StatePath;
            Application.focusChanged += OnFocus;
        }

        private void OnFocus(bool obj)
        {
            if (!obj)
            {
                WriteStateSync();
            }
        }

        public PlayerState Read()
        {
            return _serializer.Deserialize<PlayerState>(new JsonTextReader(new StreamReader(StatePath)));
        }

        public void SetState(PlayerState state)
        {
            _state = state;
            WriteState();
        }

        public void WriteStateSync()
        {
            if (_lock.IsWriterLockHeld)
            {
                Debug.Log("[SAVE] locked!");
                return;
            }

            if (_state != null)
            {
                Debug.Log("[SAVE] save stated!");
                try
                {
                    _lock.AcquireWriterLock(50);
                    var data = JsonConvert.SerializeObject(_state, Formatting.Indented);
                    File.WriteAllText(_path, data);
                    Debug.Log("[SAVE] File writed!");
                }
                catch (Exception ex)
                {
                    Debug.LogException(new Exception("[SAVE] esception", ex));
                }
                finally
                {
                    _lock.ReleaseWriterLock();
                }
            }
        }

        private ReaderWriterLock _lock = new ReaderWriterLock();

        private async Task WriteState()
        {
            while (_state != null)
            {
                await Task.Delay(_writeInterval);
                if (_state == null)
                    return;
                WriteStateSync();
            }
        }

        public void Dispose()
        {
            WriteStateSync();
            Application.focusChanged -= OnFocus;
            _state = null;
        }
    }
}