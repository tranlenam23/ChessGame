using Microsoft.AspNetCore.SignalR;
using System.Runtime.CompilerServices;

    public class ChatHub : Hub
    {
        public async Task SendData(string ReceiveData ,string data,string id, int a)
        {
        
        // Gửi dữ liệu đến tất cả các phiên làm việc (clients) trên trang Razor
        await Clients.Others.SendAsync(ReceiveData, data,id,a);
        }
    public async Task extendA(string ChangeA)
    {
        
        // Gửi dữ liệu đến tất cả các phiên làm việc (clients) trên trang Razor
        await Clients.Others.SendAsync(ChangeA);
    }
    // send x,y,valu
    public async Task sendPos(string getPos,int X,int Y, int Value, int newX, int newY, int newValue)
    {
        
        await Clients.Others.SendAsync(getPos, X, Y, Value, newX, newY, newValue);
    }
    public async Task sendNewGame(string NewGame)
    {
        
        await Clients.Others.SendAsync(NewGame);
    }

    public async Task sendLoading(string Loading)
    {
        
        await Clients.Others.SendAsync(Loading);
    }
    public async Task readyStart(string Ready)
    {
        
        await Clients.Others.SendAsync(Ready);
    }
    public async Task readyStart1(string Ready1)
    {

        await Clients.Others.SendAsync(Ready1);
    }
    public async Task GameOver(string Over,string a)
    {
        
        await Clients.Others.SendAsync(Over, a);
    }
    public async Task GameWin(string Win, string a)
    {

        await Clients.Others.SendAsync(Win, a);
    }
    public async Task SendAvatar(string GetAvatar, string a)
    {

        await Clients.Others.SendAsync(GetAvatar, a);
    }
}
