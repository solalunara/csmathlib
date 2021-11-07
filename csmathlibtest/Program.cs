using csmathlib;
using static csmathlib.Statics;

Function X = new Function( x => x ).Fourier();
Function y = new Function( w => X[ w ] / ( 2 * Math.PI * i * w - 1 ) ).InverseFourier();


for ( double x = -1; x < 1; x += .1 )
{
    Console.WriteLine( $"{x:F3}: {y[ x ]}" );
}