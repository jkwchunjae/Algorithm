
#define _CRT_SECURE_NO_WARNINGS

#include <iostream>
#include <cstdio>
#include <iomanip>
#include <cmath>
#include <vector>
#include <algorithm>
#include <functional>
#include <string>
#include <cstring>
#include <sstream>
#include <fstream>
#include <cstdlib>
#include <stack>
#include <queue>
#include <map>
#include <ctime>
#include <cassert>

using namespace std;

#define REP(v, hi) for (int v=0;v<(hi);v++)
#define REPD(v, hi) for (int v=((hi)-1);v>=0;v--)
#define FOR(v, lo, hi) for (int v=(lo);v<(hi);v++)
#define FORD(v, lo, hi) for (int v=((hi)-1);v>=(lo);v--)
#define REP1(v, hi) for (int v=1;v<=(hi);v++)
#define REPD1(v, hi) for (int v=(hi);v>=1;v--)
#define FOR1(v, lo, hi) for (int v=(lo);v<=(hi);v++)
#define FORD1(v, lo, hi) for (int v=(hi);v>=(lo);v--)

const double eps = 1 / (double)1000000000;
const int INT__MIN = 1 << 31;
const int INT__MAX = 0x8fffffff;
const long long LL_MIN = 1L << 63;
const long long LL_MAX = 0x8fffffffffffffff;

int main(){
#ifdef _DEBUG
	freopen("input.txt", "r", stdin);
	//freopen("output.txt", "w+", stdout);
#endif
	int N;
	while (cin >> N)
	{
		int result = -1;
		//cout << N << " ";
		if (N == 1) result = 0;
		else if (N % 2 == 0) result = N >> 1;
		else
		{
			int sq = sqrt(N);
			FOR1(i, 3, sq)
			{
				if (N % i == 0)
				{
					result = N - (N / i);
					break;
				}
			}
			if (result == -1) result = N - 1;
		}
		cout << result << endl;
	}
	/*
	while (cin >> N)
	{
		cout << N << " ";
		int cnt = 0;
		FORD(i, 1, N)
		{
			++cnt;
			if (N % i == 0) break;
		}
		cout << cnt << endl;
	}
	*/
	return 0;
}

