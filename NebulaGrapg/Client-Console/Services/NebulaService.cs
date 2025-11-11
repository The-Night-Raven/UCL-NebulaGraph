using Client.Console.Models;
using Nebula.Graph;
using NebulaNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Console.Services
{
    public class NebulaService : IDisposable
    {
        private bool _DisposedValue;
        private readonly NebulaConnection _Connection;
        private AuthResponse _Auth;

        private static async Task<Tuple<NebulaConnection,AuthResponse>> ConnectClient(string ip, int port, string username, string pass)
        {
            NebulaConnection connection = new NebulaConnection();
            await connection.OpenAsync(ip, port);
            AuthResponse authResponse = await connection.AuthenticateAsync(username, pass);
            return new Tuple<NebulaConnection, AuthResponse>(connection,authResponse);
        }
        public NebulaService(string ip, int port, string username, string pass)
        {
            Tuple<NebulaConnection, AuthResponse> result = ConnectClient(ip, port, username, pass).Result;
            _Connection = result.Item1;
            _Auth = result.Item2;
        }

        public async Task<ExecutionResponse> CreateVertexAsync<T>(T model) where T : INebulaTag
        {
            var query = model.Create();
            var resp = await _Connection.ExecuteAsync(_Auth.Session_id, query);
            return resp;
        }
        public async Task<ExecutionResponse> CreateEdgeAsync<T>(T model) where T : INebulaEdge
        {
            var query = model.Create();
            var resp = await _Connection.ExecuteAsync(_Auth.Session_id, query);
            return resp;
        }

        public async Task<ExecutionResponse> ExecuteAsync(string nGQL)
        {
            var resp = await _Connection.ExecuteAsync(_Auth.Session_id, nGQL);
            return resp;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_DisposedValue)
            {
                if (disposing)
                {
                    _Connection.Dispose();
                }
                _DisposedValue = true;
            }
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
