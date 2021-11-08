using csmathlib;
using static csmathlib.Statics;

//δ is dirac delta

Function δ1 = δ.Derivative();

Function X = new Function( x => x ).Fourier();
Function y = new Function( w => X[ w ] / ( 2 * Math.PI * i * w ) ).InverseFourier();


for ( double x = -1; x < 1; x += .1 )
{
    Console.WriteLine( $"{x:F3}: {y[ x ]}" );
}