using System.Collections;

public static class MathUtilities
{
    public static int Modulo(int n, int m)
    {
        return n >= 0 ? n % m : ((n % m) + m) % m;
    }
}