
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

double fn(int a, int b, int x)
{
	return a * x / 2000.0 + b / 80.0 * (1.0 + x / 1000.0) * (1.0 + x / 1000.0);
}

int main(){
#ifdef _DEBUG
	freopen("input.txt", "r", stdin);
	//freopen("output.txt", "w+", stdout);
#endif
	const int Cycle = 400;

	int N;
	cin >> N;

	vector<int> a(N + 1), b(N + 1);
	vector<vector<double>> dp(N + 1, vector<double>(41, 99999999.9));
	dp[0][20] = 0;

	REP1(n, N) cin >> a[n] >> b[n];
	REP1(n, N) REP(i, 41) REP(j, 41)
		dp[n][i] = min(dp[n][i], dp[n - 1][j] + fn(a[n], b[n], Cycle + i - j));
	cout << dp[N][20] << endl;
	return 0;
}

