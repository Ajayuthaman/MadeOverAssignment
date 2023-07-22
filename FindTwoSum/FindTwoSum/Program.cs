public class FindSum
{
    public void IsSumTwoZero(int[] arr)
    {
        bool found = false;
        for (int i = 0; i < arr.Length - 1; i++)
        {

            for (int j = i + 1; j < arr.Length; j++)
            {
                if (arr[i] + arr[j] == 0)
                {
                    found = true;
                    Console.WriteLine("True: There are numbers whose sum is 0 numbers are: " + arr[i] + " & " + arr[j]);
                    break;
                }
            }

        }
        if (!found)
        {
            Console.WriteLine("False: There are No such numbers whose sum is 0");
        }
    }

    public static void Main(string[] args)
    {
        int[] arr = { -7, -5, 4, 5, 6 };
        int[] arr2 = { -7, -3, 4, 6, 10, 15 };

        FindSum sum = new FindSum();

        sum.IsSumTwoZero(arr);
    }


}
