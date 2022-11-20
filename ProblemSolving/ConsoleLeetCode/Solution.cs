namespace ConsoleLeetCode;

public class Solution
{
    public int Calculate(string inputText)
    {
        var tokens = new Tokens($"{inputText}=".Replace(" ", ""));
        ICalculator input = new Input(tokens);
        var result = input.Calculate();
        return (int)result;
    }
}


public class Tokens : List<char>
{
    public int Index;
    public char Current => this[Index];
    public char Next => this[Index + 1];

    public Tokens(string tokens)
    {
        AddRange(tokens);
        Index = 0;
    }

    public char GetCurrent()
    {
        var curr = Current;
        Index++;
        return curr;
    }
}

public interface ICalculator
{
    long Calculate();
}

public class Input : ICalculator
{
    private Expr _expr;
    public Input(Tokens tokens)
    {
        _expr = new Expr(tokens);
    }

    public long Calculate()
    {
        return _expr.Calculate();
    }
}

public class Expr : ICalculator
{
    private Term _term;
    private char _operator = default;
    private Expr _expr = null;
    public Expr(Tokens tokens)
    {
        _term = new Term(tokens);
        if (tokens.Current == '+' || tokens.Current == '-')
        {
            _operator = tokens.GetCurrent();
            _expr = new Expr(tokens);
        }
    }

    public long Calculate()
    {
        long left = _term.Calculate();
        if (_operator == default)
        {
            return left;
        }
        else
        {
            return _expr.CalcExpr(left, _operator);
        }
    }

    public long CalcExpr(long prevLeft, char op)
    {
        var left = _term.Calculate();
        var newLeft = op == '+' ? prevLeft + left : prevLeft - left;
        if (_operator == default)
        {
            return newLeft;
        }
        else
        {
            return _expr.CalcExpr(newLeft, _operator);
        }
    }
}

public class Term : ICalculator
{
    private Factor _factor;
    private char _operator = default;
    private Term _term = null;
    public Term(Tokens tokens)
    {
        _factor = new Factor(tokens);
        if (tokens.Current == '*' || tokens.Current == '/')
        {
            _operator = tokens.GetCurrent();
            _term = new Term(tokens);
        }
    }

    public long Calculate()
    {
        long left = _factor.Calculate();
        if (_operator == default)
        {
            return left;
        }
        else
        {
            return _term.CalcTerm(left, _operator);
        }
    }

    public long CalcTerm(long prevLeft, char op)
    {
        var left = _factor.Calculate();
        var newLeft = op == '*' ? prevLeft * left : prevLeft / left;
        if (_operator == default)
        {
            return newLeft;
        }
        else
        {
            return _term.CalcTerm(newLeft, _operator);
        }

    }
}

public class Factor : ICalculator
{
    private Power _power;
    private char _operator = default;
    private Factor _factor;
    public Factor(Tokens tokens)
    {
        _power = new Power(tokens);
        if (tokens.Current == '^')
        {
            _operator = tokens.GetCurrent();
            _factor = new Factor(tokens);
        }
    }

    public long Calculate()
    {
        long basee = _power.Calculate();
        if (_operator != default)
        {
            long aaa = _factor.Calculate();
            return basee.Pow((int)aaa);
        }
        return basee;
    }
}

public class Power : ICalculator
{
    private char _operator = default;
    private Power _power;
    private Root _root;
    public Power(Tokens tokens)
    {
        if (tokens.Current == '#')
        {
            _operator = tokens.GetCurrent();
            _power = new Power(tokens);
        }
        else
        {
            _root = new Root(tokens);
        }
    }

    public long Calculate()
    {
        if (_operator != default)
        {
            long value = _power.Calculate();
            return MathEx.Sqrt(value);
        }
        else
        {
            return _root.Calculate();
        }
    }
}

public class Root : ICalculator
{
    private Num _num;
    private Expr _expr;
    public Root(Tokens tokens)
    {
        if (tokens.Current == '(')
        {
            tokens.GetCurrent(); // "("
            _expr = new Expr(tokens);
            tokens.GetCurrent(); // ")"
        }
        else
        {
            _num = new Num(tokens);
        }
    }

    public long Calculate()
    {
        return _num?.Calculate() ?? _expr.Calculate();
    }
}

