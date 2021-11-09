using csmathlib;
using static csmathlib.Statics;

//δ is dirac delta

//HOW TO USE THE FOURIER TRANSFORM TO SOLVE DIFFERENTIAL EQUATIONS
//  Step 1: fourier transform both sides, making sure to replace derivative{n}'s with (2*pi*i*w)^n
//  Step 2: solve for what you want to solve for and inverse fourier transform the rest for a particular solution
//  Step 3: solve the original differential equation with x=0
//  Step 4: add the inverse fourier transform and the solution at x=0 to get a general solution
//
//example is shown below

//const double C = 1;
//Function X = new Function( x => x ).Fourier();
//Function y = new( x => new Function( w => X[ w ] / ( 2 * Math.PI * i * w - 1 ) ).InverseFourier( x ) + C * Exp( x ) );

Function SinW = new Function( x => Math.Sin( x ) ).Fourier();
Function yp = new Function( w => SinW[ w ] / ( Pow( 2 * Math.PI * i * w, 2 ) + ( 2 * Math.PI * i * w ) - 1 ) ).InverseFourier();

const double C1 = 2;
const double C2 = 3;
Function yh = new( x => C1 * Exp( -1/2 * ( 1 + Math.Sqrt( 5 ) ) * x ) + C2 * Exp( 1/2 * ( Math.Sqrt( 5 ) - 1 ) * x ) );

Function y = new( x => yp[ x ] + yh[ x ] );


for ( double x = -1; x < 1; x += .1 )
{
    Console.WriteLine( $"{x:F3}: {y[ x ]}" );
}