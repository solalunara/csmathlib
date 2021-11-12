using csmathlib;
using static csmathlib.Statics;

//δ is dirac delta

//HOW TO USE THE FOURIER TRANSFORM TO SOLVE DIFFERENTIAL EQUATIONS
//  Step 1: fourier transform both sides, making sure to replace derivative{n}'s with (2*pi*i*w)^n
//  Step 2: solve for what you want to solve for and inverse fourier transform the rest for a particular solution
//  Step 3: solve the original differential equation with x=0
//  Step 4: add the inverse fourier transform and the solution at x=0 to get a general solution
//

Function X = new Function( x => x ).Fourier();
Function y = new Function( w => X[ w ] / ( 2 * Math.PI * i * w ) ).InverseFourier();

for ( double t = -1; t < 1; t += .05 )
{
    Console.WriteLine( $"{t:F3}: {y[ t ]}" );
}