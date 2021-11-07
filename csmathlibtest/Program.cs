using csmathlib;
using static csmathlib.Statics;

Function X = new Function( x => Math.Sin( x ) ).Fourier();
Function y = new Function( w => X[ w ] / ( 2 * Math.PI * i * w - 1 )).InverseFourier();


for ( double x = -5; x < 5; x += .01 )
{
    Console.WriteLine( $"{x:F3}: {X[ x ]}" );
}