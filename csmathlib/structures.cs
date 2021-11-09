namespace csmathlib;

using System.Collections.Generic;
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
    public static Complex operator /( Complex a, Complex b )
    {
        if ( Math.Abs( b.a * b.b ) > .1 )
            return a * new Complex( b.a / ( b.a * b.a + b.b * b.b ), -b.b / ( b.a * b.a + b.b * b.b ) );

        return 0;
    }

    public static implicit operator Complex( double a ) => new( a, 0 );

    public override string? ToString()
    {
        return $"{a} + {b}i";
    }
}

public class Function
{
    public Function( Func<double, Complex> f ) => this.f = f;
    protected Func<double, Complex> f;

    public Complex this[ double x ]
    {
        get => f( x );
    }

    public Complex Derivative( double x )
    {
        return ( f( x + dx ) - f( x ) ) / dx;
    }
    public Function Derivative()
    {
        return new Function( x => Derivative( x ) );
    }
    public Complex DerivativeN( double x, int N )
    {
        if ( N == 1 )
            return Derivative( x );
        return Derivative().DerivativeN( x, N - 1 );
    }

    public Function Integral( Complex C )
    {
        return new Function( x =>
        {
            Complex ret = C;
            for ( double y = 0; y < Math.Abs( x ); y += dx )
            {
                Complex c = f( y * Math.Sign( x ) );
                ret += f( y * Math.Sign( x ) ) * Math.Sign( x ) * dx;
            }
            return ret;
        } );
    }
    

    public Complex Fourier( double w )
    {
        Function fn = new Function( z => f( z ) * Exp( -2 * Math.PI * i * z * w ) ).Integral( 0 );
        Complex ret = fn[ Infinity ] - fn[ -Infinity ];
        return ret;
    }
    public Function Fourier()
    {
        return new Function( z => Fourier( z ) );
    }
    public Complex InverseFourier( double x )
    {
        Function fn = new Function( z => f( z ) * Exp( 2 * Math.PI * i * z * x ) ).Integral( 0 );
        Complex ret = fn[ Infinity ] - fn[ -Infinity ];
        return ret;
    }
    public Function InverseFourier()
    {
        return new Function( z => InverseFourier( z ) );
    }

    public Function Convolution( Function g )
    {
        return new Function( t =>
        {
            Function l = new Function( tau =>
            {
                return f( tau ) * g[ t - tau ];
            } ).Integral( 0 );
            return l[ Infinity ] - l[ -Infinity ];
        } );
    }
}