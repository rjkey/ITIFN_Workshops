namespace tpudpserver
{
    public class Program
    {
        static void Main(string[] args)
        {
            var server = new UdpServer();
            server.StartServer();
        }
    }
}
