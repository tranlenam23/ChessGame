
using System.Collections.Generic;
using System.Diagnostics;
using System.Xml.Linq;

namespace ChessGame.Controllers
{

	public class InputArrayModel
	{
		public List<List<int>> InputArray { get; set; }
	}
    public class ChessComparer : IComparer<int[][]>
    {
		private bool luotdi;
		public ChessComparer(bool luotdi)
		{
			this.luotdi = luotdi;
		}
        public int Compare(int[][] x, int[][] y)
        {
			int X = SumArray(x, luotdi);
			int Y = SumArray(y, luotdi);
			if (luotdi)
			{
				if(X == Y)
				{
					return 1;
				}
                return X.CompareTo(Y);
			}
			else
			{
                if (X == Y)
                {
                    return 1;
                }
                return Y.CompareTo(X);
            }
                
        }
        public int SumArray(int[][] input, bool luot)
        {
            var u = Danger_King(input, true);
            var v = Danger_King(input, false);
            // độ liên kết giữa các quân cờ
            int sum = 0;
            // không gian chiếm giữ
            double space = 0;
            double score = 0.5;
            int value = 2;
            for (int i = 1; i <= 8; i++)
            {
                for (int j = 1; j <= 8; j++)
                {
                    if (input[i][j] == 0)
                    {
                    }
                    else if (input[i][j] == 1)
                    {
                        sum -= 10 * value;
                        sum -= (i + 1);
                        if (i + 1 <= 8)
                        {
                            if (j + 1 <= 8)
                            {
                                if (input[i + 1][j + 1] == 0)
                                {
                                    space -= score;
                                }
                                else
                                {
                                    sum--;
                                }
                            }
                            if (j - 1 >= 1)
                            {
                                if (input[i + 1][j - 1] == 0)
                                {
                                    space -= score;
                                }
                                else
                                {
                                    sum--;
                                }
                            }

                        }


                    }
                    else if (input[i][j] == 5)
                    {
                        sum -= 50 * value;
                        for (int k = i - 1; k >= 1; k--)
                        {
                            if (input[k][j] == 0 || input[k][j] == 20)
                            {
                                space -= score;
                                continue;
                            }
                            else if (input[k][j] > 0)
                            {
                                sum--;
                                break;
                            }
                            break;
                        }
                        for (int k = i + 1; k <= 8; k++)
                        {
                            if (input[k][j] == 0 || input[k][j] == 20)
                            {
                                space -= score;
                                continue;
                            }
                            else if (input[k][j] > 0)
                            {
                                sum--;
                                break;
                            }
                            break;
                        }
                        for (int k = j - 1; k >= 1; k--)
                        {
                            if (input[i][k] == 0 || input[i][k] == 20)
                            {
                                space -= score;
                                continue;
                            }
                            else if (input[i][k] > 0)
                            {
                                sum--;
                                break;
                            }
                            break;
                        }
                        for (int k = j + 1; k <= 8; k++)
                        {
                            if (input[i][k] == 0 || input[i][k] == 20)
                            {
                                space -= score;
                                continue;
                            }
                            else if (input[i][k] > 0)
                            {
                                sum--;
                                break;
                            }
                            break;
                        }
                    }
                    else if (input[i][j] == 2)
                    {
                        sum -= 30 * value;
                        for (int k = 1; k <= 8; k++)
                        {
                            for (int l = 1; l <= 8; l++)
                            {
                                if ((k == i - 1 && l == j - 2) || (k == i + 1 && l == j - 2) || (k == i + 2 && l == j - 1) || (k == i + 2 && l == j + 1) || (k == i + 1 && l == j + 2) || (k == i - 1 && l == j + 2) || (k == i - 2 && l == j + 1) || (k == i - 2 && l == j - 1))
                                {
                                    if (k > 0 && k <= 8 && l > 0 && l <= 8)
                                    {
                                        if (input[k][l] == 0)
                                        {
                                            space -= score;
                                        }
                                        else if (input[i][j] > 0)
                                        {
                                            sum--;
                                        }
                                    }

                                }

                            }
                        }
                    }
                    else if (input[i][j] == 3)
                    {
                        sum -= 30 * value;
                        for (int k = 1; k <= 8; k++)
                        {
                            if (i - k > 0 && j - k > 0)
                            {
                                if (input[i - k][j - k] == 0 || input[i - k][j - k] == 20)
                                {
                                    space -= score;
                                    continue;
                                }
                                else
                                {
                                    sum--;
                                    break;
                                }
                                break;
                            }
                        }
                        for (int k = 1; k <= 8; k++)
                        {
                            if (i + k <= 8 && j + k <= 8)
                            {
                                if (input[i + k][j + k] == 0 || input[i + k][j + k] == 20)
                                {
                                    space -= score;
                                    continue;
                                }
                                else
                                {
                                    sum--;
                                    break;
                                }
                                break;
                            }
                        }
                        for (int k = 1; k <= 8; k++)
                        {
                            if (i - k > 0 && j + k <= 8)
                            {
                                if (input[i - k][j + k] == 0 || input[i - k][j + k] == 20)
                                {
                                    space -= score;
                                    continue;
                                }
                                else
                                {
                                    sum--;
                                    break;
                                }
                                break;
                            }
                        }
                        for (int k = 1; k <= 8; k++)
                        {
                            if (i + k <= 8 && j - k > 0)
                            {
                                if (input[i + k][j - k] == 0 || input[i + k][j - k] == 20)
                                {
                                    space -= score;
                                    continue;
                                }
                                else
                                {
                                    sum--;
                                    break;
                                }
                                break;
                            }
                        }
                    }
                    else if (input[i][j] == 9)
                    {
                        sum -= 90 * value;
                        for (int k = i - 1; k >= 1; k--)
                        {
                            if (input[k][j] == 0 || input[k][j] == 20)
                            {
                                space -= score;
                                continue;
                            }
                            else if (input[k][j] > 0)
                            {
                                sum--;
                                break;
                            }
                            break;

                        }
                        for (int k = i + 1; k <= 8; k++)
                        {
                            if (input[k][j] == 0 || input[k][j] == 20)
                            {
                                space -= score;
                                continue;
                            }
                            else if (input[k][j] > 0)
                            {
                                sum--;
                                break;
                            }
                            break;
                        }
                        for (int k = j - 1; k >= 1; k--)
                        {
                            if (input[i][k] == 0 || input[i][k] == 20)
                            {
                                space -= score;
                                continue;
                            }
                            else if (input[i][k] > 0)
                            {
                                sum--;
                                break;
                            }
                            break;
                        }
                        for (int k = j + 1; k <= 8; k++)
                        {
                            if (input[i][k] == 0 || input[i][k] == 20)
                            {
                                space -= score;
                                continue;
                            }
                            else if (input[i][k] > 0)
                            {
                                sum--;
                                break;
                            }
                            break;
                        }
                        // tuong
                        for (int k = 1; k <= 8; k++)
                        {
                            if (i - k > 0 && j - k > 0)
                            {
                                if (input[i - k][j - k] == 0 || input[i - k][j - k] == 20)
                                {
                                    space -= score;
                                    continue;
                                }
                                else
                                {
                                    sum--;
                                    break;
                                }
                                break;
                            }
                        }
                        for (int k = 1; k <= 8; k++)
                        {
                            if (i + k <= 8 && j + k <= 8)
                            {
                                if (input[i + k][j + k] == 0 || input[i + k][j + k] == 20)
                                {
                                    space -= score;
                                    continue;
                                }
                                else
                                {
                                    sum--;
                                    break;
                                }
                                break;
                            }
                        }
                        for (int k = 1; k <= 8; k++)
                        {
                            if (i - k > 0 && j + k <= 8)
                            {
                                if (input[i - k][j + k] == 0 || input[i - k][j + k] == 20)
                                {
                                    space -= score;
                                    continue;
                                }
                                else
                                {
                                    sum--;
                                    break;
                                }
                                break;
                            }
                        }
                        for (int k = 1; k <= 8; k++)
                        {
                            if (i + k <= 8 && j - k > 0)
                            {
                                if (input[i + k][j - k] == 0 || input[i + k][j - k] == 20)
                                {
                                    space -= score;
                                    continue;
                                }
                                else
                                {
                                    sum--;
                                    break;
                                }
                                break;
                            }
                        }
                    }
                    else if (input[i][j] == 10)
                    {

                        sum -= 100000000;
                        if (u[i][j] == 1)
                        {
                            if (luot)
                            {
                                sum += 1000000;
                            }
                            else
                            {
                                if (!Defense_King(input, !luot))
                                {
                                    sum += 1000000;
                                }
                            }
                        }
                        else
                        {
                            for (int k = 1; k <= 8; k++)
                            {
                                for (int l = 1; l <= 8; l++)
                                {
                                    if ((k == i - 1 && l == j - 1) || (k == i && l == j - 1) || (k == i + 1 && l == j - 1) || (k == i + 1 && l == j) || (k == i + 1 && l == j + 1) || (k == i && l == j + 1) || (k == i - 1 && l == j + 1) || (k == i - 1 && l == j))
                                    {

                                        if (input[k][l] > 0 && input[k][l] <= 10)
                                        {
                                            sum--;
                                        }
                                        else if (input[k][l] == 0)
                                        {
                                            space -= score;
                                            if (u[k][l] == 1)
                                            {
                                                sum += 8;
                                            }
                                        }
                                    }
                                }
                            }
                        }

                    }
                    else if (input[i][j] == 11)
                    {
                        sum += 10 * value;
                        sum += 9 - i;
                        if (i - 1 >= 1)
                        {
                            if (j + 1 <= 8)
                            {
                                if (input[i - 1][j + 1] == 0)
                                {
                                    space += score;
                                }
                                else
                                {
                                    sum++;
                                }

                            }
                            if (j - 1 >= 1)
                            {
                                if (input[i - 1][j - 1] == 0)
                                {
                                    space += score;
                                }
                                else
                                {
                                    sum++;
                                }
                            }

                        }
                    }
                    else if (input[i][j] == 15)
                    {
                        sum += 50 * value;
                        for (int k = i - 1; k >= 1; k--)
                        {
                            if (input[k][j] == 0 || input[k][j] == 10)
                            {
                                space += score;
                                continue;
                            }
                            else if (input[k][j] > 0)
                            {
                                sum++;
                                break;
                            }
                            break;
                        }
                        for (int k = i + 1; k <= 8; k++)
                        {
                            if (input[k][j] == 0 || input[k][j] == 10)
                            {
                                space += score;
                                continue;
                            }
                            else if (input[k][j] > 0)
                            {
                                sum++;
                                break;
                            }
                            break;
                        }
                        for (int k = j - 1; k >= 1; k--)
                        {
                            if (input[i][k] == 0 || input[i][k] == 10)
                            {
                                space += score;
                                continue;
                            }
                            else if (input[i][k] > 0)
                            {
                                sum++;
                                break;
                            }
                            break;
                        }
                        for (int k = j + 1; k <= 8; k++)
                        {
                            if (input[i][k] == 0 || input[i][k] == 10)
                            {
                                space += score;
                                continue;
                            }
                            else if (input[i][k] > 0)
                            {
                                sum++;
                                break;
                            }
                            break;
                        }
                    }
                    else if (input[i][j] == 12)
                    {
                        sum += 30 * value;
                        for (int k = 1; k <= 8; k++)
                        {
                            for (int l = 1; l <= 8; l++)
                            {
                                if ((k == i - 1 && l == j - 2) || (k == i + 1 && l == j - 2) || (k == i + 2 && l == j - 1) || (k == i + 2 && l == j + 1) || (k == i + 1 && l == j + 2) || (k == i - 1 && l == j + 2) || (k == i - 2 && l == j + 1) || (k == i - 2 && l == j - 1))
                                {
                                    if (k > 0 && k <= 8 && l > 0 && l <= 8)
                                    {
                                        if (input[k][l] == 0)
                                        {
                                            space += score;
                                        }
                                        else if (input[k][l] > 0)
                                        {
                                            sum++;
                                        }
                                    }

                                }

                            }
                        }
                    }
                    else if (input[i][j] == 13)
                    {
                        sum += 30 * value;
                        for (int k = 1; k <= 8; k++)
                        {
                            if (i - k > 0 && j - k > 0)
                            {
                                if (input[i - k][j - k] == 0 || input[i - k][j - k] == 10)
                                {
                                    space += score;
                                    continue;
                                }
                                else
                                {
                                    sum++;
                                    break;
                                }
                                break;
                            }
                        }
                        for (int k = 1; k <= 8; k++)
                        {
                            if (i + k <= 8 && j + k <= 8)
                            {
                                if (input[i + k][j + k] == 0 || input[i + k][j + k] == 10)
                                {
                                    space += score;
                                    continue;
                                }
                                else
                                {
                                    sum++;
                                    break;
                                }
                                break;
                            }
                        }
                        for (int k = 1; k <= 8; k++)
                        {
                            if (i - k > 0 && j + k <= 8)
                            {
                                if (input[i - k][j + k] == 0 || input[i - k][j + k] == 10)
                                {
                                    space += score;
                                    continue;
                                }
                                else
                                {
                                    sum++;
                                    break;
                                }
                                break;
                            }
                        }
                        for (int k = 1; k <= 8; k++)
                        {
                            if (i + k <= 8 && j - k > 0)
                            {
                                if (input[i + k][j - k] == 0 || input[i + k][j - k] == 10)
                                {
                                    space += score;
                                    continue;
                                }
                                else
                                {
                                    sum++;
                                    break;
                                }
                                break;
                            }
                        }
                    }
                    else if (input[i][j] == 19)
                    {
                        sum += 90 * value;
                        for (int k = i - 1; k >= 1; k--)
                        {
                            if (input[k][j] == 0 || input[k][j] == 10)
                            {
                                space += score;
                                continue;
                            }
                            else if (input[k][j] > 0)
                            {
                                sum++;
                                break;
                            }
                            break;
                        }
                        for (int k = i + 1; k <= 8; k++)
                        {
                            if (input[k][j] == 0 || input[k][j] == 10)
                            {
                                space += score;
                                continue;
                            }
                            else if (input[k][j] > 0)
                            {
                                sum++;
                                break;
                            }
                            break;
                        }
                        for (int k = j - 1; k >= 1; k--)
                        {
                            if (input[i][k] == 0 || input[i][k] == 10)
                            {
                                space += score;
                                continue;
                            }
                            else if (input[i][k] > 0)
                            {
                                sum++;
                                break;
                            }
                            break;
                        }
                        for (int k = j + 1; k <= 8; k++)
                        {
                            if (input[i][k] == 0 || input[i][k] == 10)
                            {
                                space += score;
                                continue;
                            }
                            else if (input[i][k] > 0)
                            {
                                sum++;
                                break;
                            }
                            break;
                        }
                        // tuong
                        for (int k = 1; k <= 8; k++)
                        {
                            if (i - k > 0 && j - k > 0)
                            {
                                if (input[i - k][j - k] == 0 || input[i - k][j - k] == 10)
                                {
                                    space += score;
                                    continue;
                                }
                                else
                                {
                                    sum++;
                                    break;
                                }
                                break;
                            }
                        }
                        for (int k = 1; k <= 8; k++)
                        {
                            if (i + k <= 8 && j + k <= 8)
                            {
                                if (input[i + k][j + k] == 0 || input[i + k][j + k] == 10)
                                {
                                    space += score;
                                    continue;
                                }
                                else
                                {
                                    sum++;
                                    break;
                                }
                                break;
                            }
                        }
                        for (int k = 1; k <= 8; k++)
                        {
                            if (i - k > 0 && j + k <= 8)
                            {
                                if (input[i - k][j + k] == 0 || input[i - k][j + k] == 10)
                                {
                                    space += score;
                                    continue;
                                }
                                else
                                {
                                    sum++;
                                    break;
                                }
                                break;
                            }
                        }
                        for (int k = 1; k <= 8; k++)
                        {
                            if (i + k <= 8 && j - k > 0)
                            {
                                if (input[i + k][j - k] == 0 || input[i + k][j - k] == 10)
                                {
                                    space += score;
                                    continue;
                                }
                                else
                                {
                                    sum++;
                                    break;
                                }
                                break;
                            }
                        }
                    }
                    else if (input[i][j] == 20)
                    {
                        sum += 100000000;
                        if (v[i][j] == 2)
                        {
                            if (!luot)
                            {
                                sum -= 1000000;
                            }
                            else
                            {
                                if (!Defense_King(input, !luot))
                                {

                                    sum -= 1000000;
                                }
                            }
                        }
                        else
                        {
                            for (int k = 1; k <= 8; k++)
                            {
                                for (int l = 1; l <= 8; l++)
                                {
                                    if ((k == i - 1 && l == j - 1) || (k == i && l == j - 1) || (k == i + 1 && l == j - 1) || (k == i + 1 && l == j) || (k == i + 1 && l == j + 1) || (k == i && l == j + 1) || (k == i - 1 && l == j + 1) || (k == i - 1 && l == j))
                                    {

                                        if (input[k][l] > 10)
                                        {
                                            sum++;
                                        }
                                        else if (input[k][l] == 0)
                                        {
                                            space += score;
                                            if (v[k][l] == 2)
                                            {
                                                sum -= 8;
                                            }
                                        }

                                    }
                                }
                            }
                        }


                    }
                }
            }
            return (int)(sum + score);
        }
        public int[][] Danger_King(int[][] input, bool luot)
        {
            int[][] u = new int[10][];
            for (int i = 0; i < 10; i++)
            {
                u[i] = new int[10];
            }
            if (luot)
            {
                for (int a = 1; a <= 8; a++)
                {
                    for (int b = 1; b <= 8; b++)
                    {

                        if (input[a][b] == 11)
                        {
                            u[a - 1][b - 1] = 1;
                            u[a - 1][b + 1] = 1;
                        }

                        else if (input[a][b] == 15)
                        {
                            for (int i = a - 1; i >= 1; i--)
                            {
                                if (input[i][b] == 0 || input[i][b] == 10)
                                {
                                    u[i][b] = 1;
                                    continue;
                                }
                                else if (input[i][b] > 10 || input[i][b] == 10)
                                {
                                    u[i][b] = 1;
                                }
                                break;
                            }
                            for (int i = a + 1; i <= 8; i++)
                            {
                                if (input[i][b] == 0 || input[i][b] == 10)
                                {
                                    u[i][b] = 1;
                                    continue;
                                }
                                else if (input[i][b] > 10 || input[i][b] == 10)
                                {
                                    u[i][b] = 1;
                                }
                                break;
                            }
                            for (int i = b - 1; i >= 1; i--)
                            {
                                if (input[a][i] == 0 || input[a][i] == 10)
                                {
                                    u[a][i] = 1;
                                    continue;
                                }
                                else if (input[a][i] > 10 || input[a][i] == 10)
                                {
                                    u[a][i] = 1;
                                }
                                break;
                            }
                            for (int i = b + 1; i <= 8; i++)
                            {
                                if (input[a][i] == 0 || input[a][i] == 10)
                                {
                                    u[a][i] = 1;
                                    continue;
                                }
                                else if (input[a][i] > 10 || input[a][i] == 10)
                                {
                                    u[a][i] = 1;
                                }
                                break;
                            }
                        }
                        else if (input[a][b] == 12)
                        {
                            for (int i = 1; i <= 8; i++)
                            {
                                for (int j = 1; j <= 8; j++)
                                {
                                    if (input[i][j] == 0 || input[i][j] == 10 || input[i][j] > 10)
                                    {
                                        if ((i == a - 1 && j == b - 2) || (i == a + 1 && j == b - 2) || (i == a + 2 && j == b - 1) || (i == a + 2 && j == b + 1) || (i == a + 1 && j == b + 2) || (i == a - 1 && j == b + 2) || (i == a - 2 && j == b + 1) || (i == a - 2 && j == b - 1))
                                        {
                                            u[i][j] = 1;
                                        }
                                    }
                                }
                            }
                        }
                        else if (input[a][b] == 13)
                        {
                            for (int i = 1; i <= 8; i++)
                            {
                                if (a - i > 0 && b - i > 0)
                                {
                                    if (input[a - i][b - i] == 0 || input[a - i][b - i] == 10)
                                    {
                                        u[a - i][b - i] = 1;
                                        continue;
                                    }
                                    else if (input[a - i][b - i] > 10 || input[a - i][b - i] == 10)
                                    {
                                        u[a - i][b - i] = 1;
                                    }
                                    break;
                                }
                                else
                                {
                                    break;
                                }
                            }
                            for (int i = 1; i <= 8; i++)
                            {
                                if (a + i <= 8 && b + i <= 8)
                                {
                                    if (input[a + i][b + i] == 0 || input[a + i][b + i] == 10)
                                    {
                                        u[a + 1][b + i] = 1;
                                        continue;
                                    }
                                    else if (input[a + i][b + i] > 10 || input[a + i][b + i] == 10)
                                    {
                                        u[a + 1][b + i] = 1;
                                    }
                                    break;
                                }
                                else
                                {
                                    break;
                                }
                            }
                            for (int i = 1; i <= 8; i++)
                            {
                                if (a - i > 0 && b + i <= 8)
                                {
                                    if (input[a - i][b + i] == 0 || input[a - i][b + i] == 10)
                                    {
                                        u[a - i][b + i] = 1;
                                        continue;
                                    }
                                    else if (input[a - i][b + i] > 10 || input[a - i][b + i] == 10)
                                    {
                                        u[a - i][b + i] = 1;
                                    }
                                    break;
                                }
                                else
                                {
                                    break;
                                }
                            }
                            for (int i = 1; i <= 8; i++)
                            {
                                if (a + i <= 8 && b - i > 0)
                                {
                                    if (input[a + i][b - i] == 0 || input[a + i][b - i] == 10)
                                    {
                                        u[a + i][b - i] = 1;
                                        continue;
                                    }
                                    else if (input[a + i][b - i] > 10 || input[a + i][b - i] == 10)
                                    {
                                        u[a + i][b - i] = 1;
                                    }
                                    break;
                                }
                                else
                                {
                                    break;
                                }
                            }
                        }
                        else if (input[a][b] == 19)
                        {
                            for (int i = a - 1; i >= 1; i--)
                            {
                                if (input[i][b] == 0 || input[i][b] == 10)
                                {
                                    u[i][b] = 1;
                                    continue;
                                }
                                else if (input[i][b] > 10 || input[i][b] == 10)
                                {
                                    u[i][b] = 1;
                                }
                                break;
                            }
                            for (int i = a + 1; i <= 8; i++)
                            {
                                if (input[i][b] == 0 || input[i][b] == 10)
                                {
                                    u[i][b] = 1;
                                    continue;
                                }
                                else if (input[i][b] > 10 || input[i][b] == 10)
                                {
                                    u[i][b] = 1;
                                }
                                break;
                            }
                            for (int i = b - 1; i >= 1; i--)
                            {
                                if (input[a][i] == 0 || input[a][i] == 10)
                                {
                                    u[a][i] = 1;
                                    continue;
                                }
                                else if (input[a][i] > 10 || input[a][i] == 10)
                                {
                                    u[a][i] = 1;
                                }
                                break;
                            }
                            for (int i = b + 1; i <= 8; i++)
                            {
                                if (input[a][i] == 0 || input[a][i] == 10)
                                {
                                    u[a][i] = 1;
                                    continue;
                                }
                                else if (input[a][i] > 10 || input[a][i] == 10)
                                {
                                    u[a][i] = 1;
                                }
                                break;
                            }
                            // tuong
                            for (int i = 1; i <= 8; i++)
                            {
                                if (a - i > 0 && b - i > 0)
                                {
                                    if (input[a - i][b - i] == 0 || input[a - i][b - i] == 10)
                                    {
                                        u[a - i][b - i] = 1;
                                        continue;
                                    }
                                    else if (input[a - i][b - i] > 10 || input[a - i][b - i] == 10)
                                    {
                                        u[a - i][b - i] = 1;
                                    }
                                    break;
                                }
                                else
                                {
                                    break;
                                }
                            }
                            for (int i = 1; i <= 8; i++)
                            {
                                if (a + i <= 8 && b + i <= 8)
                                {
                                    if (input[a + i][b + i] == 0 || input[a + i][b + i] == 10)
                                    {
                                        u[a + i][b + i] = 1;
                                        continue;
                                    }
                                    else if (input[a + i][b + i] > 10 || input[a + i][b + i] == 10)
                                    {
                                        u[a + i][b + i] = 1;
                                    }
                                    break;
                                }
                                else
                                {
                                    break;
                                }
                            }
                            for (int i = 1; i <= 8; i++)
                            {
                                if (a - i > 0 && b + i <= 8)
                                {
                                    if (input[a - i][b + i] == 0 || input[a - i][b + i] == 10)
                                    {
                                        u[a - i][b + i] = 1;
                                        continue;
                                    }
                                    else if (input[a - i][b + i] > 10 || input[a - i][b + i] == 10)
                                    {
                                        u[a - i][b + i] = 1;
                                    }
                                    break;
                                }
                                else
                                {
                                    break;
                                }
                            }
                            for (int i = 1; i <= 8; i++)
                            {
                                if (a + i <= 8 && b - i > 0)
                                {
                                    if (input[a + i][b - i] == 0 || input[a + i][b - i] == 10)
                                    {
                                        u[a + i][b - i] = 1;
                                        continue;
                                    }
                                    else if (input[a + i][b - i] > 10 || input[a + i][b - i] == 10)
                                    {
                                        u[a + i][b - i] = 1;
                                    }
                                    break;
                                }
                                else
                                {
                                    break;
                                }
                            }
                        }
                        else if (input[a][b] == 20)
                        {
                            for (int i = 1; i <= 8; i++)
                            {
                                for (int j = 1; j <= 8; j++)
                                {
                                    if (input[i][j] == 0)
                                    {
                                        if ((i == a - 1 && j == b - 1) || (i == a && j == b - 1) || (i == a + 1 && j == b - 1) || (i == a + 1 && j == b) || (i == a + 1 && j == b + 1) || (i == a && j == b + 1) || (i == a - 1 && j == b + 1) || (i == a - 1 && j == b))
                                        {
                                            u[i][j] = 1;
                                        }
                                    }
                                }
                            }
                        }

                    }
                }
            }
            else
            {
                for (int a = 1; a <= 8; a++)
                {
                    for (int b = 1; b <= 8; b++)
                    {
                        if (input[a][b] == 1)
                        {
                            u[a + 1][b - 1] = 2;
                            u[a + 1][b + 1] = 2;
                        }
                        else if (input[a][b] == 5)
                        {
                            for (int i = a - 1; i >= 1; i--)
                            {
                                if (input[i][b] == 0 || input[i][b] == 20)
                                {
                                    u[i][b] = 2;
                                    continue;
                                }
                                else if (input[i][b] < 10)
                                {
                                    u[i][b] = 2;
                                }
                                break;
                            }
                            for (int i = a + 1; i <= 8; i++)
                            {
                                if (input[i][b] == 0 || input[i][b] == 20)
                                {
                                    u[i][b] = 2;
                                    continue;
                                }
                                else if (input[i][b] < 10)
                                {
                                    u[i][b] = 2;
                                }
                                break;
                            }
                            for (int i = b - 1; i >= 1; i--)
                            {
                                if (input[a][i] == 0 || input[a][i] == 20)
                                {
                                    u[a][i] = 2;
                                    continue;
                                }
                                else if (input[a][i] < 10)
                                {
                                    u[a][i] = 2;
                                }
                                break;
                            }
                            for (int i = b + 1; i <= 8; i++)
                            {
                                if (input[a][i] == 0 || input[a][i] == 20)
                                {
                                    u[a][i] = 2;
                                    continue;
                                }
                                else if (input[a][i] < 10)
                                {
                                    u[a][i] = 2;
                                }
                                break;
                            }
                        }
                        else if (input[a][b] == 2)
                        {
                            for (int i = 1; i <= 8; i++)
                            {
                                for (int l = 1; l <= 8; l++)
                                {
                                    if (input[i][l] == 0 || input[i][l] == 20 || input[i][l] < 10)
                                    {
                                        if ((i == a - 1 && l == b - 2) || (i == a + 1 && l == b - 2) || (i == a + 2 && l == b - 1) || (i == a + 2 && l == b + 1) || (i == a + 1 && l == b + 2) || (i == a - 1 && l == b + 2) || (i == a - 2 && l == b + 1) || (i == a - 2 && l == b - 1))
                                        {
                                            u[i][l] = 2;
                                        }
                                    }
                                }
                            }
                        }
                        else if (input[a][b] == 3)
                        {
                            for (int i = 1; i <= 8; i++)
                            {
                                if (a - i > 0 && b - i > 0)
                                {
                                    if (input[a - i][b - i] == 0 || input[a - i][b - i] == 20)
                                    {
                                        u[a - i][b - i] = 2;
                                        continue;
                                    }
                                    else if (input[a - i][b - i] < 10)
                                    {
                                        u[a - i][b - i] = 2;
                                    }
                                    break;
                                }
                                else
                                {
                                    break;
                                }
                            }
                            for (int i = 1; i <= 8; i++)
                            {
                                if (a + i <= 8 && b + i <= 8)
                                {
                                    if (input[a + i][b + i] == 0 || input[a + i][b + i] == 20)
                                    {
                                        u[a + i][b + i] = 2;
                                        continue;
                                    }
                                    else if (input[a + i][b + i] < 10)
                                    {
                                        u[a + i][b + i] = 2;
                                    }
                                    break;
                                }
                                else
                                {
                                    break;
                                }
                            }
                            for (int i = 1; i <= 8; i++)
                            {
                                if (a - i > 0 && b + i <= 8)
                                {
                                    if (input[a - i][b + i] == 0 || input[a - i][b + i] == 20)
                                    {
                                        u[a - i][b + i] = 2;
                                        continue;
                                    }
                                    else if (input[a - i][b + i] < 10)
                                    {
                                        u[a - i][b + i] = 2;
                                    }
                                    break;
                                }
                                else
                                {
                                    break;
                                }
                            }
                            for (int i = 1; i <= 8; i++)
                            {
                                if (a + i <= 8 && b - i > 0)
                                {
                                    if (input[a + i][b - i] == 0 || input[a + i][b - i] == 20)
                                    {
                                        u[a + i][b - i] = 2;
                                        continue;
                                    }
                                    else if (input[a + i][b - i] < 10)
                                    {
                                        u[a + i][b - i] = 2;
                                    }
                                    break;
                                }
                                else
                                {
                                    break;
                                }
                            }
                        }
                        else if (input[a][b] == 9)
                        {
                            for (int i = a - 1; i >= 1; i--)
                            {
                                if (input[i][b] == 0 || input[i][b] == 20)
                                {
                                    u[i][b] = 2;
                                    continue;
                                }
                                else if (input[i][b] < 10)
                                {
                                    u[i][b] = 2;
                                }
                                break;
                            }
                            for (int i = a + 1; i <= 8; i++)
                            {
                                if (input[i][b] == 0 || input[i][b] == 20)
                                {
                                    u[i][b] = 2;
                                    continue;
                                }
                                else if (input[i][b] < 10)
                                {
                                    u[i][b] = 2;
                                }
                                break;
                            }
                            for (int i = b - 1; i >= 1; i--)
                            {
                                if (input[a][i] == 0 || input[a][i] == 20)
                                {
                                    u[a][i] = 2;
                                    continue;
                                }
                                else if (input[a][i] < 10)
                                {
                                    u[a][i] = 2;
                                }
                                break;
                            }
                            for (int i = b + 1; i <= 8; i++)
                            {
                                if (input[a][i] == 0 || input[a][i] == 20)
                                {
                                    u[a][i] = 2;
                                    continue;
                                }
                                else if (input[a][i] < 10)
                                {
                                    u[a][i] = 2;
                                }
                                break;
                            }
                            // tuong
                            for (int i = 1; i <= 8; i++)
                            {
                                if (a - i > 0 && b - i > 0)
                                {
                                    if (input[a - i][b - i] == 0 || input[a - i][b - i] == 20)
                                    {
                                        u[a - i][b - i] = 2;
                                        continue;
                                    }
                                    else if (input[a - i][b - i] < 10)
                                    {
                                        u[a - i][b - i] = 2;
                                    }
                                    break;
                                }
                                else
                                {
                                    break;

                                }
                            }
                            for (int i = 1; i <= 8; i++)
                            {
                                if (a + i <= 8 && b + i <= 8)
                                {
                                    if (input[a + i][b + i] == 0 || input[a + i][b + i] == 20)
                                    {
                                        u[a + i][b + i] = 2;
                                        continue;
                                    }
                                    else if (input[a + i][b + i] < 10)
                                    {
                                        u[a + i][b + i] = 2;
                                    }
                                    break;
                                }
                                else
                                {
                                    break;
                                }
                            }
                            for (int i = 1; i <= 8; i++)
                            {
                                if (a - i > 0 && b + i <= 8)
                                {
                                    if (input[a - i][b + i] == 0 || input[a - i][b + i] == 20)
                                    {
                                        u[a - i][b + i] = 2;
                                        continue;
                                    }
                                    else if (input[a - i][b + i] < 10)
                                    {
                                        u[a - i][b + i] = 2;
                                    }
                                    break;
                                }
                                else
                                {
                                    break;
                                }
                            }
                            for (int i = 1; i <= 8; i++)
                            {
                                if (a + i <= 8 && b - i > 0)
                                {
                                    if (input[a + i][b - i] == 0 || input[a + i][b - i] == 20)
                                    {
                                        u[a + i][b - i] = 2;
                                        continue;
                                    }
                                    else if (input[a + i][b - i] < 10)
                                    {
                                        u[a + i][b - i] = 2;
                                    }
                                    break;
                                }
                                else
                                {
                                    break;
                                }
                            }
                        }
                        else if (input[a][b] == 10)
                        {
                            for (int i = 1; i <= 8; i++)
                            {
                                for (int l = 1; l <= 8; l++)
                                {
                                    if (input[i][l] == 0 || input[i][l] == 20 || input[i][l] < 10)
                                    {
                                        if ((i == a - 1 && l == b - 1) || (i == a && l == b - 1) || (i == a + 1 && l == b - 1) || (i == a + 1 && l == b) || (i == a + 1 && l == b + 1) || (i == a && l == b + 1) || (i == a - 1 && l == b + 1) || (i == a - 1 && l == b))
                                        {
                                            u[i][l] = 2;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return u;
        }
        public bool Defense_King(int[][] input, bool luot)
        {

            int[][] u = new int[9][];
            for (int i = 1; i <= 8; i++)
            {
                u[i] = new int[9];
                for (int j = 1; j <= 8; j++)
                {
                    u[i][j] = input[i][j];
                }
            }
            int a = 1; int b = 1;
            if (luot)
            {

                for (int i = 1; i <= 8; i++)
                {
                    for (int j = 1; j <= 8; j++)
                    {
                        if (input[i][j] == 0)
                        {
                        }
                        else if (input[i][j] == 10)
                        {
                            a = i; b = j;
                            for (int k = 1; k <= 8; k++)
                            {
                                for (int l = 1; l <= 8; l++)
                                {
                                    if (input[k][l] == 0 || input[k][l] > 10)
                                    {
                                        if ((k == i - 1 && l == j - 1) || (k == i && l == j - 1) || (k == i + 1 && l == j - 1) || (k == i + 1 && l == j) || (k == i + 1 && l == j + 1) || (k == i && l == j + 1) || (k == i - 1 && l == j + 1) || (k == i - 1 && l == j))
                                        {
                                            u[k][l] = 10;
                                            u[i][j] = 0;

                                            if (Danger_King(u, luot)[k][l] != 1)
                                            {
                                                return true;
                                            }
                                            u[k][l] = input[k][l];
                                            u[i][j] = 10;
                                        }
                                    }
                                }
                            }
                            goto EndOfBothLoops;
                        }
                    }
                }
            EndOfBothLoops:
                for (int i = 1; i <= 8; i++)
                {
                    for (int j = 1; j <= 8; j++)
                    {
                        if (input[i][j] == 0)
                        {
                        }
                        else if (input[i][j] == 1)
                        {
                            if (i + 1 <= 8)
                            {
                                if (j - 1 >= 0)
                                {
                                    if (input[i + 1][j - 1] > 10)
                                    {
                                        u[i + 1][j - 1] = 1;
                                        u[i][j] = 0;

                                        if (Danger_King(u, luot)[a][b] != 1)
                                        {
                                            return true;
                                        }
                                        u[i + 1][j - 1] = input[i + 1][j - 1];
                                        u[i][j] = 1;

                                    }
                                }
                                if (j + 1 <= 8)
                                {
                                    if (input[i + 1][j + 1] > 10)
                                    {
                                        u[i + 1][j + 1] = 1;
                                        u[i][j] = 0;

                                        if (Danger_King(u, luot)[a][b] != 1)
                                        {
                                            return true;
                                        }
                                        u[i + 1][j + 1] = input[i + 1][j + 1];
                                        u[i][j] = 1;
                                    }
                                }
                                if (input[i + 1][j] == 0)
                                {
                                    if (i == 7)
                                    {
                                        u[i + 1][j - 1] = 9;
                                        u[i][j] = 0;

                                        if (Danger_King(u, luot)[a][b] != 1)
                                        {
                                            return true;
                                        }
                                        u[i + 1][j] = 0;
                                        u[i][j] = 1;
                                    }
                                    else
                                    {
                                        u[i + 1][j - 1] = 1;
                                        u[i][j] = 0;

                                        if (Danger_King(u, luot)[a][b] != 1)
                                        {
                                            return true;
                                        }
                                        u[i + 1][j] = 0;
                                        u[i][j] = 1;
                                    }
                                }
                                if (i == 2 && input[i + 1][j] == 0)
                                {
                                    if (input[i + 2][j] == 0)
                                    {
                                        u[i + 2][j] = 1;
                                        u[i][j] = 0;

                                        if (Danger_King(u, luot)[a][b] != 1)
                                        {
                                            return true;
                                        }
                                        u[i + 2][j] = 0;
                                        u[i][j] = 1;
                                    }
                                }
                            }

                        }
                        else if (input[i][j] == 5)
                        {

                            for (int k = i - 1; k >= 1; k--)
                            {
                                if (input[k][j] == 0)
                                {
                                    u[k][j] = 5;
                                    u[i][j] = 0;

                                    if (Danger_King(u, luot)[a][b] != 1)
                                    {
                                        return true;
                                    }
                                    u[k][j] = 0;
                                    u[i][j] = 5;
                                    continue;
                                }
                                else if (input[k][j] > 10)
                                {
                                    u[k][j] = 5;
                                    u[i][j] = 0;

                                    if (Danger_King(u, luot)[a][b] != 1)
                                    {
                                        return true;
                                    }
                                    u[k][j] = input[k][j];
                                    u[i][j] = 5;
                                    break;
                                }
                                break;

                            }
                            for (int k = i + 1; k <= 8; k++)
                            {
                                if (input[k][j] == 0)
                                {
                                    u[k][j] = 5;
                                    u[i][j] = 0;

                                    if (Danger_King(u, luot)[a][b] != 1)
                                    {
                                        return true;
                                    }
                                    u[k][j] = 0;
                                    u[i][j] = 5;
                                    continue;
                                }
                                else if (input[k][j] > 10)
                                {
                                    u[k][j] = 5;
                                    u[i][j] = 0;

                                    if (Danger_King(u, luot)[a][b] != 1)
                                    {
                                        return true;
                                    }
                                    u[k][j] = input[k][j];
                                    u[i][j] = 5;
                                    break;
                                }
                                break;
                            }
                            for (int k = j - 1; k >= 1; k--)
                            {
                                if (input[i][k] == 0)
                                {
                                    u[i][k] = 5;
                                    u[i][j] = 0;

                                    if (Danger_King(u, luot)[a][b] != 1)
                                    {
                                        return true;
                                    }
                                    u[i][k] = 0;
                                    u[i][j] = 5;
                                    continue;
                                }
                                else if (input[i][k] > 10)
                                {
                                    u[i][k] = 5;
                                    u[i][j] = 0;

                                    if (Danger_King(u, luot)[a][b] != 1)
                                    {
                                        return true;
                                    }
                                    u[i][k] = input[i][k];
                                    u[i][j] = 5;
                                    break;
                                }
                                break;
                            }
                            for (int k = j + 1; k <= 8; k++)
                            {
                                if (input[i][k] == 0)
                                {
                                    u[i][k] = 5;
                                    u[i][j] = 0;

                                    if (Danger_King(u, luot)[a][b] != 1)
                                    {
                                        return true;
                                    }
                                    u[i][k] = 0;
                                    u[i][j] = 5;
                                    continue;
                                }
                                else if (input[i][k] > 10)
                                {
                                    u[i][k] = 5;
                                    u[i][j] = 0;

                                    if (Danger_King(u, luot)[a][b] != 1)
                                    {
                                        return true;
                                    }
                                    u[i][k] = input[i][k];
                                    u[i][j] = 5;
                                    break;
                                }
                                break;
                            }
                        }
                        else if (input[i][j] == 2)
                        {
                            for (int k = 1; k <= 8; k++)
                            {
                                for (int l = 1; l <= 8; l++)
                                {
                                    if (input[k][l] == 0 || input[k][l] > 10)
                                    {
                                        if ((k == i - 1 && l == j - 2) || (k == i + 1 && l == j - 2) || (k == i + 2 && l == j - 1) || (k == i + 2 && l == j + 1) || (k == i + 1 && l == j + 2) || (k == i - 1 && l == j + 2) || (k == i - 2 && l == j + 1) || (k == i - 2 && l == j - 1))
                                        {
                                            u[k][l] = 3;
                                            u[i][j] = 0;

                                            if (Danger_King(u, luot)[a][b] != 1)
                                            {
                                                return true;
                                            }
                                            u[k][l] = input[k][l];
                                            u[i][j] = 3;
                                        }
                                    }
                                }
                            }
                        }
                        else if (input[i][j] == 3)
                        {
                            for (int k = 1; k <= 8; k++)
                            {
                                if (i - k > 0 && j - k > 0)
                                {
                                    if (input[i - k][j - k] == 0)
                                    {
                                        u[i - k][j - k] = 3;
                                        u[i][j] = 0;

                                        if (Danger_King(u, luot)[a][b] != 1)
                                        {
                                            return true;
                                        }
                                        u[i - k][j - k] = 0;
                                        u[i][j] = 3;
                                        continue;
                                    }
                                    else if (input[i - k][j - k] > 10)
                                    {
                                        u[i - k][j - k] = 3;
                                        u[i][j] = 0;

                                        if (Danger_King(u, luot)[a][b] != 1)
                                        {
                                            return true;
                                        }
                                        u[i - k][j - k] = input[i - k][j - k];
                                        u[i][j] = 3;
                                        break;
                                    }
                                    break;
                                }
                            }
                            for (int k = 1; k <= 8; k++)
                            {
                                if (i + k <= 8 && j + k <= 8)
                                {
                                    if (input[i + k][j + k] == 0)
                                    {
                                        u[i + k][j + k] = 3;
                                        u[i][j] = 0;

                                        if (Danger_King(u, luot)[a][b] != 1)
                                        {
                                            return true;
                                        }
                                        u[i + k][j + k] = 0;
                                        u[i][j] = 3;
                                        continue;
                                    }
                                    else if (input[i + k][j + k] > 10)
                                    {
                                        u[i + k][j + k] = 3;
                                        u[i][j] = 0;

                                        if (Danger_King(u, luot)[a][b] != 1)
                                        {
                                            return true;
                                        }
                                        u[i + k][j + k] = input[i + k][j + k];
                                        u[i][j] = 3;
                                        break;
                                    }
                                    break;
                                }
                            }
                            for (int k = 1; k <= 8; k++)
                            {
                                if (i - k > 0 && j + k <= 8)
                                {
                                    if (input[i - k][j + k] == 0)
                                    {
                                        u[i - k][j + k] = 3;
                                        u[i][j] = 0;

                                        if (Danger_King(u, luot)[a][b] != 1)
                                        {
                                            return true;
                                        }
                                        u[i - k][j + k] = 0;
                                        u[i][j] = 3;
                                        continue;
                                    }
                                    else if (input[i - k][j + k] > 10)
                                    {
                                        u[i - k][j + k] = 3;
                                        u[i][j] = 0;

                                        if (Danger_King(u, luot)[a][b] != 1)
                                        {
                                            return true;
                                        }
                                        u[i - k][j + k] = input[i - k][j + k];
                                        u[i][j] = 3;
                                        break;
                                    }
                                    break;
                                }
                            }
                            for (int k = 1; k <= 8; k++)
                            {
                                if (i + k <= 8 && j - k > 0)
                                {
                                    if (input[i + k][j - k] == 0)
                                    {
                                        u[i + k][j - k] = 3;
                                        u[i][j] = 0;

                                        if (Danger_King(u, luot)[a][b] != 1)
                                        {
                                            return true;
                                        }
                                        u[i + k][j - k] = 0;
                                        u[i][j] = 3;
                                        continue;
                                    }
                                    else if (input[i + k][j - k] > 10)
                                    {
                                        u[i + k][j - k] = 3;
                                        u[i][j] = 0;

                                        if (Danger_King(u, luot)[a][b] != 1)
                                        {
                                            return true;
                                        }
                                        u[i + k][j - k] = input[i + k][j - k];
                                        u[i][j] = 3;
                                        break;
                                    }
                                    break;
                                }
                            }
                        }
                        else if (input[i][j] == 9)
                        {
                            for (int k = i - 1; k >= 1; k--)
                            {
                                if (input[k][j] == 0)
                                {
                                    u[k][j] = 9;
                                    u[i][j] = 0;

                                    if (Danger_King(u, luot)[a][b] != 1)
                                    {
                                        return true;
                                    }
                                    u[k][j] = 0;
                                    u[i][j] = 9;
                                    continue;
                                }
                                else if (input[k][j] > 10)
                                {
                                    u[k][j] = 9;
                                    u[i][j] = 0;

                                    if (Danger_King(u, luot)[a][b] != 1)
                                    {
                                        return true;
                                    }
                                    u[k][j] = input[k][j];
                                    u[i][j] = 9;
                                    break;
                                }
                                break;

                            }
                            for (int k = i + 1; k <= 8; k++)
                            {
                                if (input[k][j] == 0)
                                {
                                    u[k][j] = 9;
                                    u[i][j] = 0;

                                    if (Danger_King(u, luot)[a][b] != 1)
                                    {
                                        return true;
                                    }
                                    u[k][j] = 0;
                                    u[i][j] = 9;
                                    continue;
                                }
                                else if (input[k][j] > 10)
                                {
                                    u[k][j] = 9;
                                    u[i][j] = 0;

                                    if (Danger_King(u, luot)[a][b] != 1)
                                    {
                                        return true;
                                    }
                                    u[k][j] = input[k][j];
                                    u[i][j] = 9;
                                    break;
                                }
                                break;
                            }
                            for (int k = j - 1; k >= 1; k--)
                            {
                                if (input[i][k] == 0)
                                {
                                    u[i][k] = 9;
                                    u[i][j] = 0;

                                    if (Danger_King(u, luot)[a][b] != 1)
                                    {
                                        return true;
                                    }
                                    u[i][k] = 0;
                                    u[i][j] = 9;
                                    continue;
                                }
                                else if (input[i][k] > 10)
                                {
                                    u[i][k] = 9;
                                    u[i][j] = 0;

                                    if (Danger_King(u, luot)[a][b] != 1)
                                    {
                                        return true;
                                    }
                                    u[i][k] = input[i][k];
                                    u[i][j] = 9;
                                    break;
                                }
                                break;
                            }
                            for (int k = j + 1; k <= 8; k++)
                            {
                                if (input[i][k] == 0)
                                {
                                    u[i][k] = 9;
                                    u[i][j] = 0;

                                    if (Danger_King(u, luot)[a][b] != 1)
                                    {
                                        return true;
                                    }
                                    u[i][k] = 0;
                                    u[i][j] = 9;
                                    continue;
                                }
                                else if (input[i][k] > 10)
                                {
                                    u[i][k] = 9;
                                    u[i][j] = 0;

                                    if (Danger_King(u, luot)[a][b] != 1)
                                    {
                                        return true;
                                    }
                                    u[i][k] = input[i][k];
                                    u[i][j] = 9;
                                    break;
                                }
                                break;
                            }
                            // tuong
                            for (int k = 1; k <= 8; k++)
                            {
                                if (i - k > 0 && j - k > 0)
                                {
                                    if (input[i - k][j - k] == 0)
                                    {
                                        u[i - k][j - k] = 9;
                                        u[i][j] = 0;

                                        if (Danger_King(u, luot)[a][b] != 1)
                                        {
                                            return true;
                                        }
                                        u[i - k][j - k] = 0;
                                        u[i][j] = 9;
                                        continue;
                                    }
                                    else if (input[i - k][j - k] > 10)
                                    {
                                        u[i - k][j - k] = 9;
                                        u[i][j] = 0;

                                        if (Danger_King(u, luot)[a][b] != 1)
                                        {
                                            return true;
                                        }
                                        u[i - k][j - k] = input[i - k][j - k];
                                        u[i][j] = 9;
                                        break;
                                    }
                                    break;
                                }
                            }
                            for (int k = 1; k <= 8; k++)
                            {
                                if (i + k <= 8 && j + k <= 8)
                                {
                                    if (input[i + k][j + k] == 0)
                                    {
                                        u[i + k][j + k] = 9;
                                        u[i][j] = 0;

                                        if (Danger_King(u, luot)[a][b] != 1)
                                        {
                                            return true;
                                        }
                                        u[i + k][j + k] = 0;
                                        u[i][j] = 9;
                                        continue;
                                    }
                                    else if (input[i + k][j + k] > 10)
                                    {
                                        u[i + k][j + k] = 9;
                                        u[i][j] = 0;

                                        if (Danger_King(u, luot)[a][b] != 1)
                                        {
                                            return true;
                                        }
                                        u[i + k][j + k] = input[i + k][j + k];
                                        u[i][j] = 9;
                                        break;
                                    }
                                    break;
                                }
                            }
                            for (int k = 1; k <= 8; k++)
                            {
                                if (i - k > 0 && j + k <= 8)
                                {
                                    if (input[i - k][j + k] == 0)
                                    {
                                        u[i - k][j + k] = 9;
                                        u[i][j] = 0;

                                        if (Danger_King(u, luot)[a][b] != 1)
                                        {
                                            return true;
                                        }
                                        u[i - k][j + k] = 0;
                                        u[i][j] = 9;
                                        continue;
                                    }
                                    else if (input[i - k][j + k] > 10)
                                    {
                                        u[i - k][j + k] = 9;
                                        u[i][j] = 0;

                                        if (Danger_King(u, luot)[a][b] != 1)
                                        {
                                            return true;
                                        }
                                        u[i - k][j + k] = input[i - k][j + k];
                                        u[i][j] = 9;
                                        break;
                                    }
                                    break;
                                }
                            }
                            for (int k = 1; k <= 8; k++)
                            {
                                if (i + k <= 8 && j - k > 0)
                                {
                                    if (input[i + k][j - k] == 0)
                                    {
                                        u[i + k][j - k] = 9;
                                        u[i][j] = 0;

                                        if (Danger_King(u, luot)[a][b] != 1)
                                        {
                                            return true;
                                        }
                                        u[i + k][j - k] = 0;
                                        u[i][j] = 9;
                                        continue;
                                    }
                                    else if (input[i + k][j - k] > 10)
                                    {
                                        u[i + k][j - k] = 9;
                                        u[i][j] = 0;

                                        if (Danger_King(u, luot)[a][b] != 1)
                                        {
                                            return true;
                                        }
                                        u[i + k][j - k] = input[i + k][j - k];
                                        u[i][j] = 9;
                                        break;
                                    }
                                    break;
                                }
                            }
                        }

                    }
                }
            }
            else
            {

                for (int i = 8; i >= 1; i--)
                {
                    for (int j = 1; j <= 8; j++)
                    {
                        if (input[i][j] == 20)
                        {
                            a = i; b = j;
                            for (int k = 1; k <= 8; k++)
                            {
                                for (int l = 1; l <= 8; l++)
                                {
                                    if (input[k][l] <= 10)
                                    {
                                        if ((k == i - 1 && l == j - 1) || (k == i && l == j - 1) || (k == i + 1 && l == j - 1) || (k == i + 1 && l == j) || (k == i + 1 && l == j + 1) || (k == i && l == j + 1) || (k == i - 1 && l == j + 1) || (k == i - 1 && l == j))
                                        {
                                            u[k][l] = 20;
                                            u[i][j] = 0;

                                            if (Danger_King(u, luot)[k][l] != 2)
                                            {
                                                return true;
                                            }
                                            u[k][l] = input[k][l];
                                            u[i][j] = 20;

                                        }
                                    }
                                }
                            }
                            goto EndOfBothLoops;
                        }
                    }
                }
            EndOfBothLoops:
                for (int i = 8; i >= 1; i--)
                {
                    for (int j = 1; j <= 8; j++)
                    {

                        if (input[i][j] == 0)
                        {
                        }
                        else if (input[i][j] == 11)
                        {
                            if (i - 1 > 0 && j - 1 >= 1 && input[i - 1][j - 1] <= 10)
                            {
                                u[i - 1][j - 1] = 11;
                                u[i][j] = 0;

                                if (Danger_King(u, luot)[a][b] != 2)
                                {
                                    return true;
                                }
                                u[i - 1][j - 1] = input[i - 1][j - 1];
                                u[i][j] = 11;
                            }
                            if (i - 1 > 0 && j + 1 <= 8 && input[i - 1][j + 1] <= 10)
                            {
                                u[i - 1][j + 1] = 11;
                                u[i][j] = 0;

                                if (Danger_King(u, luot)[a][b] != 2)
                                {
                                    return true;
                                }
                                u[i - 1][j + 1] = input[i - 1][j + 1];
                                u[i][j] = 11;
                            }
                            if (input[i - 1][j] == 0)
                            {
                                if (i == 2)
                                {
                                    u[i - 1][j] = 19;
                                    u[i][j] = 0;

                                    if (Danger_King(u, luot)[a][b] != 2)
                                    {
                                        return true;
                                    }
                                    u[i - 1][j] = 0;
                                    u[i][j] = 11;
                                }
                                else
                                {
                                    u[i - 1][j] = 11;
                                    u[i][j] = 0;

                                    if (Danger_King(u, luot)[a][b] != 2)
                                    {
                                        return true;
                                    }
                                    u[i - 1][j] = 0;
                                    u[i][j] = 11;
                                }
                            }
                            if (i == 7)
                            {
                                if (input[i - 2][j] == 0)
                                {
                                    u[i - 2][j] = 11;
                                    u[i][j] = 0;

                                    if (Danger_King(u, luot)[a][b] != 2)
                                    {
                                        return true;
                                    }
                                    u[i - 2][j] = 0;
                                    u[i][j] = 11;
                                }
                            }
                        }
                        else if (input[i][j] == 15)
                        {

                            for (int k = i - 1; k >= 1; k--)
                            {
                                if (input[k][j] == 0)
                                {
                                    u[k][j] = 15;
                                    u[i][j] = 0;

                                    if (Danger_King(u, luot)[a][b] != 2)
                                    {
                                        return true;
                                    }
                                    u[k][j] = input[k][j];
                                    u[i][j] = 15;
                                    continue;
                                }
                                else if (input[k][j] <= 10)
                                {
                                    u[k][j] = 15;
                                    u[i][j] = 0;

                                    if (Danger_King(u, luot)[a][b] != 2)
                                    {
                                        return true;
                                    }
                                    u[k][j] = input[k][j];
                                    u[i][j] = 15;
                                    break;
                                }
                                break;

                            }
                            for (int k = i + 1; k <= 8; k++)
                            {
                                if (input[k][j] == 0)
                                {
                                    u[k][j] = 15;
                                    u[i][j] = 0;

                                    if (Danger_King(u, luot)[a][b] != 2)
                                    {
                                        return true;
                                    }
                                    u[k][j] = input[k][j];
                                    u[i][j] = 15;
                                    continue;
                                }
                                else if (input[k][j] <= 10)
                                {
                                    u[k][j] = 15;
                                    u[i][j] = 0;

                                    if (Danger_King(u, luot)[a][b] != 2)
                                    {
                                        return true;
                                    }
                                    u[k][j] = input[k][j];
                                    u[i][j] = 15;
                                    break;
                                }
                                break;
                            }
                            for (int k = j - 1; k >= 1; k--)
                            {
                                if (input[i][k] == 0)
                                {
                                    u[i][k] = 15;
                                    u[i][j] = 0;

                                    if (Danger_King(u, luot)[a][b] != 2)
                                    {
                                        return true;
                                    }
                                    u[i][k] = input[i][k];
                                    u[i][j] = 15;
                                    continue;
                                }
                                else if (input[i][k] <= 10)
                                {
                                    u[i][k] = 15;
                                    u[i][j] = 0;

                                    if (Danger_King(u, luot)[a][b] != 2)
                                    {
                                        return true;
                                    }
                                    u[i][k] = input[i][k];
                                    u[i][j] = 15;
                                    break;
                                }
                                break;
                            }
                            for (int k = j + 1; k <= 8; k++)
                            {
                                if (input[i][k] == 0)
                                {
                                    u[i][k] = 15;
                                    u[i][j] = 0;

                                    if (Danger_King(u, luot)[a][b] != 2)
                                    {
                                        return true;
                                    }
                                    u[i][k] = input[i][k];
                                    u[i][j] = 15;
                                    continue;
                                }
                                else if (input[i][k] <= 10)
                                {
                                    u[i][k] = 15;
                                    u[i][j] = 0;

                                    if (Danger_King(u, luot)[a][b] != 2)
                                    {
                                        return true;
                                    }
                                    u[i][k] = input[i][k];
                                    u[i][j] = 15;
                                    break;
                                }
                                break;
                            }
                        }
                        else if (input[i][j] == 12)
                        {
                            for (int k = 1; k <= 8; k++)
                            {
                                for (int l = 1; l <= 8; l++)
                                {
                                    if (input[k][l] <= 10)
                                    {
                                        if ((k == i - 1 && l == j - 2) || (k == i + 1 && l == j - 2) || (k == i + 2 && l == j - 1) || (k == i + 2 && l == j + 1) || (k == i + 1 && l == j + 2) || (k == i - 1 && l == j + 2) || (k == i - 2 && l == j + 1) || (k == i - 2 && l == j - 1))
                                        {
                                            u[k][l] = 12;
                                            u[i][j] = 0;

                                            if (Danger_King(u, luot)[a][b] != 2)
                                            {
                                                return true;
                                            }
                                            u[k][l] = input[k][l];
                                            u[i][j] = 12;
                                        }
                                    }
                                }
                            }
                        }
                        else if (input[i][j] == 13)
                        {
                            for (int k = 1; k <= 8; k++)
                            {
                                if (i - k > 0 && j - k > 0)
                                {
                                    if (input[i - k][j - k] == 0)
                                    {
                                        u[i - k][j - k] = 13;
                                        u[i][j] = 0;

                                        if (Danger_King(u, luot)[a][b] != 2)
                                        {
                                            return true;
                                        }
                                        u[i - k][j - k] = input[i - k][j - k];
                                        u[i][j] = 13;
                                        continue;
                                    }
                                    else if (input[i - k][j - k] <= 10)
                                    {
                                        u[i - k][j - k] = 13;
                                        u[i][j] = 0;

                                        if (Danger_King(u, luot)[a][b] != 2)
                                        {
                                            return true;
                                        }
                                        u[i - k][j - k] = input[i - k][j - k];
                                        u[i][j] = 13;
                                        break;
                                    }
                                    break;
                                }
                            }
                            for (int k = 1; k <= 8; k++)
                            {
                                if (i + k <= 8 && j + k <= 8)
                                {
                                    if (input[i + k][j + k] == 0)
                                    {
                                        u[i + k][j + k] = 13;
                                        u[i][j] = 0;

                                        if (Danger_King(u, luot)[a][b] != 2)
                                        {
                                            return true;
                                        }
                                        u[i + k][j + k] = input[i + k][j + k];
                                        u[i][j] = 13;
                                        continue;
                                    }
                                    else if (input[i + k][j + k] <= 10)
                                    {
                                        u[i + k][j + k] = 13;
                                        u[i][j] = 0;

                                        if (Danger_King(u, luot)[a][b] != 2)
                                        {
                                            return true;
                                        }
                                        u[i + k][j + k] = input[i + k][j + k];
                                        u[i][j] = 13;
                                        break;
                                    }
                                    break;
                                }
                            }
                            for (int k = 1; k <= 8; k++)
                            {
                                if (i - k > 0 && j + k <= 8)
                                {
                                    if (input[i - k][j + k] == 0)
                                    {
                                        u[i - k][j + k] = 13;
                                        u[i][j] = 0;

                                        if (Danger_King(u, luot)[a][b] != 2)
                                        {
                                            return true;
                                        }
                                        u[i - k][j + k] = input[i - k][j + k];
                                        u[i][j] = 13;
                                        continue;
                                    }
                                    else if (input[i - k][j + k] <= 10)
                                    {
                                        u[i - k][j + k] = 13;
                                        u[i][j] = 0;

                                        if (Danger_King(u, luot)[a][b] != 2)
                                        {
                                            return true;
                                        }
                                        u[i - k][j + k] = input[i - k][j + k];
                                        u[i][j] = 13;
                                        break;
                                    }
                                    break;
                                }
                            }
                            for (int k = 1; k <= 8; k++)
                            {
                                if (i + k <= 8 && j - k > 0)
                                {
                                    if (input[i + k][j - k] == 0)
                                    {
                                        u[i + k][j - k] = 13;
                                        u[i][j] = 0;

                                        if (Danger_King(u, luot)[a][b] != 2)
                                        {
                                            return true;
                                        }
                                        u[i + k][j - k] = input[i + k][j - k];
                                        u[i][j] = 13;
                                        continue;
                                    }
                                    else if (input[i + k][j - k] <= 10)
                                    {
                                        u[i + k][j - k] = 13;
                                        u[i][j] = 0;

                                        if (Danger_King(u, luot)[a][b] != 2)
                                        {
                                            return true;
                                        }
                                        u[i + k][j - k] = input[i + k][j - k];
                                        u[i][j] = 13;
                                        break;
                                    }
                                    break;
                                }
                            }
                        }
                        else if (input[i][j] == 19)
                        {
                            for (int k = i - 1; k >= 1; k--)
                            {
                                if (input[k][j] == 0)
                                {
                                    u[k][j] = 19;
                                    u[i][j] = 0;

                                    if (Danger_King(u, luot)[a][b] != 2)
                                    {
                                        return true;
                                    }
                                    u[k][j] = input[k][j];
                                    u[i][j] = 19;
                                    continue;
                                }
                                else if (input[k][j] <= 10)
                                {
                                    u[k][j] = 19;
                                    u[i][j] = 0;

                                    if (Danger_King(u, luot)[a][b] != 2)
                                    {
                                        return true;
                                    }
                                    u[k][j] = input[k][j];
                                    u[i][j] = 19;
                                    break;
                                }
                                break;

                            }
                            for (int k = i + 1; k <= 8; k++)
                            {
                                if (input[k][j] == 0)
                                {
                                    u[k][j] = 19;
                                    u[i][j] = 0;

                                    if (Danger_King(u, luot)[a][b] != 2)
                                    {
                                        return true;
                                    }
                                    u[k][j] = input[k][j];
                                    u[i][j] = 19;
                                    continue;
                                }
                                else if (input[k][j] <= 10)
                                {
                                    u[k][j] = 19;
                                    u[i][j] = 0;

                                    if (Danger_King(u, luot)[a][b] != 2)
                                    {
                                        return true;
                                    }
                                    u[k][j] = input[k][j];
                                    u[i][j] = 19;
                                    break;
                                }
                                break;
                            }
                            for (int k = j - 1; k >= 1; k--)
                            {
                                if (input[i][k] == 0)
                                {
                                    u[i][k] = 19;
                                    u[i][j] = 0;

                                    if (Danger_King(u, luot)[a][b] != 2)
                                    {
                                        return true;
                                    }
                                    u[i][k] = input[i][k];
                                    u[i][j] = 19;
                                    continue;
                                }
                                else if (input[i][k] <= 10)
                                {
                                    u[i][k] = 19;
                                    u[i][j] = 0;

                                    if (Danger_King(u, luot)[a][b] != 2)
                                    {
                                        return true;
                                    }
                                    u[i][k] = input[i][k];
                                    u[i][j] = 19;
                                    break;
                                }
                                break;
                            }
                            for (int k = j + 1; k <= 8; k++)
                            {
                                if (input[i][k] == 0)
                                {
                                    u[i][k] = 19;
                                    u[i][j] = 0;

                                    if (Danger_King(u, luot)[a][b] != 2)
                                    {
                                        return true;
                                    }
                                    u[i][k] = input[i][k];
                                    u[i][j] = 19;
                                    continue;
                                }
                                else if (input[i][k] <= 10)
                                {
                                    u[i][k] = 19;
                                    u[i][j] = 0;

                                    if (Danger_King(u, luot)[a][b] != 2)
                                    {
                                        return true;
                                    }
                                    u[i][k] = input[i][k];
                                    u[i][j] = 19;
                                    break;
                                }
                                break;
                            }
                            // tuong
                            for (int k = 1; k <= 8; k++)
                            {
                                if (i - k > 0 && j - k > 0)
                                {
                                    if (input[i - k][j - k] == 0)
                                    {
                                        u[i - k][j - k] = 19;
                                        u[i][j] = 0;

                                        if (Danger_King(u, luot)[a][b] != 2)
                                        {
                                            return true;
                                        }
                                        u[i - k][j - k] = input[i - k][j - k];
                                        u[i][j] = 19;
                                        continue;
                                    }
                                    else if (input[i - k][j - k] <= 10)
                                    {
                                        u[i - k][j - k] = 19;
                                        u[i][j] = 0;

                                        if (Danger_King(u, luot)[a][b] != 2)
                                        {
                                            return true;
                                        }
                                        u[i - k][j - k] = input[i - k][j - k];
                                        u[i][j] = 19;
                                        break;
                                    }
                                    break;
                                }
                            }
                            for (int k = 1; k <= 8; k++)
                            {
                                if (i + k <= 8 && j + k <= 8)
                                {
                                    if (input[i + k][j + k] == 0)
                                    {
                                        u[i + k][j + k] = 19;
                                        u[i][j] = 0;

                                        if (Danger_King(u, luot)[a][b] != 2)
                                        {
                                            return true;
                                        }
                                        u[i + k][j + k] = input[i + k][j + k];
                                        u[i][j] = 19;
                                        continue;
                                    }
                                    else if (input[i + k][j + k] <= 10)
                                    {
                                        u[i + k][j + k] = 19;
                                        u[i][j] = 0;

                                        if (Danger_King(u, luot)[a][b] != 2)
                                        {
                                            return true;
                                        }
                                        u[i + k][j + k] = input[i + k][j + k];
                                        u[i][j] = 19;
                                        break;
                                    }
                                    break;
                                }
                            }
                            for (int k = 1; k <= 8; k++)
                            {
                                if (i - k > 0 && j + k <= 8)
                                {
                                    if (input[i - k][j + k] == 0)
                                    {
                                        u[i - k][j + k] = 19;
                                        u[i][j] = 0;

                                        if (Danger_King(u, luot)[a][b] != 2)
                                        {
                                            return true;
                                        }
                                        u[i - k][j + k] = input[i - k][j + k];
                                        u[i][j] = 19;
                                        continue;
                                    }
                                    else if (input[i - k][j + k] <= 10)
                                    {
                                        u[i - k][j + k] = 19;
                                        u[i][j] = 0;

                                        if (Danger_King(u, luot)[a][b] != 2)
                                        {
                                            return true;
                                        }
                                        u[i - k][j + k] = input[i - k][j + k];
                                        u[i][j] = 19;
                                        break;
                                    }
                                    break;
                                }
                            }
                            for (int k = 1; k <= 8; k++)
                            {
                                if (i + k <= 8 && j - k > 0)
                                {
                                    if (input[i + k][j - k] == 0)
                                    {
                                        u[i + k][j - k] = 19;
                                        u[i][j] = 0;

                                        if (Danger_King(u, luot)[a][b] != 2)
                                        {
                                            return true;
                                        }
                                        u[i + k][j - k] = input[i + k][j - k];
                                        u[i][j] = 19;
                                        continue;
                                    }
                                    else if (input[i + k][j - k] <= 10)
                                    {
                                        u[i + k][j - k] = 19;
                                        u[i][j] = 0;

                                        if (Danger_King(u, luot)[a][b] != 2)
                                        {
                                            return true;
                                        }
                                        u[i + k][j - k] = input[i + k][j - k];
                                        u[i][j] = 19;
                                        break;
                                    }
                                    break;
                                }
                            }
                        }

                    }
                }
            }
            return false;
        }
    }
    public class ChessAI : IDisposable
    {
		int[][] t = new int[9][];
		int[][] d = new int[9][];
		public ChessAI()
		{
			for (int i = 0; i < 9; i++)
			{
				t[i] = new int[9];
				d[i] = new int[9];
				for (int j = 1; j < 9; j++)
				{
					t[i][j] = 20;
					d[i][j] = 10;
				}
			}
		}
        private bool disposed = false;

        // Phương thức giải phóng tài nguyên
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    // Giải phóng các tài nguyên không quản lý ở đây (nếu có)
                }

                // Đặt cờ disposed thành true để tránh giải phóng tài nguyên nhiều lần
                disposed = true;
            }
        }

        // Phương thức Dispose cho việc sử dụng trong khối using
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        public int SumArray(int[][] input, bool luot)
		{
			var u = Danger_King(input,false);
			var v = Danger_King(input, true);
			// độ liên kết giữa các quân cờ
			int sum = 0;
			// không gian chiếm giữ
			double space = 0;
			double score = 0.5;
			int value = 2;
			for (int i = 1; i <= 8; i++)
			{
				for (int j = 1; j <= 8; j++)
				{
					if (input[i][j] == 0)
					{
					}
					else if (input[i][j] == 1)
					{
						sum -= 10*value;
						sum -= (i + 1);
						if (i + 1 <= 8)
						{
                            if (i <= 4)
                            {
                                space -= score * i;
                            }
                            else
                            {
                                space -= score * (9 - i);
                            }
                            if (j <= 4)
                            {
                                space -= score * j;
                            }
                            else
                            {
                                space -= score * (9 - j);
                            }
                            if (j + 1 <= 8)
							{
                                
                                if (input[i + 1][j + 1] == 0)
								{
                                    space -= score;
                                }
								else
								{
									sum--;
								}
							}
							if (j - 1 >= 1)
							{
								if (input[i + 1][j - 1] == 0)
								{
									space -= score;
								}
								else
								{
									sum--;
								}
							}

						}


					}
					else if (input[i][j] == 5)
					{
						sum -= 50 * value;
						for (int k = i - 1; k >= 1; k--)
						{
							if (input[k][j] == 0 || input[k][j] == 20)
							{
								space -= score;
								continue;
							}
							else if (input[k][j] > 0)
							{
								sum--;
								break;
							}
							break;
						}
						for (int k = i + 1; k <= 8; k++)
						{
							if (input[k][j] == 0 || input[k][j] == 20)
							{
								space -= score;
								continue;
							}
							else if (input[k][j] > 0)
							{
								sum--;
								break;
							}
							break;
						}
						for (int k = j - 1; k >= 1; k--)
						{
							if (input[i][k] == 0 || input[i][k] == 20)
							{
								space -= score;
								continue;
							}
							else if (input[i][k] > 0)
							{
								sum--;
								break;
							}
							break;
						}
						for (int k = j + 1; k <= 8; k++)
						{
							if (input[i][k] == 0 || input[i][k] == 20)
							{
								space -= score;
								continue;
							}
							else if (input[i][k] > 0)
							{
								sum--;
								break;
							}
							break;
						}
					}
					else if (input[i][j] == 2)
					{
						sum -= 30 * value;
						for (int k = 1; k <= 8; k++)
						{
							for (int l = 1; l <= 8; l++)
							{
								if ((k == i - 1 && l == j - 2) || (k == i + 1 && l == j - 2) || (k == i + 2 && l == j - 1) || (k == i + 2 && l == j + 1) || (k == i + 1 && l == j + 2) || (k == i - 1 && l == j + 2) || (k == i - 2 && l == j + 1) || (k == i - 2 && l == j - 1))
								{
									if (k > 0 && k <= 8 && l > 0 && l <= 8)
									{
										if (input[k][l] == 0)
										{
											space -= score;
										}
										else if (input[i][j] > 0)
										{
											sum--;
										}
									}

								}

							}
						}
					}
					else if (input[i][j] == 3)
					{
						sum -= 30 * value;
						for (int k = 1; k <= 8; k++)
						{
							if (i - k > 0 && j - k > 0)
							{
								if (input[i - k][j - k] == 0 || input[i - k][j - k] == 20)
								{
									space -= score;
									continue;
								}
								else
								{
									sum--;
									break;
								}
								break;
							}
						}
						for (int k = 1; k <= 8; k++)
						{
							if (i + k <= 8 && j + k <= 8)
							{
								if (input[i + k][j + k] == 0 || input[i + k][j + k] == 20)
								{
									space -= score;
									continue;
								}
								else
								{
									sum--;
									break;
								}
								break;
							}
						}
						for (int k = 1; k <= 8; k++)
						{
							if (i - k > 0 && j + k <= 8)
							{
								if (input[i - k][j + k] == 0 || input[i - k][j + k] == 20)
								{
									space -= score;
									continue;
								}
								else
								{
									sum--;
									break;
								}
								break;
							}
						}
						for (int k = 1; k <= 8; k++)
						{
							if (i + k <= 8 && j - k > 0)
							{
								if (input[i + k][j - k] == 0 || input[i + k][j - k] == 20)
								{
									space -= score;
									continue;
								}
								else
								{
									sum--;
									break;
								}
								break;
							}
						}
					}
					else if (input[i][j] == 9)
					{
						sum -= 90 * value;
						for (int k = i - 1; k >= 1; k--)
						{
							if (input[k][j] == 0 || input[k][j] == 20)
							{
								space -= score;
								continue;
							}
							else if (input[k][j] > 0)
							{
								sum--;
								break;
							}
							break;

						}
						for (int k = i + 1; k <= 8; k++)
						{
							if (input[k][j] == 0 || input[k][j] == 20)
							{
								space -= score;
								continue;
							}
							else if (input[k][j] > 0)
							{
								sum--;
								break;
							}
							break;
						}
						for (int k = j - 1; k >= 1; k--)
						{
							if (input[i][k] == 0 || input[i][k] == 20)
							{
								space -= score;
								continue;
							}
							else if (input[i][k] > 0)
							{
								sum--;
								break;
							}
							break;
						}
						for (int k = j + 1; k <= 8; k++)
						{
							if (input[i][k] == 0 || input[i][k] == 20)
							{
								space -= score;
								continue;
							}
							else if (input[i][k] > 0)
							{
								sum--;
								break;
							}
							break;
						}
						// tuong
						for (int k = 1; k <= 8; k++)
						{
							if (i - k > 0 && j - k > 0)
							{
								if (input[i - k][j - k] == 0 || input[i - k][j - k] == 20)
								{
									space -= score;
									continue;
								}
								else
								{
									sum--;
									break;
								}
								break;
							}
						}
						for (int k = 1; k <= 8; k++)
						{
							if (i + k <= 8 && j + k <= 8)
							{
								if (input[i + k][j + k] == 0 || input[i + k][j + k] == 20)
								{
									space -= score;
									continue;
								}
								else
								{
									sum--;
									break;
								}
								break;
							}
						}
						for (int k = 1; k <= 8; k++)
						{
							if (i - k > 0 && j + k <= 8)
							{
								if (input[i - k][j + k] == 0 || input[i - k][j + k] == 20)
								{
									space -= score;
									continue;
								}
								else
								{
									sum--;
									break;
								}
								break;
							}
						}
						for (int k = 1; k <= 8; k++)
						{
							if (i + k <= 8 && j - k > 0)
							{
								if (input[i + k][j - k] == 0 || input[i + k][j - k] == 20)
								{
									space -= score;
									continue;
								}
								else
								{
									sum--;
									break;
								}
								break;
							}
						}
					}
					else if (input[i][j] == 10)
					{
						
						sum -= 100000000;
						if (u[i][j] == 1)
						{
							if (luot)
							{
								return 1000000;
							}
							else
							{
								if (!Defense_King(input, !luot))
								{
									return 1000000;
								}
							}
						}
						else
						{
							for (int k = 1; k <= 8; k++)
							{
								for (int l = 1; l <= 8; l++)
								{
									if ((k == i - 1 && l == j - 1) || (k == i && l == j - 1) || (k == i + 1 && l == j - 1) || (k == i + 1 && l == j) || (k == i + 1 && l == j + 1) || (k == i && l == j + 1) || (k == i - 1 && l == j + 1) || (k == i - 1 && l == j))
									{

										if (input[k][l] > 0 && input[k][l] <= 10)
										{
											sum--;
										}
										else if (input[k][l] == 0)
										{
											space -= score;
											if (u[k][l] == 1)
											{
												sum += 8;
											}
										}
									}
								}
							}
						}
						
					}
					else if (input[i][j] == 11)
					{
						sum += 10 * value;
						sum += 9 - i;
						if (i - 1 >= 1)
						{
                            if (i <= 4)
                            {
                                space += score * i;
                            }
                            else
                            {
                                space += score * (9 - i);
                            }
                            if (j <= 4)
                            {
                                space += score * j;
                            }
                            else
                            {
                                space += score * (9 - j);
                            }
                            if (j + 1 <= 8)
							{
								if (input[i - 1][j + 1] == 0)
								{
									space += score;
								}
								else
								{
									sum++;
								}

							}
							if (j - 1 >= 1)
							{
								if (input[i - 1][j - 1] == 0)
								{
									space += score;
								}
								else
								{
									sum++;
								}
							}

						}
					}
					else if (input[i][j] == 15)
					{
						sum += 50 * value;
						for (int k = i - 1; k >= 1; k--)
						{
							if (input[k][j] == 0 || input[k][j] == 10)
							{
								space += score;
								continue;
							}
							else if (input[k][j] > 0)
							{
								sum++;
								break;
							}
							break;
						}
						for (int k = i + 1; k <= 8; k++)
						{
							if (input[k][j] == 0 || input[k][j] == 10)
							{
								space += score;
								continue;
							}
							else if (input[k][j] > 0)
							{
								sum++;
								break;
							}
							break;
						}
						for (int k = j - 1; k >= 1; k--)
						{
							if (input[i][k] == 0 || input[i][k] == 10)
							{
								space += score;
								continue;
							}
							else if (input[i][k] > 0)
							{
								sum++;
								break;
							}
							break;
						}
						for (int k = j + 1; k <= 8; k++)
						{
							if (input[i][k] == 0 || input[i][k] == 10)
							{
								space += score;
								continue;
							}
							else if (input[i][k] > 0)
							{
								sum++;
								break;
							}
							break;
						}
					}
					else if (input[i][j] == 12)
					{
						sum += 30 * value;
						for (int k = 1; k <= 8; k++)
						{
							for (int l = 1; l <= 8; l++)
							{
								if ((k == i - 1 && l == j - 2) || (k == i + 1 && l == j - 2) || (k == i + 2 && l == j - 1) || (k == i + 2 && l == j + 1) || (k == i + 1 && l == j + 2) || (k == i - 1 && l == j + 2) || (k == i - 2 && l == j + 1) || (k == i - 2 && l == j - 1))
								{
									if (k > 0 && k <= 8 && l > 0 && l <= 8)
									{
										if (input[k][l] == 0)
										{
											space += score;
										}
										else if (input[k][l] > 0)
										{
											sum++;
										}
									}

								}

							}
						}
					}
					else if (input[i][j] == 13)
					{
						sum += 30 * value;
						for (int k = 1; k <= 8; k++)
						{
							if (i - k > 0 && j - k > 0)
							{
								if (input[i - k][j - k] == 0 || input[i - k][j - k] == 10)
								{
									space += score;
									continue;
								}
								else
								{
									sum++;
									break;
								}
								break;
							}
						}
						for (int k = 1; k <= 8; k++)
						{
							if (i + k <= 8 && j + k <= 8)
							{
								if (input[i + k][j + k] == 0 || input[i + k][j + k] == 10)
								{
									space += score;
									continue;
								}
								else
								{
									sum++;
									break;
								}
								break;
							}
						}
						for (int k = 1; k <= 8; k++)
						{
							if (i - k > 0 && j + k <= 8)
							{
								if (input[i - k][j + k] == 0 || input[i - k][j + k] == 10)
								{
									space += score;
									continue;
								}
								else
								{
									sum++;
									break;
								}
								break;
							}
						}
						for (int k = 1; k <= 8; k++)
						{
							if (i + k <= 8 && j - k > 0)
							{
								if (input[i + k][j - k] == 0 || input[i + k][j - k] == 10)
								{
									space += score;
									continue;
								}
								else
								{
									sum++;
									break;
								}
								break;
							}
						}
					}
					else if (input[i][j] == 19)
					{
						sum += 90 * value;
						for (int k = i - 1; k >= 1; k--)
						{
							if (input[k][j] == 0 || input[k][j] == 10)
							{
								space += score;
								continue;
							}
							else if (input[k][j] > 0)
							{
								sum++;
								break;
							}
							break;
						}
						for (int k = i + 1; k <= 8; k++)
						{
							if (input[k][j] == 0 || input[k][j] == 10)
							{
								space += score;
								continue;
							}
							else if (input[k][j] > 0)
							{
								sum++;
								break;
							}
							break;
						}
						for (int k = j - 1; k >= 1; k--)
						{
							if (input[i][k] == 0 || input[i][k] == 10)
							{
								space += score;
								continue;
							}
							else if (input[i][k] > 0)
							{
								sum++;
								break;
							}
							break;
						}
						for (int k = j + 1; k <= 8; k++)
						{
							if (input[i][k] == 0 || input[i][k] == 10)
							{
								space += score;
								continue;
							}
							else if (input[i][k] > 0)
							{
								sum++;
								break;
							}
							break;
						}
						// tuong
						for (int k = 1; k <= 8; k++)
						{
							if (i - k > 0 && j - k > 0)
							{
								if (input[i - k][j - k] == 0 || input[i - k][j - k] == 10)
								{
									space += score;
									continue;
								}
								else
								{
									sum++;
									break;
								}
								break;
							}
						}
						for (int k = 1; k <= 8; k++)
						{
							if (i + k <= 8 && j + k <= 8)
							{
								if (input[i + k][j + k] == 0 || input[i + k][j + k] == 10)
								{
									space += score;
									continue;
								}
								else
								{
									sum++;
									break;
								}
								break;
							}
						}
						for (int k = 1; k <= 8; k++)
						{
							if (i - k > 0 && j + k <= 8)
							{
								if (input[i - k][j + k] == 0 || input[i - k][j + k] == 10)
								{
									space += score;
									continue;
								}
								else
								{
									sum++;
									break;
								}
								break;
							}
						}
						for (int k = 1; k <= 8; k++)
						{
							if (i + k <= 8 && j - k > 0)
							{
								if (input[i + k][j - k] == 0 || input[i + k][j - k] == 10)
								{
									space += score;
									continue;
								}
								else
								{
									sum++;
									break;
								}
								break;
							}
						}
					}
					else if (input[i][j] == 20)
					{
						sum += 100000000;
						if (v[i][j] == 2 )
						{
							if (!luot)
							{
								return -1000000;

                            }
							else
							{
								if (!Defense_King(input, luot))
								{
									
									return -1000000;
								}
							}
						}
						else
						{
							for (int k = 1; k <= 8; k++)
							{
								for (int l = 1; l <= 8; l++)
								{
									if ((k == i - 1 && l == j - 1) || (k == i && l == j - 1) || (k == i + 1 && l == j - 1) || (k == i + 1 && l == j) || (k == i + 1 && l == j + 1) || (k == i && l == j + 1) || (k == i - 1 && l == j + 1) || (k == i - 1 && l == j))
									{

										if (input[k][l] > 10)
										{
											sum++;
										}
										else if (input[k][l] == 0)
										{
											space += score;
											if (v[k][l] == 2)
											{
												sum -= 8;
											}
										}

									}
								}
							}
						}

						
					}
				}
			}
			return (int)(sum + score);
		}
		int[][] CloneArray(int[][] source, int x, int y, int a, int b, int value, bool luot)
		{
			int[][] clone = new int[source.Length][];
			for (int i = 0; i < source.Length; i++)
			{
				clone[i] = new int[source[i].Length];
				Array.Copy(source[i], clone[i], source[i].Length);
			}
			clone[x][y] = value;
			clone[a][b] = 0;
			return clone;
		}

		// luot = true: den, false: trang
		public int[][] Danger_King(int[][] input, bool luot)
		{
			int[][] u = new int[10][];
			for (int i = 0; i < 10; i++)
			{
				u[i] = new int[10];
			}
			if (luot)
			{
				for (int a = 1; a <= 8; a++)
				{
					for (int b = 1; b <= 8; b++)
					{

						if (input[a][b] == 11)
						{
							u[a - 1][b - 1] = 1;
							u[a - 1][b + 1] = 1;
						}

						else if (input[a][b] == 15)
						{
							for (int i = a - 1; i >= 1; i--)
							{
								if (input[i][b] == 0 || input[i][b] == 10)
								{
									u[i][b] = 1;
									continue;
								}
								else if (input[i][b] > 10 || input[i][b] == 10)
								{
									u[i][b] = 1;
								}
								break;
							}
							for (int i = a + 1; i <= 8; i++)
							{
								if (input[i][b] == 0 || input[i][b] == 10)
								{
									u[i][b] = 1;
									continue;
								}
								else if (input[i][b] > 10 || input[i][b] == 10)
								{
									u[i][b] = 1;
								}
								break;
							}
							for (int i = b - 1; i >= 1; i--)
							{
								if (input[a][i] == 0 || input[a][i] == 10)
								{
									u[a][i] = 1;
									continue;
								}
								else if (input[a][i] > 10 || input[a][i] == 10)
								{
									u[a][i] = 1;
								}
								break;
							}
							for (int i = b + 1; i <= 8; i++)
							{
								if (input[a][i] == 0 || input[a][i] == 10)
								{
									u[a][i] = 1;
									continue;
								}
								else if (input[a][i] > 10 || input[a][i] == 10)
								{
									u[a][i] = 1;
								}
								break;
							}
						}
						else if (input[a][b] == 12)
						{
							for (int i = 1; i <= 8; i++)
							{
								for (int j = 1; j <= 8; j++)
								{
									if (input[i][j] == 0 || input[i][j] == 10 || input[i][j] > 10)
									{
										if ((i == a - 1 && j == b - 2) || (i == a + 1 && j == b - 2) || (i == a + 2 && j == b - 1) || (i == a + 2 && j == b + 1) || (i == a + 1 && j == b + 2) || (i == a - 1 && j == b + 2) || (i == a - 2 && j == b + 1) || (i == a - 2 && j == b - 1))
										{
											u[i][j] = 1;
										}
									}
								}
							}
						}
						else if (input[a][b] == 13)
						{
							for (int i = 1; i <= 8; i++)
							{
								if (a - i > 0 && b - i > 0)
								{
									if (input[a - i][b - i] == 0 || input[a - i][b - i] == 10)
									{
										u[a - i][b - i] = 1;
										continue;
									}
									else if (input[a - i][b - i] > 10 || input[a - i][b - i] == 10)
									{
										u[a - i][b - i] = 1;
									}
									break;
								}
								else
								{
									break;
								}
							}
							for (int i = 1; i <= 8; i++)
							{
								if (a + i <= 8 && b + i <= 8)
								{
									if (input[a + i][b + i] == 0 || input[a + i][b + i] == 10)
									{
										u[a + 1][b + i] = 1;
										continue;
									}
									else if (input[a + i][b + i] > 10 || input[a + i][b + i] == 10)
									{
										u[a + 1][b + i] = 1;
									}
									break;
								}
								else
								{
									break;
								}
							}
							for (int i = 1; i <= 8; i++)
							{
								if (a - i > 0 && b + i <= 8)
								{
									if (input[a - i][b + i] == 0 || input[a - i][b + i] == 10)
									{
										u[a - i][b + i] = 1;
										continue;
									}
									else if (input[a - i][b + i] > 10 || input[a - i][b + i] == 10)
									{
										u[a - i][b + i] = 1;
									}
									break;
								}
								else
								{
									break;
								}
							}
							for (int i = 1; i <= 8; i++)
							{
								if (a + i <= 8 && b - i > 0)
								{
									if (input[a + i][b - i] == 0 || input[a + i][b - i] == 10)
									{
										u[a + i][b - i] = 1;
										continue;
									}
									else if (input[a + i][b - i] > 10 || input[a + i][b - i] == 10)
									{
										u[a + i][b - i] = 1;
									}
									break;
								}
								else
								{
									break;
								}
							}
						}
						else if (input[a][b] == 19)
						{
							for (int i = a - 1; i >= 1; i--)
							{
								if (input[i][b] == 0 || input[i][b] == 10)
								{
									u[i][b] = 1;
									continue;
								}
								else if (input[i][b] > 10 || input[i][b] == 10)
								{
									u[i][b] = 1;
								}
								break;
							}
							for (int i = a + 1; i <= 8; i++)
							{
								if (input[i][b] == 0 || input[i][b] == 10)
								{
									u[i][b] = 1;
									continue;
								}
								else if (input[i][b] > 10 || input[i][b] == 10)
								{
									u[i][b] = 1;
								}
								break;
							}
							for (int i = b - 1; i >= 1; i--)
							{
								if (input[a][i] == 0 || input[a][i] == 10)
								{
									u[a][i] = 1;
									continue;
								}
								else if (input[a][i] > 10 || input[a][i] == 10)
								{
									u[a][i] = 1;
								}
								break;
							}
							for (int i = b + 1; i <= 8; i++)
							{
								if (input[a][i] == 0 || input[a][i] == 10)
								{
									u[a][i] = 1;
									continue;
								}
								else if (input[a][i] > 10 || input[a][i] == 10)
								{
									u[a][i] = 1;
								}
								break;
							}
							// tuong
							for (int i = 1; i <= 8; i++)
							{
								if (a - i > 0 && b - i > 0)
								{
									if (input[a - i][b - i] == 0 || input[a - i][b - i] == 10)
									{
										u[a - i][b - i] = 1;
										continue;
									}
									else if (input[a - i][b - i] > 10 || input[a - i][b - i] == 10)
									{
										u[a - i][b - i] = 1;
									}
									break;
								}
								else
								{
									break;
								}
							}
							for (int i = 1; i <= 8; i++)
							{
								if (a + i <= 8 && b + i <= 8)
								{
									if (input[a + i][b + i] == 0 || input[a + i][b + i] == 10)
									{
										u[a + i][b + i] = 1;
										continue;
									}
									else if (input[a + i][b + i] > 10 || input[a + i][b + i] == 10)
									{
										u[a + i][b + i] = 1;
									}
									break;
								}
								else
								{
									break;
								}
							}
							for (int i = 1; i <= 8; i++)
							{
								if (a - i > 0 && b + i <= 8)
								{
									if (input[a - i][b + i] == 0 || input[a - i][b + i] == 10)
									{
										u[a - i][b + i] = 1;
										continue;
									}
									else if (input[a - i][b + i] > 10 || input[a - i][b + i] == 10)
									{
										u[a - i][b + i] = 1;
									}
									break;
								}
								else
								{
									break;
								}
							}
							for (int i = 1; i <= 8; i++)
							{
								if (a + i <= 8 && b - i > 0)
								{
									if (input[a + i][b - i] == 0 || input[a + i][b - i] == 10)
									{
										u[a + i][b - i] = 1;
										continue;
									}
									else if (input[a + i][b - i] > 10 || input[a + i][b - i] == 10)
									{
										u[a + i][b - i] = 1;
									}
									break;
								}
								else
								{
									break;
								}
							}
						}
						else if (input[a][b] == 20)
						{
							for (int i = 1; i <= 8; i++)
							{
								for (int j = 1; j <= 8; j++)
								{
									if (input[i][j] == 0)
									{
										if ((i == a - 1 && j == b - 1) || (i == a && j == b - 1) || (i == a + 1 && j == b - 1) || (i == a + 1 && j == b) || (i == a + 1 && j == b + 1) || (i == a && j == b + 1) || (i == a - 1 && j == b + 1) || (i == a - 1 && j == b))
										{
											u[i][j] = 1;
										}
									}
								}
							}
						}

					}
				}
			}
			else
			{
				for(int a = 1; a<= 8; a++)
				{
					for(int  b = 1; b<= 8; b++)
					{
						if (input[a][b] == 1)
						{
							u[a + 1][b - 1] = 2;
							u[a + 1][b + 1] = 2;
						}
						else if (input[a][b] == 5)
						{
							for (int i = a - 1; i >= 1; i--)
							{
								if (input[i][b] == 0 || input[i][b] == 20)
								{
									u[i][b] = 2;
									continue;
								}
								else if (input[i][b] < 10)
								{
									u[i][b] = 2;
								}
								break;
							}
							for (int i = a + 1; i <= 8; i++)
							{
								if (input[i][b] == 0 || input[i][b] == 20)
								{
									u[i][b] = 2;
									continue;
								}
								else if (input[i][b] < 10)
								{
									u[i][b] = 2;
								}
								break;
							}
							for (int i = b - 1; i >= 1; i--)
							{
								if (input[a][i] == 0 || input[a][i] == 20)
								{
									u[a][i] = 2;
									continue;
								}
								else if (input[a][i] < 10)
								{
									u[a][i] = 2;
								}
								break;
							}
							for (int i = b + 1; i <= 8; i++)
							{
								if (input[a][i] == 0 || input[a][i] == 20)
								{
									u[a][i] = 2;
									continue;
								}
								else if (input[a][i] < 10)
								{
									u[a][i] = 2;
								}
								break;
							}
						}
						else if (input[a][b] == 2)
						{
							for (int i = 1; i <= 8; i++)
							{
								for (int l = 1; l <= 8; l++)
								{
									if (input[i][l] == 0 || input[i][l] == 20 || input[i][l] < 10)
									{
										if ((i == a - 1 && l == b - 2) || (i == a + 1 && l == b - 2) || (i == a + 2 && l == b - 1) || (i == a + 2 && l == b + 1) || (i == a + 1 && l == b + 2) || (i == a - 1 && l == b + 2) || (i == a - 2 && l == b + 1) || (i == a - 2 && l == b - 1))
										{
											u[i][l] = 2;
										}
									}
								}
							}
						}
						else if (input[a][b] == 3)
						{
							for (int i = 1; i <= 8; i++)
							{
								if (a - i > 0 && b - i > 0)
								{
									if (input[a - i][b - i] == 0 || input[a - i][b - i] == 20)
									{
										u[a - i][b - i] = 2;
										continue;
									}
									else if (input[a - i][b - i] < 10)
									{
										u[a - i][b - i] = 2;
									}
									break;
								}
								else
								{
									break;
								}
							}
							for (int i = 1; i <= 8; i++)
							{
								if (a + i <= 8 && b + i <= 8)
								{
									if (input[a + i][b + i] == 0 || input[a + i][b + i] == 20)
									{
										u[a + i][b + i] = 2;
										continue;
									}
									else if (input[a + i][b + i] < 10)
									{
										u[a + i][b + i] = 2;
									}
									break;
								}
								else
								{
									break;
								}
							}
							for (int i = 1; i <= 8; i++)
							{
								if (a - i > 0 && b + i <= 8)
								{
									if (input[a - i][b + i] == 0 || input[a - i][b + i] == 20)
									{
										u[a - i][b + i] = 2;
										continue;
									}
									else if (input[a - i][b + i] < 10)
									{
										u[a - i][b + i] = 2;
									}
									break;
								}
								else
								{
									break;
								}
							}
							for (int i = 1; i <= 8; i++)
							{
								if (a + i <= 8 && b - i > 0)
								{
									if (input[a + i][b - i] == 0 || input[a + i][b - i] == 20)
									{
										u[a + i][b - i] = 2;
										continue;
									}
									else if (input[a + i][b - i] < 10)
									{
										u[a + i][b - i] = 2;
									}
									break;
								}
								else
								{
									break;
								}
							}
						}
						else if (input[a][b] == 9)
						{
							for (int i = a - 1; i >= 1; i--)
							{
								if (input[i][b] == 0 || input[i][b] == 20)
								{
									u[i][b] = 2;
									continue;
								}
								else if (input[i][b] < 10)
								{
									u[i][b] = 2;
								}
								break;
							}
							for (int i = a + 1; i <= 8; i++)
							{
								if (input[i][b] == 0 || input[i][b] == 20)
								{
									u[i][b] = 2;
									continue;
								}
								else if (input[i][b] < 10)
								{
									u[i][b] = 2;
								}
								break;
							}
							for (int i = b - 1; i >= 1; i--)
							{
								if (input[a][i] == 0 || input[a][i] == 20)
								{
									u[a][i] = 2;
									continue;
								}
								else if (input[a][i] < 10)
								{
									u[a][i] = 2;
								}
								break;
							}
							for (int i = b + 1; i <= 8; i++)
							{
								if (input[a][i] == 0 || input[a][i] == 20)
								{
									u[a][i] = 2;
									continue;
								}
								else if (input[a][i] < 10)
								{
									u[a][i] = 2;
								}
								break;
							}
							// tuong
							for (int i = 1; i <= 8; i++)
							{
								if (a - i > 0 && b - i > 0)
								{
									if (input[a - i][b - i] == 0 || input[a - i][b - i] == 20)
									{
										u[a - i][b - i] = 2;
										continue;
									}
									else if (input[a - i][b - i] < 10)
									{
										u[a - i][b - i] = 2;
									}
									break;
								}
								else
								{
									break;

								}
							}
							for (int i = 1; i <= 8; i++)
							{
								if (a + i <= 8 && b + i <= 8)
								{
									if (input[a + i][b + i] == 0 || input[a + i][b + i] == 20)
									{
										u[a + i][b + i] = 2;
										continue;
									}
									else if (input[a + i][b + i] < 10)
									{
										u[a + i][b + i] = 2;
									}
									break;
								}
								else
								{
									break;
								}
							}
							for (int i = 1; i <= 8; i++)
							{
								if (a - i > 0 && b + i <= 8)
								{
									if (input[a - i][b + i] == 0 || input[a - i][b + i] == 20)
									{
										u[a - i][b + i] = 2;
										continue;
									}
									else if (input[a - i][b + i] < 10)
									{
										u[a - i][b + i] = 2;
									}
									break;
								}
								else
								{
									break;
								}
							}
							for (int i = 1; i <= 8; i++)
							{
								if (a + i <= 8 && b - i > 0)
								{
									if (input[a + i][b - i] == 0 || input[a + i][b - i] == 20)
									{
										u[a + i][b - i] = 2;
										continue;
									}
									else if (input[a + i][b - i] < 10)
									{
										u[a + i][b - i] = 2;
									}
									break;
								}
								else
								{
									break;
								}
							}
						}
						else if (input[a][b] == 10)
						{
							for (int i = 1; i <= 8; i++)
							{
								for (int l = 1; l <= 8; l++)
								{
									if (input[i][l] == 0 || input[i][l] == 20 || input[i][l] < 10)
									{
										if ((i == a - 1 && l == b - 1) || (i == a && l == b - 1) || (i == a + 1 && l == b - 1) || (i == a + 1 && l == b) || (i == a + 1 && l == b + 1) || (i == a && l == b + 1) || (i == a - 1 && l == b + 1) || (i == a - 1 && l == b))
										{
											u[i][l] = 2;
										}
									}
								}
							}
						}
					}
				}
			}
			
			return u;
		}

		public bool Defense_King(int[][] input, bool luot)
		{

			int[][] u = new int[9][];
			for (int i = 1; i <= 8; i++)
			{
				u[i] = new int[9];
				for(int j = 1;j <= 8; j++)
				{
					u[i][j] = input[i][j];
				}
			}
			int a = 1; int b = 1;
			if (luot)
			{
				
				for (int i = 1; i <= 8; i++)
				{
					for (int j = 1; j <= 8; j++)
					{
						if (input[i][j] == 0)
						{
						}
						else if (input[i][j] == 10)
						{
							a = i; b = j;
							for (int k = 1; k <= 8; k++)
							{
								for (int l = 1; l <= 8; l++)
								{
									if (input[k][l] == 0 || input[k][l] > 10)
									{
										if ((k == i - 1 && l == j - 1) || (k == i && l == j - 1) || (k == i + 1 && l == j - 1) || (k == i + 1 && l == j) || (k == i + 1 && l == j + 1) || (k == i && l == j + 1) || (k == i - 1 && l == j + 1) || (k == i - 1 && l == j))
										{
											u[k][l] = 10;
											u[i][j] = 0;
											
											if (Danger_King(u,luot)[k][l] != 1)
											{
												return true;
											}
											u[k][l] = input[k][l];
											u[i][j] = 10;
										}
									}
								}
							}
							goto EndOfBothLoops;
						}
					}
				}
				EndOfBothLoops:
				for (int i = 1; i <= 8; i++)
				{
					for (int j = 1; j <= 8; j++)
					{
						if (input[i][j] == 0)
						{
						}
						else if (input[i][j] == 1)
						{
							if (i + 1 <= 8)
							{
								if (j - 1 >= 0)
								{
									if (input[i + 1][j - 1] > 10)
									{
										u[i+1][j-1] = 1;
										u[i][j] = 0;
										
										if (Danger_King(u,luot)[a][b] != 1)
										{
											return true;
										}
										u[i + 1][j - 1] = input[i+1][j-1];
										u[i][j] = 1;

									}
								}
								if (j + 1 <= 8)
								{
									if (input[i + 1][j + 1] > 10)
									{
										u[i + 1][j + 1] = 1;
										u[i][j] = 0;
										
										if (Danger_King(u,luot)[a][b] != 1)
										{
											return true;
										}
										u[i + 1][j + 1] = input[i + 1][j + 1];
										u[i][j] = 1;
									}
								}
								if (input[i + 1][j] == 0)
								{
									if (i == 7)
									{
										u[i+1][j-1] = 9;
										u[i][j] = 0;
										
										if (Danger_King(u,luot)[a][b] != 1)
										{
											return true;
										}
										u[i + 1][j] = 0;
										u[i][j] = 1;
									}
									else
									{
										u[i + 1][j - 1] = 1;
										u[i][j] = 0;
										
										if (Danger_King(u,luot)[a][b] != 1)
										{
											return true;
										}
										u[i + 1][j] = 0;
										u[i][j] = 1;
									}
								}
								if (i == 2 && input[i + 1][j] == 0)
								{
									if (input[i + 2][j] == 0)
									{
										u[i + 2][j] = 1;
										u[i][j] = 0;
										
										if (Danger_King(u,luot)[a][b] != 1)
										{
											return true;
										}
										u[i + 2][j] = 0;
										u[i][j] = 1;
									}
								}
							}

						}
						else if (input[i][j] == 5)
						{

							for (int k = i - 1; k >= 1; k--)
							{
								if (input[k][j] == 0)
								{
									u[k][j] = 5;
									u[i][j] = 0;
									
									if (Danger_King(u,luot)[a][b] != 1)
									{
										return true;
									}
									u[k][j] = 0;
									u[i][j] = 5;
									continue;
								}
								else if (input[k][j] > 10)
								{
									u[k][j] = 5;
									u[i][j] = 0;
									
									if (Danger_King(u,luot)[a][b] != 1)
									{
										return true;
									}
									u[k][j] = input[k][j];
									u[i][j] = 5;
									break;
								}
								break;

							}
							for (int k = i + 1; k <= 8; k++)
							{
								if (input[k][j] == 0)
								{
									u[k][j] = 5;
									u[i][j] = 0;
									
									if (Danger_King(u,luot)[a][b] != 1)
									{
										return true;
									}
									u[k][j] = 0;
									u[i][j] = 5;
									continue;
								}
								else if (input[k][j] > 10)
								{
									u[k][j] = 5;
									u[i][j] = 0;
									
									if (Danger_King(u,luot)[a][b] != 1)
									{
										return true;
									}
									u[k][j] = input[k][j];
									u[i][j] = 5;
									break;
								}
								break;
							}
							for (int k = j - 1; k >= 1; k--)
							{
								if (input[i][k] == 0)
								{
									u[i][k] = 5;
									u[i][j] = 0;
									
									if (Danger_King(u,luot)[a][b] != 1)
									{
										return true;
									}
									u[i][k] = 0;
									u[i][j] = 5;
									continue;
								}
								else if (input[i][k] > 10)
								{
									u[i][k] = 5;
									u[i][j] = 0;
									
									if (Danger_King(u,luot)[a][b] != 1)
									{
										return true;
									}
									u[i][k] = input[i][k];
									u[i][j] = 5;
									break;
								}
								break;
							}
							for (int k = j + 1; k <= 8; k++)
							{
								if (input[i][k] == 0)
								{
									u[i][k] = 5;
									u[i][j] = 0;
									
									if (Danger_King(u,luot)[a][b] != 1)
									{
										return true;
									}
									u[i][k] = 0;
									u[i][j] = 5;
									continue;
								}
								else if (input[i][k] > 10)
								{
									u[i][k] = 5;
									u[i][j] = 0;
									
									if (Danger_King(u,luot)[a][b] != 1)
									{
										return true;
									}
									u[i][k] = input[i][k];
									u[i][j] = 5;
									break;
								}
								break;
							}
						}
						else if (input[i][j] == 2)
						{
							for (int k = 1; k <= 8; k++)
							{
								for (int l = 1; l <= 8; l++)
								{
									if (input[k][l] == 0 || input[k][l] > 10)
									{
										if ((k == i - 1 && l == j - 2) || (k == i + 1 && l == j - 2) || (k == i + 2 && l == j - 1) || (k == i + 2 && l == j + 1) || (k == i + 1 && l == j + 2) || (k == i - 1 && l == j + 2) || (k == i - 2 && l == j + 1) || (k == i - 2 && l == j - 1))
										{
											u[k][l] = 3;
											u[i][j] = 0;
											
											if (Danger_King(u,luot)[a][b] != 1)
											{
												return true;
											}
											u[k][l] = input[k][l];
											u[i][j] = 3;
										}
									}
								}
							}
						}
						else if (input[i][j] == 3)
						{
							for (int k = 1; k <= 8; k++)
							{
								if (i - k > 0 && j - k > 0)
								{
									if (input[i - k][j - k] == 0)
									{
										u[i - k][j - k] = 3;
										u[i][j] = 0;
										
										if (Danger_King(u,luot)[a][b] != 1)
										{
											return true;
										}
										u[i - k][j - k] = 0;
										u[i][j] = 3;
										continue;
									}
									else if (input[i - k][j - k] > 10)
									{
										u[i - k][j - k] = 3;
										u[i][j] = 0;
										
										if (Danger_King(u,luot)[a][b] != 1)
										{
											return true;
										}
										u[i - k][j - k] = input[i-k][j-k];
										u[i][j] = 3;
										break;
									}
									break;
								}
							}
							for (int k = 1; k <= 8; k++)
							{
								if (i + k <= 8 && j + k <= 8)
								{
									if (input[i + k][j + k] == 0)
									{
										u[i + k][j + k] = 3;
										u[i][j] = 0;
										
										if (Danger_King(u,luot)[a][b] != 1)
										{
											return true;
										}
										u[i + k][j + k] = 0;
										u[i][j] = 3;
										continue;
									}
									else if (input[i + k][j + k] > 10)
									{
										u[i + k][j + k] = 3;
										u[i][j] = 0;
										
										if (Danger_King(u,luot)[a][b] != 1)
										{
											return true;
										}
										u[i + k][j + k] = input[i + k][j + k];
										u[i][j] = 3;
										break;
									}
									break;
								}
							}
							for (int k = 1; k <= 8; k++)
							{
								if (i - k > 0 && j + k <= 8)
								{
									if (input[i - k][j + k] == 0)
									{
										u[i - k][j + k] = 3;
										u[i][j] = 0;
										
										if (Danger_King(u,luot)[a][b] != 1)
										{
											return true;
										}
										u[i - k][j + k] = 0;
										u[i][j] = 3;
										continue;
									}
									else if (input[i - k][j + k] > 10)
									{
										u[i - k][j + k] = 3;
										u[i][j] = 0;
										
										if (Danger_King(u,luot)[a][b] != 1)
										{
											return true;
										}
										u[i - k][j + k] = input[i - k][j + k];
										u[i][j] = 3;
										break;
									}
									break;
								}
							}
							for (int k = 1; k <= 8; k++)
							{
								if (i + k <= 8 && j - k > 0)
								{
									if (input[i + k][j - k] == 0)
									{
										u[i + k][j - k] = 3;
										u[i][j] = 0;
										
										if (Danger_King(u,luot)[a][b] != 1)
										{
											return true;
										}
										u[i + k][j - k] = 0;
										u[i][j] = 3;
										continue;
									}
									else if (input[i + k][j - k] > 10)
									{
										u[i + k][j - k] = 3;
										u[i][j] = 0;
										
										if (Danger_King(u,luot)[a][b] != 1)
										{
											return true;
										}
										u[i + k][j - k] = input[i + k][j - k];
										u[i][j] = 3;
										break;
									}
									break;
								}
							}
						}
						else if (input[i][j] == 9)
						{
							for (int k = i - 1; k >= 1; k--)
							{
								if (input[k][j] == 0)
								{
									u[k][j] = 9;
									u[i][j] = 0;
									
									if (Danger_King(u,luot)[a][b] != 1)
									{
										return true;
									}
									u[k][j] = 0;
									u[i][j] = 9;
									continue;
								}
								else if (input[k][j] > 10)
								{
									u[k][j] = 9;
									u[i][j] = 0;
									
									if (Danger_King(u,luot)[a][b] != 1)
									{
										return true;
									}
									u[k][j] = input[k][j];
									u[i][j] = 9;
									break;
								}
								break;

							}
							for (int k = i + 1; k <= 8; k++)
							{
								if (input[k][j] == 0)
								{
									u[k][j] = 9;
									u[i][j] = 0;
									
									if (Danger_King(u,luot)[a][b] != 1)
									{
										return true;
									}
									u[k][j] = 0;
									u[i][j] = 9;
									continue;
								}
								else if (input[k][j] > 10)
								{
									u[k][j] = 9;
									u[i][j] = 0;
									
									if (Danger_King(u,luot)[a][b] != 1)
									{
										return true;
									}
									u[k][j] = input[k][j];
									u[i][j] = 9;
									break;
								}
								break;
							}
							for (int k = j - 1; k >= 1; k--)
							{
								if (input[i][k] == 0)
								{
									u[i][k] = 9;
									u[i][j] = 0;
									
									if (Danger_King(u,luot)[a][b] != 1)
									{
										return true;
									}
									u[i][k] = 0;
									u[i][j] = 9;
									continue;
								}
								else if (input[i][k] > 10)
								{
									u[i][k] = 9;
									u[i][j] = 0;
									
									if (Danger_King(u,luot)[a][b] != 1)
									{
										return true;
									}
									u[i][k] = input[i][k];
									u[i][j] = 9;
									break;
								}
								break;
							}
							for (int k = j + 1; k <= 8; k++)
							{
								if (input[i][k] == 0)
								{
									u[i][k] = 9;
									u[i][j] = 0;
									
									if (Danger_King(u,luot)[a][b] != 1)
									{
										return true;
									}
									u[i][k] = 0;
									u[i][j] = 9;
									continue;
								}
								else if (input[i][k] > 10)
								{
									u[i][k] = 9;
									u[i][j] = 0;
									
									if (Danger_King(u,luot)[a][b] != 1)
									{
										return true;
									}
									u[i][k] = input[i][k];
									u[i][j] = 9;
									break;
								}
								break;
							}
							// tuong
							for (int k = 1; k <= 8; k++)
							{
								if (i - k > 0 && j - k > 0)
								{
									if (input[i - k][j - k] == 0)
									{
										u[i - k][j - k] = 9;
										u[i][j] = 0;
										
										if (Danger_King(u,luot)[a][b] != 1)
										{
											return true;
										}
										u[i - k][j - k] = 0;
										u[i][j] = 9;
										continue;
									}
									else if (input[i - k][j - k] > 10)
									{
										u[i - k][j - k] = 9;
										u[i][j] = 0;
										
										if (Danger_King(u,luot)[a][b] != 1)
										{
											return true;
										}
										u[i - k][j - k] = input[i - k][j - k];
										u[i][j] = 9;
										break;
									}
									break;
								}
							}
							for (int k = 1; k <= 8; k++)
							{
								if (i + k <= 8 && j + k <= 8)
								{
									if (input[i + k][j + k] == 0)
									{
										u[i + k][j + k] = 9;
										u[i][j] = 0;
										
										if (Danger_King(u,luot)[a][b] != 1)
										{
											return true;
										}
										u[i + k][j + k] = 0;
										u[i][j] = 9;
										continue;
									}
									else if (input[i + k][j + k] > 10)
									{
										u[i + k][j + k] = 9;
										u[i][j] = 0;
										
										if (Danger_King(u,luot)[a][b] != 1)
										{
											return true;
										}
										u[i + k][j + k] = input[i + k][j + k];
										u[i][j] = 9;
										break;
									}
									break;
								}
							}
							for (int k = 1; k <= 8; k++)
							{
								if (i - k > 0 && j + k <= 8)
								{
									if (input[i - k][j + k] == 0)
									{
										u[i - k][j + k] = 9;
										u[i][j] = 0;
										
										if (Danger_King(u,luot)[a][b] != 1)
										{
											return true;
										}
										u[i - k][j + k] = 0;
										u[i][j] = 9;
										continue;
									}
									else if (input[i - k][j + k] > 10)
									{
										u[i - k][j + k] = 9;
										u[i][j] = 0;
										
										if (Danger_King(u,luot)[a][b] != 1)
										{
											return true;
										}
										u[i - k][j + k] = input[i - k][j + k];
										u[i][j] = 9;
										break;
									}
									break;
								}
							}
							for (int k = 1; k <= 8; k++)
							{
								if (i + k <= 8 && j - k > 0)
								{
									if (input[i + k][j - k] == 0)
									{
										u[i + k][j - k] = 9;
										u[i][j] = 0;
										
										if (Danger_King(u,luot)[a][b] != 1)
										{
											return true;
										}
										u[i + k][j - k] = 0;
										u[i][j] = 9;
										continue;
									}
									else if (input[i + k][j - k] > 10)
									{
										u[i + k][j - k] = 9;
										u[i][j] = 0;
										
										if (Danger_King(u,luot)[a][b] != 1)
										{
											return true;
										}
										u[i + k][j - k] = input[i + k][j - k];
										u[i][j] = 9;
										break;
									}
									break;
								}
							}
						}
						
					}
				}
			}
			else
			{

				for (int i = 8; i >= 1; i--)
				{
					for (int j = 1; j <= 8; j++)
					{
						 if (input[i][j] == 20)
						{
							a = i; b = j;
							for (int k = 1; k <= 8; k++)
							{
								for (int l = 1; l <= 8; l++)
								{
									if (input[k][l] <= 10)
									{
										if ((k == i - 1 && l == j - 1) || (k == i && l == j - 1) || (k == i + 1 && l == j - 1) || (k == i + 1 && l == j) || (k == i + 1 && l == j + 1) || (k == i && l == j + 1) || (k == i - 1 && l == j + 1) || (k == i - 1 && l == j))
										{
											u[k][l] = 20;
											u[i][j] = 0;
											
											if (Danger_King(u,luot)[k][l] != 2)
											{
												return true;
											}
											u[k][l] = input[k][l];
											u[i][j] = 20;

										}
									}
								}
							}
							goto EndOfBothLoops1;
						}
					}
				}
				EndOfBothLoops1:
				for (int i = 8; i >= 1; i--)
				{
					for (int j = 1; j <= 8; j++)
					{

						if (input[i][j] == 0)
						{
						}
						else if (input[i][j] == 11)
						{
							if (i - 1 > 0 && j - 1 >= 1 && input[i - 1][j - 1] <= 10)
							{
								u[i - 1][j - 1] = 11;
								u[i][j] = 0;
								
								if (Danger_King(u,luot)[a][b] != 2)
								{
									return true;
								}
								u[i - 1][j - 1] = input[i - 1][j - 1];
								u[i][j] = 11;
							}
							if (i - 1 > 0 && j + 1 <= 8 && input[i - 1][j + 1] <= 10)
							{
								u[i - 1][j + 1] = 11;
								u[i][j] = 0;
								
								if (Danger_King(u,luot)[a][b] != 2)
								{
									return true;
								}
								u[i - 1][j + 1] = input[i - 1][j + 1];
								u[i][j] = 11;
							}
							if (input[i - 1][j] == 0)
							{
								if (i == 2)
								{
									u[i - 1][j] = 19;
									u[i][j] = 0;
									
									if (Danger_King(u,luot)[a][b] != 2)
									{
										return true;
									}
									u[i - 1][j] = 0;
									u[i][j] = 11;
								}
								else
								{
									u[i - 1][j] = 11;
									u[i][j] = 0;
									
									if (Danger_King(u,luot)[a][b] != 2)
									{
										return true;
									}
									u[i - 1][j] = 0;
									u[i][j] = 11;
								}
							}
							if (i == 7)
							{
								if (input[i - 2][j] == 0)
								{
									u[i - 2][j] = 11;
									u[i][j] = 0;
									
									if (Danger_King(u,luot)[a][b] != 2)
									{
										return true;
									}
									u[i - 2][j ] = 0;
									u[i][j] = 11;
								}
							}
						}
						else if (input[i][j] == 15)
						{

							for (int k = i - 1; k >= 1; k--)
							{
								if (input[k][j] == 0)
								{
									u[k][j] = 15;
									u[i][j] = 0;
									
									if (Danger_King(u,luot)[a][b] != 2)
									{
										return true;
									}
									u[k][j] = input[k][j];
									u[i][j] = 15;
									continue;
								}
								else if (input[k][j] <= 10)
								{
									u[k][j] = 15;
									u[i][j] = 0;
									
									if (Danger_King(u,luot)[a][b] != 2)
									{
										return true;
									}
									u[k][j] = input[k][j];
									u[i][j] = 15;
									break;
								}
								break;

							}
							for (int k = i + 1; k <= 8; k++)
							{
								if (input[k][j] == 0)
								{
									u[k][j] = 15;
									u[i][j] = 0;
									
									if (Danger_King(u,luot)[a][b] != 2)
									{
										return true;
									}
									u[k][j] = input[k][j];
									u[i][j] = 15;
									continue;
								}
								else if (input[k][j] <= 10)
								{
									u[k][j] = 15;
									u[i][j] = 0;
									
									if (Danger_King(u,luot)[a][b] != 2)
									{
										return true;
									}
									u[k][j] = input[k][j];
									u[i][j] = 15;
									break;
								}
								break;
							}
							for (int k = j - 1; k >= 1; k--)
							{
								if (input[i][k] == 0)
								{
									u[i][k] = 15;
									u[i][j] = 0;
									
									if (Danger_King(u,luot)[a][b] != 2)
									{
										return true;
									}
									u[i][k] = input[i][k];
									u[i][j] = 15;
									continue;
								}
								else if (input[i][k] <= 10)
								{
									u[i][k] = 15;
									u[i][j] = 0;
									
									if (Danger_King(u,luot)[a][b] != 2)
									{
										return true;
									}
									u[i][k] = input[i][k];
									u[i][j] = 15;
									break;
								}
								break;
							}
							for (int k = j + 1; k <= 8; k++)
							{
								if (input[i][k] == 0)
								{
									u[i][k] = 15;
									u[i][j] = 0;
									
									if (Danger_King(u,luot)[a][b] != 2)
									{
										return true;
									}
									u[i][k] = input[i][k];
									u[i][j] = 15;
									continue;
								}
								else if (input[i][k] <= 10)
								{
									u[i][k] = 15;
									u[i][j] = 0;
									
									if (Danger_King(u,luot)[a][b] != 2)
									{
										return true;
									}
									u[i][k] = input[i][k];
									u[i][j] = 15;
									break;
								}
								break;
							}
						}
						else if (input[i][j] == 12)
						{
							for (int k = 1; k <= 8; k++)
							{
								for (int l = 1; l <= 8; l++)
								{
									if (input[k][l] <= 10)
									{
										if ((k == i - 1 && l == j - 2) || (k == i + 1 && l == j - 2) || (k == i + 2 && l == j - 1) || (k == i + 2 && l == j + 1) || (k == i + 1 && l == j + 2) || (k == i - 1 && l == j + 2) || (k == i - 2 && l == j + 1) || (k == i - 2 && l == j - 1))
										{
											u[k][l] = 12;
											u[i][j] = 0;
											
											if (Danger_King(u,luot)[a][b] != 2)
											{
												return true;
											}
											u[k][l] = input[k][l];
											u[i][j] = 12;
										}
									}
								}
							}
						}
						else if (input[i][j] == 13)
						{
							for (int k = 1; k <= 8; k++)
							{
								if (i - k > 0 && j - k > 0)
								{
									if (input[i - k][j - k] == 0)
									{
										u[i - k][j - k] = 13;
										u[i][j] = 0;
										
										if (Danger_King(u,luot)[a][b] != 2)
										{
											return true;
										}
										u[i - k][j - k] = input[i - k][j - k];
										u[i][j] = 13;
										continue;
									}
									else if (input[i - k][j - k] <= 10)
									{
										u[i - k][j - k] = 13;
										u[i][j] = 0;
										
										if (Danger_King(u,luot)[a][b] != 2)
										{
											return true;
										}
										u[i - k][j - k] = input[i - k][j - k];
										u[i][j] = 13;
										break;
									}
									break;
								}
							}
							for (int k = 1; k <= 8; k++)
							{
								if (i + k <= 8 && j + k <= 8)
								{
									if (input[i + k][j + k] == 0)
									{
										u[i + k][j + k] = 13;
										u[i][j] = 0;
										
										if (Danger_King(u,luot)[a][b] != 2)
										{
											return true;
										}
										u[i + k][j + k] = input[i + k][j + k];
										u[i][j] = 13;
										continue;
									}
									else if (input[i + k][j + k] <= 10)
									{
										u[i + k][j + k] = 13;
										u[i][j] = 0;
										
										if (Danger_King(u,luot)[a][b] != 2)
										{
											return true;
										}
										u[i + k][j + k] = input[i + k][j + k];
										u[i][j] = 13;
										break;
									}
									break;
								}
							}
							for (int k = 1; k <= 8; k++)
							{
								if (i - k > 0 && j + k <= 8)
								{
									if (input[i - k][j + k] == 0)
									{
										u[i - k][j + k] = 13;
										u[i][j] = 0;
										
										if (Danger_King(u,luot)[a][b] != 2)
										{
											return true;
										}
										u[i - k][j + k] = input[i - k][j + k];
										u[i][j] = 13;
										continue;
									}
									else if (input[i - k][j + k] <= 10)
									{
										u[i - k][j + k] = 13;
										u[i][j] = 0;
										
										if (Danger_King(u,luot)[a][b] != 2)
										{
											return true;
										}
										u[i - k][j + k] = input[i - k][j + k];
										u[i][j] = 13;
										break;
									}
									break;
								}
							}
							for (int k = 1; k <= 8; k++)
							{
								if (i + k <= 8 && j - k > 0)
								{
									if (input[i + k][j - k] == 0)
									{
										u[i + k][j - k] = 13;
										u[i][j] = 0;
										
										if (Danger_King(u,luot)[a][b] != 2)
										{
											return true;
										}
										u[i + k][j - k] = input[i + k][j - k];
										u[i][j] = 13;
										continue;
									}
									else if (input[i + k][j - k] <= 10)
									{
										u[i + k][j - k] = 13;
										u[i][j] = 0;
										
										if (Danger_King(u,luot)[a][b] != 2)
										{
											return true;
										}
										u[i + k][j - k] = input[i + k][j - k];
										u[i][j] = 13;
										break;
									}
									break;
								}
							}
						}
						else if (input[i][j] == 19)
						{
							for (int k = i - 1; k >= 1; k--)
							{
								if (input[k][j] == 0)
								{
									u[k][j] = 19;
									u[i][j] = 0;
									
									if (Danger_King(u,luot)[a][b] != 2)
									{
										return true;
									}
									u[k][j] = input[k][j];
									u[i][j] = 19;
									continue;
								}
								else if (input[k][j] <= 10)
								{
									u[k][j] = 19;
									u[i][j] = 0;
									
									if (Danger_King(u,luot)[a][b] != 2)
									{
										return true;
									}
									u[k][j] = input[k][j];
									u[i][j] = 19;
									break;
								}
								break;

							}
							for (int k = i + 1; k <= 8; k++)
							{
								if (input[k][j] == 0)
								{
									u[k][j] = 19;
									u[i][j] = 0;
									
									if (Danger_King(u,luot)[a][b] != 2)
									{
										return true;
									}
									u[k][j] = input[k][j];
									u[i][j] = 19;
									continue;
								}
								else if (input[k][j] <= 10)
								{
									u[k][j] = 19;
									u[i][j] = 0;
									
									if (Danger_King(u,luot)[a][b] != 2)
									{
										return true;
									}
									u[k][j] = input[k][j];
									u[i][j] = 19;
									break;
								}
								break;
							}
							for (int k = j - 1; k >= 1; k--)
							{
								if (input[i][k] == 0)
								{
									u[i][k] = 19;
									u[i][j] = 0;
									
									if (Danger_King(u,luot)[a][b] != 2)
									{
										return true;
									}
									u[i][k] = input[i][k];
									u[i][j] = 19;
									continue;
								}
								else if (input[i][k] <= 10)
								{
									u[i][k] = 19;
									u[i][j] = 0;
									
									if (Danger_King(u,luot)[a][b] != 2)
									{
										return true;
									}
									u[i][k] = input[i][k];
									u[i][j] = 19;
									break;
								}
								break;
							}
							for (int k = j + 1; k <= 8; k++)
							{
								if (input[i][k] == 0)
								{
									u[i][k] = 19;
									u[i][j] = 0;
									
									if (Danger_King(u,luot)[a][b] != 2)
									{
										return true;
									}
									u[i][k] = input[i][k];
									u[i][j] = 19;
									continue;
								}
								else if (input[i][k] <= 10)
								{
									u[i][k] = 19;
									u[i][j] = 0;
									
									if (Danger_King(u,luot)[a][b] != 2)
									{
										return true;
									}
									u[i][k] = input[i][k];
									u[i][j] = 19;
									break;
								}
								break;
							}
							// tuong
							for (int k = 1; k <= 8; k++)
							{
								if (i - k > 0 && j - k > 0)
								{
									if (input[i - k][j - k] == 0)
									{
										u[i - k][j - k] = 19;
										u[i][j] = 0;
										
										if (Danger_King(u,luot)[a][b] != 2)
										{
											return true;
										}
										u[i - k][j - k] = input[i - k][j - k];
										u[i][j] = 19;
										continue;
									}
									else if (input[i - k][j - k] <= 10)
									{
										u[i - k][j - k] = 19;
										u[i][j] = 0;
										
										if (Danger_King(u,luot)[a][b] != 2)
										{
											return true;
										}
										u[i - k][j - k] = input[i - k][j - k];
										u[i][j] = 19;
										break;
									}
									break;
								}
							}
							for (int k = 1; k <= 8; k++)
							{
								if (i + k <= 8 && j + k <= 8)
								{
									if (input[i + k][j + k] == 0)
									{
										u[i + k][j + k] = 19;
										u[i][j] = 0;
										
										if (Danger_King(u,luot)[a][b] != 2)
										{
											return true;
										}
										u[i + k][j + k] = input[i + k][j + k];
										u[i][j] = 19;
										continue;
									}
									else if (input[i + k][j + k] <= 10)
									{
										u[i + k][j + k] = 19;
										u[i][j] = 0;
										
										if (Danger_King(u,luot)[a][b] != 2)
										{
											return true;
										}
										u[i + k][j + k] = input[i + k][j + k];
										u[i][j] = 19;
										break;
									}
									break;
								}
							}
							for (int k = 1; k <= 8; k++)
							{
								if (i - k > 0 && j + k <= 8)
								{
									if (input[i - k][j + k] == 0)
									{
										u[i - k][j + k] = 19;
										u[i][j] = 0;
										
										if (Danger_King(u,luot)[a][b] != 2)
										{
											return true;
										}
										u[i - k][j + k] = input[i - k][j + k];
										u[i][j] = 19;
										continue;
									}
									else if (input[i - k][j + k] <= 10)
									{
										u[i - k][j + k] = 19;
										u[i][j] = 0;
										
										if (Danger_King(u,luot)[a][b] != 2)
										{
											return true;
										}
										u[i - k][j + k] = input[i - k][j + k];
										u[i][j] = 19;
										break;
									}
									break;
								}
							}
							for (int k = 1; k <= 8; k++)
							{
								if (i + k <= 8 && j - k > 0)
								{
									if (input[i + k][j - k] == 0)
									{
										u[i + k][j - k] = 19;
										u[i][j] = 0;
										
										if (Danger_King(u,luot)[a][b] != 2)
										{
											return true;
										}
										u[i + k][j - k] = input[i + k][j - k];
										u[i][j] = 19;
										continue;
									}
									else if (input[i + k][j - k] <= 10)
									{
										u[i + k][j - k] = 19;
										u[i][j] = 0;
										
										if (Danger_King(u,luot)[a][b] != 2)
										{
											return true;
										}
										u[i + k][j - k] = input[i + k][j - k];
										u[i][j] = 19;
										break;
									}
									break;
								}
							}
						}
						
					}
				}
			}
			return false;
		}
		// luot = true: Den, false: trang
		public List<int[][]> ValidMoves(int[][] input, bool luot)
		{
			//SortedSet<int[][]> ValidMoves = new SortedSet<int[][]>(new ChessComparer(luot));
			List<int[][]> ValidMoves = new List<int[][]>();
            if (luot)
			{

				for (int i = 1; i <= 8; i++)
				{
					for (int j = 1; j <= 8; j++)
					{
						if (input[i][j] == 0)
						{
						}
						else if (input[i][j] == 1)
						{
							if(i + 1 <= 8)
							{
								if(j-1 >= 0)
								{
									if (input[i + 1][j - 1] > 10 && i+1 <= 8 && j-1 >=1)
									{
										ValidMoves.Add(CloneArray(input, i + 1, j - 1, i, j, 1, luot));
									}
								} 
								if(j+1 <= 8)
								{
									if (input[i + 1][j + 1] > 10 && i + 1 <= 8 && j +1 <= 8)
									{
										ValidMoves.Add(CloneArray(input, i + 1, j + 1, i, j, 1, luot));
									}
								}
								if (input[i + 1][j] == 0)
								{
									if (i == 7)
									{
										ValidMoves.Add(CloneArray(input, i + 1, j, i, j, 9, luot));
									}
									else
									{
										ValidMoves.Add(CloneArray(input, i + 1, j, i, j, 1, luot));
									}
								}
								if (i == 2 && input[i + 1][j] == 0)
								{
									if (input[i + 2][j] == 0)
									{
										ValidMoves.Add(CloneArray(input, i + 2, j, i, j, 1, luot));
									}
								}
							}
							
						}
						else if (input[i][j] == 5)
						{

							for (int k = i - 1; k >= 1; k--)
							{
								if (input[k][j] == 0)
								{
									ValidMoves.Add(CloneArray(input, k, j, i, j, 5, luot));
									continue;
								}
								else if (input[k][j] > 10)
								{
									ValidMoves.Add(CloneArray(input, k, j, i, j, 5, luot));
									break;
								}
								break;

							}
							for (int k = i + 1; k <= 8; k++)
							{
								if (input[k][j] == 0)
								{
									ValidMoves.Add(CloneArray(input, k, j, i, j, 5, luot));
									continue;
								}
								else if (input[k][j] > 10)
								{
									ValidMoves.Add(CloneArray(input, k, j, i, j, 5, luot));
									break;
								}
								break;
							}
							for (int k = j - 1; k >= 1; k--)
							{
								if (input[i][k] == 0)
								{
									ValidMoves.Add(CloneArray(input, i, k, i, j, 5, luot));
									continue;
								}
								else if (input[i][k] > 10)
								{
									ValidMoves.Add(CloneArray(input, i, k, i, j, 5, luot));
									break;
								}
								break;
							}
							for (int k = j + 1; k <= 8; k++)
							{
								if (input[i][k] == 0)
								{
									ValidMoves.Add(CloneArray(input, i, k, i, j, 5, luot));
									continue;
								}
								else if (input[i][k] > 10)
								{
									ValidMoves.Add(CloneArray(input, i, k, i, j, 5, luot));
									break;
								}
								break;
							}
						}
						else if (input[i][j] == 2)
						{
							for (int k = 1; k <= 8; k++)
							{
								for (int l = 1; l <= 8; l++)
								{
									if (input[k][l] == 0 || input[k][l] > 10)
									{
										if ((k == i - 1 && l == j - 2) || (k == i + 1 && l == j - 2) || (k == i + 2 && l == j - 1) || (k == i + 2 && l == j + 1) || (k == i + 1 && l == j + 2) || (k == i - 1 && l == j + 2) || (k == i - 2 && l == j + 1) || (k == i - 2 && l == j - 1))
										{
											ValidMoves.Add(CloneArray(input, k, l, i, j, 2, luot));
										}
									}
								}
							}
						}
						else if (input[i][j] == 3)
						{
							for (int k = 1; k <= 8; k++)
							{
								if (i - k > 0 && j - k > 0)
								{
									if (input[i - k][j - k] == 0)
									{
										ValidMoves.Add(CloneArray(input, i - k, j - k, i, j, 3, luot));
										continue;
									}
									else if (input[i - k][j - k] > 10)
									{
										ValidMoves.Add(CloneArray(input, i - k, j - k, i, j, 3, luot));
										break;
									}
									break;
								}
								else
								{
									break;
								}
							}
							for (int k = 1; k <= 8; k++)
							{
								if (i + k <= 8 && j + k <= 8)
								{
									if (input[i + k][j + k] == 0)
									{
										ValidMoves.Add(CloneArray(input, i + k, j + k, i, j, 3, luot));
										continue;
									}
									else if (input[i + k][j + k] > 10)
									{
										ValidMoves.Add(CloneArray(input, i + k, j + k, i, j, 3, luot));
										break;
									}
									break;
								}
								else
								{
									break;
								}
							}
							for (int k = 1; k <= 8; k++)
							{
								if (i - k > 0 && j + k <= 8)
								{
									if (input[i - k][j + k] == 0)
									{
										ValidMoves.Add(CloneArray(input, i - k, j + k, i, j, 3, luot));
										continue;
									}
									else if (input[i - k][j + k] > 10)
									{
										ValidMoves.Add(CloneArray(input, i - k, j + k, i, j, 3, luot));
										break;
									}
									break;
								}
								else
								{
									break;
								}
							}
							for (int k = 1; k <= 8; k++)
							{
								if (i + k <= 8 && j - k > 0)
								{
									if (input[i + k][j - k] == 0)
									{
										ValidMoves.Add(CloneArray(input, i + k, j - k, i, j, 3, luot));
										continue;
									}
									else if (input[i + k][j - k] > 10)
									{
										ValidMoves.Add(CloneArray(input, i + k, j - k, i, j, 3, luot));
										break;
									}
									break;
								}
								else
								{
									break;
								}
							}
						}
						else if (input[i][j] == 9)
						{
							for (int k = i - 1; k >= 1; k--)
							{
								if (input[k][j] == 0)
								{
									ValidMoves.Add(CloneArray(input, k, j, i, j, 9, luot));
									continue;
								}
								else if (input[k][j] > 10)
								{
									ValidMoves.Add(CloneArray(input, k, j, i, j, 9, luot));
									break;
								}
								break;

							}
							for (int k = i + 1; k <= 8; k++)
							{
								if (input[k][j] == 0)
								{
									ValidMoves.Add(CloneArray(input, k, j, i, j, 9, luot));
									continue;
								}
								else if (input[k][j] > 10)
								{
									ValidMoves.Add(CloneArray(input, k, j, i, j, 9, luot));
									break;
								}
								break;
							}
							for (int k = j - 1; k >= 1; k--)
							{
								if (input[i][k] == 0)
								{
									ValidMoves.Add(CloneArray(input, i, k, i, j, 9, luot));
									continue;
								}
								else if (input[i][k] > 10)
								{
									ValidMoves.Add(CloneArray(input, i, k, i, j, 9, luot));
									break;
								}
								break;
							}
							for (int k = j + 1; k <= 8; k++)
							{
								if (input[i][k] == 0)
								{
									ValidMoves.Add(CloneArray(input, i, k, i, j, 9, luot));
									continue;
								}
								else if (input[i][k] > 10)
								{
									ValidMoves.Add(CloneArray(input, i, k, i, j, 9, luot));
									break;
								}
								break;
							}
							// tuong
							for (int k = 1; k <= 8; k++)
							{
								if (i - k > 0 && j - k > 0)
								{
									if (input[i - k][j - k] == 0)
									{
										ValidMoves.Add(CloneArray(input, i - k, j - k, i, j, 9, luot));
										continue;
									}
									else if (input[i - k][j - k] > 10)
									{
										ValidMoves.Add(CloneArray(input, i - k, j - k, i, j, 9, luot));
										break;
									}
									break;
								}
								else
								{
									break;
								}
							}
							for (int k = 1; k <= 8; k++)
							{
								if (i + k <= 8 && j + k <= 8)
								{
									if (input[i + k][j + k] == 0)
									{
										ValidMoves.Add(CloneArray(input, i + k, j + k, i, j, 9, luot));
										continue;
									}
									else if (input[i + k][j + k] > 10)
									{
										ValidMoves.Add(CloneArray(input, i + k, j + k, i, j, 9, luot));
										break;
									}
									break;
								}
								else
								{
									break;
								}
							}
							for (int k = 1; k <= 8; k++)
							{
								if (i - k > 0 && j + k <= 8)
								{
									if (input[i - k][j + k] == 0)
									{
										ValidMoves.Add(CloneArray(input, i - k, j + k, i, j, 9, luot));
										continue;
									}
									else if (input[i - k][j + k] > 10)
									{
										ValidMoves.Add(CloneArray(input, i - k, j + k, i, j, 9, luot));
										break;
									}
									break;
								}
								else
								{
									break;
								}
							}
							for (int k = 1; k <= 8; k++)
							{
								if (i + k <= 8 && j - k > 0)
								{
									if (input[i + k][j - k] == 0)
									{
										ValidMoves.Add(CloneArray(input, i + k, j - k, i, j, 9, luot));
										continue;
									}
									else if (input[i + k][j - k] > 10)
									{
										ValidMoves.Add(CloneArray(input, i + k, j - k, i, j, 9, luot));
										break;
									}
									break;
								}
								else
								{
									break;
								}
							}
						}
						else if (input[i][j] == 10)
						{
							
							for (int k = 1; k <= 8; k++)
							{
								for (int l = 1; l <= 8; l++)
								{
									if (input[k][l] == 0 || input[k][l] > 10)
									{
										if ((k == i - 1 && l == j - 1) || (k == i && l == j - 1) || (k == i + 1 && l == j - 1) || (k == i + 1 && l == j) || (k == i + 1 && l == j + 1) || (k == i && l == j + 1) || (k == i - 1 && l == j + 1) || (k == i - 1 && l == j))
										{
												ValidMoves.Add(CloneArray(input, k, l, i, j, 10, luot));
											
										}
									}
								}
							}
						}
					}
				}
			}
			else
			{
				for (int i = 8; i >=1; i--)
				{
					for (int j = 1; j <= 8; j++)
					{

						if (input[i][j] == 0)
						{
						}
						else if(input[i][j] == 11 )
						{
							if (i - 1 > 0 && j - 1 >= 1 && input[i - 1][j - 1] <= 10)
							{
								ValidMoves.Add(CloneArray(input, i - 1, j - 1, i, j, 11, luot));
							}
							if (i - 1 > 0 && j + 1 <= 8 && input[i - 1][j + 1] <= 10)
							{
								ValidMoves.Add(CloneArray(input, i - 1, j + 1, i, j, 11, luot));
							}
							if (input[i - 1][j] == 0)
							{
								if (i == 2)
								{
									ValidMoves.Add(CloneArray(input, i - 1, j, i, j, 19, luot));
								}
								else
								{
									ValidMoves.Add(CloneArray(input, i - 1, j, i, j, 11, luot));
								}
							}
							if (i == 7)
							{
								if (input[i - 2][j] == 0)
								{
									ValidMoves.Add(CloneArray(input, i - 2, j, i, j, 11, luot));
								}
							}
						}
						else if (input[i][j] == 15)
						{

							for (int k = i - 1; k >= 1; k--)
							{
								if (input[k][j] == 0)
								{
									ValidMoves.Add(CloneArray(input, k, j, i, j, 15, luot));
									continue;
								}
								else if (input[k][j] <= 10)
								{
									ValidMoves.Add(CloneArray(input, k, j, i, j, 15, luot));
									break;
								}
								break;

							}
							for (int k = i + 1; k <= 8; k++)
							{
								if (input[k][j] == 0)
								{
									ValidMoves.Add(CloneArray(input, k, j, i, j, 15, luot));
									continue;
								}
								else if (input[k][j] <= 10)
								{
									ValidMoves.Add(CloneArray(input, k, j, i, j, 15, luot));
									break;
								}
								break;
							}
							for (int k = j - 1; k >= 1; k--)
							{
								if (input[i][k] == 0)
								{
									ValidMoves.Add(CloneArray(input, i, k, i, j, 15, luot));
									continue;
								}
								else if (input[i][k] <= 10)
								{
									ValidMoves.Add(CloneArray(input, i, k, i, j, 15, luot));
									break;
								}
								break;
							}
							for (int k = j + 1; k <= 8; k++)
							{
								if (input[i][k] == 0)
								{
									ValidMoves.Add(CloneArray(input, i, k, i, j, 15, luot));
									continue;
								}
								else if (input[i][k] <= 10)
								{
									ValidMoves.Add(CloneArray(input, i, k, i, j, 15, luot));
									break;
								}
								break;
							}
						}
						else if (input[i][j] == 12)
						{
							for (int k = 1; k <= 8; k++)
							{
								for (int l = 1; l <= 8; l++)
								{
									if (input[k][l] <= 10)
									{
										if ((k == i - 1 && l == j - 2) || (k == i + 1 && l == j - 2) || (k == i + 2 && l == j - 1) || (k == i + 2 && l == j + 1) || (k == i + 1 && l == j + 2) || (k == i - 1 && l == j + 2) || (k == i - 2 && l == j + 1) || (k == i - 2 && l == j - 1))
										{
											ValidMoves.Add(CloneArray(input, k, l, i, j, 12, luot));
										}
									}
								}
							}
						}
						else if (input[i][j] == 13)
						{
							for (int k = 1; k <= 8; k++)
							{
								if (i - k > 0 && j - k > 0)
								{
									if (input[i - k][j - k] == 0)
									{
										ValidMoves.Add(CloneArray(input, i - k, j - k, i, j, 13, luot));
										continue;
									}
									else if (input[i - k][j - k] <= 10)
									{
										ValidMoves.Add(CloneArray(input, i - k, j - k, i, j, 13, luot));
										break;
									}
									break;
								}
								else
								{
									break;
								}
							}
							for (int k = 1; k <= 8; k++)
							{
								if (i + k <= 8 && j + k <= 8)
								{
									if (input[i + k][j + k] == 0)
									{
										ValidMoves.Add(CloneArray(input, i + k, j + k, i, j, 13, luot));
										continue;
									}
									else if (input[i + k][j + k] <= 10)
									{
										ValidMoves.Add(CloneArray(input, i + k, j + k, i, j, 13, luot));
										break;
									}
									break;
								}
								else
								{
									break;
								}
							}
							for (int k = 1; k <= 8; k++)
							{
								if (i - k > 0 && j + k <= 8)
								{
									if (input[i - k][j + k] == 0)
									{
										ValidMoves.Add(CloneArray(input, i - k, j + k, i, j, 13, luot));
										continue;
									}
									else if (input[i - k][j + k] <= 10)
									{
										ValidMoves.Add(CloneArray(input, i - k, j + k, i, j, 13, luot));
										break;
									}
									break;
								}
								else
								{
									break;
								}
							}
							for (int k = 1; k <= 8; k++)
							{
								if (i + k <= 8 && j - k > 0)
								{
									if (input[i + k][j - k] == 0)
									{
										ValidMoves.Add(CloneArray(input, i + k, j - k, i, j, 13, luot));
										continue;
									}
									else if (input[i + k][j - k] <= 10)
									{
										ValidMoves.Add(CloneArray(input, i + k, j - k, i, j, 13, luot));
										break;
									}
									break;
								}
								else
								{
									break;
								}
							}
						}
						else if (input[i][j] == 19)
						{
							for (int k = i - 1; k >= 1; k--)
							{
								if (input[k][j] == 0)
								{
									ValidMoves.Add(CloneArray(input, k, j, i, j, 19, luot));
									continue;
								}
								else if (input[k][j] <= 10)
								{
									ValidMoves.Add(CloneArray(input, k, j, i, j, 19, luot));
									break;
								}
								break;

							}
							for (int k = i + 1; k <= 8; k++)
							{
								if (input[k][j] == 0)
								{
									ValidMoves.Add(CloneArray(input, k, j, i, j, 19, luot));
									continue;
								}
								else if (input[k][j] <= 10)
								{
									ValidMoves.Add(CloneArray(input, k, j, i, j, 19, luot));
									break;
								}
								break;
							}
							for (int k = j - 1; k >= 1; k--)
							{
								if (input[i][k] == 0)
								{
									ValidMoves.Add(CloneArray(input, i, k, i, j, 19, luot));
									continue;
								}
								else if (input[i][k] <= 10)
								{
									ValidMoves.Add(CloneArray(input, i, k, i, j, 19, luot));
									break;
								}
								break;
							}
							for (int k = j + 1; k <= 8; k++)
							{
								if (input[i][k] == 0)
								{
									ValidMoves.Add(CloneArray(input, i, k, i, j, 19, luot));
									continue;
								}
								else if (input[i][k] <= 10)
								{
									ValidMoves.Add(CloneArray(input, i, k, i, j, 19, luot));
									break;
								}
								break;
							}
							// tuong
							for (int k = 1; k <= 8; k++)
							{
								if (i - k > 0 && j - k > 0)
								{
									if (input[i - k][j - k] == 0)
									{
										ValidMoves.Add(CloneArray(input, i - k, j - k, i, j, 19, luot));
										continue;
									}
									else if (input[i - k][j - k] <= 10)
									{
										ValidMoves.Add(CloneArray(input, i - k, j - k, i, j, 19, luot));
										break;
									}
									break;
								}
							}
							for (int k = 1; k <= 8; k++)
							{
								if (i + k <= 8 && j + k <= 8)
								{
									if (input[i + k][j + k] == 0)
									{
										ValidMoves.Add(CloneArray(input, i + k, j + k, i, j, 19, luot));
										continue;
									}
									else if (input[i + k][j + k] <= 10)
									{
										ValidMoves.Add(CloneArray(input, i + k, j + k, i, j, 19, luot));
										break;
									}
									break;
								}
							}
							for (int k = 1; k <= 8; k++)
							{
								if (i - k > 0 && j + k <= 8)
								{
									if (input[i - k][j + k] == 0)
									{
										ValidMoves.Add(CloneArray(input, i - k, j + k, i, j, 19, luot));
										continue;
									}
									else if (input[i - k][j + k] <= 10)
									{
										ValidMoves.Add(CloneArray(input, i - k, j + k, i, j, 19, luot));
										break;
									}
									break;
								}
							}
							for (int k = 1; k <= 8; k++)
							{
								if (i + k <= 8 && j - k > 0)
								{
									if (input[i + k][j - k] == 0)
									{
										ValidMoves.Add(CloneArray(input, i + k, j - k, i, j, 19, luot));
										continue;
									}
									else if (input[i + k][j - k] <= 10)
									{
										ValidMoves.Add(CloneArray(input, i + k, j - k, i, j, 19, luot));
										break;
									}
									break;
								}
							}
						}
						else if (input[i][j] == 20)
						{
							
							for (int k = 1; k <= 8; k++)
							{
								for (int l = 1; l <= 8; l++)
								{
									if (input[k][l] <= 10)
									{
										if ((k == i - 1 && l == j - 1) || (k == i && l == j - 1) || (k == i + 1 && l == j - 1) || (k == i + 1 && l == j) || (k == i + 1 && l == j + 1) || (k == i && l == j + 1) || (k == i - 1 && l == j + 1) || (k == i - 1 && l == j))
										{
												ValidMoves.Add(CloneArray(input, k, l, i, j, 20, luot));
											
										}
									}
								}
                            }
                        }
                    }
                }
			}
            //List<int[][]> listFromSortedSet = ValidMoves.ToList();
            return ValidMoves;
		}
        public List<int[][]> ValidMoves_Arrange(int[][] input, bool luot)
        {
            SortedSet<int[][]> ValidMoves = new SortedSet<int[][]>(new ChessComparer(luot));
            
            if (luot)
            {

                for (int i = 1; i <= 8; i++)
                {
                    for (int j = 1; j <= 8; j++)
                    {
                        if (input[i][j] == 0)
                        {
                        }
                        else if (input[i][j] == 1)
                        {
                            if (i + 1 <= 8)
                            {
                                if (j - 1 >= 0)
                                {
                                    if (input[i + 1][j - 1] > 10 && i + 1 <= 8 && j - 1 >= 1)
                                    {
                                        ValidMoves.Add(CloneArray(input, i + 1, j - 1, i, j, 1, luot));
                                    }
                                }
                                if (j + 1 <= 8)
                                {
                                    if (input[i + 1][j + 1] > 10 && i + 1 <= 8 && j + 1 <= 8)
                                    {
                                        ValidMoves.Add(CloneArray(input, i + 1, j + 1, i, j, 1, luot));
                                    }
                                }
                                if (input[i + 1][j] == 0)
                                {
                                    if (i == 7)
                                    {
                                        ValidMoves.Add(CloneArray(input, i + 1, j, i, j, 9, luot));
                                    }
                                    else
                                    {
                                        ValidMoves.Add(CloneArray(input, i + 1, j, i, j, 1, luot));
                                    }
                                }
                                if (i == 2 && input[i + 1][j] == 0)
                                {
                                    if (input[i + 2][j] == 0)
                                    {
                                        ValidMoves.Add(CloneArray(input, i + 2, j, i, j, 1, luot));
                                    }
                                }
                            }

                        }
                        else if (input[i][j] == 5)
                        {

                            for (int k = i - 1; k >= 1; k--)
                            {
                                if (input[k][j] == 0)
                                {
                                    ValidMoves.Add(CloneArray(input, k, j, i, j, 5, luot));
                                    continue;
                                }
                                else if (input[k][j] > 10)
                                {
                                    ValidMoves.Add(CloneArray(input, k, j, i, j, 5, luot));
                                    break;
                                }
                                break;

                            }
                            for (int k = i + 1; k <= 8; k++)
                            {
                                if (input[k][j] == 0)
                                {
                                    ValidMoves.Add(CloneArray(input, k, j, i, j, 5, luot));
                                    continue;
                                }
                                else if (input[k][j] > 10)
                                {
                                    ValidMoves.Add(CloneArray(input, k, j, i, j, 5, luot));
                                    break;
                                }
                                break;
                            }
                            for (int k = j - 1; k >= 1; k--)
                            {
                                if (input[i][k] == 0)
                                {
                                    ValidMoves.Add(CloneArray(input, i, k, i, j, 5, luot));
                                    continue;
                                }
                                else if (input[i][k] > 10)
                                {
                                    ValidMoves.Add(CloneArray(input, i, k, i, j, 5, luot));
                                    break;
                                }
                                break;
                            }
                            for (int k = j + 1; k <= 8; k++)
                            {
                                if (input[i][k] == 0)
                                {
                                    ValidMoves.Add(CloneArray(input, i, k, i, j, 5, luot));
                                    continue;
                                }
                                else if (input[i][k] > 10)
                                {
                                    ValidMoves.Add(CloneArray(input, i, k, i, j, 5, luot));
                                    break;
                                }
                                break;
                            }
                        }
                        else if (input[i][j] == 2)
                        {
                            for (int k = 1; k <= 8; k++)
                            {
                                for (int l = 1; l <= 8; l++)
                                {
                                    if (input[k][l] == 0 || input[k][l] > 10)
                                    {
                                        if ((k == i - 1 && l == j - 2) || (k == i + 1 && l == j - 2) || (k == i + 2 && l == j - 1) || (k == i + 2 && l == j + 1) || (k == i + 1 && l == j + 2) || (k == i - 1 && l == j + 2) || (k == i - 2 && l == j + 1) || (k == i - 2 && l == j - 1))
                                        {
                                            ValidMoves.Add(CloneArray(input, k, l, i, j, 2, luot));
                                        }
                                    }
                                }
                            }
                        }
                        else if (input[i][j] == 3)
                        {
                            for (int k = 1; k <= 8; k++)
                            {
                                if (i - k > 0 && j - k > 0)
                                {
                                    if (input[i - k][j - k] == 0)
                                    {
                                        ValidMoves.Add(CloneArray(input, i - k, j - k, i, j, 3, luot));
                                        continue;
                                    }
                                    else if (input[i - k][j - k] > 10)
                                    {
                                        ValidMoves.Add(CloneArray(input, i - k, j - k, i, j, 3, luot));
                                        break;
                                    }
                                    break;
                                }
                                else
                                {
                                    break;
                                }
                            }
                            for (int k = 1; k <= 8; k++)
                            {
                                if (i + k <= 8 && j + k <= 8)
                                {
                                    if (input[i + k][j + k] == 0)
                                    {
                                        ValidMoves.Add(CloneArray(input, i + k, j + k, i, j, 3, luot));
                                        continue;
                                    }
                                    else if (input[i + k][j + k] > 10)
                                    {
                                        ValidMoves.Add(CloneArray(input, i + k, j + k, i, j, 3, luot));
                                        break;
                                    }
                                    break;
                                }
                                else
                                {
                                    break;
                                }
                            }
                            for (int k = 1; k <= 8; k++)
                            {
                                if (i - k > 0 && j + k <= 8)
                                {
                                    if (input[i - k][j + k] == 0)
                                    {
                                        ValidMoves.Add(CloneArray(input, i - k, j + k, i, j, 3, luot));
                                        continue;
                                    }
                                    else if (input[i - k][j + k] > 10)
                                    {
                                        ValidMoves.Add(CloneArray(input, i - k, j + k, i, j, 3, luot));
                                        break;
                                    }
                                    break;
                                }
                                else
                                {
                                    break;
                                }
                            }
                            for (int k = 1; k <= 8; k++)
                            {
                                if (i + k <= 8 && j - k > 0)
                                {
                                    if (input[i + k][j - k] == 0)
                                    {
                                        ValidMoves.Add(CloneArray(input, i + k, j - k, i, j, 3, luot));
                                        continue;
                                    }
                                    else if (input[i + k][j - k] > 10)
                                    {
                                        ValidMoves.Add(CloneArray(input, i + k, j - k, i, j, 3, luot));
                                        break;
                                    }
                                    break;
                                }
                                else
                                {
                                    break;
                                }
                            }
                        }
                        else if (input[i][j] == 9)
                        {
                            for (int k = i - 1; k >= 1; k--)
                            {
                                if (input[k][j] == 0)
                                {
                                    ValidMoves.Add(CloneArray(input, k, j, i, j, 9, luot));
                                    continue;
                                }
                                else if (input[k][j] > 10)
                                {
                                    ValidMoves.Add(CloneArray(input, k, j, i, j, 9, luot));
                                    break;
                                }
                                break;

                            }
                            for (int k = i + 1; k <= 8; k++)
                            {
                                if (input[k][j] == 0)
                                {
                                    ValidMoves.Add(CloneArray(input, k, j, i, j, 9, luot));
                                    continue;
                                }
                                else if (input[k][j] > 10)
                                {
                                    ValidMoves.Add(CloneArray(input, k, j, i, j, 9, luot));
                                    break;
                                }
                                break;
                            }
                            for (int k = j - 1; k >= 1; k--)
                            {
                                if (input[i][k] == 0)
                                {
                                    ValidMoves.Add(CloneArray(input, i, k, i, j, 9, luot));
                                    continue;
                                }
                                else if (input[i][k] > 10)
                                {
                                    ValidMoves.Add(CloneArray(input, i, k, i, j, 9, luot));
                                    break;
                                }
                                break;
                            }
                            for (int k = j + 1; k <= 8; k++)
                            {
                                if (input[i][k] == 0)
                                {
                                    ValidMoves.Add(CloneArray(input, i, k, i, j, 9, luot));
                                    continue;
                                }
                                else if (input[i][k] > 10)
                                {
                                    ValidMoves.Add(CloneArray(input, i, k, i, j, 9, luot));
                                    break;
                                }
                                break;
                            }
                            // tuong
                            for (int k = 1; k <= 8; k++)
                            {
                                if (i - k > 0 && j - k > 0)
                                {
                                    if (input[i - k][j - k] == 0)
                                    {
                                        ValidMoves.Add(CloneArray(input, i - k, j - k, i, j, 9, luot));
                                        continue;
                                    }
                                    else if (input[i - k][j - k] > 10)
                                    {
                                        ValidMoves.Add(CloneArray(input, i - k, j - k, i, j, 9, luot));
                                        break;
                                    }
                                    break;
                                }
                                else
                                {
                                    break;
                                }
                            }
                            for (int k = 1; k <= 8; k++)
                            {
                                if (i + k <= 8 && j + k <= 8)
                                {
                                    if (input[i + k][j + k] == 0)
                                    {
                                        ValidMoves.Add(CloneArray(input, i + k, j + k, i, j, 9, luot));
                                        continue;
                                    }
                                    else if (input[i + k][j + k] > 10)
                                    {
                                        ValidMoves.Add(CloneArray(input, i + k, j + k, i, j, 9, luot));
                                        break;
                                    }
                                    break;
                                }
                                else
                                {
                                    break;
                                }
                            }
                            for (int k = 1; k <= 8; k++)
                            {
                                if (i - k > 0 && j + k <= 8)
                                {
                                    if (input[i - k][j + k] == 0)
                                    {
                                        ValidMoves.Add(CloneArray(input, i - k, j + k, i, j, 9, luot));
                                        continue;
                                    }
                                    else if (input[i - k][j + k] > 10)
                                    {
                                        ValidMoves.Add(CloneArray(input, i - k, j + k, i, j, 9, luot));
                                        break;
                                    }
                                    break;
                                }
                                else
                                {
                                    break;
                                }
                            }
                            for (int k = 1; k <= 8; k++)
                            {
                                if (i + k <= 8 && j - k > 0)
                                {
                                    if (input[i + k][j - k] == 0)
                                    {
                                        ValidMoves.Add(CloneArray(input, i + k, j - k, i, j, 9, luot));
                                        continue;
                                    }
                                    else if (input[i + k][j - k] > 10)
                                    {
                                        ValidMoves.Add(CloneArray(input, i + k, j - k, i, j, 9, luot));
                                        break;
                                    }
                                    break;
                                }
                                else
                                {
                                    break;
                                }
                            }
                        }
                        else if (input[i][j] == 10)
                        {

                            for (int k = 1; k <= 8; k++)
                            {
                                for (int l = 1; l <= 8; l++)
                                {
                                    if (input[k][l] == 0 || input[k][l] > 10)
                                    {
                                        if ((k == i - 1 && l == j - 1) || (k == i && l == j - 1) || (k == i + 1 && l == j - 1) || (k == i + 1 && l == j) || (k == i + 1 && l == j + 1) || (k == i && l == j + 1) || (k == i - 1 && l == j + 1) || (k == i - 1 && l == j))
                                        {
                                            ValidMoves.Add(CloneArray(input, k, l, i, j, 10, luot));

                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                for (int i = 8; i >= 1; i--)
                {
                    for (int j = 1; j <= 8; j++)
                    {

                        if (input[i][j] == 0)
                        {
                        }
                        else if (input[i][j] == 11)
                        {
                            if (i - 1 > 0 && j - 1 >= 1 && input[i - 1][j - 1] <= 10)
                            {
                                ValidMoves.Add(CloneArray(input, i - 1, j - 1, i, j, 11, luot));
                            }
                            if (i - 1 > 0 && j + 1 <= 8 && input[i - 1][j + 1] <= 10)
                            {
                                ValidMoves.Add(CloneArray(input, i - 1, j + 1, i, j, 11, luot));
                            }
                            if (input[i - 1][j] == 0)
                            {
                                if (i == 2)
                                {
                                    ValidMoves.Add(CloneArray(input, i - 1, j, i, j, 19, luot));
                                }
                                else
                                {
                                    ValidMoves.Add(CloneArray(input, i - 1, j, i, j, 11, luot));
                                }
                            }
                            if (i == 7)
                            {
                                if (input[i - 2][j] == 0)
                                {
                                    ValidMoves.Add(CloneArray(input, i - 2, j, i, j, 11, luot));
                                }
                            }
                        }
                        else if (input[i][j] == 15)
                        {

                            for (int k = i - 1; k >= 1; k--)
                            {
                                if (input[k][j] == 0)
                                {
                                    ValidMoves.Add(CloneArray(input, k, j, i, j, 15, luot));
                                    continue;
                                }
                                else if (input[k][j] <= 10)
                                {
                                    ValidMoves.Add(CloneArray(input, k, j, i, j, 15, luot));
                                    break;
                                }
                                break;

                            }
                            for (int k = i + 1; k <= 8; k++)
                            {
                                if (input[k][j] == 0)
                                {
                                    ValidMoves.Add(CloneArray(input, k, j, i, j, 15, luot));
                                    continue;
                                }
                                else if (input[k][j] <= 10)
                                {
                                    ValidMoves.Add(CloneArray(input, k, j, i, j, 15, luot));
                                    break;
                                }
                                break;
                            }
                            for (int k = j - 1; k >= 1; k--)
                            {
                                if (input[i][k] == 0)
                                {
                                    ValidMoves.Add(CloneArray(input, i, k, i, j, 15, luot));
                                    continue;
                                }
                                else if (input[i][k] <= 10)
                                {
                                    ValidMoves.Add(CloneArray(input, i, k, i, j, 15, luot));
                                    break;
                                }
                                break;
                            }
                            for (int k = j + 1; k <= 8; k++)
                            {
                                if (input[i][k] == 0)
                                {
                                    ValidMoves.Add(CloneArray(input, i, k, i, j, 15, luot));
                                    continue;
                                }
                                else if (input[i][k] <= 10)
                                {
                                    ValidMoves.Add(CloneArray(input, i, k, i, j, 15, luot));
                                    break;
                                }
                                break;
                            }
                        }
                        else if (input[i][j] == 12)
                        {
                            for (int k = 1; k <= 8; k++)
                            {
                                for (int l = 1; l <= 8; l++)
                                {
                                    if (input[k][l] <= 10)
                                    {
                                        if ((k == i - 1 && l == j - 2) || (k == i + 1 && l == j - 2) || (k == i + 2 && l == j - 1) || (k == i + 2 && l == j + 1) || (k == i + 1 && l == j + 2) || (k == i - 1 && l == j + 2) || (k == i - 2 && l == j + 1) || (k == i - 2 && l == j - 1))
                                        {
                                            ValidMoves.Add(CloneArray(input, k, l, i, j, 12, luot));
                                        }
                                    }
                                }
                            }
                        }
                        else if (input[i][j] == 13)
                        {
                            for (int k = 1; k <= 8; k++)
                            {
                                if (i - k > 0 && j - k > 0)
                                {
                                    if (input[i - k][j - k] == 0)
                                    {
                                        ValidMoves.Add(CloneArray(input, i - k, j - k, i, j, 13, luot));
                                        continue;
                                    }
                                    else if (input[i - k][j - k] <= 10)
                                    {
                                        ValidMoves.Add(CloneArray(input, i - k, j - k, i, j, 13, luot));
                                        break;
                                    }
                                    break;
                                }
                                else
                                {
                                    break;
                                }
                            }
                            for (int k = 1; k <= 8; k++)
                            {
                                if (i + k <= 8 && j + k <= 8)
                                {
                                    if (input[i + k][j + k] == 0)
                                    {
                                        ValidMoves.Add(CloneArray(input, i + k, j + k, i, j, 13, luot));
                                        continue;
                                    }
                                    else if (input[i + k][j + k] <= 10)
                                    {
                                        ValidMoves.Add(CloneArray(input, i + k, j + k, i, j, 13, luot));
                                        break;
                                    }
                                    break;
                                }
                                else
                                {
                                    break;
                                }
                            }
                            for (int k = 1; k <= 8; k++)
                            {
                                if (i - k > 0 && j + k <= 8)
                                {
                                    if (input[i - k][j + k] == 0)
                                    {
                                        ValidMoves.Add(CloneArray(input, i - k, j + k, i, j, 13, luot));
                                        continue;
                                    }
                                    else if (input[i - k][j + k] <= 10)
                                    {
                                        ValidMoves.Add(CloneArray(input, i - k, j + k, i, j, 13, luot));
                                        break;
                                    }
                                    break;
                                }
                                else
                                {
                                    break;
                                }
                            }
                            for (int k = 1; k <= 8; k++)
                            {
                                if (i + k <= 8 && j - k > 0)
                                {
                                    if (input[i + k][j - k] == 0)
                                    {
                                        ValidMoves.Add(CloneArray(input, i + k, j - k, i, j, 13, luot));
                                        continue;
                                    }
                                    else if (input[i + k][j - k] <= 10)
                                    {
                                        ValidMoves.Add(CloneArray(input, i + k, j - k, i, j, 13, luot));
                                        break;
                                    }
                                    break;
                                }
                                else
                                {
                                    break;
                                }
                            }
                        }
                        else if (input[i][j] == 19)
                        {
                            for (int k = i - 1; k >= 1; k--)
                            {
                                if (input[k][j] == 0)
                                {
                                    ValidMoves.Add(CloneArray(input, k, j, i, j, 19, luot));
                                    continue;
                                }
                                else if (input[k][j] <= 10)
                                {
                                    ValidMoves.Add(CloneArray(input, k, j, i, j, 19, luot));
                                    break;
                                }
                                break;

                            }
                            for (int k = i + 1; k <= 8; k++)
                            {
                                if (input[k][j] == 0)
                                {
                                    ValidMoves.Add(CloneArray(input, k, j, i, j, 19, luot));
                                    continue;
                                }
                                else if (input[k][j] <= 10)
                                {
                                    ValidMoves.Add(CloneArray(input, k, j, i, j, 19, luot));
                                    break;
                                }
                                break;
                            }
                            for (int k = j - 1; k >= 1; k--)
                            {
                                if (input[i][k] == 0)
                                {
                                    ValidMoves.Add(CloneArray(input, i, k, i, j, 19, luot));
                                    continue;
                                }
                                else if (input[i][k] <= 10)
                                {
                                    ValidMoves.Add(CloneArray(input, i, k, i, j, 19, luot));
                                    break;
                                }
                                break;
                            }
                            for (int k = j + 1; k <= 8; k++)
                            {
                                if (input[i][k] == 0)
                                {
                                    ValidMoves.Add(CloneArray(input, i, k, i, j, 19, luot));
                                    continue;
                                }
                                else if (input[i][k] <= 10)
                                {
                                    ValidMoves.Add(CloneArray(input, i, k, i, j, 19, luot));
                                    break;
                                }
                                break;
                            }
                            // tuong
                            for (int k = 1; k <= 8; k++)
                            {
                                if (i - k > 0 && j - k > 0)
                                {
                                    if (input[i - k][j - k] == 0)
                                    {
                                        ValidMoves.Add(CloneArray(input, i - k, j - k, i, j, 19, luot));
                                        continue;
                                    }
                                    else if (input[i - k][j - k] <= 10)
                                    {
                                        ValidMoves.Add(CloneArray(input, i - k, j - k, i, j, 19, luot));
                                        break;
                                    }
                                    break;
                                }
                            }
                            for (int k = 1; k <= 8; k++)
                            {
                                if (i + k <= 8 && j + k <= 8)
                                {
                                    if (input[i + k][j + k] == 0)
                                    {
                                        ValidMoves.Add(CloneArray(input, i + k, j + k, i, j, 19, luot));
                                        continue;
                                    }
                                    else if (input[i + k][j + k] <= 10)
                                    {
                                        ValidMoves.Add(CloneArray(input, i + k, j + k, i, j, 19, luot));
                                        break;
                                    }
                                    break;
                                }
                            }
                            for (int k = 1; k <= 8; k++)
                            {
                                if (i - k > 0 && j + k <= 8)
                                {
                                    if (input[i - k][j + k] == 0)
                                    {
                                        ValidMoves.Add(CloneArray(input, i - k, j + k, i, j, 19, luot));
                                        continue;
                                    }
                                    else if (input[i - k][j + k] <= 10)
                                    {
                                        ValidMoves.Add(CloneArray(input, i - k, j + k, i, j, 19, luot));
                                        break;
                                    }
                                    break;
                                }
                            }
                            for (int k = 1; k <= 8; k++)
                            {
                                if (i + k <= 8 && j - k > 0)
                                {
                                    if (input[i + k][j - k] == 0)
                                    {
                                        ValidMoves.Add(CloneArray(input, i + k, j - k, i, j, 19, luot));
                                        continue;
                                    }
                                    else if (input[i + k][j - k] <= 10)
                                    {
                                        ValidMoves.Add(CloneArray(input, i + k, j - k, i, j, 19, luot));
                                        break;
                                    }
                                    break;
                                }
                            }
                        }
                        else if (input[i][j] == 20)
                        {

                            for (int k = 1; k <= 8; k++)
                            {
                                for (int l = 1; l <= 8; l++)
                                {
                                    if (input[k][l] <= 10)
                                    {
                                        if ((k == i - 1 && l == j - 1) || (k == i && l == j - 1) || (k == i + 1 && l == j - 1) || (k == i + 1 && l == j) || (k == i + 1 && l == j + 1) || (k == i && l == j + 1) || (k == i - 1 && l == j + 1) || (k == i - 1 && l == j))
                                        {
                                            ValidMoves.Add(CloneArray(input, k, l, i, j, 20, luot));

                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            List<int[][]> listFromSortedSet = ValidMoves.ToList();
            return listFromSortedSet;
        }
        public int[][] MaxVal(int[][] u, int a, int b, int count)
		{
			List<int[][]> listOfArrays;

            if (count == 3)
			{
				listOfArrays = ValidMoves_Arrange(u, false);
			}
			else
			{
                listOfArrays = ValidMoves(u, false);
            }
				if (listOfArrays.Count() == 0)
				{
					return d;
				}
			if (count == 1)
				{
					int temp = -10000;
					var result = u;
					foreach (var item in listOfArrays)
					{
					bool checkVuaden = false;
					for (int i = 1; i <= 8; i++)
					{
						for (int j = 1; j <= 8; j++)
						{
							if (item[i][j] == 10)
							{
								checkVuaden = true;
							}
						}
					}
					if (!checkVuaden) { return item; }
					int diem = SumArray(item,false);
					
						if (diem > temp) {  temp = diem; result = item; }
					}
					return result;
				}
				int val = -100000;
				var x = u;
				foreach (int[][] maxElement in listOfArrays)
				{
					bool checkVuaden = false;
					for(int i = 1; i <= 8; i++)
					{
						for(int j = 1; j <= 8; j++)
						{
							if (maxElement[i][j] == 10)
							{
							checkVuaden = true;
							}
						}
					}
					if(!checkVuaden) { return maxElement; }
					int[][] minVal = MinVal(maxElement, a, b, count-1);
					int tongdiem = 0;
					if (minVal != null)
					{
						tongdiem = SumArray(minVal, true);
						if(tongdiem < -1000)
						{
						continue;
						}else if(tongdiem > 10000000)
						{
							return t;
						}
						if (val < tongdiem)
						{
							val = tongdiem;
							x = maxElement;
						}
					}
					
					if (val >= b)
					{
						return x;
					}
					a = a > val ? a : val;
				}
			return x;
			
		}
		public int[][] MinVal(int[][] u, int a, int b, int count)
		{

            List<int[][]> listOfArrays;

            if (count == 2)
            {
                listOfArrays = ValidMoves_Arrange(u, true);
            }
            else
            {
                listOfArrays = ValidMoves(u, true);
            }
            if (listOfArrays.Count() == 0)
			{
				return t;
			}
			int val = 100000;
				var x = u;
			foreach (int[][] minElement in listOfArrays)
				{
					bool checkVuatrang = false;
					for (int i = 8; i >= 1; i--)
					{
						for (int j = 1; j <= 8; j++)
						{
							if (minElement[i][j] == 20)
							{
								checkVuatrang = true;
							}
						}
					}
					if (!checkVuatrang) { return minElement; }
					int[][] maxVal = MaxVal(minElement, a, b, count );
					int tongdiem = 0;
					if (maxVal != null)
					{
						tongdiem = SumArray(maxVal,false);
						if (tongdiem > 1000)
						{
							continue;
						}
						else if (tongdiem < -100000000)
						{
							return d;
						}
						
							if (val > tongdiem)
							{
								val = tongdiem;
								x = minElement;
							}
					}
					

						if (val <= a)
						{
							return x;
						}
						b = b < val ? b : val;
				}
			
			return x;
			
		}



	}
}
