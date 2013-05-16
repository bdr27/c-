
namespace TicTacToeClient
{
    interface MessageHandler
    {
        void ConnectTo(string ipAddress, int portNumber);
        void SendRequest(string request);
        string GetResponse();
    }
}
