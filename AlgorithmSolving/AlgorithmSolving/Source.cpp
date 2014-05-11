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
#include <set>
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

string dec2bin(int N)
{
	string str;
	while (N > 0)
	{
		str.push_back('0' + N % 2);
		N >>= 1;
	}
	return str;
}

int main(){
#ifdef _DEBUG
	freopen("input.txt", "r", stdin);
	//freopen("output.txt", "w+", stdout);
#endif
	int N;
	while (cin >> N)
	{
		if (N == 0)
		{
			cout << "0 0" << endl;
			continue;
		}
		auto bin = dec2bin(N);
		if (bin.find('0') == -1)
		{
			cout << "0 " << N * 2 << endl;
			continue;
		}

		auto minBin = bin;
		auto flagZero = false;
		auto cutOne = 0;
		auto cntOne = 0;
		REP(i, minBin.length())
		{
			if (minBin[i] == '0') flagZero = true;
			if (minBin[i] == '1') ++cntOne;
			if (flagZero && minBin[i] == '1')
			{
				cutOne = i;
				break;
			}
		}
		minBin[cutOne] = '0';
		FORD(i, cutOne - cntOne, cutOne) minBin[i] = '1';
		REPD(i, cutOne - cntOne) minBin[i] = '0';
		cout << minBin << endl;
		
		auto maxBin = bin;
		auto flagOne = false;
		auto cutZero = -1;
		cntOne = 0;
	}
	return 0;
}