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

//dy/dx = cos( x ) + y

const double F = 10;
const double M = 100;
const double r = -1;
const double C = 0;

Function x = new Function( t => (F*t + C)/(M + r*t) ).Integral( 0 );

for ( double t = 0; t < 10; t += .5 )
{
    Console.WriteLine( $"{t:F3}: {x[ t ]}" );
}