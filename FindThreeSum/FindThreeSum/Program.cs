public class FindThreeSum
{
    public bool IsSumThreeZero(int[] arr)
    {
        int n = arr.Length;
        Array.Sort(arr);
        for (int i = 0; i < n - 2; i++)
        {
            int left = i + 1;
            int right = n - 1;

            while (left < right)
            {
                int sum = arr[i] + arr[left] + arr[right];

                if (sum == 0)
                {
                    return true;
                    break;
                }
                else if (sum < 0)
                {
                    left++;
                }
                else
                {
                    right--;
                }
            }
        }
        return false;
    }

    static void Main(string[] args)
    {
        int[] arr = { -1, -2, 3, 4 };
        FindThreeSum sum = new FindThreeSum();

        bool value = sum.IsSumThreeZero(arr);

        Console.WriteLine(value);
    }

}