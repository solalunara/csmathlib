using csmathlib;
using static csmathlib.Statics;

//Function X = new Function( x => x ).Fourier( 0 );
Function y = new Function( w => 1 / ( 2 * Math.PI * i * w - 1 )).InverseFourier( 0 );

for ( double x = -20; x < 10; x += .1 )
{
    Console.WriteLine( $"{x:F3}: {y[ x ]}" );
}