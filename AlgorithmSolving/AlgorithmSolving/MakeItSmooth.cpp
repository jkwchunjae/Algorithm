
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

int main(){
	freopen("MakeItSmooth.in", "r", stdin);
	freopen("output.txt", "w+", stdout);
	int t;
	cin >> t;
	while (t--)
	{
		int D, I, M, N;
		cin >> D >> I >> M >> N;

		int num[110] = { 0 };
		REP1(i, N) cin >> num[i];

		int dp[110][256] = { 0 };
		REP1(i, N) REP(j, 256) dp[i][j] = 99999999;
		//REP(i, 300) dp[0][i] = 0;
		REP1(curr, N)
		{
			int prev = curr - 1;

			// 1. 앞에꺼 다 지운 상태에서 나만 남은 경우
			dp[curr][num[curr]] = (prev * D);

			REP(i, 256)
			{
				// 2. 지우는 경우
				dp[curr][i] = min(dp[curr][i], dp[prev][i] + D);

				// 3. 수정(삭제/추가) 하는 경우 (num[curr] 을 i 로)
				int cost = min(abs(num[curr] - i), D + I); // 수정이 좋은지, 삭제/추가 가 좋은지
				if (M == 0)
				{
					dp[curr][i] = min(dp[curr][i], dp[prev][i] + cost);
				}
				else
				{
					REP(j, 256)
					{
						// addCost : 간격 차로 인해 추가를 해야하는 경우
						int addCost = (abs(i - j) <= M) ? 0 : ((abs(i - j) - 1) / M) * I;
						dp[curr][i] = min(dp[curr][i], dp[prev][j] + cost + addCost);
					}
				}
			}
		}

		int result = 99999999;
		REP(i, 256) result = min(result, dp[N][i]);
		cout << result << endl;
	}
	return 0;
}

