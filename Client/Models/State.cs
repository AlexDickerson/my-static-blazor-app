namespace Client.Models
{
    public interface IState
    {
        string SessionID { get; set; }
    }
    public class State : IState
    {
        public string SessionID { get; set; }
        public State() { 
        }
    }
}
