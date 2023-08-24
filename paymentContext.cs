public abstract class Strategy
{
    public abstract void paymentMethod();
}

public class creditCardMethod : Strategy
{
    public override void paymentMethod()
    {
        //add logic
    }
}

public class payPalMethod : Strategy
{
    public override void paymentMethod()
    {
        //add logic
    }
}

public class cashMethod : Strategy
{
    public override void paymentMethod()
    {
        //add logic
    }
}

public class Context
{
    Strategy strategy;
    // Constructor
    public Context(Strategy strategy)
    {
        this.strategy = strategy;
    }
    public void ContextInterface()
    {
        strategy.paymentMethod();
    }
}