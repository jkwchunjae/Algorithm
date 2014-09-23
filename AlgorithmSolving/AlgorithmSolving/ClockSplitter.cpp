
#define _CRT_SECURE_NO_WARNINGS

#include <iostream>
#include <cstdio>
#include <iomanip>
#include <cmath>
#include <vector>
#include <algorithm>
#include <functional>
#include <numeric>
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
const int INT__MAX = INT__MIN - 1;
const long long LL_MIN = 1 << 63;
const long long LL_MAX = LL_MIN - 1;

typedef long long ll;

int main(){
#ifdef _DEBUG
	freopen("input.txt", "r", stdin);
	//freopen("output.txt", "w+", stdout);
#endif
	istream_iterator<int> input(cin), eof;
	for_each(input, eof, [&](long long N)
	{
		cout << N << endl;
		long long left = N / 4 + (N % 4 + 1) % 3;
		long long right = N - N / 4 - (N % 4) / 2;
		long long ans1 = N * (N + 1) / 4;
		long long ans2 = N * (N + 1) / 2 - ans1;
		long long leftSum = left * (left - 1) / 2;
		long long a = 1;
		long long b = right - ((2 * right + 1) - (long long)sqrt((2 * right + 1)*(2 * right + 1) - 8 * leftSum)) / 2;
		long long rightSum = right * (right + 1) / 2 - b * (b - 1) / 2;
		long long currSum = b * (b - 1) / 2;
		long long begin = 1;
		long long end = b - 1;
		cout << left << " " << right << " " << ans1 << " " << ans2 << " " << leftSum << " " << b << endl;
	});
	return 0;
}






