public class Num : ICalculator
{
    private char _zero = default;
    private char _negative = default;
    private Expr _expr;
    private NonZero _nonZero;
    private List<Digit> _digits = new();
    public Num(Tokens tokens)
    {
        if (tokens.Current == '0')
        {
            _zero = tokens.GetCurrent();
        }
        else if (tokens.Current == '-')
        { 
            _negative = tokens.GetCurrent();
            if (tokens.Current == '(')
            { 
		        tokens.GetCurrent(); // "("
		        _expr = new Expr(tokens);
		        tokens.GetCurrent(); // ")"
	        }
            else
            { 
                _nonZero = new NonZero(tokens);
                while (Digit.IsDigit(tokens.Current))
                {
                    var digit = new Digit(tokens);
                    _digits.Add(digit);
                }
	        }
	    }
        else
        {
            _nonZero = new NonZero(tokens);
            while (Digit.IsDigit(tokens.Current))
            {
                var digit = new Digit(tokens);
                _digits.Add(digit);
            }
        }
    }

    public long Calculate()
    {
        if (_zero != default)
        {
            return 0;
        }
        else if (_negative != default)
        { 
            if (_expr != null)
            {
                return -1 * _expr.Calculate();
	        }
	    }

        long value = _nonZero.Calculate();
        foreach (var digit in _digits)
        {
            value *= 10;
            value += digit.Calculate();
        }
        return _negative == default ? value : -value;
    }
}

public class Digit : ICalculator
{
    private char _zero = default;
    private NonZero _nonZero;
    public Digit(Tokens tokens)
    {
        if (tokens.Current == '0')
        {
            _zero = tokens.GetCurrent();
        }
        else
        {
            _nonZero = new NonZero(tokens);
        }
    }

    public long Calculate()
    {
        if (_zero != default)
        {
            return 0;
        }
        else
        {
            return _nonZero.Calculate();
        }
    }

    public static bool IsDigit(char token)
    {
        return token == '0'
            || token == '1'
            || token == '2'
            || token == '3'
            || token == '4'
            || token == '5'
            || token == '6'
            || token == '7'
            || token == '8'
            || token == '9';
    }
}

public class NonZero : ICalculator
{
    private char _digit = default;
    public NonZero(Tokens tokens)
    {
        _digit = tokens.GetCurrent();
    }

    public long Calculate()
    {
        return _digit - '0';
    }
}



public static class MathEx
{

    /// <summary>
    /// pow1의 exp제곱을 구한다. \n
    /// 2^10 = 2.Pow(10, 1, (a, b) => a * b);
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="base">밑</param>
    /// <param name="exp">지수</param>
    /// <param name="pow0">밑의 0제곱</param>
    /// <param name="fnMultifly">곱셈연산</param>
    /// <returns></returns>
    public static T Pow<T>(this T @base, int exp, T pow0, Func<T, T, T> fnMultifly)
    {
        // Addition-Chain exponentiation

        var basee = @base;
        var res = pow0;

        while (exp > 0)
        {
            if ((exp & 1) != 0)
                res = fnMultifly(res, basee);
            exp >>= 1;
            basee = fnMultifly(basee, basee);
        }

        return res;
    }

    public static int Pow(this int @base, int exp)
    {
        return @base.Pow(exp, 1, (a, b) => a * b);
    }

    public static long Pow(this long @base, int exp)
    {
        return @base.Pow(exp, 1, (a, b) => a * b);
    }
    public static long Gcd(long a, long b)
    {
        if (a == b) { return a; }
        else if (a > b && a % b == 0) { return b; }
        else if (b > a && b % a == 0) { return a; }

        long _gcd = 0;
        while (b != 0)
        {
            _gcd = b;
            b = a % b;
            a = _gcd;
        }
        return _gcd;
    }

    public static long Lcm(long a, long b)
    {
        var gcd = Gcd(a, b);
        var lcm = (a / gcd) * b;
        return lcm;
    }

    /// <summary> [a, b) 인지 판단한다.  </summary>
    public static bool Between(this int value, int a, int b)
    {
        return value >= a && value < b;
    }

    public static long Sqrt(long value)
    {
        long a = 0;
        long c = 3037000499;

        while (a <= c)
        {
            long b = (a + c) / 2;
            long square = b * b;

            if (value == square)
            {
                return b;
            }
            else if (value < square)
            {
                c = b - 1;
            }
            else
            {
                a = b + 1;
            }
        }

        return c;
    }
}

