using MySql.Data.MySqlClient;
public interface IHandler
{
    IHandler SetNext(IHandler handler);
    string HandleRequest(string userCommand, User activeUser, string connectionstring, PaymentSetter paymentSetter, CreditCardPayment activeCredit, PaymentInfoToStore paymentInfoToStore);
}
public class LoginHandler : IHandler
{
    private IHandler _nextHandler;

    public IHandler SetNext(IHandler handler)
    {
        _nextHandler = handler;
        return handler;
    }

    public string HandleRequest(string userCommand, User activeUser, string connectionstring, PaymentSetter paymentSetter, CreditCardPayment activeCredit, PaymentInfoToStore paymentInfoToStore)
    {
        if (userCommand == "login" || userCommand == "Login")
        {
            return UserManager.Instance.Login(userCommand, activeUser, connectionstring, paymentSetter, activeCredit, paymentInfoToStore);
        }
        else if (_nextHandler != null)
        {
            return _nextHandler.HandleRequest(userCommand, activeUser, connectionstring, paymentSetter, activeCredit, paymentInfoToStore);
        }
        else
        {
            return "please re enter your command.";
        }
    }
}

// Concrete register handler
public class RegisterHandler : IHandler
{
    private IHandler _nextHandler;

    public IHandler SetNext(IHandler handler)
    {
        _nextHandler = handler;
        return handler;
    }

    public string HandleRequest(string userCommand, User activeUser, string connectionstring, PaymentSetter paymentSetter, CreditCardPayment activeCredit, PaymentInfoToStore paymentInfoToStore)
    {
        if (userCommand == "Register" || userCommand == "register")
        {
            UserManager.Instance.getUserData(activeUser);
            PaymentMethods.GetUserPaymentMethod(paymentSetter, activeCredit, paymentInfoToStore);
            return UserManager.Instance.Register(activeUser, connectionstring, paymentInfoToStore, activeCredit);
        }
        else if (_nextHandler != null)
        {
            return _nextHandler.HandleRequest(userCommand, activeUser, connectionstring, paymentSetter, activeCredit, paymentInfoToStore);
        }
        else
        {
            return "please re enter your command.";
        }
    }
}