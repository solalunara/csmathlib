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
    protected Func<double, Complex> f;

    public Complex this[ double x ]
    {
        get => f( x );
    }

    public Complex Derivative( double x, double dx = .1 )
    {
        return ( f( x + dx ) - f( x ) ) / dx;
    }
    public Function Derivative( double dx = .1 )
    {
        return new Function( x => Derivative( x, dx ) );
    }
    public Complex DerivativeN( double x, int N, double dx = .1 )
    {
        if ( N == 1 )
            return Derivative( x, dx );
        return Derivative( dx ).DerivativeN( x, N - 1, dx );
    }

    public Polynomial TaylorSeries( int N )
    {
        List<(Complex, int)> terms = new();
        terms.Add( (f( 0 ).a, 0) );
        for ( int b = 1; b < N; ++b )
        {
            terms.Add( (DerivativeN( 0, b ) / Factorial( b ), b) );
        }
        return new Polynomial( terms.ToArray() );
    }

    private long Factorial( long N )
    {
        if ( N <= 1 )
            return 1;
        return N * Factorial( N - 1 );
    }

    public Complex Fourier( Complex w, Complex C )
    {
        Function fn = new Function( z => f( z ) * Exp( -2 * Math.PI * i * z * w ) ).TaylorSeries( 10 ).Integral( C );
        return fn[ 1000 ] - fn[ -1000 ] + C;
    }
    public Function Fourier( Complex C )
    {
        return new Function( z => Fourier( z, C ) );
    }
    public Complex InverseFourier( Complex x, Complex C )
    {
        Function fn = new Function( z => f( z ) * Exp( 2 * Math.PI * i * z * x ) ).TaylorSeries( 10 ).Integral( C );
        return fn[ 1000 ] - fn[ -1000 ] + C;
    }
    public Function InverseFourier( Complex C )
    {
        return new Function( z => InverseFourier( z, C ) );
    }
}

public class Polynomial : Function
{
    public Polynomial( params (Complex, int)[] terms ) :
        base( x =>
        {
            Complex ret = 0;
            for ( int a = 0; a < terms.Length; ++a )
                ret += terms[ a ].Item1 * Math.Pow( x, terms[ a ].Item2 );
            return ret;
        } )
    {
        this.terms = terms;
    }

    private (Complex, int)[] terms;

    public Polynomial Integral( Complex C )
    {
        List<(Complex, int)> NewTerms = new();
        for ( int a = 0; a < terms.Length; ++a )
        {
            if ( terms[ a ].Item2 != 0 && terms[ a ].Item2 != -1 )
                NewTerms.Add( (terms[ a ].Item1 / terms[ a ].Item2, terms[ a ].Item2 + 1) );
        }
        NewTerms.Add( (C, 0) );
        return new Polynomial( NewTerms.ToArray() );
    }
    public Polynomial Derivative()
    {
        List<(Complex, int)> NewTerms = new();
        for ( int a = 0; a < terms.Length; ++a )
        {
            if ( terms[ a ].Item2 != 0 )
                NewTerms.Add( (terms[ a ].Item1 * terms[ a ].Item2, terms[ a ].Item2 - 1) );
        }
        return new Polynomial( NewTerms.ToArray() );
    }

    
}