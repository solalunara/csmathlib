namespace csmathlib;

using static Statics;


public struct Complex
{
    internal Complex( double a, double b ) => (this.a, this.b) = (a, b);
    public double a;
    public double b;

    public static Complex operator +( Complex a ) => a;
    public static Complex operator -( Complex a ) => new( -a.a, -a.b );
    public static Complex operator +( Complex a, Complex b ) => new( a.a + b.a, a.b + b.b );
    public static Complex operator -( Complex a, Complex b ) => new( a.a - b.a, a.b - b.b );
    public static Complex operator *( Complex a, Complex b ) => new( a.a * b.a - a.b * b.b, a.b * b.a + a.a * b.b );
    public static Complex operator /( Complex a, Complex b ) => a * new Complex( b.a / ( b.a * b.a + b.b * b.b ), -b.b / ( b.a * b.a + b.b * b.b ) );

    public static implicit operator Complex( double a ) => new( a, 0 );

    public override string? ToString()
    {
        return $"{a} + {b}i";
    }
}

public class Function
{
    public Function( Func<double, Complex> f ) => this.f = f;
    private Func<double, Complex> f;

    public Complex this[ double x ]
    {
        get => f( x );
    }

    public Complex Integral( double b, double a )
    {
        Complex ret = 0;
        for ( double z = b; z < a; z += dx )
            ret += f( z ) * dx;
        return ret;
    }
    public Complex Integral( Complex C )
    {
        Complex ret = C;
        for ( double z = -Infinity; z < Infinity; z += dx )
        {
            ret += f( z ) * dx;
        }
        return ret;
    }

    public Complex Fourier( Complex w, Complex C )
    {
        return new Function( z => f( z ) * Exp( -2 * Math.PI * i * z * w ) ).Integral( C );
    }
    public Function Fourier( Complex C )
    {
        return new Function( z => Fourier( z, C ) );
    }
    public Complex InverseFourier( Complex x, Complex C )
    {
        return new Function( z => f( z ) * Exp( 2 * Math.PI * i * z * x ) ).Integral( C );
    }
    public Function InverseFourier( Complex C )
    {
        return new Function( z => InverseFourier( z, C ) );
    }
}