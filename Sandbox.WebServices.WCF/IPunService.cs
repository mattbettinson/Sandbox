using System.ServiceModel;
using Sandbox.Data;

namespace Sandbox.WebServices.WCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IPunService" in both code and config file together.
    [ServiceContract]
    public interface IPunService
    {
        [OperationContract]
        Pun[] GetPuns();

        [OperationContract]
        Pun GetPunById(int id);

        [OperationContract]
        void CreatePun(Pun pun);

        [OperationContract]
        void UpdatePun(Pun pun);

        [OperationContract]
        void DeletePun(int id);

    }
}
