
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

string str[16];

int main(){
#ifdef _DEBUG
	freopen("input.txt", "r", stdin);
	//freopen("output.txt", "w+", stdout);
#endif
	map<int, int> mapp;
	str[0] = "{}";
	FOR1(i, 1, 15)
	{
		str[i] = "{";
		REP(j, i - 1) str[i] += str[j] + ",";
		str[i] += str[i - 1] + "}";
	}
	REP(i, 16) mapp[str[i].length()] = i;
	int T;
	cin >> T;
	while (T--)
	{
		string str1, str2;
		cin >> str1 >> str2;
		cout << str[mapp[str1.length()] + mapp[str2.length()]] << endl;
	}
	return 0;
}

