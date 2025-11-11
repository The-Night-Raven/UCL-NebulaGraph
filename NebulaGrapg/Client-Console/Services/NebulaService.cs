using NebulaNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client_Console.Services
{
    public class NebulaService : IDisposable
    {
        private bool _DisposedValue;
        private readonly NebulaConnection _Connection;

        public NebulaService(string ip, int port, string username, string pass)
        {
            
            _Connection = ConnectClient(ip, port, username, pass).Result;
        }
        private static async Task<NebulaConnection> ConnectClient(string ip, int port, string username, string pass)
        {
            NebulaConnection connection = new NebulaConnection();
            await connection.OpenAsync(ip, port);
            await connection.AuthenticateAsync(username, pass);
            return connection;
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
