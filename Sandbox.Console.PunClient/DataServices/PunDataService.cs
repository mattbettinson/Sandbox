using System;
using Sandbox.Console.PunClient.WCFPunService;
using Sandbox.Data;

namespace Sandbox.Console.PunClient.DataServices
{
    public class PunDataService
    {
        public PunDataService()
        {
            _client = new PunServiceClient();
        }

        public Pun[] GetPuns()
        {
            return _client.GetPuns();
        }

        public Pun GetPunByID(int punID)
        {
            return _client.GetPunById(punID);
        }

        public void CreatePun(Pun pun)
        {
            _client.CreatePun(pun);
        }

        public void UpdatePun(Pun pun)
        {
            _client.UpdatePun(pun);
        }

        public void DeletePun(int punID)
        {
            _client.DeletePun(punID);
        }

        private readonly PunServiceClient _client;
    }
}
