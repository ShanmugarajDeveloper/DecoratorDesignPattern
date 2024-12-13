// Step 1: Component interface is missing, violating the pattern.


public interface ICoffee
{
    public string GetDescription();
    public double GetCost();
}
public class Coffee : ICoffee
{
    public string GetDescription()
    {
        return "Plain Coffee";
    }

    public double GetCost()
    {
        return 5.0;
    }
}

public class CoffeeDecorator : ICoffee
{
    public ICoffee _coffee;
    public CoffeeDecorator(ICoffee coffee)
    {
        _coffee = coffee;
    }
    public virtual string GetDescription()
    {
        return _coffee.GetDescription();
    }

    public virtual double GetCost()
    {
        return _coffee.GetCost();
    }
}

// Step 2: Instead of using composition, decorators inherit directly from Coffee.
public class MilkCoffee : CoffeeDecorator
{
    public MilkCoffee(ICoffee coffee) : base(coffee)
    {

    }
    public override string GetDescription()
    {
        return base.GetDescription() + ", Milk"; // Direct dependency on base class.
    }

    public override double GetCost()
    {
        return base.GetCost() + 1.5; // Violates Open/Closed principle.
    }
}

public class SugarCoffee : CoffeeDecorator
{
    public SugarCoffee(ICoffee coffee) : base(coffee)
    {

    }
    public override string GetDescription()
    {
        return base.GetDescription() + ", Sugar"; // Breaks SRP by mixing concerns.
    }

    public override double GetCost()
    {
        return base.GetCost() + 0.5; // Hardcoding cost without flexibility.
    }
}

// Step 3: Client code tightly couples to concrete classes instead of an interface.
class Program
{
    static void Main(string[] args)
    {
        // Plain coffee
        ICoffee coffee = new Coffee();
        Console.WriteLine($"{coffee.GetDescription()} - ${coffee.GetCost()}");

        // Adding milk
        coffee = new MilkCoffee(coffee);
        Console.WriteLine($"{coffee.GetDescription()} - ${coffee.GetCost()}");

        // Adding sugar
        coffee = new SugarCoffee(coffee);
        Console.WriteLine($"{coffee.GetDescription()} - ${coffee.GetCost()}");
    }
}
