namespace csmathlib;

//δ is dirac delta
public static class Statics
{
    public static readonly Complex i = new( 0, 1 );
    public static double Infinity = 6 * Math.PI;
    public static double dx = .03;
    public static readonly Function δ = new Function( x => 1 ).Fourier();

    public static Complex Exp( Complex z ) =>
        Math.Pow( Math.E, z.a ) * ( Math.Cos( z.b ) + Math.Sin( z.b ) * i );
    public static Complex Pow( double a, Complex b ) =>
        Exp( Math.Log( a ) * b );
    public static Complex Pow( Complex a, Complex b ) => 
        Exp( Math.Log( Math.Sqrt( a.a * a.a + a.b * a.b ) ) * b + b * Math.Atan( a.b / a.a ) * i );
}
