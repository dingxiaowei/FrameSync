using System;
//long a = 1000000;
//long b = 10000;
//a = a + b;


//long value = (long)(b * 1000 * 1.0f / a);

//Console.WriteLine($"{b * 1000 * 1.0f}/{a} 乘法扩大  =" + value);
////Example6();
//long value2 = (long)((b << 10) * 1.0f / a);
//Console.WriteLine($"10000000/1010000  移位运算 =" + value2);
//Console.ReadKey();
//为什莫采用x1024 而不是 x1000  因为程序中 2的次幂运算是最快的，一个字节是1024 byte 那么 1 这个数字就是由1024个字节组成的，
//因为我们是要给数字做一个扩大处理，来解决浮点数问题， 所以我们在运算的时候，1000和1024 是有差别的， 这个时候我们可以把一个int数值理解为一个byte字节
// 而1024个byte才代表一个值，而我们把一个字节扩大1000倍很显然是不准的，这种不准在数值计算量大的时候就会显现出来，会造成数值误差。
//所以说 1024 才是准确的一个倍数，我们使用这个值进行扩大之后，能够保证我们在计算中不会出现偏差。
//下面老师拿一个扩大1024 和扩大1000的例子，让大家看一下为什莫会有偏差。
[Serializable]
public struct VInt:IComparable<VInt>
{
    private long i;
    //位移计数
    const int FIX_MULTIPLE = 1024;

    public static readonly VInt one = new VInt((long)FIX_MULTIPLE);
    public int Int { get { return (int)i; } }
    public float RawFloat { get { return (float)this.i * 1.0f / FIX_MULTIPLE; } }
    public int RawInt { get { return (int)i / FIX_MULTIPLE; } }

    private VInt(long i)
    {
        this.i = i;
    }
    public VInt(int i)
    {
        this.i = i * FIX_MULTIPLE;
    }
    public VInt(float f)
    {
        this.i = (int)Math.Round((double)(f * 1.0f * FIX_MULTIPLE));
    }

    public override bool Equals(object o)
    {
        if (o == null)
        {
            return false;
        }
        VInt vInt = (VInt)o;
        return this.i == vInt.i;
    }

    public override int GetHashCode()
    {
        return this.i.GetHashCode();
    }

    public static VInt Min(VInt a, VInt b)
    {
        return new VInt(Math.Min(a.i, b.i));
    }

    public static VInt Max(VInt a, VInt b)
    {
        return new VInt(Math.Max(a.i, b.i));
    }

    public override string ToString()
    {
        return this.RawFloat.ToString();
    }

    public int CompareTo(VInt other)
    {
        return i.CompareTo(other.i);
    }

    public static explicit operator VInt(float f)
    {
        return new VInt((int)Math.Round((double)(f * 1.0f * FIX_MULTIPLE)));
    }

    public static implicit operator VInt(int i)
    {

        return new VInt(i);
    }

    public static explicit operator float(VInt ob)
    {
        return (float)ob.i * 1.0f / FIX_MULTIPLE;
    }

    public static explicit operator long(VInt ob)
    {
        return (long)ob.i;
    }

    public static VInt operator +(VInt a, VInt b)
    {
        return new VInt(a.i + b.i);
    }

    public static VInt operator -(VInt a, VInt b)
    {
        return new VInt(a.i - b.i);
    }

    public static VInt operator *(VInt a, VInt b)
    {
        long value = a.i * b.i;
        if (value >= 0)
        {
            value /= FIX_MULTIPLE;
        }
        else
        {
            value = -(-value / FIX_MULTIPLE);
        }
        return new VInt(value);
    }

    public static VInt operator /(VInt a, VInt b)
    {
        return new VInt((a.i * FIX_MULTIPLE / b.i));
    }
    public static bool operator ==(VInt a, VInt b)
    {
        return a.i == b.i;
    }
    public static VInt operator -(VInt a)
    {
        return new VInt(-a.i);
    }

    public static bool operator !=(VInt a, VInt b)
    {
        return a.i != b.i;
    }

    public static bool operator >(VInt a, VInt b)
    {
        return a.i > b.i;
    }
    public static bool operator <(VInt a, VInt b)
    {
        return a.i < b.i;
    }
    public static bool operator >=(VInt a, VInt b)
    {
        return a.i >= b.i;
    }
    public static bool operator <=(VInt a, VInt b)
    {
        return a.i <= b.i;
    }
    public static VInt operator >>(VInt value, int moveCount)
    {
        if (value.i >= 0)
        {
            return new VInt(value.i >> moveCount);
        }
        else
        {
            return new VInt(-(-value.i >> moveCount));
        }

    }
    public static VInt operator <<(VInt value, int moveCount)
    {
        return new VInt(value.i << moveCount);
    }
}
